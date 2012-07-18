using System;
using System.Collections.Generic;
using BingImageSearch.Adapters;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;

namespace BingImageSearch.Tests.Mocks
{
    class MockFileOpenPickerUiManager : IFileOpenPickerUiManager
    {
        public MockFileOpenPickerUiManager()
        {
            AddedFileIds = new List<string>();
            RemovedFileIds = new List<string>();

            ExpectedResult = AddFileResult.Added;
        }

        public void Initialize(FileOpenPickerUI fileOpenPicker)
        {
            throw new NotImplementedException();
        }

        public FileSelectionMode SelectionMode { get; set; }
        public IReadOnlyList<string> AllowedFileTypes { get; set; }

        public List<string> AddedFileIds { get; set; }
        public List<string> RemovedFileIds { get; set; }

        public AddFileResult ExpectedResult { get; set; }

        public AddFileResult AddFile(string id, IStorageFile storageFile)
        {
            AddedFileIds.Add(id);
            return ExpectedResult;
        }

        public void RemoveFile(string id)
        {
            RemovedFileIds.Add(id);
        }
    }
}
