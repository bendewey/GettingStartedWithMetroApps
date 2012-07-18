using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace BingImageSearch.Common
{
    public class AsyncDelegateCommand<T> : DelegateCommand<T>
    {
        /// <summary>
        /// Creates a new instance of <see cref="AsyncDelegateCommand"/> with the <see cref="Func<Task>"/> to invoke on execution.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Func<Task>"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
        public AsyncDelegateCommand(Func<T, Task> executeMethod)
            : this(executeMethod, parameter => true)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="AsyncDelegateCommand"/> with the <see cref="Func<Task>"/> to invoke on execution
        /// and a <see langword="Func" /> to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Func<Task>"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
        /// <param name="canExecuteMethod">The <see cref="Func{TResult}"/> to invoke when <see cref="ICommand.CanExecute"/> is called</param>
        public AsyncDelegateCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
            : base(async parameter =>
            {
                try
                {
                    await executeMethod(parameter);
                }
                catch (Exception ex)
                {
                    // Due to an issues I've noted online: http://social.msdn.microsoft.com/Forums/en/winappswithcsharp/thread/bea154b0-08b0-4fdc-be31-058d9f5d1c4e
                    // I am limiting the use of 'async void'  In a few rare occasions I use it
                    // and manually route the exceptions to the UnhandledExceptionHandler
                    ((App)App.Current).OnUnhandledException(ex);
                }
            }, canExecuteMethod)
        {
        }
    }

    public class AsyncDelegateCommand : DelegateCommand
    {
        /// <summary>
        /// Creates a new instance of <see cref="AsyncDelegateCommand"/> with the <see cref="Func<Task>"/> to invoke on execution.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Func<Task>"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
        public AsyncDelegateCommand(Func<Task> executeMethod)
            : this(executeMethod, () => true)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="AsyncDelegateCommand"/> with the <see cref="Func<Task>"/> to invoke on execution
        /// and a <see langword="Func" /> to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Func<Task>"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
        /// <param name="canExecuteMethod">The <see cref="Func{TResult}"/> to invoke when <see cref="ICommand.CanExecute"/> is called</param>
        public AsyncDelegateCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod)
            : base(async () =>
            {
                try
                {
                    await executeMethod();
                }
                catch (Exception ex)
                {
                    // Due to an issues I've noted online: http://social.msdn.microsoft.com/Forums/en/winappswithcsharp/thread/bea154b0-08b0-4fdc-be31-058d9f5d1c4e
                    // I am limiting the use of 'async void'  In a few rare occasions I use it
                    // and manually route the exceptions to the UnhandledExceptionHandler
                    ((App)App.Current).OnUnhandledException(ex);
                }
            }, canExecuteMethod)
        {
        }
    }
}
