using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.Fillings
{
    public class TagFilling : BaseFilling
    {
        public TagOutputModel CreateTagCorrect(string token)
        {
            AuthenticateClient(token);

            _endPoint = AddTagPoint;
            var postData = TagData.GetTagInputModel_Correct();

            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute<TagOutputModel>(request);
            var result = response.Data;

            postData.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.IsDeleted));
            return result;
        }
    }
}