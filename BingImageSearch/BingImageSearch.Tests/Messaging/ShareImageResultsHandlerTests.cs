using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using BingImageSearch.Messaging;
using BingImageSearch.Messaging.Handlers;
using BingImageSearch.Model;
using BingImageSearch.Tests.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.ApplicationModel.DataTransfer;

namespace BingImageSearch.Tests.Messaging
{
    public class ShareImageResultsHandlerTests
    {
        [TestClass]
        public class WhenSharingAnImage : Test<ShareImageResultsHandlerScenario>
        {
            private ImageResult _image;
            private SettableDataRequestedEventArgs _args;

            [TestInitialize]
            public void Setup()
            {
                _image = Given.SampleImage();
                _args = Given.MockDataRequest();
                When.SharingWith(_image, _args);
            }
            
            [TestMethod]
            public async void WhenSharingAnImage_ShouldSetBitmap()
            {
                Assert.IsTrue(await Then.BitmapIsSet(_args));
            }

            [TestMethod]
            public void WhenSharingAnImage_ShouldSetTitle()
            {
                Assert.IsTrue(Then.TitleIs(_args, "Bing Search Image"));
            }
        }
    }

    public class ShareImageResultsHandlerScenario : HandlerScenario
    {
        public ShareImageResultsHandlerScenario ()
	    {
            var applicationSettings = new MockApplicationSettings();
            Handler = new ShareImageResultsHandler(applicationSettings);
        }

        public ImageResult SampleImage()
        {
            return new ImageResult() { MediaUrl = "http://example.com/sample_image.jpg", SourceUrl = "http://example.com" };
        }

        public SettableDataRequestedEventArgs MockDataRequest()
        {
            return new SettableDataRequestedEventArgs()
                    {
                        Request = new SettableDataRequest()
                        {
                            Data = new DataPackage(),
                            Deferral = new MockDeferral()
                        }
                    };
        }

        public void SharingWith(ImageResult image, SettableDataRequestedEventArgs dataRequestArgs)
        {
            WhenHandling(() => new ShareImageResultsMessage(image, null, dataRequestArgs));
        }

        public async Task<bool> BitmapIsSet(SettableDataRequestedEventArgs args)
        {
            return (await args.Request.Data.GetView().GetBitmapAsync()) != null;
        }

        public bool TitleIs(SettableDataRequestedEventArgs args, string title)
        {
            return args.Request.Data.Properties.Title == title;
        }
    }
}
