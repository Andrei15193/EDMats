using System;
using System.Windows.Input;

namespace EDMats.ViewModels
{
    public abstract class Command : ICommand
    {
        public abstract event EventHandler CanExecuteChanged;

        public abstract bool CanExecute();

        bool ICommand.CanExecute(object parameter)
            => CanExecute();

        public abstract void Execute();

        void ICommand.Execute(object parameter)
            => Execute();
    }

    public abstract class Command<TParameter> : ICommand
    {
        public abstract event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(TParameter parameter);

        bool ICommand.CanExecute(object parameter)
            => CanExecute(parameter is null ? default : (TParameter)parameter);

        public abstract void Execute(TParameter parameter);

        void ICommand.Execute(object parameter)
            => Execute(parameter is null ? default : (TParameter)parameter);
    }
}