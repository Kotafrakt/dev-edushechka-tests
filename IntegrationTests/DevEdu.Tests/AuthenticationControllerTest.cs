using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using static DevEdu.Tests.AuthenticationControllerData;
namespace DevEdu.Tests
{
    public class AuthenticationControllerTest : BaseControllerTest
    {
        [Test]
        public void Register()
        {
            _endPoint = $"{BaseEndPoint}{RegisterEndPoint}";
            var postData = GetUserInsertInputModelForRegistration_1();

            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var result = _request.Post(_client, _endPoint, _headers, jsonData);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void SignIn()
        {
            _endPoint = $"{BaseEndPoint}{SignInEndPoint}";
            var postData = GetUserSignInputModelDefault();
            var jsonData = JsonConvert.SerializeObject(postData);

            _headers.Add("content-type", "application/json");

            var result = _request.Post(_client, _endPoint, _headers, jsonData);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}