using DevEdu.Core.Requests;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests
{
    public abstract class BaseControllerTest
    {
        private const string Authorization = "Authorization";
        protected const string BaseEndPoint = "https://localhost:44386/";

        protected RestClient _client;
        protected RequestHelper _requestHelper;
        protected Dictionary<string, string> _headers;
        protected string _endPoint;
        protected string _token;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(BaseEndPoint);
            _requestHelper = new RequestHelper();
            _headers = new Dictionary<string, string>();
        }

        protected void SignInByEmailAndPassword_ReturnToken(string email, string password)
        {
            _endPoint = SignInPoint;
            var postData = AuthenticationControllerData.GetUserSignInputModelByEmailAndPassword(email, password);
            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            _token = _client.Execute<string>(request).Data;
        }

        protected void AuthenticateClient()
        {
            CleanHeader();
            _headers.Add(Authorization, $"Bearer {_token}");
        }

        protected void CleanHeader()
        {
            _headers.Clear();
        }
        protected void CleanToken()
        {
            _token = string.Empty;
        }
    }
}