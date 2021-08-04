using DevEdu.Core.Requests;
using RestSharp;

namespace DevEdu.Tests
{
    public abstract class BaseControllerTest
    {
        protected IRequestHelper _request;
        protected static string _baseUrl =@"https://localhost:44386/api";        
        
        protected void AuthenticateClient(IRestClient client, IRestRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}