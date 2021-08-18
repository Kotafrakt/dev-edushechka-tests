using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class TagCreator : BaseCreator
    {
        public TagOutputModel AddTag(string token)
        {
            _endPoint = TagPoints.AddTagPoint;
            var postData = TagData.GetValidTagInputModel();
            var request = _requestHelper.CreatePost(_endPoint, postData, token);
            var response = _client.Execute<TagOutputModel>(request);
            var result = response.Data;
            return result;
        }
    }
}