using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Creators;
using DevEdu.Tests.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Net;

namespace DevEdu.Tests.ControllersTests
{
    public class AuthenticationControllerTest : BaseControllerTest
    {
        private AuthenticationClient _authentication = new();

        [TestCase(Role.Student)]
        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Methodist)]
        public void Register(Role role)
        {
            //Given
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRole(Role.Manager);
            userInfo.Token = _authentication.SignInByEmailAndPasswordReturnToken(userInfo.Email, userInfo.Token);
            _endPoint = AuthorizationPoints.RegisterPoint;
            var newUser = UserData.GetValidUserInsertInputModelForRegistration(role);
            //When
            var request = _requestHelper.Post(_endPoint, newUser);
            request = _requestHelper.Autorize(request, userInfo.Token);
            var response = _client.Execute<UserFullInfoOutPutModel>(request);
            var result = response.Data;
            //Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().BeEquivalentTo
            (
                newUser, options => options
                    .Excluding(obj => obj.IsDeleted)
                    .Excluding(obj => obj.Password)
                    .Excluding(obj => obj.Patronymic)
            );
        }

        [TestCase(Role.Admin)]
        [TestCase(Role.Student)]
        [TestCase(Role.Manager)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Teacher)]
        public void SignIn(Role role)
        {
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRole(role);
            _facade.LogOut(userInfo);
            _endPoint = AuthorizationPoints.SignInPoint;
            var user = UserData.GetUserSignInputModelByEmailAndPassword(userInfo.Email, userInfo.Password);

            var request = _requestHelper.Post(_endPoint, user);
            var response = _client.Execute<string>(request);
            var result = response.Data;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().NotBeNull();
        }
    }
}