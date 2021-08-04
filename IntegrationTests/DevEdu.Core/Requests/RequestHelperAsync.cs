using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Core.Requests
{
    public class RequestHelperAsync : IRequestHelperAsync
    {
        public async Task<IRestResponse> GetAsync(IRestClient client, string endPoint, Dictionary<string, string> headers)
        {
            return await CallingApi(Method.GET, client, headers, endPoint);
        }

        public async Task<IRestResponse> PostAsync(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return await CallingApi(Method.POST, client, headers, endPoint, jsonData);
        }

        public async Task<IRestResponse> PutAsync(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return await CallingApi(Method.PUT, client, headers, endPoint, jsonData);
        }

        public async Task<IRestResponse> DeleteAsync(IRestClient client, string endPoint, Dictionary<string, string> headers)
        {
            return await CallingApi(Method.DELETE, client, headers, endPoint);
        }

        private static async Task<IRestResponse> CallingApi
        (
            Method httpMethod,
            IRestClient client,
            Dictionary<string, string> headers,
            string endPoint,
            string jsonData = ""
        )
        {
            client.BaseUrl = new Uri(endPoint);
            var request = new RestRequest(httpMethod);
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.AddHeader(item.Key, item.Value);
                }
            }

            if (httpMethod == Method.PUT || httpMethod == Method.POST)
            {
                request.AddParameter(headers["content-type"], jsonData, ParameterType.RequestBody);
            }
            IRestResponse response = await client.ExecuteGetAsync(request);
            return response;
        }
    }
}