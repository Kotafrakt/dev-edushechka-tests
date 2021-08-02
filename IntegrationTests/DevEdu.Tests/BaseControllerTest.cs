using DevEdu.Core.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace DevEdu.Tests
{
    [TestClass]
    public abstract class BaseControllerTest
    {
        protected IRequestHelper _request;
        protected static string _baseUrl =@"https://localhost:44386/api";
        public BaseControllerTest(IRequestHelper request) { _request = request; }
        
        protected void AuthenticateClient(IRestClient client, IRestRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}