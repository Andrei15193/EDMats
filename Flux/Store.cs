using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Flux
{
    public abstract class Store : INotifyPropertyChanged
    {
        private readonly Lazy<IReadOnlyCollection<DispatchHandlerInfo>> _dispatchHandlers;

        protected Store()
        {
            _dispatchHandlers = new Lazy<IReadOnlyCollection<DispatchHandlerInfo>>(_GetHandlers);
            Dispatcher.Instance.Register(Handle);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Handle(ActionData actionData)
            => _TryFindDispatchHandler(actionData?.GetType() ?? typeof(ActionData))?.Invoke(actionData);

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void SetProperty<TProperty>(Expression<Func<TProperty>> property, TProperty value)
        {
            switch (property?.Body)
            {
                case MemberExpression memberExpression when
                        memberExpression.Member is PropertyInfo propertyInfo
                        && propertyInfo.SetMethod != null
                        && memberExpression.Expression is ConstantExpression constantExpression
                        && ReferenceEquals(constantExpression.Value, this):

                    propertyInfo.SetMethod.Invoke(this, new object[] { value });
                    NotifyPropertyChanged(propertyInfo.Name);
                    break;

                case null:
                    throw new ArgumentNullException(nameof(property));

                default:
                    throw new ArgumentException("Property expression does not resolve to a settable property of the current store.", nameof(property));
            }
        }

        private Action<ActionData> _TryFindDispatchHandler(Type actionDataType)
        {
            Action<ActionData> _bestMatch = null;
            int _acceptableMatchPrecision = 0;
            Action<ActionData> _acceptableMatch = null;

            using (var dispatchHandlerInfo = _dispatchHandlers.Value.GetEnumerator())
                while (dispatchHandlerInfo.MoveNext() && _bestMatch == null)
                {
                    if (dispatchHandlerInfo.Current.ParameterType == actionDataType)
                        _bestMatch = dispatchHandlerInfo.Current.DispatchHandler;
                    else if (dispatchHandlerInfo.Current.ParameterType.IsAssignableFrom(actionDataType))
                    {
                        var matchPrecision = _GetMatchPrecisionBetween(dispatchHandlerInfo.Current.ParameterType, actionDataType);
                        if (matchPrecision < _acceptableMatchPrecision || _acceptableMatch == null)
                        {
                            _acceptableMatchPrecision = matchPrecision;
                            _acceptableMatch = dispatchHandlerInfo.Current.DispatchHandler;
                        }
                    }
                }

            return _bestMatch ?? _acceptableMatch;

            int _GetMatchPrecisionBetween(Type target, Type actual)
            {
                var precision = 0;

                var current = actual;
                while (current != target)
                {
                    current = current.BaseType;
                    precision++;
                }

                return precision;
            }
        }


        private IReadOnlyCollection<DispatchHandlerInfo> _GetHandlers()
            => (
                from storeType in _GetTypesUntilStoreBaseType()
                from method in storeType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                where method.DeclaringType != typeof(Store) && method.ReturnType == typeof(void)
                let parameters = method.GetParameters()
                where parameters.Length == 1
                let actionDataType = parameters.Single().ParameterType
                where typeof(ActionData).IsAssignableFrom(actionDataType)
                select new DispatchHandlerInfo(actionDataType, _CreateDispatchHandler(method, actionDataType))
            ).ToList();

        private IEnumerable<Type> _GetTypesUntilStoreBaseType()
        {
            var current = GetType();
            while (current != typeof(Store))
            {
                yield return current;
                current = current.BaseType;
            }
        }

        private sealed class DispatchHandlerInfo
        {
            public DispatchHandlerInfo(Type parameterType, Action<ActionData> dispatchHandler)
            {
                ParameterType = parameterType;
                DispatchHandler = dispatchHandler;
            }

            public Type ParameterType { get; }

            public Action<ActionData> DispatchHandler { get; }
        }

        private Action<ActionData> _CreateDispatchHandler(MethodInfo dispatchHandlerMethodInfo, Type actionDataType)
        {
            var dispatchHandler = (DispatchHandler)Activator.CreateInstance(
                typeof(DispatchHandler<>).MakeGenericType(actionDataType),
                this,
                dispatchHandlerMethodInfo
            );
            return dispatchHandler.Execute;
        }

        private abstract class DispatchHandler
        {
            public abstract void Execute(ActionData actionData);
        }

        private sealed class DispatchHandler<TActionData> : DispatchHandler where TActionData : ActionData
        {
            private readonly Action<TActionData> _dispatchHandler;

            public DispatchHandler(object target, MethodInfo methodInfo)
            {
                _dispatchHandler = (Action<TActionData>)Delegate.CreateDelegate(typeof(Action<TActionData>), target, methodInfo, true);
            }

            public override void Execute(ActionData actionData)
                => _dispatchHandler((TActionData)actionData);
        }
    }
}