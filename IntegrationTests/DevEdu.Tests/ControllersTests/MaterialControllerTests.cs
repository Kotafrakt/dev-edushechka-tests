using DevEdu.Core.Enums;
using DevEdu.Core.Models.Material;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.ControllersTests
{
    public class MaterialControllerTests : BaseControllerTest
    {
        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesTeacherAndTutor))]
        public void AddMaterialWithGroups_MaterialDtoWithoutGroups_MaterialCreated(List<Role> roles)
        {
            var groupsId = new List<int>();
            var countNewGroup = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewGroup; i++)
            {
                var group = _facade.CreateCourse(token); //To Do
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
        public void AddMaterialWithCourses_MaterialDtoWithoutCourse_MaterialCreated(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _facade.CreateCourse(token);
                coursesId.Add(course.Id);
            }

            AuthenticateClient(token);
            _endPoint = AddMaterialWithCoursesPoint;

            var material = MaterialData.GetMaterialWithCoursesInputModelForFillingDB(coursesId);
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
        public void GetAllMaterials_NoEntryForAllRolesButManager_ListOfMaterialDtoReturned(List<Role> roles)
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
        public void GetMaterialByIdWithCoursesAndGroups_ExistingMaterialId_MaterialDtoWithCoursesAndGroupsReturned(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _facade.CreateCourse(token);
                coursesId.Add(course.Id);
            }
            var material = _facade.CreateMaterialInfoWithCourses(token, coursesId);

            AuthenticateClient(token);
            _endPoint = string.Format(GetMaterialByIdWithCoursesAndGroupsPoint, material.Id);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRolesButManager))]
        public void GetMaterialByIdWithTags_ExistingMaterialIdAccessibleForAllRolesButManagerByGroups_MaterialDtoWithTagsReturned(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            var course = _facade.CreateCourse(token);
            var material = _facade.CreateMaterialInfoWithCourses(token, new List<int>() { course.Id });
            var tag = _facade.CreateTag(token); //To Do
            _facade.AddTagToMaterial(token, material.Id, tag.Id);
            material.Tags.Add(tag);

            AuthenticateClient(token);
            _endPoint = string.Format(GetMaterialByIdWithTagsPoint, material.Id);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            material.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesTeacherAndMethodist))]
        public void UpdateMaterial_ByIdWithUpdateModelMaterial_UpdatedMaterialReturned(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            var course = _facade.CreateCourse(token);
            var material = _facade.CreateMaterialInfoWithCourses(token, new List<int>() { course.Id });

            AuthenticateClient(token);
            _endPoint = string.Format(UpdateMaterialPoint, material.Id);

            var updateMaterial = MaterialData.GetUpdateMaterialInputModel();
            var jsonData = JsonConvert.SerializeObject(updateMaterial);
            var request = _requestHelper.Put(_endPoint, _headers, jsonData);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<MaterialInfoOutputModel>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            updateMaterial.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesTeacherAndMethodist))]
        public void DeleteTagFromMaterial_WithMaterialId_SoftDeleted(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            var course = _facade.CreateCourse(token);
            var material = _facade.CreateMaterialInfoWithCourses(token, new List<int>() { course.Id });
            var isDeleted = true;

            AuthenticateClient(token);
            _endPoint = string.Format(DeleteMaterialPoint, material.Id, isDeleted);

            var request = _requestHelper.Delete(_endPoint, _headers);
            var response = _client.Execute(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRoles))]
        public void AddTagToMaterial_WithMaterialIdAndTopicId_Added(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            var course = _facade.CreateCourse(token);
            var material = _facade.CreateMaterialInfoWithCourses(token, new List<int>() { course.Id });
            var tag = _facade.CreateTag(token); //To Do
            var expected = $"Tag id: {tag.Id} added for material id: {material.Id}";

            AuthenticateClient(token);
            _endPoint = string.Format(AddTagToMaterialPoint, material.Id, tag.Id);

            string jsonData = null;
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<string>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            expected.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRoles))]
        public void DeleteTagFromMaterial_WithMaterialIdAndTopicId_SoftDeleted(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            var course = _facade.CreateCourse(token);
            var material = _facade.CreateMaterialInfoWithCourses(token, new List<int>() { course.Id });
            var tag = _facade.CreateTag(token); //To Do
            _facade.AddTagToMaterial(token, material.Id, tag.Id);
            material.Tags.Add(tag);
            var expected = $"Tag id: {tag.Id} deleted from material id: {material.Id}";

            AuthenticateClient(token);
            _endPoint = string.Format(DeleteTagFromMaterialPoint, material.Id, tag.Id);

            var request = _requestHelper.Delete(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<string>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            expected.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRolesButManager))]
        public void GetMaterialsByTagId_ExistingTagIdAccessibleForAllRolesButManager_ListOfMaterialDtoReturned(List<Role> roles)
        {
            var materials = new List<MaterialInfoWithCoursesOutputModel>();
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);
            var course = _facade.CreateCourse(token);
            var material = _facade.CreateMaterialInfoWithCourses(token, new List<int>() { course.Id });
            var tag = _facade.CreateTag(token); //To Do
            _facade.AddTagToMaterial(token, material.Id, tag.Id);
            material.Tags.Add(tag);
            materials.Add(material);

            AuthenticateClient(token);
            _endPoint = string.Format(GetMaterialsByTagIdPoint, tag.Id);

            var request = _requestHelper.Get(_endPoint, _headers);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<List<MaterialInfoOutputModel>>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            materials.Should().BeEquivalentTo(result);
        }
    }
}