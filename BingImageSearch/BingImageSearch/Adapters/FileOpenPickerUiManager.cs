using System.Collections.Generic;
using BingImageSearch.Services;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;

namespace BingImageSearch.Adapters
{
    public class FileOpenPickerUiManager : IFileOpenPickerUiManager
    {
        private FileOpenPickerUI _fileOpenPicker;

        public void Initialize(FileOpenPickerUI fileOpenPicker)
        {
            _fileOpenPicker = fileOpenPicker;
        }

        public FileSelectionMode SelectionMode
        {
            get { return _fileOpenPicker.SelectionMode; }
        }

        public IReadOnlyList<string> AllowedFileTypes
        {
            get { return _fileOpenPicker.AllowedFileTypes; }
        }

        public AddFileResult AddFile(string id, IStorageFile file)
        {
            return _fileOpenPicker.AddFile(id, file);
        }

        public void RemoveFile(string id)
        {
            _fileOpenPicker.RemoveFile(id);
        }
    }
}
