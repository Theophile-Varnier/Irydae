using System;
using System.Windows;

namespace Irydae.Helpers
{
    public class OpenDialogCommand<T> : RelayCommand where T : Window
    {
        public OpenDialogCommand(Action execute) : base(execute)
        {
        }

        public OpenDialogCommand(Action execute, Func<bool> canExecute) : base(execute, canExecute)
        {
        }

        public OpenDialogCommand(Action<object> execute) : base(execute)
        {
        }

        public OpenDialogCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
        {
        }
    }
}