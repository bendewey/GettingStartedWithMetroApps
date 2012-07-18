using System;
using System.Linq;
using System.Net;
using Bing;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingSimpleSearch
{
    /// <summary>
    /// The code-behind for the MainPage of the app
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Event Handler for the Search Button
        /// </summary>
        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            string accountKey = "<Account Key>";

            var context = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Data.ashx/Bing/Search"));
            context.Credentials = new NetworkCredential(accountKey, accountKey);

            var result = await context.Image(this.SearchQuery.Text, "en-US", null, null, null, null).ExecuteAsync();
            ImagesList.ItemsSource = result.ToList();
        }

        /// <summary>
        /// Event Handler for the Save Button
        /// </summary>
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            var image = ImagesList.SelectedItem as ImageResult;
            if (image == null) return;

            var uri = new Uri(image.MediaUrl);
            var filename = uri.Segments[uri.Segments.Length - 1];
            var extension = System.IO.Path.GetExtension(filename);

            var picker = new FileSavePicker();
            picker.SuggestedFileName = filename;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeChoices.Add(extension.Trim('.').ToUpper(), new string[] { extension });

            var saveFile = await picker.PickSaveFileAsync();
            if (saveFile != null)
            {
                Status.Text = "Download Started";
                var download = new BackgroundDownloader().CreateDownload(uri, saveFile);
                await download.StartAsync();
                Status.Text = "Download Complete";
            }
        }
    }
}
