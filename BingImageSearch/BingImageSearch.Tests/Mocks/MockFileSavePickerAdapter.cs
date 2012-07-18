using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace BingImageSearch.Tests.Mocks
{
    public class MockFileSavePickerAdapter : IFileSavePicker
    {
        public MockFileSavePickerAdapter()
        {
            FileTypeChoices = new Dictionary<string, IList<string>>();
        }

        public string CommitButtonText { get; set; }
        public string DefaultFileExtension { get; set; }
        public IDictionary<string, IList<string>> FileTypeChoices { get; set; }
        public string SuggestedFileName { get; set; }
        public PickerLocationId SuggestedStartLocation { get; set; }

        public bool PickerWasRequested { get; set; }
        public IStorageFile Result { get; set; }

        public Task<IStorageFile> PickSaveFileAsync()
        {
            PickerWasRequested = true;
            return Task.Run(() => Result);
        }
    }
}
