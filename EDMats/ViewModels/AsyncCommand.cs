using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EDMats.ViewModels
{
    public abstract class AsyncCommand : ICommand
    {
        public abstract event EventHandler CanExecuteChanged;

        public abstract bool CanExecute();

        bool ICommand.CanExecute(object parameter)
            => CanExecute();

        public abstract Task ExecuteAsync();

        void ICommand.Execute(object parameter)
            => ExecuteAsync();
    }

    public abstract class AsyncCommand<TParameter> : ICommand
    {
        public abstract event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(TParameter parameter);

        bool ICommand.CanExecute(object parameter)
            => CanExecute(parameter is null ? default : (TParameter)parameter);

        public abstract Task ExecuteAsync(TParameter parameter);

        void ICommand.Execute(object parameter)
            => ExecuteAsync(parameter is null ? default : (TParameter)parameter);
    }
}