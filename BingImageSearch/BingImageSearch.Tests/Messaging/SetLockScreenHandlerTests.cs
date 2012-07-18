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

namespace BingImageSearch.Tests.Messaging
{
    public class SetLockScreenHandlerTests
    {
            [TestClass]
            public class WhenSettingLockScreen : Test<SetLockScreenHandlerScenario>
            {
                ImageResult _image;

                [TestInitialize]
                public void Setup()
                {
                    _image = Given.SampleImage();
                    When.SettingLockScreen(_image);
                }

                [TestMethod]
                public void WhenSettingLockScreen_ShouldUpdateLockScreen()
                {
                    Assert.IsTrue(Then.ALockScreenIsSet);
                }

                [TestMethod]
                public void WhenSettingLockScreen_ShouldUpdateStatusWhenComplete()
                {
                    Assert.IsTrue(Then.StatusTempMessageSetTo("Image " + _image.Title + " set as lock screen."));
                }
            }

            public class SetLockScreenHandlerScenario : HandlerScenario
            {
                private MockApplicationSettings _settings;
                private MockLockScreen _mockLockScreen;
                private MockStatusService _mockStatusService;

                public SetLockScreenHandlerScenario()
                {
                    _settings = new MockApplicationSettings();
                    _mockLockScreen = new MockLockScreen();
                    _mockStatusService = new MockStatusService();

                    Handler = new SetLockScreenHandler(_settings, _mockLockScreen, _mockStatusService);
                }

                public ImageResult SampleImage()
                {
                    return new ImageResult()
                        {
                            Title = "Sample Image 1",
                            MediaUrl = "http://example.com/sample1.jpg",
                            Thumbnail = new Thumbnail() { MediaUrl = "http://example.com/thumbnail.jpg" }
                        };
                }

                public void SettingLockScreen(ImageResult image)
                {
                    WhenHandling(() => new SetLockScreenMessage(image));
                }

                public bool ALockScreenIsSet
                {
                    get { return _mockLockScreen.CurrentLockScreen != null; }
                }

                public bool StatusTempMessageSetTo(string expectedMessage)
                {
                    return _mockStatusService.TemporaryMessage == expectedMessage;
                }

                //public bool DownloadWasStartedFor(string uri)
                //{
                //    return _mockDownloader.StartedDownloadingWithUrl == uri;
                //}
            }
    
    }
}
