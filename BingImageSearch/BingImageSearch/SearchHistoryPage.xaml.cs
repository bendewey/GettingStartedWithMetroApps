using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BingImageSearch.Model;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

namespace BingImageSearch
{
    public sealed partial class SearchHistoryPage
    {
        public SearchHistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
        
        }

        private void ItemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ((SearchHistoryPageViewModel) DataContext).SelectedItem = (SearchInstance)e.ClickedItem;
        }
    }
}
