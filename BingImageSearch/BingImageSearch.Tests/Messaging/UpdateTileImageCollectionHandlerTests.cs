using System.Linq;
using BingImageSearch.Messaging;
using BingImageSearch.Messaging.Handlers;
using BingImageSearch.Model;
using NotificationsExtensions.TileContent;
using BingImageSearch.Tests.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace BingImageSearch.Tests.Messaging
{
    public class UpdateTileImageCollectionHandlerTests
    {
        [TestClass]
        public class WhenUpdatingTileWithSearchInstance : Test<UpdateTileHandlerScenario>
        {
            private SearchInstance _instance;

            [TestInitialize]
            public void Setup()
            {
                _instance = Given.SampleSearchInstance();
                When.UpdatingTileWith(_instance);
            }

            [TestMethod]
            public void WhenUpdatingTileWithSearchInstance_ShouldSendTileNotification()
            {
                Assert.IsTrue(Then.NotificationSent);
            }

            [TestMethod]
            public void WhenUpdatingTileWithSearchInstance_ShouldHaveAllImagesSet()
            {
                Assert.IsTrue(Then.AllImagesAreSet());
            }

            [TestMethod]
            public void WhenUpdatingTileWithSmallImage_ShouldHaveTitleSetToQuery()
            {
                Assert.IsTrue(Then.TileTitleSetTo("Search for " + _instance.Query));
            }
        }

        public class UpdateTileHandlerScenario : HandlerScenario
        {
            private MockTileUpdateManagerAdapter _mockTileUpdateManager;

            public UpdateTileHandlerScenario()
            {
                _mockTileUpdateManager = new MockTileUpdateManagerAdapter();
                Handler = new UpdateTileImageCollectionHandler(_mockTileUpdateManager);
            }

            public SearchInstance SampleSearchInstance(
                string query = "Sample Query",
                int imageCount = 10)
            {
                var instance = new SearchInstance() { Query = query, Images = new List<ImageResult>() };

                for(int i = 0; i<imageCount; i++)
                {
                    instance.Images.Add(new ImageResult() {  
                        Title = "Sample Image " + i,
                        Thumbnail = new Thumbnail() { MediaUrl = "http://example.com/sample" + i + ".jpg" }
                    });
                }

                return instance;
            }

            public ITileWidePeekImageCollection06 LastTile
            {
                get { return _mockTileUpdateManager.Last<ITileWidePeekImageCollection06>(); }
            }

            public bool NotificationSent
            {
                get { return LastTile != null; }
            }

            public void UpdatingTileWith(SearchInstance ImageResult)
            {
                WhenHandling(() => new UpdateTileImageCollectionMessage(ImageResult));
            }

            public bool AllImagesAreSet()
            {
                var images = new[] 
                { 
                    LastTile.ImageMain, 
                    LastTile.ImageSecondary, 
                    LastTile.ImageSmallColumn1Row1,
                    LastTile.ImageSmallColumn1Row2,
                    LastTile.ImageSmallColumn2Row1,
                    LastTile.ImageSmallColumn2Row2
                };

                return images.All(img => img.Src.StartsWith("http://example.com"));
            }                                           
                                            
            public bool TileTitleSetTo(string heading)
            {
                return LastTile.TextHeadingWrap.Text == heading;
            }
        }
    }
}