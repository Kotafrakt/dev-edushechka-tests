using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using static DevEdu.Tests.Constants.TagPoints;
using DevEdu.Tests.Facades;

namespace DevEdu.Tests.ControllersTests
{
	public class TagControllerTest : BaseControllerTest
	{
		private readonly TagSub _tagSub = new();

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void AddTag_TagDto_TagCreated<T>(T roles)
		{
			//Given
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			_endPoint = AddTagPoint;
			var postData = TagData.GetValidTagInputModel();
			var request = _requestHelper.CreatePost(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute<TagOutputModel>(request);

			//Then
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
			//Given
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			var result = _tagSub.AddTag(userInfo.Token);
			var tagId = result.Id;
			_endPoint = string.Format(DeleteTagPoint, tagId);
			var request = _requestHelper.CreateDelete(_endPoint, userInfo.Token);

			//When
			var response = _client.Execute(request);

			//Then
			_endPoint = string.Format(GetTagByIdPoint, tagId);
			var getRequest = _requestHelper.CreateGet(_endPoint, userInfo.Token);
			var getResponse = _client.Execute<TagOutputModel>(getRequest);
			var getResult = getResponse.Data;
			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
			getResult.IsDeleted.Should().Be(true);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void UpdateTag_TagDto_Id_TagDto<T>(T roles)
		{
			//Given
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			var result = _tagSub.AddTag(userInfo.Token);
			var tagId = result.Id;
			var postData = TagData.GetTagInputModel_UpdatedModel();
			_endPoint = string.Format(UpdateTagPoint, tagId);
			var request = _requestHelper.CreatePut(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute<TagOutputModel>(request);

			//Then
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
			//Given
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			var result = _tagSub.AddTag(userInfo.Token);
			var tagId = result.Id;
			_endPoint = string.Format(GetTagByIdPoint, tagId);
			var request = _requestHelper.CreateGet(_endPoint, userInfo.Token);

			//When
			var response = _client.Execute<TagOutputModel>(request);

			//Then
			var updatedResult = response.Data;
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			result.Should().BeEquivalentTo(updatedResult, options => options);
			result.Should().NotBeNull(result.Id.ToString());
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void GetAllTags_NoEntries_ListTagDto<T>(T roles)
		{
			//Given
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			List<int> ids = new List<int>();
            for (int i = 0; i < 5; i++)
            {
				var createResult = _tagSub.AddTag(userInfo.Token);
				ids.Add(createResult.Id);
			}
			_endPoint = GetAllTagsPoint;
			var request = _requestHelper.CreateGet(_endPoint, userInfo.Token);

			//When
			var response = _client.Execute<List<TagOutputModel>>(request);

			//Then
			var result = response.Data;
			response.StatusCode.Should().Be(HttpStatusCode.OK);
            foreach (var i in ids)
            {
				result.Should().Contain(x=> x.Id ==i);
			}
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void GetTagById_TagDoesntExist_EntityNotFoundException<T>(T roles)
		{
			//Given
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			var tagId = 0;
			_endPoint = string.Format(GetTagByIdPoint, tagId);
			var request = _requestHelper.CreateGet(_endPoint, userInfo.Token);

			//When
			var response = _client.Execute<TagOutputModel>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void UpdateTag_TagDoesntExist_EntityNotFoundException<T>(T roles)
		{
			//Given
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			var tagId = 0;
			var postData = TagData.GetTagInputModel_UpdatedModel();
			_endPoint = string.Format(UpdateTagPoint, tagId);
			var request = _requestHelper.CreatePut(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute<TagOutputModel>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithStudentAndTutor))]
		public void AddTag_TagDto_AuthorizationExceptionThrown<T>(T roles)
		{
			//Given
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			_endPoint = AddTagPoint;
			var postData = TagData.GetValidTagInputModel();
			var request = _requestHelper.CreatePost(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute<TagOutputModel>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithStudentAndTutor))]
		public void DeleteTag_TagId_AuthorizationExceptionThrown<T>(T roles)
		{
			//Given
			var tokenAdmin = _facade.SignInByAdmin();
			var result = _tagSub.AddTag(tokenAdmin);
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			var tagId = result.Id;
			_endPoint = string.Format(DeleteTagPoint, tagId);
			var request = _requestHelper.CreateDelete(_endPoint, userInfo.Token);

			//When
			var response = _client.Execute(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithStudentAndTutor))]
		public void UpdateTag_TagDto_Id_AuthorizationExceptionThrown<T>(T roles)
		{
			//Given
			var tokenAdmin = _facade.SignInByAdmin();
			var result = _tagSub.AddTag(tokenAdmin);
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(roles);
			var tagId = result.Id;
			var postData = TagData.GetTagInputModel_UpdatedModel();
			_endPoint = string.Format(UpdateTagPoint, tagId);
			var request = _requestHelper.CreatePut(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}
	}
}