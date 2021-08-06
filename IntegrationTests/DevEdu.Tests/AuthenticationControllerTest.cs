using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
namespace DevEdu.Tests
{
    public class AuthenticationControllerTest : BaseControllerTest
    {
        [Test]
        public void Register()
        {
            _endPoint = "https://localhost:44386/register";
            var postData = AuthenticationControllerData.GetUserInsertInputModelForRegistration_1();

            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var result = _request.Post(_client, _endPoint, _headers, jsonData);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void SignIn()
        {
            _endPoint = "https://localhost:44386/sign-in";
            var postData = AuthenticationControllerData.GetUserSignInputModelDefault();
            var jsonData = JsonConvert.SerializeObject(postData);

            _headers.Add("content-type", "application/json");

            var result = _request.Post(_client, _endPoint, _headers, jsonData);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}