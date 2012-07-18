using BingImageSearch.Common;
using BingImageSearch.Messaging;
using BingImageSearch.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace BingImageSearch.Tests.ViewModels
{
    public class ShellViewModelTests
    {
        [TestClass]
        public class WhenShellLoads : Test<ShellViewModelScenario>
        {
            [TestMethod]
            public void WhenShellLoads_MessageShouldBeInitializing()
            {
                Assert.IsTrue(Then.MessageIs("Initializing..."));
            }

            [TestMethod]
            public void WhenShellLoads_TitleIsSameAsAppName()
            {
                Assert.IsTrue(Then.TitleIsSameAsAppName);
            }

            [TestMethod]
            public void WhenShellLoads_ShouldBeLoading()
            {
                Assert.IsTrue(Then.ViewModel.IsLoading);
            }
        }

        [TestClass]
        public class WhenShellNetworkIsUnavailable : Test<ShellViewModelScenario>
        {
            [TestMethod]
            public void WhenShellNetworkIsUnavailable_StatusCommandShouldShowSettings()
            {
                Given.NetworkIsUnavailable();
                When.ExecutingStatusCommand();
                Assert.IsTrue(Then.MessageSent<ShowSettingsMessage>());
            }

            [TestMethod]
            public void WhenShellNetworkIsUnavailable_ErrorMessageIsSet()
            {
                Given.NetworkIsUnavailable();
                Assert.IsTrue(Then.ErrorMessageIsSet);
            }
        }

        [TestClass]
        public class WhenShellBingIsUnavailable : Test<ShellViewModelScenario>
        {
            [TestMethod]
            public void WhenShellBingIsUnavailable_StatusCommandShouldShowSettings()
            {
                Given.BingIsUnavailable();
                When.ExecutingStatusCommand();
                Assert.IsTrue(Then.MessageSent<ShowSettingsMessage>());
            }

            [TestMethod]
            public void WhenShellBingIsUnavailable_ErrorMessageIsSet()
            {
                Given.BingIsUnavailable();
                Assert.IsTrue(Then.ErrorMessageIsSet);
            }
        }
    }

    public class ShellViewModelScenario : ViewModelScenarioBase
    {
        public ShellViewModel ViewModel { get; private set; }
        public AppName AppName { get; private set; }

        public ShellViewModelScenario()
        {
            AppName = (AppName)"Test App Name";
            ViewModel = new ShellViewModel(AppName, ApplicationSettings,  NavigationService, MessageHub);
        }
    
        public void NetworkIsUnavailable()
        {
            ViewModel.SetNetworkUnavailable();
        }

        public void BingIsUnavailable()
        {
            ViewModel.SetBingUnavailable();
        }

        public bool TitleIsSameAsAppName
        {
            get { return ViewModel.Title == AppName; }
        }

        public bool ErrorMessageIsSet
        {
            get { return !string.IsNullOrWhiteSpace(ViewModel.ErrorMessage); }
        }

        public bool MessageIs(string expected)
        {
            return ViewModel.Message == expected;
        }

        public void ExecutingStatusCommand()
        {
            ViewModel.StatusCommand.Execute(null);
        }
    }
}
