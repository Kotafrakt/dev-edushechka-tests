using RestSharp;

namespace DevEdu.Core.Requests
{
    public class RequestHelper
    {
        public IRestRequest CreateRequest(Method httpMethod, string endPoint)
        {
            return new RestRequest(endPoint, httpMethod);
        }

        public IRestRequest AddPostDataToJsonBody<T>(IRestRequest request, T postData)
        {
            return request.AddJsonBody(postData);
        }

        public IRestRequest Autorize(IRestRequest request, string token)
        {
            return request.AddHeader("Authorization", $"Bearer {token}");
        }
    }
}