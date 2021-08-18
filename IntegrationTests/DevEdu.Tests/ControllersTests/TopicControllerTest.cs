using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using DevEdu.Tests.Facades;
using static DevEdu.Tests.Constants.TopicEndpoints;

namespace DevEdu.Tests.ControllersTests
{
    class TopicControllerTest : BaseControllerTest
    {
        private readonly AuthenticationControllerFacade _authenticationFacade = new();

		[TestCase(Role.Manager)]
		[TestCase(Role.Methodist)]
		public void AddTag_TagDto_TagCreated(Role role)
		{
			//Given
			var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
			_endPoint = AddTopicEndpoint;
			var postData = TopicData.GetValidTopicInputModel();
			var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);

			//When
			var response = _client.Execute<TopicOutputModel>(request);

			//Then
			response.StatusCode.Should().Be(HttpStatusCode.Created);
			var result = response.Data;
			postData.Should().BeEquivalentTo
			(
				result, options => options
				.Excluding(obj => obj.Id)
			);
		}
	}
}