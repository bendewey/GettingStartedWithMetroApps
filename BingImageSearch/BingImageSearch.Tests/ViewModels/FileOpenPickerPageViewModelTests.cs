using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BingImageSearch.Model;
using BingImageSearch.Tests.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace BingImageSearch.Tests.ViewModels
{
    public class FileOpenPickerPageViewModelTests
    {
        [TestClass]
        public class WhenFileOpenPickerIsSearching : Test<FileOpenPickerPageViewModelScenario>
        {
            [TestInitialize]
            public async void Setup()
            {
                Given.ViewModel.SearchQuery = "Sample";
                await When.Searching();
            }

            [TestMethod]
            public void WhenFileOpenPickerIsSearching_SearchedWithTheRightQuery()
            {
                Assert.IsTrue(Then.SearchedWith("Sample"));
            }

            [TestMethod]
            public void WhenFileOpenPickerIsSearching_ShouldSetImages()
            {
                Assert.IsNotNull(Then.ViewModel.Images);
                Assert.IsTrue(Then.ViewModel.Images.Count > 0);
            }
        }


        [TestClass]
        public class WhenFileOpenPickerIsAddingAnImage : Test<FileOpenPickerPageViewModelScenario>
        {
            private ImageResult _image;

            [TestInitialize]
            public async void Setup()
            {
                _image = Given.ImageFor("http://example.com/image.jpg");
                await When.AddingImageFor(_image);
            }

            [TestMethod]
            public void WhenFileOpenPickerIsAddingAnImage_ShouldAddFile()
            {
                Assert.IsTrue(Then.ImageAddedFor(_image));
            }
        }
    }

    public class FileOpenPickerPageViewModelScenario : ViewModelScenarioBase
    {
        public FileOpenPickerPageViewModel ViewModel { get; private set; }
        private readonly MockImageSearchService _imageSearchService;
        private readonly MockFileOpenPickerUiManager _adapter;
        
        public FileOpenPickerPageViewModelScenario()
        {
            _imageSearchService = new MockImageSearchService();
            _adapter = new MockFileOpenPickerUiManager()
            {
                AllowedFileTypes = new List<string>() { "jpg" }
            };
            ViewModel = new FileOpenPickerPageViewModel(ApplicationSettings, _imageSearchService, _adapter);
        }

        public ImageResult ImageFor(string mediaUrl)
        {
            return new ImageResult() { MediaUrl = mediaUrl };
        }

        public Task Searching()
        {
            return ViewModel.Search();
        }

        public Task AddingImageFor(ImageResult image)
        {
            return ViewModel.AddImage(image);
        }

        public bool SearchedWith(string query)
        {
            return _imageSearchService.Searches.Where(s => s.Search == query).Count() == 1;
        }

        public bool ImageAddedFor(ImageResult image)
        {
            return _adapter.AddedFileIds.Contains(image.MediaUrl);
        }
    }
}
