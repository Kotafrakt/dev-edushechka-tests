using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using DevEdu.Tests.Constants;
using System.Net;

namespace DevEdu.Tests.ControllersTests
{
    public class TagControllerTest : BaseControllerTest
    {
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void CreateCorrectTag(List<Role> roles)
        {
            var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

            _endPoint = TagPoints.AddTagPoint;
            var postData = TagData.GetValidTagInputModel();

            var request = _requestHelper.Post(_endPoint, postData);
            request = _requestHelper.Autorize(request, userInfo.Token);

            var response = _client.Execute<TagOutputModel>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = response.Data;

            postData.Should().BeEquivalentTo
            (
                result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.IsDeleted)
            );
        }
    }
}