using RestSharp;

namespace DevEdu.Core.Requests
{
    public class RequestHelper
    {
        public IRestRequest CreateRequest(Method httpMethod, string endPoint)
        {
            return new RestRequest(endPoint, httpMethod);
        }

        public bool IsHaveToken(string token)
        {
            if (token == default || token == null) { return false; }
            return true;
        }
    }
}