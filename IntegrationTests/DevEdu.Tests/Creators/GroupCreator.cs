using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using DevEdu.Core.Requests;

namespace DevEdu.Tests.Creators
{
    public class GroupCreator : BaseCreator
    {
        public GroupOutputModel AddGroup(string token, int courseId)
        {
            _endPoint = GroupEndpoints.AddGroupEndpoint;
            var postData = GroupData.GetValidGroup(courseId);
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, token);
            var response = _client.Execute<GroupOutputModel>(request);
            var result = response.Data;
            return result;
        }

        public GroupInfoOutputModel UpdateGroup(string token, int groupId)
        {
            var model = new GroupInputModel();
            return new GroupInfoOutputModel();
        }

        public GroupOutputBaseModel ChangeGroupStatus(string token, int groupId, GroupStatus statusId)
        {
            return new GroupOutputBaseModel();
        }

        public int AddGroupToLesson(string token, int groupId, int lessonId)
        {
            return 5;
        }

        public int AddGroupMaterialReference(string token, int groupId, int materialId)
        {
            return 5;
        }

        public void AddUserToGroup(string token, int groupId, int userId, Role roleId)
        {
        }
    }
}