using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace DevEdu.Tests.ControllersTests
{
    public class AuthenticationControllerTest : BaseControllerTest
    {
        [Test]
        public void Register()
        {
            _endPoint = AuthorizationPoints.RegisterPoint;
            var postData = UserData.GetInvalidUserInsertInputModelForRegistration
                (new List<Role> { Role.Admin, Role.Manager, Role.Student });

            var request = _requestHelper.Post(_endPoint, postData);

            var response = _client.Execute<UserFullInfoOutPutModel>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            result.Should().BeEquivalentTo
            (
                postData, options => options
                    .Excluding(obj => obj.IsDeleted)
                    .Excluding(obj => obj.Password)
                    .Excluding(obj => obj.Patronymic)
            );
        }

        [Test]
        public void SignIn()
        {
            _endPoint = AuthorizationPoints.SignInPoint;
            var postData = UserData.GetUserSignInputModelByEmailAndPassword("a@a.ru", "12345678");

            var request = _requestHelper.Post(_endPoint, postData);
            var response = _client.Execute<string>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            result.Should().NotBeNull();
        }
    }
}