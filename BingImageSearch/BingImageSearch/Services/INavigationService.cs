using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BingImageSearch.Services
{
    public interface INavigationService
    {
        event NavigatingCancelEventHandler Navigating;

        void InitializeFrame(Frame frame);
        void Navigate(Type source, object parameter = null);
        bool CanGoBack { get; }
        void GoBack();
        ICommand GoBackCommand { get; }
    }
}
