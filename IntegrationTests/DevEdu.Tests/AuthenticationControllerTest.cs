using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using static DevEdu.Tests.AuthenticationControllerData;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests
{
    public class AuthenticationControllerTest : BaseApi
    {
        [Test]
        public void Register()
        {
            _endPoint = RegisterPoint;
            var postData = GetUserInsertInputModelForRegistration_1();

            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var result = _client.Execute<UserFullInfoOutPutModel>(request).Data;

            postData.Should().BeEquivalentTo(result, options => options
                    .Excluding(obj => obj.ExileDate)
                    .Excluding(obj => obj.Id)
                    .Excluding(obj => obj.RegistrationDate));
        }

        [Test]
        public void SignIn()
        {
            _endPoint = SignInPoint;
            var postData = GetUserSignInputModelDefault();
            var jsonData = JsonConvert.SerializeObject(postData);

            _headers.Add("content-type", "application/json");

            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var result = _client.Execute<string>(request);
            var data = JsonConvert.DeserializeObject(result.Content);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.True(data != null);
        }
    }
}