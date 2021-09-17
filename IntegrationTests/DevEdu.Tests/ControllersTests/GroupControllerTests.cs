using DevEdu.Core.Enums;
using DevEdu.Tests.Data;
using DevEdu.Tests.Facades;
using NUnit.Framework;
using static DevEdu.Tests.Constants.GroupEndpoints;
using System.Collections.Generic;
using DevEdu.Core.Requests;
using DevEdu.Core.Models;
using FluentAssertions;
using System.Net;

namespace DevEdu.Tests.ControllersTests
{
    public class GroupControllerTests : BaseControllerTest
    {
        private readonly AuthenticationFacade _authenticationFacade = new();
        private readonly GroupFacade _groupFacade = new();
        private readonly CourseFacade _courseFacade = new();
        private readonly LessonFacade _lessonFacade = new();
        private readonly TopicFacade _topicFacade = new();

        [TestCase(Role.Manager)]
        public void AddGroup_GroupDto_GroupCreated(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            _endPoint = AddGroupEndpoint;
            var token = _authenticationFacade.GetTokenByEmailAndPassword(userInfo.Email, userInfo.Password);
            var courdeId = _courseFacade.CreateCourse(token);
            var postData = GroupData.GetValidGroupInputModel(courdeId.Id);
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);
            //When
            var response = _client.Execute<GroupOutputModel>(request);

