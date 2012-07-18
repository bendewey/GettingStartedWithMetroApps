using BingImageSearch.Messaging;
using BingImageSearch.Messaging.Handlers;
using BingImageSearch.Model;
using NotificationsExtensions.TileContent;
using BingImageSearch.Tests.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace BingImageSearch.Tests.Messaging
{
    public class UpdateTileHandlerTests
    {
        [TestClass]
        public class WhenUpdatingTileWithSmallImage : Test<UpdateTileHandlerScenario>
        {
            private ImageResult _image;

            [TestInitialize]
            public void Setup()
            {
                _image = Given.SmallImage();
                When.UpdatingTileWith(_image);
            }

            [TestMethod]
            public void WhenUpdatingTileWithSmallImage_ShouldSendTileNotification()
            {
                Assert.IsTrue(Then.NotificationSent);
            }

            [TestMethod]
            public void WhenUpdatingTileWithSmallImage_ShouldHaveSquareContent()
            {
                Assert.IsTrue(Then.HasSquareContent);
            }

            [TestMethod]
            public void WhenUpdatingTileWithSmallImage_ShouldHaveImageSetToMediaUrl()
            {
                Assert.IsTrue(Then.TileImageSetTo(_image.MediaUrl));
                Assert.IsTrue(Then.SquareTileImageSetTo(_image.MediaUrl));
            }

            [TestMethod]
            public void WhenUpdatingTileWithSmallImage_ShouldHaveTitleSetToTitle()
            {
                Assert.IsTrue(Then.TileTitleSetTo(_image.Title));
            }
        }

        [TestClass]
        public class WhenUpdatingTileWithLargeImage : Test<UpdateTileHandlerScenario>
        {
            private ImageResult _image;

            [TestInitialize]
            public void Setup()
            {
                _image = Given.LargeImage();
                When.UpdatingTileWith(_image);
            }

            [TestMethod]
            public void WhenUpdatingTileWithLargeImage_ShouldHaveImageSetToThumbnailImage()
            {
                Assert.IsTrue(Then.TileImageSetTo(_image.Thumbnail.MediaUrl));
                Assert.IsTrue(Then.SquareTileImageSetTo(_image.Thumbnail.MediaUrl));
            }
        }

        public class UpdateTileHandlerScenario : HandlerScenario
        {
            private MockTileUpdateManagerAdapter _mockTileUpdateManager;
            private MockStatusService _mockStatusService;

            public UpdateTileHandlerScenario()
            {
                _mockTileUpdateManager = new MockTileUpdateManagerAdapter();
                _mockStatusService = new MockStatusService();

                Handler = new UpdateTileHandler(_mockTileUpdateManager, _mockStatusService);
            }

            public ImageResult LargeImage(
                string mediaUrl = "http://example.com/sample_image.jpg", 
                string title = "Sample Image",
                string thumbnailUrl = "http://example.com/sample_thumbnail.jpg")
            {
                return new ImageResult() { MediaUrl = mediaUrl, Title = title, Height = 1000, Width = 1200, Thumbnail = new Thumbnail() { MediaUrl = thumbnailUrl } };
            }

            public ImageResult SmallImage(
                string mediaUrl = "http://example.com/sample_image.jpg", 
                string title = "Sample Image")
            {
                return new ImageResult() { MediaUrl = mediaUrl, Title = title };
            }

            public void UpdatingTileWith(ImageResult ImageResult)
            {
                WhenHandling(() => new UpdateTileMessage(ImageResult));
            }

            public ITileWidePeekImageAndText01 LastTile
            {
                get { return _mockTileUpdateManager.Last<ITileWidePeekImageAndText01>(); }
            }

            public ITileSquareImage SquareContent
            {
                get { return LastTile.SquareContent as ITileSquareImage; }
            }

            public bool NotificationSent
            {
                get { return LastTile != null; }
            }

            public bool HasSquareContent
            {
                get { return SquareContent != null; }
            }

            public bool TileImageSetTo(string source)
            {
                return LastTile.Image.Src == source;
            }

            public bool SquareTileImageSetTo(string source)
            {
                return SquareContent.Image.Src == source;
            }

            public bool TileTitleSetTo(string title)
            {
                return LastTile.TextBodyWrap.Text == title;
            }
        }
    }
}