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
    public class SearchResultsPageViewModelTests
    {
        [TestClass]
        public class WhenSettingThumbnailView : Test<SearchResultsPageViewModelScenario>
        {
            [TestInitialize]
            public void Setup()
            {
                When.ExecutingThumbnailCommand();
            }

            [TestMethod]
            public void WhenSettingThumbnailView_ShouldChangeCurrentView()
            {
                Assert.IsTrue(Then.CurrentViewIs(SearchResultsPageViewModes.ThumbnailView));
            }

            [TestMethod]
            public void WhenSettingThumbnailView_ShouldBeInThumbnailView()
            {
                Assert.IsTrue(Then.ViewModel.IsThumbnailViewVisible);
            }

            [TestMethod]
            public void WhenSettingThumbnailView_ShouldNotBeInListView()
            {
                Assert.IsFalse(Then.ViewModel.IsListViewVisible);
            }

            [TestMethod]
            public void WhenSettingThumbnailView_ShouldNotBeInSplitView()
            {
                Assert.IsFalse(Then.ViewModel.IsSplitViewVisible);
            }
        }

        [TestClass]
        public class WhenSettingListView : Test<SearchResultsPageViewModelScenario>
        {
            [TestInitialize]
            public void Setup()
            {
                When.ExecutingListCommand();
            }

            [TestMethod]
            public void WhenSettingListView_ShouldChangeCurrentView()
            {
                Assert.IsTrue(Then.CurrentViewIs(SearchResultsPageViewModes.ListView));
            }

            [TestMethod]
            public void WhenSettingListView_ShouldNotBeInThumbnailView()
            {
                Assert.IsFalse(Then.ViewModel.IsThumbnailViewVisible);
            }

            [TestMethod]
            public void WhenSettingListView_ShouldBeInListView()
            {
                Assert.IsTrue(Then.ViewModel.IsListViewVisible);
            }

            [TestMethod]
            public void WhenSettingListView_ShouldNotBeInSplitView()
            {
                Assert.IsFalse(Then.ViewModel.IsSplitViewVisible);
            }
        }

        [TestClass]
        public class WhenSettingSplitView : Test<SearchResultsPageViewModelScenario>
        {
            [TestInitialize]
            public void Setup()
            {
                When.ExecutingSplitCommand();
            }

            [TestMethod]
            public void WhenSettingThumbnailView_ShouldChangeCurrentView()
            {
                Assert.IsTrue(Then.CurrentViewIs(SearchResultsPageViewModes.SplitView));
            }

            [TestMethod]
            public void WhenSettingThumbnailView_ShouldNotBeInThumbnailView()
            {
                Assert.IsFalse(Then.ViewModel.IsThumbnailViewVisible);
            }

            [TestMethod]
            public void WhenSettingThumbnailView_ShouldNotBeInListView()
            {
                Assert.IsFalse(Then.ViewModel.IsListViewVisible);
            }

            [TestMethod]
            public void WhenSettingThumbnailView_ShouldBeInSplitView()
            {
                Assert.IsTrue(Then.ViewModel.IsSplitViewVisible);
            }
        }
    }

    public class SearchResultsPageViewModelScenario : ViewModelScenarioBase
    {
        public SearchResultsPageViewModel ViewModel { get; set; }
        private readonly MockImageSearchService _imageSearchService;
        private readonly MockAccelerometer _accelerometer;
        private readonly MockShareDataRequestedPump _sharePump;

        public SearchResultsPageViewModelScenario()
        {
            var testInstance = new SearchInstance()
            {
                Query = "Search1",
                Images = new List<ImageResult>() { new ImageResult() { MediaUrl = "http://example.com" } }
            };
            ApplicationSettings.Searches.Add(testInstance);
            ApplicationSettings.SelectedInstance = testInstance;

            _imageSearchService = new MockImageSearchService();
            _accelerometer = new MockAccelerometer();
            _sharePump = new MockShareDataRequestedPump();
            ViewModel = new SearchResultsPageViewModel(ApplicationSettings, 
                NavigationService, _imageSearchService, MessageHub, 
                _accelerometer, new MockStatusService(), _sharePump);
        }
    
        public void ExecutingThumbnailCommand()
        {
            ViewModel.ThumbnailViewCommand.Execute(null);
        }

        public void ExecutingListCommand()
        {
            ViewModel.ListViewCommand.Execute(null);
        }

        public void ExecutingSplitCommand()
        {
            ViewModel.SplitViewCommand.Execute(null);
        }

        public bool CurrentViewIs(string view)
        {
            return ViewModel.CurrentView == view;
        }
    }
}
