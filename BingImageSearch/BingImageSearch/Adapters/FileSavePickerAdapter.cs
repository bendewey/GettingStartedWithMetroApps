using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace BingImageSearch.Adapters
{
    public class FileSavePickerAdapter : IFileSavePicker
    {
        public FileSavePickerAdapter()
        {
            CommitButtonText = String.Empty;
            FileTypeChoices = new Dictionary<string, IList<string>>();
            SuggestedFileName = String.Empty;
            // The system default is DocumentsLibrary, but this app is all about images
            SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        }

        public string CommitButtonText { get; set; }
        public string DefaultFileExtension { get; set; }
        public IDictionary<string, IList<string>> FileTypeChoices { get; set; }
        public string SuggestedFileName { get; set; }
        public PickerLocationId SuggestedStartLocation { get; set; }
        
        public async Task<IStorageFile> PickSaveFileAsync()
        {
            var picker = new FileSavePicker();
            picker.CommitButtonText = CommitButtonText;
            picker.SuggestedFileName = SuggestedFileName;
            picker.SuggestedStartLocation = SuggestedStartLocation;
            if (DefaultFileExtension != null)
            {
                picker.DefaultFileExtension = DefaultFileExtension;
            }

            foreach(var choice in FileTypeChoices.Keys)
            {
                picker.FileTypeChoices.Add(choice, FileTypeChoices[choice]);
            }

            return await picker.PickSaveFileAsync();
        }
    }
}
