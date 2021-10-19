using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataTableDemo.Generic
{
    public static class HttpRequest
    {
        public static async Task<string> GetRequestAsync<T>(string _url, T body, string endPoint)

        {
            var url = _url + "api/Customers/" + endPoint;// _configuration["Data:MicroServiceEndPoint"] + "api/" + functionName;

            using (HttpClient client = new HttpClient())
            {
                var serializeObject = JsonConvert.SerializeObject(body);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(url);
                    request.Content = new StringContent(serializeObject, Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(request);
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody;
                }
            }
        }
    }
}
