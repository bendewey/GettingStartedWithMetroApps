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

namespace BingImageSearch.Tests.Messaging
{
    public class SearchQuerySubmittedHandlerTests
    {
        [TestClass]
        public class WhenSearching : Test<SearchQuerySubmittedHandlerScenario>
        {
            [TestInitialize]
            public void Setup()
            {
                When.Searching("Sample");
            }

            [TestMethod]
            public void WhenSearching_ShouldToggleLoading()
            {
                Assert.IsTrue(Then.LoadingWasToggled);
            }

            [TestMethod]
            public void WhenSearching_ShouldSearchForQuery()
            {
                Assert.IsTrue(Then.ImageSearchServiceSearchedWith("Sample"));
            }

            [TestMethod]
            public void WhenSearching_ShouldAddSearchWithQuery()
            {
                Assert.IsTrue(Then.InstanceHasQuery("Sample"));
            }

            [TestMethod]
            public void WhenSearching_ShouldNavigateToResults()
            {
                Assert.IsTrue(Then.NavigatedTo<SearchResultsPage>());
            }
        }

        [TestClass]
        public class WhenSearchingForExisting : Test<SearchQuerySubmittedHandlerScenario>
        {
            [TestInitialize]
            public void Setup()
            {
                Given.InstanceExistsFor("Sample");
                When.Searching("Sample");
            }

            [TestMethod]
            public void WhenSearchingForExisting_ShouldResultInOneInstance()
            {
                Assert.IsTrue(Then.OnlyOneInstanceOf("Sample"));
            }
        }

        [TestClass]
        public class WhenSearchingAndNoImagesReturned : Test<SearchQuerySubmittedHandlerScenario>
        {
            [TestInitialize]
            public void Setup()
            {
                Given.ServiceReturnsNoResults();
                When.Searching("Sample");
            }

            [TestMethod]
            public void WhenSearchingAndNoImagesReturned_ShouldSetStatusToBingUnavailable()
            {
                Assert.IsTrue(Then.StatusIsSetToBingUnavailable);
            }
        }

        public class SearchQuerySubmittedHandlerScenario : HandlerScenario
        {
            private readonly MockApplicationSettings _settings;
            private readonly MockImageSearchService _imageSearchService;
            private readonly MockNavigationService _navigationService;
            private readonly MockStatusService _statusService;

            public SearchQuerySubmittedHandlerScenario()
            {
                _settings = new MockApplicationSettings();
               _imageSearchService = new MockImageSearchService();
               _navigationService =  new MockNavigationService();
               _statusService = new MockStatusService();

                Handler = new SearchQuerySubmittedHandler(_settings, _imageSearchService, _navigationService, _statusService);
            }

            public void InstanceExistsFor(string query)
            {
                _settings.Searches.Add(new SearchInstance() { Query = query });
            }

            public void ServiceReturnsNoResults()
            {
                _imageSearchService.ReturnNoResults = true;
            }

            public void Searching(string query)
            {
                WhenHandling(() => new SearchQuerySubmittedMessage(query));
            }

            public bool LoadingWasToggled
            {
                get { return _statusService.WasToggled; }
            }

            public bool ImageSearchServiceSearchedWith(string query)
            {
                return _imageSearchService.Searches.All(s => s.Search == query);
            }

            public bool InstanceHasQuery(string query)
            {
                var first = _settings.Searches.FirstOrDefault();
                return first.Query == query;
            }

            public bool NavigatedTo<T>()
            {
                return _navigationService.NavigationStack.Pop().Source == typeof(T);
            }

            public bool OnlyOneInstanceOf(string query)
            {
                return _settings.Searches.Count(s => s.Query == query) == 1;
            }

            public bool StatusIsSetToBingUnavailable
            {
                get
                {
                    return _statusService.IsBingUnavailable;
                }
            }
        }
    }
}