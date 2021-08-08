using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using DevEdu.Tests.Facades;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests
{
    public abstract class BaseApi
    {
        protected const string BaseEndPoint = "https://localhost:44386/";
        protected string _endPoint;
        protected Dictionary<string, string> _headers;
        protected RestClient _client;
        protected RequestHelper _requestHelper;
        protected Facade _facade;


        [SetUp]
        public void Setup()
        {
            _client = new RestClient(BaseEndPoint);
            _requestHelper = new RequestHelper();
            _headers = new Dictionary<string, string>();
            _facade = new Facade();
        }

        protected string SignInByEmailAndPassword_ReturnToken(string email, string password)
        {
            _endPoint = SignInPoint;
            var postData = UserData.GetUserSignInputModelByEmailAndPassword(email, password);
            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            return _client.Execute<string>(request).Data;
        }

        protected void AuthenticateClient(string token)
        {
            CleanHeader();
            _headers.Add("Authorization", $"Bearer {token}");
        }

        protected void CleanHeader()
        {
            _headers.Clear();
        }
    }
}