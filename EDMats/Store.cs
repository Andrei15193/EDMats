using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EDMats
{
    public abstract class Store : INotifyPropertyChanged
    {
        public Store()
            => Dispatcher.Instance.Register(Handle);

        public event PropertyChangedEventHandler PropertyChanged;

        protected abstract void Handle(ActionData actionData);

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

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}