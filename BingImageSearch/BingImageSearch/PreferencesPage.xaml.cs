using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace BingImageSearch
{
    public sealed partial class PreferencesPage
    {
        public PreferencesPage()
        {
            InitializeComponent();
            BackButton.IsEnabled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        public void Show()
        {
            VisualStateManager.GoToState(this, "PreferencesOpened", true);
        }

        public void Hide()
        {
            VisualStateManager.GoToState(this, "PreferencesClosed", true);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Overlay_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Hide();
        }
    }
}
