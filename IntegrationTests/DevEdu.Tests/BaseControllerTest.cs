using DevEdu.Core.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace DevEdu.Tests
{
    [TestClass]
    public abstract class BaseControllerTest
    {
        protected IRequestHelper _request;
        protected static string _baseUrl =
            @"Data Source=80.78.240.16;Initial Catalog = DevEdu; Persist Security Info=True;User ID = student;Password=qwe!23;";
        public BaseControllerTest(IRequestHelper request) { _request = request; }
        
        protected void AuthenticateClient(IRestClient client, IRestRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}