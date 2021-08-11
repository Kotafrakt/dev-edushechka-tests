﻿using DevEdu.Core.Enums;
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

        [TestCaseSource(typeof(UserData), nameof(UserData.AdminCreatedUserByAllRoles))]
        [TestCaseSource(typeof(UserData), nameof(UserData.ManagerCreatedUserByRoleStudent))]
        public void Register<T>(Role role, T roles)
        {
            //Given
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(role);
            _endPoint = AuthorizationPoints.RegisterPoint;
            var newUser = UserData.GetValidUserInsertInputModelForRegistration(roles);
            var request = _requestHelper.Post(_endPoint, newUser);
            request = _requestHelper.Autorize(request, userInfo.Token);

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

        [TestCaseSource(typeof(UserData), nameof(UserData.SignInByAllRoles))]
        public void SignIn<T>(T roles)
        {
            //Given
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
            var user = UserData.GetUserSignInputModelByEmailAndPassword(userInfo.Email, userInfo.Password);
            _endPoint = AuthorizationPoints.SignInPoint;
            var request = _requestHelper.Post(_endPoint, user);

            //When
            var response = _client.Execute<string>(request);

            //Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = response.Data;
            result.Should().NotBeNull();
        }
    }
}