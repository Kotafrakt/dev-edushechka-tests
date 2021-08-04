using DevEdu.Core.Requests;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Tests
{
    public abstract class BaseControllerTest
    {
        protected RequestHelper _request;
        protected Dictionary<string, string> _headers;
        protected string _endPoint;

        [SetUp]
        public void Setup()
        {
            _request = new();
            _headers = new();
        }

        protected void AuthenticateClient(IRestClient client, IRestRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}