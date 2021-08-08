﻿using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.ControllersTests
{
    public class MaterialControllerTests : BaseControllerTest
    {
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GeеAllRolesOneByOne))]
        public void AddMaterial(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            AuthenticateClient(token);

            _endPoint = AddMaterialPoint;
            var postData = MaterialData.GetMaterialInputModel_Correct();
            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            postData.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.IsDeleted)
                .Excluding(obj => obj.Tags));
        }
    }
}