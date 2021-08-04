using DevEdu.Core.Requests;
using NUnit.Framework;
using RestSharp;

namespace DevEdu.Tests
{
    public abstract class BaseControllerTest
    {
        protected RequestHelper _request;
        protected static string _baseUrl = @"https://localhost:44386/api";

        [SetUp]
        public void Setup()
        {
            _request = new();
        }

        protected void AuthenticateClient(IRestClient client, IRestRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}