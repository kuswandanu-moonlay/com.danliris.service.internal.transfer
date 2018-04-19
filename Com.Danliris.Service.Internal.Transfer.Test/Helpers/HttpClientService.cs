using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


namespace Com.Danliris.Service.Internal.Transfer.Test.Helpers
{
    public class HttpClientService : HttpClient
    {
        public static string Token;

        public HttpClientService()
        {
            var User = new { username = "dev2", password = "Standar123" };

            var response = this.PostAsync("http://localhost:5000/v1/authenticate", new StringContent(JsonConvert.SerializeObject(User).ToString(), Encoding.UTF8, "application/json")).Result;
            response.EnsureSuccessStatusCode();

            var data = response.Content.ReadAsStringAsync();
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.Result.ToString());
            var token = result["data"].ToString();
            Token = token;

            this.SetBearerToken(token);
        }
    }
}
