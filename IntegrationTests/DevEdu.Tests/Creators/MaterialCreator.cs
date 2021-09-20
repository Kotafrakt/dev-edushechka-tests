using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class MaterialCreator : BaseCreator
    {
        public MaterialInfoWithGroupsOutputModel AddMaterialWithGroups(string token)
        {
            var model = new MaterialWithGroupsInputModel();
            return new MaterialInfoWithGroupsOutputModel();
        }

        public MaterialInfoWithCoursesOutputModel AddMaterialWithCourses(string token)
        {
            var model = new MaterialWithCoursesInputModel();
            return new MaterialInfoWithCoursesOutputModel();
        }

        public MaterialInfoOutputModel UpdateMaterial(string token, int materialId)
        {
            var model = new MaterialInputModel();
            return new MaterialInfoOutputModel();
        }

        public void AddTagToMaterial(string token, int materialId, int tagId)
        {
        }


        public MaterialInfoOutputModel CreateMaterialCorrect(string token)
        {
            _endPoint = MaterialEndpoints.AddMaterial;
            var postData = MaterialData.GetValidMaterialInputModel();

            var request = _requestHelper.CreatePostRequest(_endPoint, postData, token);

            var response = _client.Execute<MaterialInfoOutputModel>(request);
            var result = response.Data;
            return result;
        }
    }
}