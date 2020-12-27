using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EDMats.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected Command CreateCommand(Action executeCallback)
            => new CallbackCommand(executeCallback);

        protected Command CreateCommand(Action executeCallback, Func<bool> canExecuteCallback, IEnumerable<string> watchedProperties)
            => new CallbackCommand(executeCallback, canExecuteCallback, this, watchedProperties);

        protected Command<TParameter> CreateCommand<TParameter>(Action<TParameter> executeCallback)
            => new CallbackCommand<TParameter>(executeCallback);

        protected Command<TParameter> CreateCommand<TParameter>(Action<TParameter> executeCallback, Func<TParameter, bool> canExecuteCallback, IEnumerable<string> watchedProperties)
            => new CallbackCommand<TParameter>(executeCallback, canExecuteCallback, this, watchedProperties);

        protected AsyncCommand CreateCommand(Func<Task> executeCallback)
            => new CallbackAsyncCommand(executeCallback);

        protected AsyncCommand CreateCommand(Func<Task> executeCallback, Func<bool> canExecuteCallback, IEnumerable<string> watchedProperties)
            => new CallbackAsyncCommand(executeCallback, canExecuteCallback, this, watchedProperties);

        protected AsyncCommand<TParameter> CreateCommand<TParameter>(Func<TParameter, Task> executeCallback)
            => new CallbackAsyncCommand<TParameter>(executeCallback);

        protected AsyncCommand<TParameter> CreateCommand<TParameter>(Func<TParameter, Task> executeCallback, Func<TParameter, bool> canExecuteCallback, IEnumerable<string> watchedProperties)
            => new CallbackAsyncCommand<TParameter>(executeCallback, canExecuteCallback, this, watchedProperties);

        private sealed class CallbackCommand : Command
        {
            private readonly Action _executeCallback;
            private readonly Func<bool> _canExecuteCallback;

            public CallbackCommand(Action executeCallback)
            {
                _executeCallback = executeCallback;
                _canExecuteCallback = null;
            }

            public CallbackCommand(Action executeCallback, Func<bool> canExecuteCallback, INotifyPropertyChanged viewModel, IEnumerable<string> watchedProperties)
            {
                _executeCallback = executeCallback;
                _canExecuteCallback = canExecuteCallback;

                var propertiesToWatchSet = new HashSet<string>(watchedProperties, StringComparer.OrdinalIgnoreCase);
                viewModel.PropertyChanged += (sender, e) =>
                {
                    if (propertiesToWatchSet.Contains(e.PropertyName))
                        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                };
            }

            public override event EventHandler CanExecuteChanged;

            public override bool CanExecute()
                => _canExecuteCallback is null || _canExecuteCallback();

            public override void Execute()
                => _executeCallback();
        }

        private sealed class CallbackCommand<TParameter> : Command<TParameter>
        {
            private readonly Action<TParameter> _executeCallback;
            private readonly Func<TParameter, bool> _canExecuteCallback;

            public CallbackCommand(Action<TParameter> executeCallback)
            {
                _executeCallback = executeCallback;
                _canExecuteCallback = null;
            }

            public CallbackCommand(Action<TParameter> executeCallback, Func<TParameter, bool> canExecuteCallback, INotifyPropertyChanged viewModel, IEnumerable<string> watchedProperties)
            {
                _executeCallback = executeCallback;
                _canExecuteCallback = canExecuteCallback;

                var propertiesToWatchSet = new HashSet<string>(watchedProperties, StringComparer.OrdinalIgnoreCase);
                viewModel.PropertyChanged += (sender, e) =>
                {
                    if (propertiesToWatchSet.Contains(e.PropertyName))
                        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                };
            }

            public override event EventHandler CanExecuteChanged;

            public override bool CanExecute(TParameter parameter)
                => _canExecuteCallback is null || _canExecuteCallback(parameter);

            public override void Execute(TParameter parameter)
                => _executeCallback(parameter);
        }

        private sealed class CallbackAsyncCommand : AsyncCommand
        {
            private readonly Func<Task> _executeCallback;
            private readonly Func<bool> _canExecuteCallback;

            public CallbackAsyncCommand(Func<Task> executeCallback)
            {
                _executeCallback = executeCallback;
                _canExecuteCallback = null;
            }

            public CallbackAsyncCommand(Func<Task> executeCallback, Func<bool> canExecuteCallback, INotifyPropertyChanged viewModel, IEnumerable<string> watchedProperties)
            {
                _executeCallback = executeCallback;
                _canExecuteCallback = canExecuteCallback;

                var propertiesToWatchSet = new HashSet<string>(watchedProperties, StringComparer.OrdinalIgnoreCase);
                viewModel.PropertyChanged += (sender, e) =>
                {
                    if (propertiesToWatchSet.Contains(e.PropertyName))
                        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                };
            }

            public override event EventHandler CanExecuteChanged;

            public override bool CanExecute()
                => _canExecuteCallback is null || _canExecuteCallback();

            public override Task ExecuteAsync()
                => _executeCallback();
        }

        private sealed class CallbackAsyncCommand<TParameter> : AsyncCommand<TParameter>
        {
            private readonly Func<TParameter, Task> _executeCallback;
            private readonly Func<TParameter, bool> _canExecuteCallback;

            public CallbackAsyncCommand(Func<TParameter, Task> executeCallback)
            {
                _executeCallback = executeCallback;
                _canExecuteCallback = null;
            }

            public CallbackAsyncCommand(Func<TParameter, Task> executeCallback, Func<TParameter, bool> canExecuteCallback, INotifyPropertyChanged viewModel, IEnumerable<string> watchedProperties)
            {
                _executeCallback = executeCallback;
                _canExecuteCallback = canExecuteCallback;

                var propertiesToWatchSet = new HashSet<string>(watchedProperties, StringComparer.OrdinalIgnoreCase);
                viewModel.PropertyChanged += (sender, e) =>
                {
                    if (propertiesToWatchSet.Contains(e.PropertyName))
                        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                };
            }

            public override event EventHandler CanExecuteChanged;

            public override bool CanExecute(TParameter parameter)
                => _canExecuteCallback is null || _canExecuteCallback(parameter);

            public override Task ExecuteAsync(TParameter parameter)
                => _executeCallback(parameter);
        }
    }
}