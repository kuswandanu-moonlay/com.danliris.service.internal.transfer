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
    public static class DivisionDataUtil
    {
        public static DivisionViewModel GetDivision (HttpClientService client)
        {
            var response = client.GetAsync($"{APIEndpoint.Core}/master/divisions?page=1&size=1").Result;
            response.EnsureSuccessStatusCode();

            var data = response.Content.ReadAsStringAsync();
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.Result.ToString());

            List<DivisionViewModel> list = JsonConvert.DeserializeObject<List<DivisionViewModel>>(result["data"].ToString());
            DivisionViewModel division = list.First();

            return division;
        }
    }
}
