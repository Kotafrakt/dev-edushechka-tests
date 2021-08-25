using DevEdu.Core.Common;
using DevEdu.Core.Enums;
using DevEdu.Core.Exceptions;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using DevEdu.Tests.Facades;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DevEdu.Tests.ControllersTests
{
    public class TaskControllerTests : BaseControllerTest
    {
        private GroupFacade _groupFacade;
        private TagFacade _tagFacade;
        private CourseFacade _courseFacade;
        private TaskFacade _taskFacade;
        private AuthenticationFacade _authenticationFacade;

        public TaskControllerTests() : base()
        {
            _groupFacade = new GroupFacade();
            _tagFacade = new TagFacade();
            _authenticationFacade = new AuthenticationFacade();
            _courseFacade = new CourseFacade();
            _taskFacade = new TaskFacade();
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]
        public void AddTaskByTeacher_AddTaskWithHomeworkByAuthorizedRole_TaskAdded(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var group = _groupFacade.CreateValidGroup(token);
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var task = TaskData.GetValidTaskByTeacherWithHomework(group.Id, tags.Select(tag => tag.Id).ToList());
            var request = _requestHelper.CreatePostRequest(TaskEndpoints.AddTaskByTeacherEndpoint, task, token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Created);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags)
            .Excluding(o => o.GroupId)
            .Excluding(o => o.Homework));
            actualResponce.Data.Id.Should().NotBe(default);
            actualResponce.Data.IsDeleted.Should().Be(false);
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]
        public void AddTaskByTeacher_AddTaskWithoutHomeworkByAuthorizedRole_TaskAdded(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var task = TaskData.GetValidTaskByTeacherWithoutHomework(tags.Select(tag => tag.Id).ToList());
            var request = _requestHelper.CreatePostRequest(TaskEndpoints.AddTaskByTeacherEndpoint, task, token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Created);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags)
            .Excluding(o => o.GroupId)
            .Excluding(o => o.Homework));
            actualResponce.Data.Id.Should().NotBe(default);
            actualResponce.Data.IsDeleted.Should().Be(false);
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]
        public void AddTaskByTeacher_AddTaskWithoutTagsByAuthorizedRole_TaskAdded(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var task = TaskData.GetValidTaskByTeacherWithoutHomework();
            var request = _requestHelper.CreatePostRequest(TaskEndpoints.AddTaskByTeacherEndpoint, task, token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Created);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags)
            .Excluding(o => o.GroupId)
            .Excluding(o => o.Homework));
            actualResponce.Data.Id.Should().NotBe(default);
            actualResponce.Data.IsDeleted.Should().Be(false);
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        public void AddTaskByTeacher_AddTaskByUnauthorizedRole_Returned403StatusCode(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var task = TaskData.GetValidTaskByTeacherWithoutHomework(tags.Select(tag => tag.Id).ToList());
            var request = _requestHelper.CreatePostRequest(TaskEndpoints.AddTaskByTeacherEndpoint, task, token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]

        public void AddTaskByTeacher_AddInvalidTask_ReturnedValidationExceptionResponse(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var task = TaskData.GetInvalidTaskByTeacher();
            var exception = TaskData.GetValidationExceptionResponse();
            var request = _requestHelper.CreatePostRequest(TaskEndpoints.AddTaskByTeacherEndpoint, task, token);

            //When
            var actualResponce = _client.Execute<ValidationExceptionResponse>(request);

            //Then
            actualResponce.Data.Message.Should().Be(exception.Message);
            actualResponce.Data.Code.Should().Be(exception.Code);
            actualResponce.Data.Errors.Count.Should().Be(exception.Errors.Count);
            actualResponce.Data.Errors.Intersect(exception.Errors).ToList().Count.Should().Be(exception.Errors.Count);
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Admin)]
        public void AddTaskByMethodist_AddTaskByAuthorizedRole_TaskAdded(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var courses = _courseFacade.CreateListOfCourses(token);
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var task = TaskData.GetValidTaskByMethodist(courses.Select(course => course.Id).ToList(), tags.Select(tag => tag.Id).ToList());
            var request = _requestHelper.CreatePostRequest(TaskEndpoints.AddTaskByMethodistEndpoint, task, token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Created);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
             .Excluding(o => o.Tags)
             .Excluding(o => o.CourseIds));
            actualResponce.Data.Id.Should().NotBe(default);
            actualResponce.Data.IsDeleted.Should().Be(false);
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Manager)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        public void AddTaskByMethodist_AddTaskByUnauthorizedRole_Returned403StatusCode(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var task = TaskData.GetValidTaskByMethodist();
            var request = _requestHelper.CreatePostRequest(TaskEndpoints.AddTaskByMethodistEndpoint, task, token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Admin)]
        public void AddTaskByMethodist_AddInvalidTask_ReturnedValidationExceptionResponse(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var exception = TaskData.GetValidationExceptionResponse();
            var task = TaskData.GetInvalidTaskByMethodist();
            var request = _requestHelper.CreatePostRequest(TaskEndpoints.AddTaskByMethodistEndpoint, task, token);

            //When
            var actualResponce = _client.Execute<ValidationExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            actualResponce.Data.Message.Should().Be(exception.Message);
            actualResponce.Data.Code.Should().Be(exception.Code);
            actualResponce.Data.Errors.Count.Should().Be(exception.Errors.Count);
            actualResponce.Data.Errors.Intersect(exception.Errors).ToList().Count.Should().Be(exception.Errors.Count);
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Admin)]
        public void UpdateTaskByMethodist_UpdateTaskByAuthorizedRole_TaskUpdated(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(token);
            var courseIds = _courseFacade.CreateListOfCourses(token).Select(course => course.Id).ToList();
            var taskId = _taskFacade.CreateValidTaskByMethodist(token, courseIds, tags.Select(tag => tag.Id).ToList()).Id;
            var task = TaskData.GetValidTaskByMethodist();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByMethodistEndpoint, taskId), task, token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags)
            .Excluding(o => o.CourseIds));
            actualResponce.Data.Id.Should().Be(taskId);
            actualResponce.Data.IsDeleted.Should().Be(false);
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Manager)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        public void UpdateTaskByMethodist_UpdateTaskByUnauthorizedRole_Returned403StatusCode(Role role)
        {
            //Given
            var taskId = 0;
            var task = TaskData.GetValidTaskByMethodist();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByMethodistEndpoint, taskId), task, token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Methodist)]
        public void UpdateTaskByMethodist_UpdateTaskByAuthorizedRoleButUnauthorizedUserId_Returned403StatusCode(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(token);
            var taskId = _taskFacade.CreateValidTaskByMethodist(token, tagIds: tags.Select(tag => tag.Id).ToList()).Id;
            var task = TaskData.GetValidTaskByMethodist();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByMethodistEndpoint, taskId), task, token);
            var expectedExeption = BaseData.GetAuthorizationExceptionResponce(
                string.Format(ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", taskId));
            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().Be(expectedExeption);
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Admin)]
        public void UpdateTaskByMethodist_InvalidModel_ReturnedValidationExceptionResponse(Role role)
        {
            //Given
            var task = TaskData.GetInvalidTaskByMethodist();
            var taskId = 0;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByMethodistEndpoint, taskId), task, token);
            var expectedExeption = TaskData.GetValidationExceptionResponse();
            //When
            var actualResponce = _client.Execute<ValidationExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            actualResponce.Data.Message.Should().Be(expectedExeption.Message);
            actualResponce.Data.Code.Should().Be(expectedExeption.Code);
            actualResponce.Data.Errors.Count.Should().Be(expectedExeption.Errors.Count);
            actualResponce.Data.Errors.Intersect(expectedExeption.Errors).ToList().Count.Should().Be(expectedExeption.Errors.Count);
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Admin)]
        public void UpdateTaskByMethodist_PutUnexistingId_ReturnedExceptionResponse(Role role)
        {
            //Given
            var task = TaskData.GetValidTaskByMethodist();
            var taskId = 0;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByMethodistEndpoint, taskId), task, token);
            var expectedExeption = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            actualResponce.Data.Should().Be(expectedExeption);
        }

        [TestCase(Role.Teacher)]
        public void UpdateTaskByTeacher_UpdateTaskByTeacher_TaskUpdated(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var groupId = _groupFacade.CreateValidGroup(token).Id;
            var tags = _tagFacade.AddValidTagList(token);
            var taskId = _taskFacade.CreateValidTaskByTeacherWithHomework(token, groupId, tags.Select(tag => tag.Id).ToList()).Id;
            var task = TaskData.GetValidTaskByTeacherUpdateInputModel();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _groupFacade.AddUserToGroup(token, groupId, user.Id, role);
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByTeacherEndpoint, taskId), task, token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags)
            .Excluding(o => o.Homework));
            actualResponce.Data.Id.Should().Be(taskId);
            actualResponce.Data.IsDeleted.Should().Be(false);
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
        }

        [TestCase(Role.Admin)]
        public void UpdateTaskByTeacher_UpdateTaskByAdmin_TaskUpdated(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var groupId = _groupFacade.CreateValidGroup(token).Id;
            var tags = _tagFacade.AddValidTagList(token);
            var taskId = _taskFacade.CreateValidTaskByTeacherWithHomework(token, groupId, tags.Select(tag => tag.Id).ToList()).Id;
            var task = TaskData.GetValidTaskByTeacherUpdateInputModel();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByTeacherEndpoint, taskId), task, token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags)
            .Excluding(o => o.Homework));
            actualResponce.Data.Id.Should().Be(taskId);
            actualResponce.Data.IsDeleted.Should().Be(default);
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        public void UpdateTaskByTeacher_UpdateTaskByUnauthorizedRole_Returned403StatusCode(Role role)
        {
            //Given
            var taskId = 0;
            var task = TaskData.GetValidTaskByTeacherUpdateInputModel();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByTeacherEndpoint, taskId), task, token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Teacher)]
        public void UpdateTaskByTeacher_UpdateTaskByTeacherUnauthorizedToGroup_Returned403StatusCode(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var groupId = _groupFacade.CreateValidGroup(token).Id;
            var tags = _tagFacade.AddValidTagList(token);
            var taskId = _taskFacade.CreateValidTaskByTeacherWithHomework(token, groupId, tags.Select(tag => tag.Id).ToList()).Id;
            var task = TaskData.GetValidTaskByTeacherUpdateInputModel();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var expectedException = BaseData.GetAuthorizationExceptionResponce(
                string.Format(ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", taskId));
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByTeacherEndpoint, taskId), task, token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().Be(expectedException);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]
        public void UpdateTaskByTeacher_InvalidModel_ReturnedExceptionResponse(Role role)
        {
            //Given
            var taskId = 0;
            var task = TaskData.GetInvalidTaskByTeacherUpdateInputModel();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var expectedException = TaskData.GetValidationExceptionResponse();
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByTeacherEndpoint, taskId), task, token);

            //When
            var actualResponce = _client.Execute<ValidationExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            actualResponce.Data.Message.Should().Be(expectedException.Message);
            actualResponce.Data.Code.Should().Be(expectedException.Code);
            actualResponce.Data.Errors.Count.Should().Be(expectedException.Errors.Count);
            actualResponce.Data.Errors.Intersect(expectedException.Errors).ToList().Count.Should().Be(expectedException.Errors.Count);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]
        public void UpdateTaskByTeacher_PutUnexistingId_ReturnedExceptionResponse(Role role)
        {
            //Given
            var taskId = 0;
            var task = TaskData.GetValidTaskByTeacherUpdateInputModel();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var expectedExeption = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByTeacherEndpoint, taskId), task, token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            actualResponce.Data.Should().Be(expectedExeption);
        }

        [TestCase(Role.Admin)]
        public void GetTaskWithTags_ByAdmin_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var groupId = _groupFacade.CreateValidGroup(token).Id;
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(token, groupId, tags.Select(tag => tag.Id).ToList());
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
           .Excluding(o => o.Tags));
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        public void GetTaskWithTags_AuthorizedByGroupUsers_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var groupId = _groupFacade.CreateValidGroup(token).Id;
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(token, groupId, tags.Select(tag => tag.Id).ToList());
            _groupFacade.AddUserToGroup(token, groupId, user.Id, role);
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags));
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
        }

        [TestCase(Role.Methodist)]
        public void GetTaskWithTags_ByMethodist_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var courseIds = _courseFacade.CreateListOfCourses(token).Select(course => course.Id).ToList();
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByMethodist(token, courseIds, tags.Select(tag => tag.Id).ToList());
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Should().BeEquivalentTo(task);
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        public void GetTaskWithTags_UnauthorizedByGroupUsers_403StatusCodeReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var groupId = _groupFacade.CreateValidGroup(token).Id;
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(token, groupId, tags.Select(tag => tag.Id).ToList());
            var expectedException = BaseData.GetAuthorizationExceptionResponce(
                string.Format(ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", task.Id));
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().Be(expectedException);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Admin)]
        public void GetTaskWithTags_PutUnexistingId_ExceptionResponceReturned(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskId = 0;
            var expectedException = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, taskId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actualResponce.Data.Should().Be(expectedException);
        }

        [TestCase(Role.Admin)]
        public void GetTaskWithTagsAndCourses_ByAdmin_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var courses = _courseFacade.CreateListOfCourses(token);
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByMethodist(
                    token,
                    courses.Select(course => course.Id).ToList(),
                    tags.Select(tag => tag.Id).ToList()
                    );
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndCoursesEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoWithCoursesOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags));
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
            actualResponce.Data.Courses.Count.Should().Be(courses.Count);
            actualResponce.Data.Courses.Intersect(courses).ToList().Count.Should().Be(courses.Count);
        }

        [TestCase(Role.Methodist)]
        public void GetTaskWithTagsAndCourses_ByMethodist_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var courses = _courseFacade.CreateListOfCourses(token);
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByMethodist(
                    token,
                    courses.Select(course => course.Id).ToList(),
                    tags.Select(tag => tag.Id).ToList()
                    );
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndCoursesEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoWithCoursesOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags));
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);
            actualResponce.Data.Courses.Count.Should().Be(courses.Count);
            actualResponce.Data.Courses.Intersect(courses).ToList().Count.Should().Be(courses.Count);
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Student)]
        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        public void GetTaskWithTagsAndCourses_ByUnauthorizedRole_403StatusCodeReturned(Role role)
        {
            //Given
            var taskId = 0;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndCoursesEndpoint, taskId), token);

            //When
            var actualResponce = _client.Execute<TaskInfoWithCoursesOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Admin)]
        [TestCase(Role.Methodist)]
        public void GetTaskWithTagsAndCourses_UnexistingId_ExceptionResponce(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskId = 0;
            var expectedException = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndCoursesEndpoint, taskId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Admin)]
        public void GetTaskWithTagsAndAnswers_ByAdmin_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var groupId = _groupFacade.CreateValidGroup(token).Id;
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(token, groupId, tags.Select(tag => tag.Id).ToList());
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndAnswersEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoWithAnswersOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags));
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            actualResponce.Data.Tags.Intersect(tags).ToList().Count.Should().Be(tags.Count);

        }
    }
}