using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace EDMats.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected Command CreateCommand(Action executeCallback)
            => new CallbackCommand(this, default, executeCallback, Array.Empty<string>());

        protected Command CreateCommand(Func<bool> canExecuteCallback, Action executeCallback, IEnumerable<string> watchedProperties)
            => new CallbackCommand(this, canExecuteCallback, executeCallback, watchedProperties as IReadOnlyCollection<string> ?? watchedProperties.ToList());

        protected Command<TParameter> CreateCommand<TParameter>(Action<TParameter> executeCallback)
            => new CallbackCommand<TParameter>(this, default, executeCallback, Array.Empty<string>());

        protected Command<TParameter> CreateCommand<TParameter>(Func<TParameter, bool> canExecuteCallback, Action<TParameter> executeCallback, IEnumerable<string> watchedProperties)
            => new CallbackCommand<TParameter>(this, canExecuteCallback, executeCallback, watchedProperties as IReadOnlyCollection<string> ?? watchedProperties.ToList());

        public abstract class Command : ICommand
        {
            public abstract event EventHandler CanExecuteChanged;

            public abstract bool CanExecute();

            public abstract void Execute();

            bool ICommand.CanExecute(object parameter)
                => CanExecute();

            void ICommand.Execute(object parameter)
                => Execute();
        }

        public abstract class Command<TParameter> : ICommand
        {
            public abstract event EventHandler CanExecuteChanged;

            public abstract bool CanExecute(TParameter parameter);

            public abstract void Execute(TParameter parameter);

            bool ICommand.CanExecute(object parameter)
                => CanExecute((TParameter)parameter);

            void ICommand.Execute(object parameter)
                => Execute((TParameter)parameter);
        }

        private class CallbackCommand : Command
        {
            private readonly Func<bool> _canExecuteCallback;
            private readonly Action _executeCallback;

            public CallbackCommand(INotifyPropertyChanged notifyPropertyChanged, Func<bool> canExecuteCallback, Action executeCallback, IReadOnlyCollection<string> watchedProperties)
            {
                _canExecuteCallback = canExecuteCallback;
                _executeCallback = executeCallback;
                if (watchedProperties.Count > 0)
                    notifyPropertyChanged.PropertyChanged += (sender, e) =>
                    {
                        if (watchedProperties.Contains(e.PropertyName, StringComparer.Ordinal))
                            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                    };
            }

            public override event EventHandler CanExecuteChanged;

            public override bool CanExecute()
                => _canExecuteCallback?.Invoke() ?? true;

            public override void Execute()
                => _executeCallback();
        }

        private class CallbackCommand<TParameter> : Command<TParameter>
        {
            private readonly Func<TParameter, bool> _canExecuteCallback;
            private readonly Action<TParameter> _executeCallback;

            public CallbackCommand(INotifyPropertyChanged notifyPropertyChanged, Func<TParameter, bool> canExecuteCallback, Action<TParameter> executeCallback, IReadOnlyCollection<string> watchedProperties)
            {
                _canExecuteCallback = canExecuteCallback;
                _executeCallback = executeCallback;
                if (watchedProperties.Count > 0)
                    notifyPropertyChanged.PropertyChanged += (sender, e) =>
                    {
                        if (watchedProperties.Contains(e.PropertyName, StringComparer.Ordinal))
                            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                    };
            }

            public override event EventHandler CanExecuteChanged;

            public override bool CanExecute(TParameter parameter)
                => _canExecuteCallback?.Invoke(parameter) ?? true;

            public override void Execute(TParameter parameter)
                => _executeCallback(parameter);
        }
    }
}