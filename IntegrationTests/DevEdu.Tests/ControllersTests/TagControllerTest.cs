﻿using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using static DevEdu.Tests.Constants.TagPoints;

namespace DevEdu.Tests.ControllersTests
{
	public class TagControllerTest : BaseControllerTest
	{
		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void AddTag_TagDto_TagCreated<T>(T roles)
		{
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

			_endPoint = AddTagPoint;
			var postData = TagData.GetValidTagInputModel();

			var request = _requestHelper.CreatePost(_endPoint, postData, userInfo.Token);

			var response = _client.Execute<TagOutputModel>(request);
			response.StatusCode.Should().Be(HttpStatusCode.Created);
			var result = response.Data;

			postData.Should().BeEquivalentTo
			(
				result, options => options
				.Excluding(obj => obj.Id)
				.Excluding(obj => obj.IsDeleted)
			);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void DeleteTag_TagId_TagDeleted<T>(T roles)
		{
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			var result = _facade.CreateTagCorrect(userInfo.Token);
			var tagId = result.Id;
			_endPoint = string.Format(DeleteTagPoint, tagId);
			var request = _requestHelper.CreateDelete(_endPoint, userInfo.Token);
			var response = _client.Execute(request);
			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void UpdateTag_TagDto_Id_TagDto<T>(T roles)
		{
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

			var result = _facade.CreateTagCorrect(userInfo.Token);
			var tagId = result.Id;
			var postData = TagData.GetTagInputModel_UpdatedModel();
			_endPoint = string.Format(UpdateTagPoint, tagId);
			var request = _requestHelper.CreatePut(_endPoint, postData, userInfo.Token);
			var response = _client.Execute<TagOutputModel>(request);
			var updatedResult = response.Data;

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			postData.Should().BeEquivalentTo(updatedResult, options => options
				.Excluding(obj => obj.Id)
				.Excluding(obj => obj.IsDeleted));
			result.Should().NotBeNull(result.Id.ToString());
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void GetTagById_Id_TagDto<T>(T roles)
		{
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

			var result = _facade.CreateTagCorrect(userInfo.Token);

			var tagId = result.Id;
			_endPoint = string.Format(GetTagByIdPoint, tagId);

			var request = _requestHelper.CreateGet(_endPoint, userInfo.Token);
			var response = _client.Execute<TagOutputModel>(request);
			var updatedResult = response.Data;

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			result.Should().BeEquivalentTo(updatedResult, options => options);
			result.Should().NotBeNull(result.Id.ToString());
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void GetAllTags_NoEntries_ListTagDto<T>(T roles)
		{
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

			_endPoint = GetAllTagsPoint;

			var request = _requestHelper.CreateGet(_endPoint, userInfo.Token);
			var response = _client.Execute<List<TagOutputModel>>(request);
			var result = response.Data;

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			result.Should().NotBeNull();
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void GetTagById_TagDoesntExist_EntityNotFoundException<T>(T roles)
		{
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

			var tagId = 0;
			_endPoint = string.Format(GetTagByIdPoint, tagId);

			var request = _requestHelper.CreateGet(_endPoint, userInfo.Token);
			var response = _client.Execute<TagOutputModel>(request);

			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void UpdateTag_TagDoesntExist_EntityNotFoundException<T>(T roles)
		{
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

			var tagId = 0;
			var postData = TagData.GetTagInputModel_UpdatedModel();
			_endPoint = string.Format(UpdateTagPoint, tagId);

			var request = _requestHelper.CreatePut(_endPoint, postData, userInfo.Token);
			var response = _client.Execute<TagOutputModel>(request);

			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithStudentAndTutor))]
		public void AddTag_TagDto_AuthorizationExceptionThrown<T>(T roles)
		{
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

			_endPoint = AddTagPoint;
			var postData = TagData.GetValidTagInputModel();
			var request = _requestHelper.CreatePost(_endPoint, postData, userInfo.Token);
			var response = _client.Execute<TagOutputModel>(request);

			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithStudentAndTutor))]
		public void DeleteTag_TagId_AuthorizationExceptionThrown<T>(T roles)
		{
			var tokenAdmin = _facade.SignInByAdmin();
			var result = _facade.CreateTagCorrect(tokenAdmin);

			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

			var tagId = result.Id;
			_endPoint = string.Format(DeleteTagPoint, tagId);
			var request = _requestHelper.CreateDelete(_endPoint, userInfo.Token);
			var response = _client.Execute(request);

			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithStudentAndTutor))]
		public void UpdateTag_TagDto_Id_AuthorizationExceptionThrown<T>(T roles)
		{
			var tokenAdmin = _facade.SignInByAdmin();
			var result = _facade.CreateTagCorrect(tokenAdmin);

			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);

			var tagId = result.Id;
			var postData = TagData.GetTagInputModel_UpdatedModel();
			_endPoint = string.Format(UpdateTagPoint, tagId);
			var request = _requestHelper.CreatePut(_endPoint, postData, userInfo.Token);
			var response = _client.Execute(request);

			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}
	}
}