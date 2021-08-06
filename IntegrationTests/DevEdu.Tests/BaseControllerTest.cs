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
        private const string Bearer = "Bearer";
        private const string MarksToken = "\"";
        private const string Space = " ";
        protected const string ContentType = "content-type";
        protected const string ApplicationJson = "application/json";
        protected const string BaseEndPoint = "https://localhost:44386/";

        protected RestClient _client;
        protected RequestHelper _request;
        protected Dictionary<string, string> _headers;
        protected string _endPoint;
        protected string _token;

        [SetUp]
        public void Setup()
        {
            _client = new();
            _request = new();
            _headers = new();
        }

        protected void SignInByEmailAndPassword_ReturnToken(string email, string password)
        {
            _endPoint = $"{BaseEndPoint}sign-in";
            var postData = AuthenticationControllerData.GetUserSignInputModelByEmailAndPassword(email, password);
            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add(ContentType, ApplicationJson);
            _token = _request.Post(_client, _endPoint, _headers, jsonData).Content;
        }

        protected void AuthenticateClient()
        {
            CleanHeader();
            if (_token.Contains(MarksToken))
            {
                Cleaning();
            }
            _headers.Add(Authorization, $"{Bearer}{Space}{_token}");
        }
        protected void CleanHeader()
        {
            _headers.Clear();
        }

        private void Cleaning()
        {
            _token = _token.Replace(MarksToken, string.Empty);
        }
    }
}