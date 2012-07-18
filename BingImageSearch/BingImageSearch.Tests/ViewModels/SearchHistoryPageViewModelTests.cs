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
    public class SearchHistoryPageViewModelTests
    {
        [TestClass]
        public class WhenSearching : Test<SearchHistoryPageViewModelScenario>
        {
            [TestMethod]
            public void WhenSearching_ShouldSendShowSearchMessage()
            {
                When.Searching();
                Assert.IsTrue(Then.MessageSent<ShowSearchPaneMessage>());
            }
        }

        [TestClass]
        public class WhenShowingSettings : Test<SearchHistoryPageViewModelScenario>
        {
            [TestMethod]
            public void WhenShowingSettings_ShouldSendShowSettingsMessage()
            {
                When.ShowingSettings();
                Assert.IsTrue(Then.MessageSent<ShowSettingsMessage>());
            }
        }

        [TestClass]
        public class WhenChangingSelectedItem : Test<SearchHistoryPageViewModelScenario>
        {
            private SearchInstance _instance;

            [TestInitialize]
            public void Setup()
            {
                _instance = Given.SampleSearchInstance();
                When.ChangingSelectedItemTo(_instance);
            }

            [TestMethod]
            public void WhenChangingSelectedItem_ShouldUpdateSettings()
            {
                Assert.IsTrue(Then.SettingsSelectedInstanceIs(_instance));
            }
        
            [TestMethod]
            public void WhenChangingSelectedItem_ShouldNavigateToResultsPage()
            {
                Assert.IsTrue(Then.NavigatedTo<SearchResultsPage>());
            }
        }
    }

    public class SearchHistoryPageViewModelScenario : ViewModelScenarioBase
    {
        public SearchHistoryPageViewModel ViewModel { get; set; }

        public SearchHistoryPageViewModelScenario()
        {
            ViewModel = new SearchHistoryPageViewModel(ApplicationSettings, NavigationService, MessageHub, new MockStatusService());
        }

        public void ShowingSettings()
        {
            ViewModel.SettingsCommand.Execute(null);
        }

        public void Searching()
        {
            ViewModel.SearchCommand.Execute(null);
        }

        public SearchInstance SampleSearchInstance()
        {
            return new SearchInstance() { Query = "Sample Query" };
        }

        public void ChangingSelectedItemTo(SearchInstance instance)
        {
            ViewModel.SelectedItem = instance;
        }

        public bool SettingsSelectedInstanceIs(SearchInstance instance)
        {
            return ApplicationSettings.SelectedInstance == instance;
        }
    }
}
