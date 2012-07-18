using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace BingImageSearch.Adapters
{
    public interface IFileSavePicker
    {
        string CommitButtonText { get; set; }
        string DefaultFileExtension { get; set; }
        IDictionary<string, IList<string>> FileTypeChoices { get; set; }
        string SuggestedFileName { get; set; }
        PickerLocationId SuggestedStartLocation { get; set; }
        Task<IStorageFile> PickSaveFileAsync();
    }
}