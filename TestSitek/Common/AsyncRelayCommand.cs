using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TestSitek.Common;

namespace TestSitek.Command
{
    internal class AsyncRelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Func<Task> execute;
        private readonly Func<bool> canExecute;

        private long isExecuting;

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || (Interlocked.Read(ref isExecuting) == 0 && canExecute());
        }

        public void Execute(object parameter)
        {
            ExecuteAsync(parameter).FireAndForgetSafeAsync();
        }

        private async Task ExecuteAsync(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            Interlocked.Exchange(ref isExecuting, 1);
            RaiseCanExecuteChanged();

            try
            {
                await execute().ConfigureAwait(false);
            }
            finally
            {
                Interlocked.Exchange(ref isExecuting, 0);
                RaiseCanExecuteChanged();
            }
        }
    }
}
