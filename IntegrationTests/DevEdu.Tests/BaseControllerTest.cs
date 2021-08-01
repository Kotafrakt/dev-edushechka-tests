using DevEdu.Tests.Requests;
using RestSharp;

namespace DevEdu.Tests
{
    public abstract class BaseControllerTest
    {
        protected IRequest _request;
        protected static string _baseUrl =
            @"Data Source=80.78.240.16;Initial Catalog = DevEdu; Persist Security Info=True;User ID = student;Password=qwe!23;";
        public BaseControllerTest(IRequest request) { _request = request; }
        
        protected void AuthenticateClient(IRestClient client, IRestRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}