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
using BingImageSearch.ViewModels;
using System.Threading.Tasks;

namespace BingImageSearch.Tests.ViewModels
{
    public class PreferencesViewModelTests
    {
        [TestClass]
        public class WhenResetingHistory : Test<PreferencesViewModelScenario>
        {
            [TestMethod]
            public async void WhenResetingHistory_ShouldClearSearches()
            {
                Given.CurrentHistoryHasItems();
                await When.ResettingHistory();
                Assert.IsTrue(Then.SettingsSearchesIsEmpty);
            }
        }
    }

    public class PreferencesViewModelScenario : ViewModelScenarioBase
    {
        public PreferencesViewModel ViewModel { get; set; }

        public PreferencesViewModelScenario()
        {
            ViewModel = new PreferencesViewModel(ApplicationSettings);
        }

        public void CurrentHistoryHasItems()
        {
            ApplicationSettings.Searches.Clear();

            ApplicationSettings.Searches.Add(new SearchInstance() { Query = "Sample1" });
            ApplicationSettings.Searches.Add(new SearchInstance() { Query = "Sample2" });
            ApplicationSettings.Searches.Add(new SearchInstance() { Query = "Sample3" });
        }

        public Task ResettingHistory()
        {
            return ViewModel.ResetHistory();
        }

        public bool SettingsSearchesIsEmpty
        {
            get { return !ApplicationSettings.Searches.Any(); }
        }
    }
}
