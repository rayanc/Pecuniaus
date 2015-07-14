using Pecuniaus.Models;
using System.Collections.Generic;
using System.Text;

namespace Pecuniaus.ApiHelper
{
    public static class SearchResults
    {
        public static IList<SearchResultModel> Search(long merchantid, string legalName="")
        {
            StringBuilder apiQuery = new StringBuilder("merchants/searchResult?");
            apiQuery.AppendFormat("Merchantid={0}", merchantid);
           
            if (!string.IsNullOrEmpty(legalName))
            {
                apiQuery.AppendFormat("&legalName={0}", legalName);
            }

            return BaseApiData.GetAPIResult<IList<SearchResultModel>>(apiQuery.ToString(), () => new List<SearchResultModel>());
        }
    }
}
