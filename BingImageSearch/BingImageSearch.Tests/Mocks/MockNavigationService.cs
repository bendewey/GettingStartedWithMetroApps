using System;
using System.Collections.Generic;
using System.Windows.Input;
using BingImageSearch.Common;
using BingImageSearch.Services;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BingImageSearch.Tests.Mocks
{
    public class MockNavigationService : INavigationService
    {
        public event NavigatingCancelEventHandler Navigating = delegate { };

        public MockNavigationService()
        {
            NavigationStack = new Stack<NavigationItem>();
            GoBackCommand = new DelegateCommand(GoBack, () => CanGoBack);
        }

        private bool WasInitialized { get; set; }
        public Stack<NavigationItem> NavigationStack { get; set; }

        public void InitializeFrame(Frame frame)
        {
            WasInitialized = true;
        }

        public void Navigate(Type source, object parameter = null)
        {
            NavigationStack.Push(new NavigationItem() {Source = source, Parameter = parameter});
        }

        public bool CanGoBack
        {
            get { return false; }
        }

        public void GoBack()
        {
        }

        public ICommand GoBackCommand { get; set; }
    }

    public class NavigationItem
    {
        public Type Source { get; set; }
        public object Parameter { get; set; }
    }
}
