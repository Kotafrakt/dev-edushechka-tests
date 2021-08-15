﻿using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace DevEdu.Tests.ControllersTests
{
    public class UniversalControllerTests : BaseControllerTest
    {
        [TestCaseSource(typeof(UniversalData), nameof(UniversalData.Universal))]
        public void Add<T, TU>(TU type, T content, List<Role> roles, string endpoint)
        {
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRole(Role.Manager);

            _endPoint = endpoint;

            var request = _requestHelper.Post(_endPoint, content);
            request = _requestHelper.Autorize(request, userInfo.Token);
            var response = _client.Execute<T>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;

            ShouldBeEquivalentTo(content, result);
        }

        public void ShouldBeEquivalentTo<T, TU>(T postData, TU responseData)
        {
            if (postData is UserInsertInputModel data && responseData is UserInfoOutPutModel result)
            {
                data.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.Email));
            }
        }
    }
}