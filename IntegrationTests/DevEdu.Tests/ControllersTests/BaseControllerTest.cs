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
        protected const string BaseEndPoint = "https://localhost:44386/";
        protected string _endPoint;
        protected RestClient _client;
        protected RequestHelper _requestHelper;
        protected Facade _facade;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(BaseEndPoint);
            _requestHelper = new RequestHelper();
            _facade = new Facade();
        }

        protected string SignInByEmailAndPassword_ReturnToken(string email, string password)
        {
            _endPoint = AuthorizationPoints.SignInPoint;
            var postData = UserData.GetUserSignInputModelByEmailAndPassword(email, password);
            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.Post(_endPoint, jsonData);
            return _client.Execute<string>(request).Data;
        }
    }
}