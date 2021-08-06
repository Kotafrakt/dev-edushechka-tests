using DevEdu.Core.Requests;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Tests
{
    public abstract class BaseControllerTest
    {
        private const string Authorization = "Authorization";
        protected const string BaseEndPoint = "https://localhost:44386/";

        protected RestClient _client;
        protected RequestHelper _request;
        protected Dictionary<string, string> _headers;
        protected string _endPoint;
        protected string _token;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(BaseEndPoint);
            _request = new();
            _headers = new();
        }

        protected void SignInByEmailAndPassword_ReturnToken(string email, string password)
        {
            _endPoint = AuthenticationControllerData.SignInEndPoint;
            var postData = AuthenticationControllerData.GetUserSignInputModelByEmailAndPassword(email, password);
            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
           var request = _request.Post(_client, _endPoint, _headers, jsonData);
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
    }
}