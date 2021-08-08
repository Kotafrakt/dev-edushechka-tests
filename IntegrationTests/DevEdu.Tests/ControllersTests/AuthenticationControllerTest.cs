using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.ControllersTests
{
    public class AuthenticationControllerTest : BaseControllerTest
    {
        [Test]
        public void Register()
        {
            _endPoint = RegisterPoint;

            var postData = UserData.GetUserInsertInputModelForRegistration_Correct
                (new List<Role> { Role.Admin, Role.Manager, Role.Student });

            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var result = _client.Execute<UserFullInfoOutPutModel>(request).Data;

            postData.Should().BeEquivalentTo(result, options => options
                    .Excluding(obj => obj.ExileDate)
                    .Excluding(obj => obj.Id)
                    .Excluding(obj => obj.RegistrationDate)
                    .Excluding(obj => obj.City));
        }

        [Test]
        public void SignIn()
        {
            _endPoint = SignInPoint;
            var postData = UserData.GetUserSignInputModelByEmailAndPassword("a@a.ru", "12345678");
            var jsonData = JsonConvert.SerializeObject(postData);

            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var result = _client.Execute<string>(request);
            var data = JsonConvert.DeserializeObject(result.Content);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.True(data != null);
        }
    }
}