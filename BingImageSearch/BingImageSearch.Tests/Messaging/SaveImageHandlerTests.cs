using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Messaging;
using BingImageSearch.Messaging.Handlers;
using BingImageSearch.Model;
using BingImageSearch.Tests.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using NotificationsExtensions.ToastContent;
using Windows.Storage;

namespace BingImageSearch.Tests.Messaging
{
    public class SaveImageHandlerTests
    {
        [TestClass]
        public class WhenSavingImageThatIsCanceled : Test<SaveImageHandlerScenario>
        {
            [TestInitialize]
            public void Setup()
            {
                Given.EmptyPickerResults();
                When.SavingImage("http://example.com/sample_image.jpg");
            }

            [TestMethod]
            public void WhenSavingImageThatIsCanceled_ShouldRetrieveImageFromFilterPicker()
            {
                Assert.IsTrue(Then.PickerWasRequested);
            }

            [TestMethod]
            public void WhenSavingImageThatIsCanceled_ShouldFilterSameFileTypeAsSelectedImage()
            {
                Assert.IsTrue(Then.FileTypesShouldContain("JPG"));
            }

            [TestMethod]
            public void WhenSavingImageThatIsCanceled_ShouldNotBeDownloaded()
            {
                Assert.IsTrue(Then.NothingDownloaded);
            }
        }

        [TestClass]
        public class WhenSavingImageWithFile : Test<SaveImageHandlerScenario>
        {
            [TestInitialize]
            public void Setup()
            {
                Given.PickerResultSetTo("FileSavedFromPicker.jpg");
                When.SavingImage("http://example.com/sample_image.jpg");
            }

            [TestMethod]
            public void WhenSavingImageWithFile_ShouldSetStatusWhenCompleted()
            {
                Assert.IsTrue(Then.TempMessageSetTo("Image FileSavedFromPicker.jpg saved."));
            }

            [TestMethod]
            public void WhenSavingImageWithFile_ShouldHaveDownloadedSampleImage()
            {
                Assert.IsTrue(Then.DownloadWasStartedFor("http://example.com/sample_image.jpg"));
            }
        }

        public class SaveImageHandlerScenario : HandlerScenario
        {
            private MockPickerFactory _pickerFactory;           
            private MockBackgroundDownloader _mockDownloader;
            private MockStatusService _mockStatusService;

            public SaveImageHandlerScenario()
            {
                _pickerFactory = new MockPickerFactory();
                _mockDownloader = new MockBackgroundDownloader();
                _mockStatusService = new MockStatusService();

                Handler = new SaveImageHandler(_pickerFactory, _mockDownloader, _mockStatusService);
            }

            public SaveImageHandlerScenario EmptyPickerResults()
            {
                return this;
            }

            public SaveImageHandlerScenario PickerResultSetTo(string name)
            {
                _pickerFactory.MockFileSavePicker.Result = new MockStorageFile() { Name = name };
                return this;
            }

            public void SavingImage(string mediaUrl)
            {
                WhenHandling(() => new SaveImageMessage(new ImageResult() { MediaUrl = mediaUrl }));
            }

            public bool PickerWasRequested
            {
                get { return _pickerFactory.MockFileSavePicker.PickerWasRequested; }
            }

            public bool FileTypesShouldContain(string fileType)
            {
                return _pickerFactory.MockFileSavePicker.FileTypeChoices.ContainsKey(fileType);
            }

            public bool TempMessageSetTo(string expectedMessage)
            {
                return _mockStatusService.TemporaryMessage == expectedMessage;
            }

            public bool NothingDownloaded
            {
                get { return _mockDownloader.StartedDownloadingWithUrl == null; }
            }

            public bool DownloadWasStartedFor(string uri)
            {
                return _mockDownloader.StartedDownloadingWithUrl == uri;
            }
        }
    }
}
