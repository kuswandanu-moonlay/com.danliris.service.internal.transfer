using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Test.DataUtils.IntegrationDataUtils
{
    public static class CategoryDataUtil
    {
        public static CategoryViewModel GetCategory(HttpClientService client)
        {
            var response = client.GetAsync($"{APIEndpoint.Core}/master/categories?page=1&size=1").Result;
            response.EnsureSuccessStatusCode();

            var data = response.Content.ReadAsStringAsync();
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.Result.ToString());

            List<CategoryViewModel> list = JsonConvert.DeserializeObject<List<CategoryViewModel>>(result["data"].ToString());
            CategoryViewModel category = list.First();

            return category;
        }
    }
}
