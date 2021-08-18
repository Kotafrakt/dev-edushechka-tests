using RestSharp;

namespace DevEdu.Core.Requests
{
    public static class RequestHelperExtension
    {
        public static IRestRequest CreateGet(this RequestHelper requestHelper, string endPoint, string token = default)
        {
            var request = requestHelper.CreateRequest(Method.GET, endPoint);
            if(requestHelper.IsHaveToken(token))
            {
                request.Autorize(token);
            }
            return request;
        }

        public static IRestRequest CreatePost<T>(this RequestHelper requestHelper, string endPoint, T postData, string token = default)
        {
            var request = requestHelper.CreateRequest(Method.POST, endPoint);
            request.AddJsonBody(postData);
            if (requestHelper.IsHaveToken(token))
            {
                request.Autorize(token);
            }
            return request;
        }

        public static IRestRequest CreatePut<T>(this RequestHelper requestHelper, string endPoint, T postData, string token = default)
        {
            var request = requestHelper.CreateRequest(Method.PUT, endPoint);
            request.AddJsonBody(postData);
            if (requestHelper.IsHaveToken(token))
            {
                request.Autorize(token);
            }
            return request;
        }

        public static IRestRequest CreateDelete(this RequestHelper requestHelper, string endPoint, string token = default)
        {
            var request = requestHelper.CreateRequest(Method.DELETE, endPoint);
            if (requestHelper.IsHaveToken(token))
            {
                request.Autorize(token);
            }
            return request;
        }

        public static void Autorize(this IRestRequest request, string token)
        {
            request.AddHeader("Authorization", $"Bearer {token}");
        }
    }
}