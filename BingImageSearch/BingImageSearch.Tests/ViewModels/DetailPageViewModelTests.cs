using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BingImageSearch.Tests.Messaging;
using BingImageSearch.Tests.Mocks;
using BingImageSearch.Messaging;
using BingImageSearch.Model;
using BingImageSearch.Services;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace BingImageSearch.Tests.ViewModels
{
    public class DetailPageViewModelTests
    {
        [TestClass]
        public class GivenDetailsForSampleImage : Test<DetailPageViewModelScenario>
        {
            ImageResult _image;

            [TestInitialize]
            public void Setup()
            {
                _image = new ImageResult() { MediaUrl = "http://example.com/sample_image.jpg" };
                Given.DetailsFor(_image);
            }

            [TestMethod]
            public void WhenSavingImage_ShouldSendSaveMessage()
            {
                When.Saving();
                Assert.IsTrue(Then.Message<SaveImageMessage>().SentWith(_image));
            }

            [TestMethod]
            public void WhenSettingLockScreen_ShouldSendSetLockScreenMessage()
            {
                When.SettingLockScreen();
                Assert.IsTrue(Then.Message<SetLockScreenMessage>().SentWith(_image));
            }

            [TestMethod]
            public void WhenSettingTile_ShouldSendSetTileMessage()
            {
                When.SettingTile();
                Assert.IsTrue(Then.Message<UpdateTileMessage>().SentWith(_image));
            }

            [TestMethod]
            public void WhenSharing_ShouldSendShowShareUiMessage()
            {
                When.Sharing();
                Assert.IsTrue(Then.MessageSent<ShowShareUiMessage>());
            }

            [TestMethod]
            public void WhenShowingSettings_ShouldSendShowSettingsMessage()
            {
                When.ShowingSettings();
                Assert.IsTrue(Then.MessageSent<ShowSettingsMessage>());
            }
        }
    }

    public class DetailPageViewModelScenario : ViewModelScenarioBase
    {
        private readonly MockShareDataRequestedPump _shareMessagePump;
        public DetailsPageViewModel ViewModel { get; set; }

        public DetailPageViewModelScenario()
        {
            _shareMessagePump = new MockShareDataRequestedPump();
        }

        public void DetailsFor(ImageResult details)
        {
            ApplicationSettings.SelectedInstance = new SearchInstance();
            ApplicationSettings.SelectedImage = details;
            ViewModel = new DetailsPageViewModel(ApplicationSettings, NavigationService, MessageHub, _shareMessagePump, new MockStatusService());
        }

        public void Saving()
        {
            ViewModel.SaveCommand.Execute(null);
        }

        public void SettingLockScreen()
        {
            ViewModel.SetLockScreenCommand.Execute(null);
        }

        public void SettingTile()
        {
            ViewModel.SetTileCommand.Execute(null);
        }

        public void Sharing()
        {
            ViewModel.ShareCommand.Execute(null);
        }

        public void ShowingSettings()
        {
            ViewModel.SettingsCommand.Execute(null);
        }
    }
}
