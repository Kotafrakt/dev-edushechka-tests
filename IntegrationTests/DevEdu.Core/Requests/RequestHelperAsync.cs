using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Core.Requests
{
    public class RequestHelperAsync : IRequestHelperAsync
    {
        public async Task<IRestResponse> GetAsync(string endPoint, Dictionary<string, string> headers)
        {
            return await CallingApi(Method.GET, headers, endPoint);
        }

        public async Task<IRestResponse> PostAsync(string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return await CallingApi(Method.POST, headers, endPoint, jsonData);
        }

        public async Task<IRestResponse> PutAsync(string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return await CallingApi(Method.PUT, headers, endPoint, jsonData);
        }

        public async Task<IRestResponse> DeleteAsync(string endPoint, Dictionary<string, string> headers)
        {
            return await CallingApi(Method.DELETE, headers, endPoint);
        }

        private static async Task<IRestResponse> CallingApi
        (
            Method httpMethod,
            Dictionary<string, string> headers,
            string endPoint,
            string jsonData = ""
        )
        {
            var client = new RestClient(endPoint);
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
            IRestResponse response =  await client.ExecuteGetAsync(request);
            return response;
        }
    }
}