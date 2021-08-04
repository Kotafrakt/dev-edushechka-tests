using DevEdu.Core.Requests;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevEdu.Tests
{
    public abstract class BaseControllerTest
    {
        protected RestClient _client;
        protected RequestHelper _request;
        protected Dictionary<string, string> _headers;
        protected string _endPoint;

        [SetUp]
        public void Setup()
        {
            _client = new();
            _request = new();
            _headers = new();
        }

        public void AuthenticateClient(IRestClient client, IRestRequest request)
        {
            client.Authenticator = new OAuth2UriQueryParameterAuthenticator("");
        }
    }
}