using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class MaterialCreator : BaseCreator
    {
        public MaterialInfoOutputModel CreateMaterialCorrect(string token)
        {
            _endPoint = MaterialPoints.AddMaterialWithCoursesPoint;
            var postData = MaterialData.GetValidMaterialInputModel();

            var request = _requestHelper.CreatePost(_endPoint, postData, token);

            var response = _client.Execute<MaterialInfoOutputModel>(request);
            var result = response.Data;
            return result;
        }
    }
}