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
    public class TagControllerTest : BaseControllerTest
    {
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void AddTag_TagDto_TagCreated(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            AuthenticateClient(token);

            _endPoint = AddTagPoint;
            var postData = TagData.GetTagInputModel_Correct();

            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute<TagOutputModel>(request);
            var result = JsonConvert.DeserializeObject<TagOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            postData.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.IsDeleted));
            result.Should().NotBeNull(result.Id.ToString());
        }

        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void DeleteTag_TagId_TagDeleted(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            AuthenticateClient(token);
            var result = _facade.CreateTagCorrect(token);
            
            var tagId = result.Id;
            _endPoint = string.Format(DeleteTagPoint, tagId);
            var request = _requestHelper.Delete(_endPoint, _headers);
            var response = _client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void UpdateTag_TagDto_Id_TagDto(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            AuthenticateClient(token);
            var result = _facade.CreateTagCorrect(token);
            var tagId = result.Id;
            var postData = TagData.GetTagInputModel_UpdatedModel();
            var jsonData = JsonConvert.SerializeObject(postData);
            _endPoint = string.Format(UpdateTagPoint, tagId);
            var request = _requestHelper.Put(_endPoint, _headers, jsonData);
            var response = _client.Execute(request);
            var updatedResult = JsonConvert.DeserializeObject<TagOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            postData.Should().BeEquivalentTo(updatedResult, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.IsDeleted));
            result.Should().NotBeNull(result.Id.ToString());
        }

        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void GetTagById_Id_TagDto(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            AuthenticateClient(token);
            var result = _facade.CreateTagCorrect(token);

            var tagId = result.Id;
            _endPoint = string.Format(GetTagByIdPoint, tagId);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute<TagOutputModel>(request);
            var updatedResult = JsonConvert.DeserializeObject<TagOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().BeEquivalentTo(updatedResult, options => options);
            result.Should().NotBeNull(result.Id.ToString());
        }

        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void GetAllTags_NoEntries_ListTagDto(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            
            AuthenticateClient(token);
            
            _endPoint = GetAllTagsPoint;

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute<List<TagOutputModel>>(request);
            var result = JsonConvert.DeserializeObject<List<TagOutputModel>>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().NotBeNull();
        }

        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void GetTagById_TagDoesntExist_EntityNotFoundException(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            AuthenticateClient(token);
            
            var tagId = 0;
            _endPoint = string.Format(GetTagByIdPoint, tagId);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute<TagOutputModel>(request);
            var updatedResult = JsonConvert.DeserializeObject<TagOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}