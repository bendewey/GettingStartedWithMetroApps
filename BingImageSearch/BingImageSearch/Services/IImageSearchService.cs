using System.Collections.Generic;
using System.Threading.Tasks;
using BingImageSearch.Model;

namespace BingImageSearch
{
    public interface IImageSearchService
    {
        Task<List<ImageResult>> Search(string search, Rating rating = Rating.Strict, ResultSize resultSize = ResultSize.Twenty, int offset = 0);
        Task LoadMore(SearchInstance instance, Rating rating, ResultSize resultSize);
    }
}