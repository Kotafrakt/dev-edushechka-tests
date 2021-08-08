using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.Fillings
{
    public class MaterialFilling : BaseFilling
    {
        public MaterialInfoOutputModel CreateMaterialCorrect(string token)
        {
            AuthenticateClient(token);

            _endPoint = AddMaterialPoint;
            var postData = MaterialData.GetMaterialInputModel_Correct();

            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute<MaterialInfoOutputModel>(request);
            var result = response.Data;

            //postData.Should().BeEquivalentTo(result, options => options
            //    .Excluding(obj => obj.Id)
            //    .Excluding(obj => obj.IsDeleted)
            //    .Excluding(obj => obj.Tags));

            return result;
        }
    }
}