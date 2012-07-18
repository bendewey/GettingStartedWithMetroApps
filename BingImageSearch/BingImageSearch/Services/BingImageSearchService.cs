using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using BingImageSearch.Model;
using BingImageSearch.Services;

namespace BingImageSearch
{
    public class BingImageSearchService : IImageSearchService
    {
        const int PageSize = 20;
        private readonly BingApi _api;

        public BingImageSearchService(BingApi api)
        {
            _api = api;
        }

        public async Task<List<ImageResult>> Search(string search, Rating rating = Rating.Strict, ResultSize resultSize = ResultSize.Twenty, int offset = 0)
        {
            var images = new List<ImageResult>();

            var context = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Data.ashx/Bing/Search"));
            context.Credentials = new NetworkCredential(_api, _api);
            var result = await context.Image(search, "en-US", rating, resultSize, offset).ExecuteAsync();
            
            images.AddRange(result.ToList());

            return images;
        }

        public async Task LoadMore(SearchInstance instance, Rating rating, ResultSize resultSize)
        {
            var imagesToAdd = await Search(instance.Query, rating, resultSize, instance.Images.Count + 1);
            foreach (var image in imagesToAdd)
            {
                instance.Images.Add(image);
            }
        }
    }
}
