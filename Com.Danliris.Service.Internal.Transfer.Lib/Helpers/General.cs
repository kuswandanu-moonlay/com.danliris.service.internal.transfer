using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Helpers
{
    public static class General
    {
        public const string ASCENDING = "asc";
        public const string DESCENDING = "desc";
        public const string JsonMediaType = "application/json";

        public static string BuildSearch(List<string> SearchAttributes)
        {
            string SearchQuery = String.Empty;
            foreach (string Attribute in SearchAttributes)
            {
                SearchQuery = string.Concat(SearchQuery, Attribute, ".Contains(@0) OR ");
            }

            SearchQuery = SearchQuery.Remove(SearchQuery.Length - 4);

            return SearchQuery;
        }
    }
}
