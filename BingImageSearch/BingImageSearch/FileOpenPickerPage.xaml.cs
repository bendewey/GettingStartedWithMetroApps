using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BingImageSearch.Model;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace BingImageSearch
{
    public sealed partial class FileOpenPickerPage
    {
        private FileOpenPickerPageViewModel ViewModel
        {
            get { return DataContext as FileOpenPickerPageViewModel; }
        }

        public FileOpenPickerPage()
        {
            InitializeComponent();
        }

        private async void ItemGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var vm = ViewModel;
                if (vm == null) return;

                foreach (var image in e.AddedItems)
                {
                    await vm.AddImage(image);
                }
                foreach (var image in e.RemovedItems)
                {
                    vm.RemoveImage(image);
                }
            }
            catch (Exception ex)
            {
                // Due to an issues I've noted online: http://social.msdn.microsoft.com/Forums/en/winappswithcsharp/thread/bea154b0-08b0-4fdc-be31-058d9f5d1c4e
                // I am limiting the use of 'async void'  In a few rare occasions I use it
                // and manually route the exceptions to the UnhandledExceptionHandler
                ((App)App.Current).OnUnhandledException(ex);
            }
        }
    }
}
