using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public class RequestHelper : IRequestHelper
    {
        public IRestResponse Get(string endPoint, Dictionary<string, string> headers)
        {
            return CallingApi(Method.GET, headers, endPoint);
        }

        public IRestResponse Post(string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return CallingApi(Method.POST, headers, endPoint, jsonData);
        }

        public IRestResponse Put(string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return CallingApi(Method.PUT, headers, endPoint, jsonData);
        }

        public IRestResponse Delete(string endPoint, Dictionary<string, string> headers)
        {
            return CallingApi(Method.DELETE, headers, endPoint);
        }

        private static IRestResponse CallingApi
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
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}