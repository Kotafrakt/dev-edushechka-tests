using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Net;

namespace DevEdu.Tests.ControllersTests
{
    public class AuthenticationControllerTest : BaseControllerTest
    {

        [TestCase(Role.Student)]
        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Methodist)]
        public void Register(Role role)
        {
            var userInfo = _facade.AuthenticationByAdminAndRegistrationNewUserByRole(Role.Manager);
            _facade.LogOut(userInfo);

            _endPoint = AuthorizationPoints.RegisterPoint;
            var user = UserData.GetUserSignInputModelByEmailAndPassword(userInfo.Email, userInfo.Password);
            var newUser = UserData.GetValidUserInsertInputModelForRegistration(role);

            var requestAuthorization = _requestHelper.Post(_endPoint, user);
            var responseAuthorization = _client.Execute<string>(requestAuthorization);
            responseAuthorization.StatusCode.Should().Be(HttpStatusCode.OK);
            var resultAuthorization = responseAuthorization.Data;
            resultAuthorization.Should().NotBeNull();
            userInfo.Token = resultAuthorization;

            var requestRegistration = _requestHelper.Post(_endPoint, newUser);
            requestRegistration = _requestHelper.Autorize(requestRegistration, userInfo.Token);
            var responseRegistration = _client.Execute<UserFullInfoOutPutModel>(requestRegistration);
            responseRegistration.StatusCode.Should().Be(HttpStatusCode.OK);
            var resultRegistration = responseRegistration.Data;

            resultRegistration.Should().BeEquivalentTo
            (
                newUser, options => options
                    .Excluding(obj => obj.IsDeleted)
                    .Excluding(obj => obj.Password)
                    .Excluding(obj => obj.Patronymic)
            );
        }

        [TestCase(Role.Student)]
        [TestCase(Role.Manager)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Methodist)]
        public void SignIn(Role role)
        {
            var token = _facade.AuthenticationByAdminAndRegistrationNewUserByRole(role);
            _endPoint = AuthorizationPoints.SignInPoint;

            var user = UserData.GetUserSignInputModelByEmailAndPassword("a@a.ru", "12345678");

            var request = _requestHelper.Post(_endPoint, user);
            var response = _client.Execute<string>(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            result.Should().NotBeNull();
        }
    }
}