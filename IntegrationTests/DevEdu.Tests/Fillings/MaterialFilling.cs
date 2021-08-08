using DevEdu.Core.Models.Material;
using DevEdu.Core.Requests;
using DevEdu.Tests.ControllersTests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace DevEdu.Tests.Fillings
{
    public class MaterialFilling : BaseFilling
    {
        public MaterialInfoWithCoursesOutputModel CreateMaterialInfoWithCourses(string token, List<int> coursesId)
        {
            AuthenticateClient(token);

            _endPoint = MaterialControllerTests.AddMaterialWithCoursesPoint;

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
            return result;
        }

        public void AddTagToMaterial(string token, int materialId, int tagId)
        {
            var expected = $"Tag id: {tagId} added for material id: {materialId}";
            AuthenticateClient(token);

            _endPoint = string.Format(MaterialControllerTests.AddTagToMaterialPoint, materialId, tagId);
            string jsonData = null;

            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute(request);
            var result = JsonConvert.DeserializeObject<string>(response.Content);

            expected.Should().Be(result);            
        }
    }
}