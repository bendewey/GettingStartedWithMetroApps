using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Model;

namespace BingImageSearch.Tests.Mocks
{
    class MockImageSearchService : IImageSearchService
    {
        public bool ReturnNoResults;

        public List<SearchHistory> Searches = new List<SearchHistory>();

        public Task<List<ImageResult>> Search(string search, Rating rating = Rating.Strict, ResultSize resultSize = ResultSize.Twenty, int offset = 0)
        {
            if (ReturnNoResults)
            {
                return Task.Run(() => new List<ImageResult>());
            }

            Searches.Add(new SearchHistory(search, resultSize, rating));
            return Task.Run(() => new List<ImageResult>() { 
                new ImageResult() 
                {
                    Title = "Sample Image",
                    MediaUrl = "http://example.com/sample.jpg"
                }
            });
        }

        public Task LoadMore(SearchInstance instance, Rating rating, ResultSize resultSize)
        {
            return Task.Run(() => {});
        }

        public class SearchHistory
        {
            public readonly string Search;
            public readonly ResultSize ResultSize;
            public readonly Rating Rating;

            public SearchHistory(string search, ResultSize resultSize, Rating rating)
            {
                Search = search;
                ResultSize = resultSize;
                Rating = rating;
            }
        }
    }
}
