using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using BingImageSearch.Messaging;
using BingImageSearch.Messaging.Handlers;
using BingImageSearch.Tests.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.ApplicationModel.DataTransfer;

namespace BingImageSearch.Tests.Messaging
{
    public class ShareUriHandlerTests
    {
        [TestClass]
        public class WhenSharingUri : Test<ShareUriHandlerScenario>
        {
            private Uri _uri;
            private SettableDataRequestedEventArgs _args;

            [TestInitialize]
            public void Setup()
            {
                _uri = Given.SearchUriFor("cars");
                _args = Given.MockDataRequest();
                When.SharingWith(_uri, _args);
            }
            
            [TestMethod]
            public async void WhenSharingUri_ShouldSetUri()
            {
                Assert.IsTrue(await Then.UriIs(_args, _uri));
            }

            [TestMethod]
            public void WhenSharingUri_ShouldSetTitle()
            {
                Assert.IsTrue(Then.TitleIs(_args, "Bing Image Search Link"));
            }
        }
    }

    public class ShareUriHandlerScenario : HandlerScenario
    {
        public ShareUriHandlerScenario()
	    {
            Handler = new ShareUriHandler();
        }

        public Uri SearchUriFor(string query)
        {
            return new Uri("http://bing.com/search/q=" + query);
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

        public void SharingWith(Uri uri, SettableDataRequestedEventArgs dataRequestArgs)
        {
            WhenHandling(() => new ShareUriMessage(uri, null, dataRequestArgs));
        }

        public async Task<bool> UriIs(SettableDataRequestedEventArgs args, Uri uri)
        {
            return (await args.Request.Data.GetView().GetUriAsync()) == uri;
        }

        public bool TitleIs(SettableDataRequestedEventArgs args, string title)
        {
            return args.Request.Data.Properties.Title == title;
        }
    }
}
