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
        private StudentHomeworkFacade _studentHomeworkFacade;
        private AuthenticationFacade _authenticationFacade;

        public TaskControllerTests() : base()
        {
            _groupFacade = new GroupFacade();
            _tagFacade = new TagFacade();
            _authenticationFacade = new AuthenticationFacade();
            _courseFacade = new CourseFacade();
            _taskFacade = new TaskFacade();
            _studentHomeworkFacade = new StudentHomeworkFacade();
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
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
            foreach (var error in exception.Errors)
            {
                actualResponce.Data.Errors.First(e => e.Code == error.Code).Should().BeEquivalentTo(error);
            }
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
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
            foreach (var error in exception.Errors)
            {
                actualResponce.Data.Errors.First(e => e.Code == error.Code).Should().BeEquivalentTo(error);
            }
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
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
            actualResponce.Data.Should().BeEquivalentTo(expectedExeption);
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
            foreach (var error in expectedExeption.Errors)
            {
                actualResponce.Data.Errors.First(e => e.Code == error.Code).Should().BeEquivalentTo(error);
            }
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
            actualResponce.Data.Should().BeEquivalentTo(expectedExeption);
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
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
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
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
            foreach (var error in expectedException.Errors)
            {
                actualResponce.Data.Errors.First(e => e.Code == error.Code).Should().BeEquivalentTo(error);
            }
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]
        public void UpdateTaskByTeacher_PutUnexistingId_ReturnedExceptionResponse(Role role)
        {
            //Given
            var taskId = 0;
            var task = TaskData.GetValidTaskByTeacherUpdateInputModel();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var expectedException = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePutRequest(string.Format(TaskEndpoints.UpdateTaskByTeacherEndpoint, taskId), task, token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
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
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
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
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
            actualResponce.Data.Courses.Count.Should().Be(courses.Count);
            foreach (var course in courses)
            {
                actualResponce.Data.Courses.First(c => c.Id == course.Id).Should().BeEquivalentTo(course);
            }
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
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
            actualResponce.Data.Courses.Count.Should().Be(courses.Count);
            foreach (var course in courses)
            {
                actualResponce.Data.Courses.First(c => c.Id == course.Id).Should().BeEquivalentTo(course);
            }
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
            var request = _requestHelper.CreateGetRequest(string.Format(HomeworkEndpoints.GetHomeworkByTaskIdEndpoint, task.Id), token);
            var homework = _client.Execute<List<HomeworkInfoWithGroupOutputModel>>(request).Data[0];
            var answers = _studentHomeworkFacade.CreateListOfStudentAnswersHomework(homework.Id, groupId)
                .Select(answer => (StudentHomeworkOutputModel)answer)
                .ToList();
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndAnswersEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoWithAnswersOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags));
            actualResponce.Data.StudentAnswers.Count.Should().Be(answers.Count);
            foreach(var answer in answers)
            {
                actualResponce.Data.StudentAnswers.First(a => a.Id == answer.Id).Should()
                    .BeEquivalentTo(answer);
            }
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        public void GetTaskWithTagsAndAnswers_ByAuthorizedRoles_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var groupId = _groupFacade.CreateValidGroup(token).Id;
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _groupFacade.AddUserToGroup(token, groupId, user.Id, role);
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(token, groupId, tags.Select(tag => tag.Id).ToList());
            var request = _requestHelper.CreateGetRequest(string.Format(HomeworkEndpoints.GetHomeworkByTaskIdEndpoint, task.Id), token);
            var homework = _client.Execute<List<HomeworkInfoWithGroupOutputModel>>(request).Data[0];
            var answers = _studentHomeworkFacade.CreateListOfStudentAnswersHomework(homework.Id, groupId)
                .Select(answer => (StudentHomeworkOutputModel)answer)
                .ToList(); token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndAnswersEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoWithAnswersOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags));
            actualResponce.Data.StudentAnswers.Count.Should().Be(answers.Count);
            foreach (var answer in answers)
            {
                actualResponce.Data.StudentAnswers.First(a => a.Id == answer.Id).Should()
                    .BeEquivalentTo(answer);
            }
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        public void GetTaskWithTagsAndAnswers_ByUnauthorizedToGroupUsers_403StatusCodeReturned(Role role)
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
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndAnswersEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        public void GetTaskWithTagsAndAnswers_ByUnauthorizedRoles_403StatusCodeReturned(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskId = 0;
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndAnswersEndpoint, taskId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Admin)]
        public void GetTaskWithTagsAndAnswers_PutUnexistingId_ExceptionResponce(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var taskId = 0;
            var expectedException = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndAnswersEndpoint, taskId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Admin)]
        public void GetTaskWithTagsAndGroups_ByAdmin_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var group = _groupFacade.CreateValidGroup(token);
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(token, group.Id, tags.Select(tag => tag.Id).ToList());
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndGroupsEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoWithGroupsOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags));
            actualResponce.Data.Groups.Count.Should().Be(1);
            actualResponce.Data.Groups[0].Should().BeEquivalentTo(group, option => option
           .ExcludingMissingMembers());
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
        }

        [TestCase(Role.Teacher)]
        public void GetTaskWithTagsAndGroups_ByTeacher_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var group = _groupFacade.CreateValidGroup(token);
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _groupFacade.AddUserToGroup(token, group.Id, user.Id, role);
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(token, group.Id, tags.Select(tag => tag.Id).ToList());
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndGroupsEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<TaskInfoWithGroupsOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponce.Data.Should().BeEquivalentTo(task, option => option
            .Excluding(o => o.Tags));
            actualResponce.Data.Groups.Count.Should().Be(1);
            actualResponce.Data.Groups[0].Should().BeEquivalentTo(group, option => option
           .ExcludingMissingMembers());
            actualResponce.Data.Tags.Count.Should().Be(tags.Count);
            foreach (var tag in tags)
            {
                actualResponce.Data.Tags.First(t => t.Id == tag.Id).Should().BeEquivalentTo(tag);
            }
        }

        [TestCase(Role.Tutor)]
        [TestCase(Role.Manager)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        public void GetTaskWithTagsAndGroups_ByUnauthorizedRole_403StatusCodeReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var taskId = 0;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndGroupsEndpoint, taskId), token);

            //When
            var actualResponce = _client.Execute<TaskInfoWithGroupsOutputModel>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Teacher)]
        public void GetTaskWithTagsAndGroups_UnauthorizedByGroup_403StatusCodeReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var group = _groupFacade.CreateValidGroup(token);
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(token, group.Id, tags.Select(tag => tag.Id).ToList());
            var expectedException = BaseData.GetAuthorizationExceptionResponce(string.Format(
                ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", task.Id));
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndGroupsEndpoint, task.Id), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]
        public void GetTaskWithTagsAndGroups_PutUnexistingId_ExceptionResponceReturned(Role role)
        {
            //Given
            var taskId = 0;
            var expectedException = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsAndGroupsEndpoint, taskId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Admin)]
        public void GetAllTasksWithTags_ByAdmin_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var tasks = new List<TaskInfoOutputModel>();
            for (int i = 0; i < 3; i++)
            {
                tasks.Add(_taskFacade.CreateValidTaskByMethodist(token, tagIds: tags.Select(tag => tag.Id).ToList()));
            }
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(TaskEndpoints.GetAllTasksWithTagsEndpoint, token);

            //When
            var actualResponce = _client.Execute<List<TaskInfoOutputModel>>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            foreach (var task in tasks)
            {
                actualResponce.Data.First(t => t.Id == task.Id).Should().BeEquivalentTo(task);
            }
        }

        [TestCase(Role.Methodist)]
        public void GetAllTasksWithTags_ByMethodist_TaskReturned(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(token);
            var course = _courseFacade.CreateCourse(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var tasks = new List<TaskInfoOutputModel>();
            for (int i = 0; i < 3; i++)
            {
                tasks.Add(_taskFacade.CreateValidTaskByMethodist(token, new List<int> { course.Id }, tags.Select(tag => tag.Id).ToList()));
            }
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(TaskEndpoints.GetAllTasksWithTagsEndpoint, token);

            //When
            var actualResponce = _client.Execute<List<TaskInfoOutputModel>>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            foreach (var task in tasks)
            {
                actualResponce.Data.First(t => t.Id == task.Id).Should().BeEquivalentTo(task);
            }
        }

        [TestCase(Role.Methodist)]
        public void GetAllTasksWithTags_MethodistTryGetTaskThatNotLinkedToCourse_ReturnedListOfTasksWithoutUnexpected(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var tasks = new List<TaskInfoOutputModel>();
            for (int i = 0; i < 3; i++)
            {
                tasks.Add(_taskFacade.CreateValidTaskByMethodist(token, tagIds: tags.Select(tag => tag.Id).ToList()));
            }
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(TaskEndpoints.GetAllTasksWithTagsEndpoint, token);

            //When
            var actualResponce = _client.Execute<List<TaskInfoOutputModel>>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            foreach (var task in tasks)
            {
                actualResponce.Data.FirstOrDefault(t => t.Id == task.Id).Should().Be(default);
            }
        }

        [TestCase(Role.Manager)]
        public void GetAllTasksWithTags_ByUnauthrizedRole_403StatusCodeReturned(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateGetRequest(TaskEndpoints.GetAllTasksWithTagsEndpoint, token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Admin)]
        public void DeleteTask_ByAdmin_TaskDeleted(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(adminToken);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var tasks = new List<TaskInfoOutputModel>();
            for (int i = 0; i < 2; i++)
            {
                tasks.Add(_taskFacade.CreateValidTaskByMethodist(adminToken, tagIds: tags.Select(tag => tag.Id).ToList()));
            }
            var taskToDelete = _taskFacade.CreateValidTaskByMethodist(adminToken, tagIds: tags.Select(tag => tag.Id).ToList());
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(string.Format(TaskEndpoints.DeleteTaskEndpoint, taskToDelete.Id), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetAllTasksWithTagsEndpoint), adminToken);
            var responce = _client.Execute<List<TaskInfoOutputModel>>(request);
            foreach (var task in tasks)
            {
                responce.Data.First(t => t.Id == task.Id).Should().BeEquivalentTo(task);
            }
            responce.Data.FirstOrDefault(t => t.Id == taskToDelete.Id).Should().Be(default);
        }

        [TestCase(Role.Methodist)]
        public void DeleteTask_ByMethodist_TaskDeleted(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(adminToken);
            var course = _courseFacade.CreateCourse(adminToken);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var tasks = new List<TaskInfoOutputModel>();
            for (int i = 0; i < 2; i++)
            {
                tasks.Add(_taskFacade.CreateValidTaskByMethodist(
                    adminToken,
                    new List<int> { course.Id },
                    tags.Select(tag => tag.Id).ToList()));
            }
            var taskToDelete = _taskFacade.CreateValidTaskByMethodist(
                adminToken,
                new List<int> { course.Id },
                tags.Select(tag => tag.Id).ToList());
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(string.Format(TaskEndpoints.DeleteTaskEndpoint, taskToDelete.Id), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetAllTasksWithTagsEndpoint), adminToken);
            var responce = _client.Execute<List<TaskInfoOutputModel>>(request);
            foreach (var task in tasks)
            {
                responce.Data.First(t => t.Id == task.Id).Should().BeEquivalentTo(task);
            }
            responce.Data.FirstOrDefault(t => t.Id == taskToDelete.Id).Should().Be(default);
        }

        [TestCase(Role.Teacher)]
        public void DeleteTask_ByTeacher_TaskDeleted(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(adminToken);
            var groupId = _groupFacade.CreateValidGroup(adminToken).Id;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _groupFacade.AddUserToGroup(adminToken, groupId, user.Id, role);
            var tasks = new List<TaskInfoOutputModel>();
            for (int i = 0; i < 2; i++)
            {
                tasks.Add(_taskFacade.CreateValidTaskByTeacherWithHomework(
                    adminToken,
                    groupId,
                    tags.Select(tag => tag.Id).ToList()));
            }
            var taskToDelete = _taskFacade.CreateValidTaskByTeacherWithHomework(
                    adminToken,
                    groupId,
                    tags.Select(tag => tag.Id).ToList());
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(string.Format(TaskEndpoints.DeleteTaskEndpoint, taskToDelete.Id), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetAllTasksWithTagsEndpoint), adminToken);
            var responce = _client.Execute<List<TaskInfoOutputModel>>(request);
            foreach (var task in tasks)
            {
                responce.Data.First(t => t.Id == task.Id).Should().BeEquivalentTo(task);
            }
            responce.Data.FirstOrDefault(t => t.Id == taskToDelete.Id).Should().Be(default);
        }

        [TestCase(Role.Methodist)]
        public void DeleteTask_MethodistTryDeleteTaskThatNotLinkedToCourse_403StatusCodeReturned(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(adminToken);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskToDelete = _taskFacade.CreateValidTaskByMethodist(adminToken, tagIds: tags.Select(tag => tag.Id).ToList());
            var expectedException = BaseData.GetAuthorizationExceptionResponce(
                string.Format(ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", taskToDelete.Id));
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(string.Format(TaskEndpoints.DeleteTaskEndpoint, taskToDelete.Id), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Teacher)]
        public void DeleteTask_ByTeacherUnauthorizedToGroup_403StatusCodeReturned(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(adminToken);
            var groupId = _groupFacade.CreateValidGroup(adminToken).Id;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskToDelete = _taskFacade.CreateValidTaskByTeacherWithHomework(adminToken, groupId, tags.Select(tag => tag.Id).ToList());
            var expectedException = BaseData.GetAuthorizationExceptionResponce(
                string.Format(ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", taskToDelete.Id));
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(string.Format(TaskEndpoints.DeleteTaskEndpoint, taskToDelete.Id), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        public void DeleteTask_ByUnauthorizedRole_403StatusCodeReturned(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskId = 0;
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(string.Format(TaskEndpoints.DeleteTaskEndpoint, taskId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Admin)]
        [TestCase(Role.Teacher)]
        [TestCase(Role.Methodist)]
        public void DeleteTask_PutUnexistingId_ExceptionResponceReturned(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskId = 0;
            var expectedException = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(string.Format(TaskEndpoints.DeleteTaskEndpoint, taskId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Admin)]
        public void AddTagToTask_ByAdmin_TagAddedToTask(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tag = _tagFacade.AddTag(adminToken);
            var groupId = _groupFacade.CreateValidGroup(adminToken).Id;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(adminToken, groupId);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePostReferenceRequest(string.Format(TaskEndpoints.AddTagToTaskEndpoint, task.Id, tag.Id), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), adminToken);
            var responce = _client.Execute<TaskInfoOutputModel>(request);
            responce.Data.Tags.Count.Should().Be(1);
            responce.Data.Tags[0].Should().BeEquivalentTo(tag);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        public void AddTagToTask_UserAuthorizedByGroup_TagAddedToTask(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tag = _tagFacade.AddTag(adminToken);
            var groupId = _groupFacade.CreateValidGroup(adminToken).Id;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _groupFacade.AddUserToGroup(adminToken, groupId, user.Id, role);
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(adminToken, groupId);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePostReferenceRequest(string.Format(TaskEndpoints.AddTagToTaskEndpoint, task.Id, tag.Id), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), adminToken);
            var responce = _client.Execute<TaskInfoOutputModel>(request);
            responce.Data.Tags.Count.Should().Be(1);
            responce.Data.Tags[0].Should().BeEquivalentTo(tag);
        }

        [TestCase(Role.Methodist)]
        public void AddTagToTask_ByMethodist_TagAddedToTask(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tag = _tagFacade.AddTag(adminToken);
            var courseIds = _courseFacade.CreateListOfCourses(adminToken).Select(c => c.Id).ToList();
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByMethodist(adminToken, courseIds);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePostReferenceRequest(string.Format(TaskEndpoints.AddTagToTaskEndpoint, task.Id, tag.Id), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), adminToken);
            var responce = _client.Execute<TaskInfoOutputModel>(request);
            responce.Data.Tags.Count.Should().Be(1);
            responce.Data.Tags[0].Should().BeEquivalentTo(tag);
        }

        [TestCase(Role.Methodist)]
        public void AddTagToTask_MethodistTryAddTagToTaskThatNotLinkedToCourse_403StatusCodeReturned(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tag = _tagFacade.AddTag(adminToken);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByMethodist(adminToken);
            var expectedException = BaseData.GetAuthorizationExceptionResponce(
                string.Format(ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", task.Id));
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePostReferenceRequest(string.Format(TaskEndpoints.AddTagToTaskEndpoint, task.Id, tag.Id), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        public void AddTagToTask_UserUnauthorizedByGroup_403StatusCodeReturned(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tag = _tagFacade.AddTag(adminToken);
            var groupId = _groupFacade.CreateValidGroup(adminToken).Id;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(adminToken, groupId);
            var expectedException = BaseData.GetAuthorizationExceptionResponce(
                string.Format(ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", task.Id));
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePostReferenceRequest(string.Format(TaskEndpoints.AddTagToTaskEndpoint, task.Id, tag.Id), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Student)]
        public void AddTagToTask_UnauthorizedRole_403StatusCodeReturned(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskId = 0;
            var tagId = 0;
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePostReferenceRequest(string.Format(TaskEndpoints.AddTagToTaskEndpoint, taskId, tagId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Admin)]
        [TestCase(Role.Teacher)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Tutor)]
        public void AddTagToTask_PutUnexistingId_ExceptionResponceReturned(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskId = 0;
            var tagId = 0;
            var expectedException = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreatePostReferenceRequest(string.Format(TaskEndpoints.AddTagToTaskEndpoint, taskId, tagId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Admin)]
        public void DeleteTagFromTask_ByAdmin_TagDeletedFromTask(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(adminToken);
            var groupId = _groupFacade.CreateValidGroup(adminToken).Id;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(adminToken, groupId, tags.Select(t => t.Id).ToList());
            var tagToDelete = tags[0];
            tags.Remove(tagToDelete);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(
                string.Format(TaskEndpoints.DeleteTagFromTaskEndpoint, task.Id, tagToDelete.Id), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), adminToken);
            var responce = _client.Execute<TaskInfoOutputModel>(request);
            responce.Data.Tags.Should().BeEquivalentTo(tags);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        public void DeleteTagFromTask_UserAuthorizedByGroup_TagDeletedFromTask(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(adminToken);
            var groupId = _groupFacade.CreateValidGroup(adminToken).Id;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _groupFacade.AddUserToGroup(adminToken, groupId, user.Id, role);
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(adminToken, groupId, tags.Select(t => t.Id).ToList());
            var tagToDelete = tags[0];
            tags.Remove(tagToDelete);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(
                string.Format(TaskEndpoints.DeleteTagFromTaskEndpoint, task.Id, tagToDelete.Id), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), adminToken);
            var responce = _client.Execute<TaskInfoOutputModel>(request);
            responce.Data.Tags.Should().BeEquivalentTo(tags);
        }

        [TestCase(Role.Methodist)]
        public void DeleteTagFromTask_ByMethodist_TagDeletedFromTask(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var course = _courseFacade.CreateCourse(adminToken);
            var tags = _tagFacade.AddValidTagList(adminToken);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByMethodist(adminToken, new List<int> { course.Id }, tags.Select(t => t.Id).ToList());
            var tagToDelete = tags[0];
            tags.Remove(tagToDelete);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(
                string.Format(TaskEndpoints.DeleteTagFromTaskEndpoint, task.Id, tagToDelete.Id), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NoContent);
            request = _requestHelper.CreateGetRequest(string.Format(TaskEndpoints.GetTaskWithTagsEndpoint, task.Id), adminToken);
            var responce = _client.Execute<TaskInfoOutputModel>(request);
            responce.Data.Tags.Should().BeEquivalentTo(tags);
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        public void DeleteTagFromTask_UserUnauthorizedByGroup_403StatusCodeReturned(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(adminToken);
            var groupId = _groupFacade.CreateValidGroup(adminToken).Id;
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByTeacherWithHomework(adminToken, groupId, tags.Select(t => t.Id).ToList());
            var tagToDelete = tags[0];
            var expectedException = BaseData.GetAuthorizationExceptionResponce(
                string.Format(ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", task.Id));
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(
                string.Format(TaskEndpoints.DeleteTagFromTaskEndpoint, task.Id, tagToDelete.Id), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Methodist)]
        public void DeleteTagFromTask_MethodistTryDeleteTagFromTaskThatNotLinkedToCourse_403StatusCodeReturned(Role role)
        {
            //Given
            var adminToken = _authenticationFacade.SignInByAdmin();
            var tags = _tagFacade.AddValidTagList(adminToken);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var task = _taskFacade.CreateValidTaskByMethodist(adminToken, tagIds: tags.Select(t => t.Id).ToList());
            var tagToDelete = tags[0];
            var expectedException = BaseData.GetAuthorizationExceptionResponce(
                string.Format(ServiceMessages.EntityDoesntHaveAcessMessage, "user", user.Id, "task", task.Id));
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(
                string.Format(TaskEndpoints.DeleteTagFromTaskEndpoint, task.Id, tagToDelete.Id), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }

        [TestCase(Role.Student)]
        [TestCase(Role.Manager)]
        public void DeleteTagFromTask_ByUnauthorizedRole_403StatusCodeReturned(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskId = 0;
            var tagId = 0;
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(
                string.Format(TaskEndpoints.DeleteTagFromTaskEndpoint, taskId, tagId), token);

            //When
            var actualResponce = _client.Execute(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [TestCase(Role.Admin)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Teacher)]
        [TestCase(Role.Tutor)]
        public void DeleteTagFromTask_PutUnexistingId_ExceptionResponceReturned(Role role)
        {
            //Given
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var taskId = 0;
            var tagId = 0;
            var expectedException = BaseData.GetEntityNotFoundExceptionResponse("task", taskId);
            var token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var request = _requestHelper.CreateDeleteRequest(
                string.Format(TaskEndpoints.DeleteTagFromTaskEndpoint, taskId, tagId), token);

            //When
            var actualResponce = _client.Execute<ExceptionResponse>(request);

            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actualResponce.Data.Should().BeEquivalentTo(expectedException);
        }
    }
}