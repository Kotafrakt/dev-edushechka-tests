using System.Collections.Generic;
using RestSharp;

namespace DevEdu.Core.Requests
{
    public static class RequestHelperExtension
    {
        public static IRestRequest Get(this RequestHelper requestHelper, string endPoint, Dictionary<string, string> headers)
        {
            return requestHelper.SendRequestToApi(Method.GET, headers, endPoint);
        }

        public static IRestRequest Post(this RequestHelper requestHelper, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return requestHelper.SendRequestToApi(Method.POST, headers, endPoint, jsonData);
        }

        public static IRestRequest Put(this RequestHelper requestHelper, string endPoint, Dictionary<string, string> headers, string jsonData)
        {
            return requestHelper.SendRequestToApi(Method.PUT, headers, endPoint, jsonData);
        }

        public static IRestRequest Delete(this RequestHelper requestHelper, string endPoint, Dictionary<string, string> headers)
        {
            return requestHelper.SendRequestToApi(Method.DELETE, headers, endPoint);
        }
    }
}