using RestSharp;
using System;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public class RequestHelper
    {
        public IRestRequest SendRequestToApi
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