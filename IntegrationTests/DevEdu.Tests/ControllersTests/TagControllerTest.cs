using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using static DevEdu.Tests.Constants.TagEndpoints;
using DevEdu.Tests.Facades;
using DevEdu.Core.Exceptions;
using DevEdu.Core.Common;

namespace DevEdu.Tests.ControllersTests
{
	public class TagControllerTest : BaseControllerTest
	{
		private readonly TagControllerFacade _tagFacade = new();
		private readonly AuthenticationControllerFacade _authenticationFacade = new();

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void AddTag_TagDto_TagCreated<T>(T roles)
		{
			//Given
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			_endPoint = AddTagEndpoint;
			var postData = TagData.GetValidTagInputModel();
			var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);

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
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var result = _tagFacade.AddTag(userInfo.Token);
			var tagId = result.Id;
			_endPoint = string.Format(DeleteTagEndpoint, tagId);
			var request = _requestHelper.CreateDeleteRequest(_endPoint, userInfo.Token);

			//When
			var response = _client.Execute(request);

			//Then
			_endPoint = string.Format(GetTagByIdEndpoint, tagId);
			var getRequest = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);
			var getResponse = _client.Execute<TagOutputModel>(getRequest);
			var getResult = getResponse.Data;
			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
			getResult.IsDeleted.Should().Be(true);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void UpdateTag_TagDto_Id_TagDto<T>(T roles)
		{
			//Given
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var result = _tagFacade.AddTag(userInfo.Token);
			var tagId = result.Id;
			var postData = TagData.GetTagInputModel_UpdatedModel();
			_endPoint = string.Format(UpdateTagEndpoint, tagId);
			var request = _requestHelper.CreatePutRequest(_endPoint, postData, userInfo.Token);

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
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var result = _tagFacade.AddTag(userInfo.Token);
			var tagId = result.Id;
			_endPoint = string.Format(GetTagByIdEndpoint, tagId);
			var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);

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
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var ids = new List<int>();
            for (int i = 0; i < 5; i++)
            {
				var createResult = _tagFacade.AddTag(userInfo.Token);
				ids.Add(createResult.Id);
			}
			_endPoint = GetAllTagsEndpoint;
			var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);

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
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var tagId = 0;
			var exception = BaseData.GetEntityNotFoundExceptionResponse("tag",tagId);
			_endPoint = string.Format(GetTagByIdEndpoint, tagId);
			var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);

			//When
			var response = _client.Execute<ExceptionResponse>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
			var result = response.Data;
			result.Should().BeEquivalentTo(exception);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void UpdateTag_TagDoesntExist_EntityNotFoundException<T>(T roles)
		{
			//Given
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var tagId = 0;
			var exception = BaseData.GetEntityNotFoundExceptionResponse("tag", tagId);
			var postData = TagData.GetTagInputModel_UpdatedModel();
			_endPoint = string.Format(UpdateTagEndpoint, tagId);
			var request = _requestHelper.CreatePutRequest(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute<ExceptionResponse>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
			var result = response.Data;
			result.Should().BeEquivalentTo(exception);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void DeleteTag_TagDoesntExist_EntityNotFoundException<T>(T roles)
		{
			//Given
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var tagId = 0;
			var exception = BaseData.GetEntityNotFoundExceptionResponse("tag", tagId);
			_endPoint = string.Format(DeleteTagEndpoint, tagId);
			var request = _requestHelper.CreateDeleteRequest(_endPoint, userInfo.Token);

			//When
			var response = _client.Execute<ExceptionResponse>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
			var result = response.Data;
			result.Should().BeEquivalentTo(exception);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithStudentAndTutor))]
		public void AddTag_TagDto_AuthorizationExceptionThrown<T>(T roles)
		{
			//Given
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			_endPoint = AddTagEndpoint;
			var postData = TagData.GetValidTagInputModel();
			var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute<TagOutputModel>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithStudentAndTutor))]
		public void DeleteTag_TagId_AuthorizationExceptionThrown<T>(T roles)
		{
			//Given
			var tokenAdmin = _authenticationFacade.SignInByAdmin();
			var result = _tagFacade.AddTag(tokenAdmin);
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var tagId = result.Id;
			_endPoint = string.Format(DeleteTagEndpoint, tagId);
			var request = _requestHelper.CreateDeleteRequest(_endPoint, userInfo.Token);

			//When
			var response = _client.Execute(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithStudentAndTutor))]
		public void UpdateTag_TagDto_Id_AuthorizationExceptionThrown<T>(T roles)
		{
			//Given
			var tokenAdmin = _authenticationFacade.SignInByAdmin();
			var result = _tagFacade.AddTag(tokenAdmin);
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var tagId = result.Id;
			var postData = TagData.GetTagInputModel_UpdatedModel();
			_endPoint = string.Format(UpdateTagEndpoint, tagId);
			var request = _requestHelper.CreatePutRequest(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute<ExceptionResponse>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[TestCaseSource(typeof(UserData), nameof(UserData.SignInByRolesWithoutStudentAndTutor))]
		public void AddTag_InvalidTagDto_ValidationExceptionThrown<T>(T roles)
		{
			//Given
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(roles);
			var exception = TagData.GetValidationExceptionResponse();
			_endPoint = AddTagEndpoint;
			var postData = TagData.GetInValidTagInputModel();
			var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute<ValidationExceptionResponse>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
			var result = response.Data;
			result.Should().BeEquivalentTo(exception); //1 вариант


            result.Errors.Should().Contain(error => error.Message.Equals(ValidationMessage.NameRequired)); //2 вариант
			result.Errors.Should().Contain(error => error.Field.Equals(nameof(TagInputModel.Name)));
		}
	}
}