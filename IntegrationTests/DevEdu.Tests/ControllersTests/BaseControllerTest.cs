using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using DevEdu.Tests.Facades;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace DevEdu.Tests.ControllersTests
{
    public abstract class BaseControllerTest
    {
        protected const string BaseEndpoint = "https://localhost:44386/";
        protected string _endPoint;
        protected RestClient _client;
        protected RequestHelper _requestHelper;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(BaseEndpoint);
            _requestHelper = new RequestHelper();
        }

        protected string SignInByEmailAndPassword_ReturnToken(string email, string password)
        {
            _endPoint = AuthorizationEndpoints.SignInEndpoint;
            var postData = UserData.GetUserSignInputModelByEmailAndPassword(email, password);
            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.CreatePostRequest(_endPoint, jsonData);
            return _client.Execute<string>(request).Data;
        }
    }
}