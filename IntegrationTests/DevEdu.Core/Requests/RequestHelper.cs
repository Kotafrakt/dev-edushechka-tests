using RestSharp;
using System;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public class RequestHelper : IRequestHelper
    {
        public IRestResponse Get(IRestClient client, string endPoint, Dictionary<string, string> headers)
        {
            return CallingApi(Method.GET, client, headers, endPoint);
        }

        public IRestResponse Post(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return CallingApi(Method.POST, client, headers, endPoint, jsonData);
        }

        public IRestResponse Put(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return CallingApi(Method.PUT, client, headers, endPoint, jsonData);
        }

        public IRestResponse Delete(IRestClient client, string endPoint, Dictionary<string, string> headers)
        {
            return CallingApi(Method.DELETE, client, headers, endPoint);
        }

        private static IRestResponse CallingApi
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
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}