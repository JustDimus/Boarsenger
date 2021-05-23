using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Boarsenger.WindowsApp.UI.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;

        private Func<object, bool> canExecute;

        public RelayCommand(
            Action<object> executeAction,
            Func<object, bool> canExecuteFunction)
        {
            this.execute = executeAction;
            this.canExecute = canExecuteFunction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            this.execute?.Invoke(parameter);
        }
    }
}
