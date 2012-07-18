using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Threading.Tasks;
using BingImageSearch.Model;

namespace BingImageSearch
{
    public static class BingSearchContainerExtensions
    {
        public static Task<IEnumerable<T>> ExecuteAsync<T>(this DataServiceQuery<T> query)
        {
            return Task.Factory.FromAsync<IEnumerable<T>>(query.BeginExecute, query.EndExecute, null);
        }

        public static DataServiceQuery<ImageResult> Image(this BingSearchContainer container, string searchText, string market, Rating rating, ResultSize resultSize, int offset)
        {
            var ratingKey = Enum.GetName(typeof(Rating), rating);
            var pageSize = GetIntFromResultSize(resultSize);

            var query = container.Image(searchText, market, ratingKey, null, null, null)
                .AddQueryOption("$top", pageSize)
                .AddQueryOption("$skip", offset);
            
            return query;
        }

        private static int GetIntFromResultSize(ResultSize resultSize)
        {
            switch (resultSize)
            {
                default:
                case ResultSize.Twenty:
                    return 20;
                case ResultSize.Forty:
                    return 50;
                case ResultSize.OneHundred:
                    return 100;
            }
        }
    }
}
