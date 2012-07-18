using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BingImageSearch;
using BingImageSearch.Common;
using BingImageSearch.Messaging;
using BingImageSearch.Model;
using Windows.Foundation;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.Web.Syndication;

namespace BingImageSearch
{
    partial class Shell
    {
        public Frame Frame
        {
            get { return MainFrame; }
        }

        public Shell()
        {
            InitializeComponent();

            Loaded += Shell_Loaded;
            KeyUp += Shell_KeyUp;
        }

        void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterSettings();

            Window.Current.SizeChanged += this.WindowSizeChanged;
        }

        private void WindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            string visualState = ApplicationView.Value.ToString();
            VisualStateManager.GoToState(this, visualState, false);
        }

        private void RegisterSettings()
        {
            SettingsPane.GetForCurrentView().CommandsRequested += (s, e) =>
            {
                var settingsCommand = new SettingsCommand("Preferences", "Preferences", (h) =>
                {
                    this.PreferencesPage.Show();
                });

                e.Request.ApplicationCommands.Add(settingsCommand);
            };
        }

        private static VirtualKey[] alphaKeys = new[] 
            {
                VirtualKey.A, VirtualKey.B, VirtualKey.C, VirtualKey.D, VirtualKey.E,
                VirtualKey.F, VirtualKey.G, VirtualKey.H, VirtualKey.I, VirtualKey.J,
                VirtualKey.K, VirtualKey.L, VirtualKey.M, VirtualKey.N, VirtualKey.O,
                VirtualKey.P, VirtualKey.Q, VirtualKey.R, VirtualKey.S, VirtualKey.T,
                VirtualKey.U, VirtualKey.V, VirtualKey.W, VirtualKey.X, VirtualKey.Y,
                VirtualKey.Z
            };

        void Shell_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Back)
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
                return;
            }

            if (alphaKeys.Contains(e.Key))
            {
                App.ViewModelLocator.Hub.Send(new ShowSearchPaneMessage());   
            }
        }
    }
}