            //Then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var result = response.Data;
            postData.Should().BeEquivalentTo
            (
                result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.Course)
                .Excluding(obj => obj.GroupStatus)
            );
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Teacher)]
        public void AddGroup_AddGroupByUnauthorizedRole_Returned403StatusCode(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var adminToken = _authenticationFacade.SignInByAdmin();
            _endPoint = AddGroupEndpoint;
            
            var courdeId = _courseFacade.CreateCourse(adminToken);
            var postData = GroupData.GetValidGroupInputModel(courdeId.Id);
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        [TestCase(Role.Manager)]
        public void AddGroup_GroupWithUncorrectDateFormat_Returned422StatusCode(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            _endPoint = AddGroupEndpoint;
            var token = _authenticationFacade.GetTokenByEmailAndPassword(userInfo.Email, userInfo.Password);
            var courdeId = _courseFacade.CreateCourse(token);
            var postData = GroupData.GetValidGroupInputModel(courdeId.Id);
            postData.StartDate = "21,03,2021";
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        }
        [TestCase(Role.Manager)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Teacher)]
        public void GetGroup_WithtAuthorizeAndWithValidId_GroupGotten(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var adminToken = _authenticationFacade.SignInByAdmin();
            _endPoint = AddGroupEndpoint;

            var courdeId = _courseFacade.CreateCourse(adminToken);
            var postData = GroupData.GetValidGroupInputModel(courdeId.Id);
            var postResponce = GroupData.CreateGroupInDbByAdminAndGetModel(postData, adminToken);
            _endPoint = string.Format(GetGroupEndpoint, postResponce.Id);

            var requestForGet = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute<GroupOutputModel>(requestForGet);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = actualResponce.Data;
            //postData.Should().BeEquivalentTo(result);
            result.Should().BeEquivalentTo
            (
                postData, options => options
                .Excluding(obj => obj.GroupStatusId)
                .Excluding(obj => obj.CourseId)
            );
        }
        [TestCase(Role.Manager)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Teacher)]
        public void GetGroup_WithtAuthorizeAndWithNotValidId_Returned404StatusCode(Role role)
        {
            //Given
            var invalidGroupId = 0;
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _endPoint = string.Format(GetGroupEndpoint, invalidGroupId);
            var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
        [Test]
        public void GetGroup_WithtoutAuthorize_Returned401StatusCode()
        {
            //Given
            _endPoint = string.Format(GetGroupEndpoint, 21);
            var userInfo = string.Empty;
            var request = _requestHelper.CreateGetRequest(_endPoint, userInfo);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [TestCase(Role.Manager)]
        public void GetAllGroups_ByManager_AllGroupsGotten(Role role)
        {
            //Given
            _endPoint = GetAllGroupsEndpoint;
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            var adminToken = _authenticationFacade.SignInByAdmin();

            for (int i = 0; i < 3; i++)
            {
                var courseId = _courseFacade.CreateCourse(adminToken);
                var postData = GroupData.GetValidGroupInputModel(courseId.Id);
                GroupData.CreateGroupInDbByAdminAndGetModel(postData, adminToken);
            }
            var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute<List<GroupOutputModel>>(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = actualResponce.Data;
            result.Should().NotBeEmpty();
        }
        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Teacher)]
        public void GetAllGroups_ByNotAllowedRoles_Returned403StatusCode(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _endPoint = GetAllGroupsEndpoint;
            var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute<List<GroupOutputModel>>(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        [TestCase(Role.Manager)]
        public void DeleteGroup_ValidGroupId_GroupDeleted(Role role)
        {
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            var adminToken = _authenticationFacade.SignInByAdmin();
            var courseId = _courseFacade.CreateCourse(adminToken);
            var postData = GroupData.GetValidGroupInputModel(courseId.Id);
            var group = GroupData.CreateGroupInDbByAdminAndGetModel(postData, adminToken);
            _endPoint = string.Format(DeleteGroupEndpoint, group.Id);
            var request = _requestHelper.CreateDeleteRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        [TestCase(Role.Manager)]
        public void DeleteGroup_InvalidGroupId_Returned404StatusCode(Role role)
        {
            var invalidId = 0;
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            _endPoint = string.Format(DeleteGroupEndpoint, invalidId);
            var request = _requestHelper.CreateDeleteRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Teacher)]
        public void DeleteGroup_ByNotAllowedRoles_Returned403StatusCode(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _endPoint = string.Format(DeleteGroupEndpoint, 2);
            var request = _requestHelper.CreateDeleteRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        [TestCase(Role.Manager)]
        public void UpdateGroup_ValidDtoAndAllowedRole_GroupUpdated(Role role)
        {
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            var adminToken = _authenticationFacade.SignInByAdmin();
            var course = _courseFacade.CreateCourse(adminToken);
            var postData = GroupData.GetValidGroupInputModel(course.Id);
            var groupInDbToUpdate = GroupData.CreateGroupInDbByAdminAndGetModel(postData,adminToken);
            _endPoint = string.Format(UpdateGroupEndpoint, groupInDbToUpdate.Id);
            var groupInfoToUpdate = GroupData.GetValidGroupInputModel(course.Id);
            var request = _requestHelper.CreatePutRequest(_endPoint, groupInfoToUpdate, userInfo.Token);
            //When
            var actualResponce = _client.Execute<GroupOutputModel>(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Teacher)]
        public void UpdateGroup_NotAllowedRoles_Returned403StatusCode(Role role)
        {
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            var updData = GroupData.GetValidGroupInputModel(0);
            _endPoint = string.Format(UpdateGroupEndpoint, 0);
            var request = _requestHelper.CreatePutRequest(_endPoint, updData, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Manager)]
        public void UpdateGroup_InvalidGroupId_Returned404StatusCode(Role role)
        {
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            var updData = GroupData.GetValidGroupInputModel(0);
            _endPoint = string.Format(UpdateGroupEndpoint, 0);
            var request = _requestHelper.CreatePutRequest(_endPoint, updData, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestCase(Role.Manager)]
        public void UpdateGroup_InvalidDataInDto_Returned422StatusCode(Role role)
        {
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            var updData = GroupData.GetInValidGroupInputModel(0);
            _endPoint = string.Format(UpdateGroupEndpoint, 0);
            var request = _requestHelper.CreatePutRequest(_endPoint, updData, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
        [TestCase(Role.Teacher)]
        public void AddGroupToLesson_ValidData_GroupAddedToLesson(Role role)
        {
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            var adminToken = _authenticationFacade.SignInByAdmin();
            var course = _courseFacade.CreateCourse(adminToken);
            var groupModel = GroupData.GetValidGroupInputModel(course.Id);
            var group = GroupData.CreateGroupInDbByAdminAndGetModel(groupModel, adminToken);
            var topic = _topicFacade.AddTopic(adminToken);
            var lesson = _lessonFacade.AddLesson(adminToken, userInfo.Id);
            _lessonFacade.AddTopicToLesson(userInfo.Token, userInfo.Id, lesson.Id, topic.Id);
            _endPoint = string.Format(AddGroupToLessonEndpoint, group.Id, lesson.Id);
            var request = _requestHelper.CreatePostReferenceRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        [TestCase(Role.Teacher)]
        public void AddGroupToLesson_InValidGroupId_Returned404StatusCode(Role role)
        {
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            var adminToken = _authenticationFacade.SignInByAdmin();
            var topic = _topicFacade.AddTopic(adminToken);
            var lesson = _lessonFacade.AddLesson(adminToken, userInfo.Id);
            _lessonFacade.AddTopicToLesson(userInfo.Token, userInfo.Id, lesson.Id, topic.Id);
            _endPoint = string.Format(AddGroupToLessonEndpoint, 0, lesson.Id);
            var request = _requestHelper.CreatePostReferenceRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        public void AddGroupToLesson_NotAllowedRoles_Returned403StatusCode(Role role)
        {
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            _endPoint = string.Format(AddGroupToLessonEndpoint, 0, 0);
            var request = _requestHelper.CreatePostReferenceRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

    }
}
