﻿using DevEdu.Core.Enums;
using DevEdu.Core.Models.Material;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace DevEdu.Tests.ControllersTests
{
    public class MaterialControllerTests : BaseControllerTest
    {
        public const string AddMaterialWithGroupsPoint = "api/material/with-groups";
        public const string AddMaterialWithCoursesPoint = "api/material/with-courses";
        public const string GetAllMaterialsPoint = "api/material";
        public const string GetMaterialByIdWithCoursesAndGroupsPoint = "api/material/{0}/full-output-model";
        public const string GetMaterialByIdWithTagsPoint = "api/material/{0}/short-output-model";
        public const string UpdateMaterialPoint = "api/material/{0}";
        public const string DeleteMaterialPoint = "api/material/{0}/isDeleted/{1}";
        public const string AddTagToMaterialPoint = "api/material/{0}/tag/{1}";
        public const string DeleteTagFromMaterialPoint = "api/material/{0}/tag/{1}";
        public const string GetMaterialsByTagIdPoint = "api/material/by-tag/{0}";

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesTeacherAndTutor))]
        public void AddMaterialWithGroups(List<Role> roles)
        {
            var groupsId = new List<int>();
            var countNewGroup = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewGroup; i++)
            {
                var group = _facade.CreateCourseCorrect(token); //To Do
                groupsId.Add(group.Id);
            }

            AuthenticateClient(token);
            _endPoint = AddMaterialWithGroupsPoint;

            var material = MaterialData.GetMaterialWithGroupsInputModel_Correct(groupsId);
            var jsonData = JsonConvert.SerializeObject(material);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoWithGroupsOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.IsDeleted)
                .Excluding(obj => obj.Tags));
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesMethodist))]
        public void AddMaterialWithCourses(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _facade.CreateCourseCorrect(token);
                coursesId.Add(course.Id);
            }

            AuthenticateClient(token);
            _endPoint = AddMaterialWithCoursesPoint;

            var material = MaterialData.GetMaterialWithCoursesInputModel_Correct(coursesId);
            var jsonData = JsonConvert.SerializeObject(material);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoWithCoursesOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.IsDeleted)
                .Excluding(obj => obj.Tags));
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRolesButManager))]
        public void GetAllMaterials(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            AuthenticateClient(token);
            _endPoint = GetAllMaterialsPoint;

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<List<MaterialInfoOutputModel>>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().NotBeNull();
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesMethodist))]
        public void GetMaterialByIdWithCoursesAndGroups(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _facade.CreateCourseCorrect(token);
                coursesId.Add(course.Id);
            }
            var material = _facade.CreateMaterialInfoWithCoursesCorrect(token, coursesId);

            AuthenticateClient(token);
            _endPoint = string.Format(GetMaterialByIdWithCoursesAndGroupsPoint, material.Id);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRolesButManager))]
        public void GetMaterialByIdWithTags(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _facade.CreateCourseCorrect(token);
                coursesId.Add(course.Id);
            }

            var material = _facade.CreateMaterialInfoWithCoursesCorrect(token, coursesId);

            AuthenticateClient(token);
            _endPoint = string.Format(GetMaterialByIdWithTagsPoint, material.Id);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesTeacherAndMethodist))]
        public void UpdateMaterial(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _facade.CreateCourseCorrect(token);
                coursesId.Add(course.Id);
            }

            var material = _facade.CreateMaterialInfoWithCoursesCorrect(token, coursesId);

            AuthenticateClient(token);
            _endPoint = string.Format(UpdateMaterialPoint, material.Id);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesTeacherAndMethodist))]
        public void DeleteMaterial(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _facade.CreateCourseCorrect(token);
                coursesId.Add(course.Id);
            }

            var material = _facade.CreateMaterialInfoWithCoursesCorrect(token, coursesId);

            AuthenticateClient(token);
            _endPoint = string.Format(DeleteMaterialPoint, material.Id, 1);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRoles))]
        public void AddTagToMaterial(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            var course = _facade.CreateCourseCorrect(token);
            var material = _facade.CreateMaterialInfoWithCoursesCorrect(token, new List<int>() { course.Id });
            var tag = _facade.CreateTagCorrect(token);

            AuthenticateClient(token);
            _endPoint = string.Format(AddTagToMaterialPoint, material.Id, tag.Id);

            string jsonData = null;
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRoles))]
        public void DeleteTagFromMaterial(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _facade.CreateCourseCorrect(token);
                coursesId.Add(course.Id);
            }

            var material = _facade.CreateMaterialInfoWithCoursesCorrect(token, coursesId);

            AuthenticateClient(token);
            _endPoint = string.Format(DeleteTagFromMaterialPoint, material.Id, 1);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRolesButManager))]
        public void GetMaterialsByTagId(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _facade.CreateCourseCorrect(token);
                coursesId.Add(course.Id);
            }

            var material = _facade.CreateMaterialInfoWithCoursesCorrect(token, coursesId);

            AuthenticateClient(token);
            _endPoint = string.Format(GetMaterialsByTagIdPoint, material.Id);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result);
        }











    }
}