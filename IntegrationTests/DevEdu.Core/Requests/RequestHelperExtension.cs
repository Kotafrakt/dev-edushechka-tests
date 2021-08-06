using System.Collections.Generic;
using RestSharp;

namespace DevEdu.Core.Requests
{
    public static class RequestHelperExtension
    {
        public static IRestRequest Get(this RequestHelper requestHelper, IRestClient client, string endPoint, Dictionary<string, string> headers)
        {
            return requestHelper.SendRequestToApi(Method.GET, client, headers, endPoint);
        }

        public static IRestRequest Post(this RequestHelper requestHelper, IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return requestHelper.SendRequestToApi(Method.POST, client, headers, endPoint, jsonData);
        }

        public static IRestRequest Put(this RequestHelper requestHelper, IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return requestHelper.SendRequestToApi(Method.PUT, client, headers, endPoint, jsonData);
        }

        public static IRestRequest Delete(this RequestHelper requestHelper, IRestClient client, string endPoint, Dictionary<string, string> headers)
        {
            return requestHelper.SendRequestToApi(Method.DELETE, client, headers, endPoint);
        }
    }
}