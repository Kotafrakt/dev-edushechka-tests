using RestSharp;

namespace DevEdu.Core.Requests
{
    public static class RequestHelperExtension
    {
        public static IRestRequest Get(this RequestHelper requestHelper, string endPoint)
        {
            return requestHelper.CreateRequest(Method.GET, endPoint);
        }

        public static IRestRequest Post<T>(this RequestHelper requestHelper, string endPoint, T postData)
        {
            var request = requestHelper.CreateRequest(Method.POST, endPoint);
            return requestHelper.AddPostDataToJsonBody(request, postData);
        }

        public static IRestRequest Put<T>(this RequestHelper requestHelper, string endPoint, T postData)
        {
            var request = requestHelper.CreateRequest(Method.PUT, endPoint);
            return requestHelper.AddPostDataToJsonBody(request, postData);
        }

        public static IRestRequest Delete(this RequestHelper requestHelper, string endPoint)
        {
            return requestHelper.CreateRequest(Method.DELETE, endPoint);
        }
    }
}