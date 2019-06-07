using System;
using System.Windows.Input;

namespace Kiosk.Classes
{
    public class Command : ICommand
    {
        public delegate void CommandOnExecute(object parameter);

        public delegate bool CommandOnCanExecute(object parameter);

        private readonly CommandOnExecute _execute;
        private readonly CommandOnCanExecute _canExecute;

        public Command(CommandOnExecute onExecuteMethod, CommandOnCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested += value;
        }
    }
}
