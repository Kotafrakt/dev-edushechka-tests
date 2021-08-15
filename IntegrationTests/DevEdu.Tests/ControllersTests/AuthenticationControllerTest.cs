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
        private AuthenticationControllerCreator _authentication = new();

        [TestCaseSource(typeof(UserData), nameof(UserData.AdminCreatedUserByAllRoles))]
        [TestCaseSource(typeof(UserData), nameof(UserData.ManagerCreatedUserByRoleStudent))]
        public void Register<T>(Role role, T roles)
        {
            //Given
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(role);
            _endPoint = AuthorizationPoints.RegisterPoint;
            var newUser = UserData.GetValidUserInsertInputModelForRegistration(roles);
            var request = _requestHelper.CreatePost(_endPoint, newUser, userInfo.Token);

            //When
            var response = _client.Execute<UserFullInfoOutPutModel>(request);

            //Then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var result = response.Data;
            result.Should().BeEquivalentTo
            (
                newUser, options => options
                    .Excluding(obj => obj.IsDeleted)
                    .Excluding(obj => obj.Password)
                    .Excluding(obj => obj.Patronymic)
            );
        }

        [TestCaseSource(typeof(UserData), nameof(UserData.AdminCreatedUserByAllRoles))]
        [TestCaseSource(typeof(UserData), nameof(UserData.ManagerCreatedUserByRoleStudent))]
        public void Register_InvalidRequest_Exception422<T>(Role role, T roles)
        {
            //Given
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(role);
            _endPoint = AuthorizationPoints.RegisterPoint;
            UserInsertInputModel newUser = null;
            var request = _requestHelper.CreatePost(_endPoint, newUser, userInfo.Token);

            //When
            var response = _client.Execute<UserFullInfoOutPutModel>(request);

            //Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCaseSource(typeof(UserData), nameof(UserData.CreatedInvalidUserInsertModelByAdmin))]
        [TestCaseSource(typeof(UserData), nameof(UserData.CreatedInvalidUserInsertModelByManager))]
        public void Register_InvalidRequest_Exception<T>(T role, UserInsertInputModel user)
        {
            //Given
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(role);
            _endPoint = AuthorizationPoints.RegisterPoint;
            var newUser = user;
            var request = _requestHelper.CreatePost(_endPoint, newUser, userInfo.Token);

            //When
            var response = _client.Execute<UserFullInfoOutPutModel>(request);

            //Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCaseSource(typeof(UserData), nameof(UserData.SignInByAllRoles))]
        public void SignIn<T>(T roles)
        {
            //Given
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
            var user = UserData.GetUserSignInputModelByEmailAndPassword(userInfo.Email, userInfo.Password);
            _endPoint = AuthorizationPoints.SignInPoint;
            var request = _requestHelper.CreatePost(_endPoint, user, userInfo.Token);

            //When
            var response = _client.Execute<string>(request);

            //Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = response.Data;
            result.Should().NotBeNull();
        }
    }
}