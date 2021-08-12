using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class GroupCreator : BaseCreator
    {
        public GroupOutputModel CreateValidGroup(string token, int courseId = 0)
        {
            _endPoint = GroupPoints.AddGroupPoint;
            var postData = GroupData.GetValidGroup(courseId);
            var request = _requestHelper.Post(_endPoint, postData);
            request = _requestHelper.Autorize(request, token);

            return _client.Execute<GroupOutputModel>(request).Data;
        }
    }
}