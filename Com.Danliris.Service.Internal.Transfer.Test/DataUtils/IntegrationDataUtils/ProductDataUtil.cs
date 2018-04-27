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
    public static class ProductDataUtil
    {
        public static ProductViewModel GetProduct(HttpClientService client)
        {
            var response = client.GetAsync($"{APIEndpoint.Core}/master/products?page=1&size=1").Result;
            response.EnsureSuccessStatusCode();

            var data = response.Content.ReadAsStringAsync();
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.Result.ToString());

            List<ProductViewModel> list = JsonConvert.DeserializeObject<List<ProductViewModel>>(result["data"].ToString());
            ProductViewModel product = list.First();

            return product;
        }
    }
}
