using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Creators;
using DevEdu.Tests.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using DevEdu.Tests.Facades;

namespace DevEdu.Tests.ControllersTests
{
    public class MaterialControllerTests : BaseControllerTest
    {
        private CourseCreator _courseCreator = new();
        private GroupCreator _groupCreator = new();
        private TagCreator _tagCreator = new();
        private MaterialCreator _materialCreator = new();
        private readonly AuthenticationFacade _authenticationFacade = new();

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesTeacherAndTutor))]
        public void AddMaterialWithGroups_MaterialDtoWithoutGroups_MaterialCreated(List<Role> roles)
        {
            var groupsId = new List<int>();
            var countNewGroup = 5;
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);
            for (int i = 0; i <= countNewGroup; i++)
            {
                var group = new GroupInfoOutputModel();// _groupCreator.CreateValidGroup(userInfo.Token); 
                groupsId.Add(group.Id);
            }

            _endPoint = MaterialEndpoints.AddMaterialWithGroupsEndpoint;

            var material = MaterialData.GetMaterialWithGroupsInputModel_Correct(groupsId);
            var request = _requestHelper.CreatePostRequest(_endPoint, material, userInfo.Token);

            var response = _client.Execute<MaterialInfoWithGroupsOutputModel>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
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
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _courseCreator.AddCourse(userInfo.Token);
                coursesId.Add(course.Id);
            }

            _endPoint = MaterialEndpoints.AddMaterialWithCoursesEndpoint;

            var material = MaterialData.GetMaterialWithCoursesInputModelForFillingDB(coursesId);
            var request = _requestHelper.CreatePostRequest(_endPoint, material, userInfo.Token);

            var response = _client.Execute<MaterialInfoWithCoursesOutputModel>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            material.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.IsDeleted)
                .Excluding(obj => obj.Tags));
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRolesButManager))]
        public void GetAllMaterials_NoEntryForAllRolesButManager_ListOfMaterialDtoReturned(List<Role> roles)
        {
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);

            _endPoint = MaterialEndpoints.GetAllMaterialsEndpoint;

            var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);

            var response = _client.Execute<List<MaterialInfoOutputModel>>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            result.Should().NotBeNull();
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesMethodist))]
        public void GetMaterialByIdWithCoursesAndGroups_ExistingMaterialId_MaterialDtoWithCoursesAndGroupsReturned(List<Role> roles)
        {
            var coursesId = new List<int>();
            var countNewCourse = 5;
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);
            for (int i = 0; i <= countNewCourse; i++)
            {
                var course = _courseCreator.AddCourse(userInfo.Token);
                coursesId.Add(course.Id);
            }
            var material = new MaterialInfoFullOutputModel();// _materialCreator.CreateMaterialInfoWithCourses(userInfo.Token, coursesId);

            _endPoint = string.Format(MaterialEndpoints.GetMaterialByIdWithCoursesAndGroupsEndpoint, material.Id);

            var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);

            var response = _client.Execute<MaterialInfoOutputModel>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            result.Should().BeEquivalentTo(material);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRolesButManager))]
        public void GetMaterialByIdWithTags_ExistingMaterialIdAccessibleForAllRolesButManagerByGroups_MaterialDtoWithTagsReturned(List<Role> roles)
        {
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);
            var course = _courseCreator.AddCourse(userInfo.Token);
            var material = new MaterialInfoOutputModel();// _materialCreator.CreateMaterialInfoWithCourses(userInfo.Token, new List<int>() { course.Id });
            var tag = new TagOutputModel(); //_tagCreator.CreateTag(userInfo.Token); //To Do
            //_materialCreator.AddTagToMaterial(userInfo.Token, material.Id, tag.Id);
            material.Tags.Add(tag);

            _endPoint = string.Format(MaterialEndpoints.GetMaterialByIdWithTagsEndpoint, material.Id);

            var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);

            var response = _client.Execute<MaterialInfoOutputModel>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            result.Should().BeEquivalentTo(material);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesTeacherAndMethodist))]
        public void UpdateMaterial_ByIdWithUpdateModelMaterial_UpdatedMaterialReturned(List<Role> roles)
        {
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);
            var course = _courseCreator.AddCourse(userInfo.Token);
            var material = new MaterialInfoOutputModel();// _materialCreator.CreateMaterialInfoWithCourses(userInfo.Token, new List<int>() { course.Id });

            _endPoint = string.Format(MaterialEndpoints.UpdateMaterialEndpoint, material.Id);
            var updateMaterial = MaterialData.GetUpdateMaterialInputModel();


            var request = _requestHelper.CreatePutRequest(_endPoint, updateMaterial, userInfo.Token);

            var response = _client.Execute<MaterialInfoOutputModel>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            result.Should().BeEquivalentTo(updateMaterial);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByRolesTeacherAndMethodist))]
        public void DeleteTagFromMaterial_WithMaterialId_SoftDeleted(List<Role> roles)
        {
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);
            var course = _courseCreator.AddCourse(userInfo.Token);
            var material = new MaterialInfoOutputModel();// _facade.CreateMaterialInfoWithCourses(userInfo.Token, new List<int>() { course.Id });
            var isDeleted = true;

            _endPoint = string.Format(MaterialEndpoints.DeleteMaterialEndpoint, material.Id, isDeleted);

            var request = _requestHelper.CreateDeleteRequest(_endPoint, userInfo.Token);

            var response = _client.Execute(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRoles))]
        public void AddTagToMaterial_WithMaterialIdAndTopicId_Added(List<Role> roles)
        {
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);
            var course = new CourseInfoFullOutputModel();// _courseCreator.CreateCourse(userInfo.Token);
            var material = new MaterialInfoOutputModel();//_facade.CreateMaterialInfoWithCourses(userInfo.Token, new List<int>() { course.Id });
            var tag = new TagOutputModel();// _tagCreator.CreateTag(userInfo.Token); //To Do
            var expected = $"Tag id: {tag.Id} added for material id: {material.Id}";

            _endPoint = string.Format(MaterialEndpoints.AddTagToMaterialEndpoint, material.Id, tag.Id);

            string data = null;

            var request = _requestHelper.CreatePostRequest(_endPoint, data, userInfo.Token);

            var response = _client.Execute<string>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            result.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRoles))]
        public void DeleteTagFromMaterial_WithMaterialIdAndTopicId_SoftDeleted(List<Role> roles)
        {
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);
            var course = new CourseInfoFullOutputModel();// _facade.CreateCourse(userInfo.Token);
            var material = new MaterialInfoOutputModel();// _facade.CreateMaterialInfoWithCourses(userInfo.Token, new List<int>() { course.Id });
            var tag = new TagOutputModel();// _facade.CreateTag(userInfo.Token); //To Do
            //_facade.AddTagToMaterial(userInfo.Token, material.Id, tag.Id);
            material.Tags.Add(tag);
            var expected = $"Tag id: {tag.Id} deleted from material id: {material.Id}";

            _endPoint = string.Format(MaterialEndpoints.DeleteTagFromMaterialEndpoint, material.Id, tag.Id);

            var request = _requestHelper.CreateDeleteRequest(_endPoint, userInfo.Token);

            var response = _client.Execute<string>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            expected.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(typeof(MaterialData), nameof(MaterialData.СheckByAllRolesButManager))]
        public void GetMaterialsByTagId_ExistingTagIdAccessibleForAllRolesButManager_ListOfMaterialDtoReturned(List<Role> roles)
        {
            var materials = new List<MaterialInfoWithCoursesOutputModel>();
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(roles);
            var course = _courseCreator.AddCourse(userInfo.Token);
            var material = new MaterialInfoWithCoursesOutputModel();// _facade.CreateMaterialInfoWithCourses(userInfo.Token, new List<int>() { course.Id });
            var tag = new TagOutputModel();// _facade.CreateTag(userInfo.Token); //To Do
            //_facade.AddTagToMaterial(userInfo.Token, material.Id, tag.Id);
            material.Tags.Add(tag);
            materials.Add(material);

            _endPoint = string.Format(MaterialEndpoints.GetMaterialsByTagIdEndpoint, tag.Id);

            var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);

            var response = _client.Execute<List<MaterialInfoOutputModel>>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;

            result.Should().BeEquivalentTo(materials);
        }
    }
}