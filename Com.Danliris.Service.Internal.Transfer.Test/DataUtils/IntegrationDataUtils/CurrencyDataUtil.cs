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
    public static class CurrencyDataUtil
    {
        public static CurrencyViewModel GetCurrencyIDR(HttpClientService client)
        {
            var response = client.GetAsync($"{APIEndpoint.Core}master/currencies").Result;
            response.EnsureSuccessStatusCode();

            var data = response.Content.ReadAsStringAsync();
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.Result.ToString());

            List<CurrencyViewModel> list = JsonConvert.DeserializeObject<List<CurrencyViewModel>>(result["data"].ToString());
            //CurrencyViewModel currency = list.First(p => p.code.Equals("IDR"));
            CurrencyViewModel currency = list.First();

            return currency;
        }
    }
}
