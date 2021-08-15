using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using static DevEdu.Tests.Constants.TopicPoints;

namespace DevEdu.Tests.ControllersTests
{
    class TopicControllerTest : BaseControllerTest
    {
		[TestCase(Role.Manager)]
		[TestCase(Role.Methodist)]
		public void AddTag_TagDto_TagCreated(Role role)
		{
			//Given
			var userInfo = _facade.SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser(role);
			_endPoint = AddTopicPoint;
			var postData = TopicData.GetValidTopicInputModel();
			var request = _requestHelper.CreatePost(_endPoint, postData, userInfo.Token);

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