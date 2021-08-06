using RestSharp;
using System;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public class RequestHelper : IRequestHelper
    {
        public IRestRequest Get(IRestClient client, string endPoint, Dictionary<string, string> headers)
        {
            return SendRequestToApi(Method.GET, client, headers, endPoint);
        }

        public IRestRequest Post(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return SendRequestToApi(Method.POST, client, headers, endPoint, jsonData);
        }

        public IRestRequest Put(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return SendRequestToApi(Method.PUT, client, headers, endPoint, jsonData);
        }

        public IRestRequest Delete(IRestClient client, string endPoint, Dictionary<string, string> headers)
        {
            return SendRequestToApi(Method.DELETE, client, headers, endPoint);
        }

        private static IRestRequest SendRequestToApi
        (
            Method httpMethod,
            IRestClient client,
            Dictionary<string, string> headers,
            string endPoint,
            string jsonData = ""
        )
        {
            var request = new RestRequest(endPoint, httpMethod);
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
            return request;
        }
    }
}