using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BingImageSearch.Model;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace BingImageSearch
{
    public sealed partial class DetailsPage
    {
        private DetailsPageViewModel ViewModel
        {
            get { return DataContext as DetailsPageViewModel; }
        }

        public DetailsPage()
        {
            InitializeComponent();
        }

        // using Windows.UI.Xaml.Navigation;.
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

        private async void ImageOptionsCommand_Click(object sender, RoutedEventArgs e)
        {
            var menu = new PopupMenu();
            menu.Commands.Add(new UICommand("Set Lock Screen", ViewModel.SetLockScreenCommand.Execute));
            menu.Commands.Add(new UICommand("Set Tile", ViewModel.SetTileCommand.Execute));
            if (ApplicationView.Value != ApplicationViewState.Snapped)
            {
                menu.Commands.Add(new UICommand("Save", ViewModel.SaveCommand.Execute));
                menu.Commands.Add(new UICommand("Share", ViewModel.ShareCommand.Execute));
            }

            var chosenCommand = await menu.ShowForSelectionAsync(GetElementRect((FrameworkElement)sender), Placement.Above);
        }

        Rect GetElementRect(FrameworkElement element)
        {
            var buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }
    }
}
