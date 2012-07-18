using System.Collections.Generic;
using System.Threading.Tasks;
using BingImageSearch.Model;

namespace BingImageSearch
{
    public class NullImageSearchService : IImageSearchService
    {
        public Task<List<ImageResult>> Search(string search, Rating rating = Rating.Strict, ResultSize resultSize = ResultSize.Twenty, int offset = 0)
        {
            return Task.Run<List<ImageResult>>(() =>
                                                   {
                                                       return new List<ImageResult>();
                                                   });
        }

        public Task LoadMore(SearchInstance instance, Rating rating, ResultSize resultSize)
        {
            return Task.Run(() => { });
        }
    }
}