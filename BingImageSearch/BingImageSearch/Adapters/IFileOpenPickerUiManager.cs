using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;

namespace BingImageSearch.Adapters
{
    public interface IFileOpenPickerUiManager
    {
        void Initialize(FileOpenPickerUI fileOpenPicker);

        FileSelectionMode SelectionMode { get; }
        IReadOnlyList<string> AllowedFileTypes { get; }
        AddFileResult AddFile(string id, IStorageFile storageFile);
        void RemoveFile(string id);
    }
}