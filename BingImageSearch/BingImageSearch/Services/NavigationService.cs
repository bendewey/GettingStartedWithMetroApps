using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BingImageSearch.Common;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BingImageSearch.Services
{
    public class NavigationService : INavigationService
    {
        public event NavigatingCancelEventHandler Navigating = delegate { };
        private Frame _frame;

        public NavigationService()
        {
            GoBackCommand = new DelegateCommand(GoBack, () => CanGoBack);
        }

        public ICommand GoBackCommand { get; set; }

        public bool CanGoBack
        {
            get { return _frame.CanGoBack; }
        }

        public void InitializeFrame(Frame frame)
        {
            if (_frame != null)
            {
                _frame.Navigating -= Frame_Navigating;
            }

            _frame = frame;
            _frame.Navigating += Frame_Navigating;
        }

        void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Navigating(sender, e);
        }

        public void Navigate(Type source, object parameter = null)
        {
            if (_frame == null)
            {
                throw new InvalidOperationException("Frame has not been initialized.");
            }
            _frame.Navigate(source, parameter);
            ((DelegateCommandBase) GoBackCommand).RaiseCanExecuteChanged();
        }

        public void GoBack()
        {
            if (CanGoBack)
            {
                _frame.GoBack();
                ((DelegateCommandBase)GoBackCommand).RaiseCanExecuteChanged();
            }
        }
    }
}
