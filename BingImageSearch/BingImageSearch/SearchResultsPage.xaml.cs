using System;
using System.Collections.Generic;
using BingImageSearch.Model;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace BingImageSearch
{
    public sealed partial class SearchResultsPage
    {
        public SearchResultsPage()
        {
            InitializeComponent();
        }

        private SearchResultsPageViewModel ViewModel
        {
            get { return DataContext as SearchResultsPageViewModel; }
        }

        private void List_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedImage = e.ClickedItem as ImageResult;
        	ViewModel.ViewDetails();
        }

        private void Item_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
        	ViewModel.ViewDetails();
        }

        private void List_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ViewModel.ViewDetails();
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void ListView_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ViewModel.ViewDetails();
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
