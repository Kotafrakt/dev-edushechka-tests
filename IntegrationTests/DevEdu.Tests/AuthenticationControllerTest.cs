using DevEdu.Core.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using DevEdu.Core.Requests;
using static DevEdu.Tests.AuthenticationControllerData;
using FluentAssertions;

namespace DevEdu.Tests
{
    public class AuthenticationControllerTest : BaseControllerTest
    {
        [Test]
        public void Register()
        {
            _endPoint = RegisterEndPoint;
            var postData = GetUserInsertInputModelForRegistration_1();

            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var request = _request.Post(_client, _endPoint, _headers, jsonData);
            var result = _client.Execute<UserFullInfoOutPutModel>(request).Data;

            postData.Should().BeEquivalentTo(result, options => options
                    .Excluding(obj => obj.ExileDate)
                    .Excluding(obj => obj.Id)
                    .Excluding(obj => obj.RegistrationDate));
        }

        [Test]
        public void SignIn()
        {
            _endPoint = SignInEndPoint;
            var postData = GetUserSignInputModelDefault();
            var jsonData = JsonConvert.SerializeObject(postData);

            _headers.Add("content-type", "application/json");

            var request = _request.Post(_client, _endPoint, _headers, jsonData);
            var result = _client.Execute<string>(request);
            var data = JsonConvert.DeserializeObject(result.Content);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.True(data != null);
        }
    }
}