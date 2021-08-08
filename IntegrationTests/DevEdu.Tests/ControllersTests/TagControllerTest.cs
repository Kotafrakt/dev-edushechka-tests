﻿using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.ControllersTests
{
    public class TagControllerTest : BaseControllerTest
    {
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void CreateCorrectTag(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            AuthenticateClient(token);

            _endPoint = AddCoursePoint;
            var postData = TagData.GetTagInputModel_Correct();

            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute<TagInfoOutputModel>(request);
            var result = response.Data;

            postData.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id));
        }
    }
}