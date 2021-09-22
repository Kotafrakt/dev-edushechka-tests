using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using System;

namespace DevEdu.Tests.Creators
{
    public class GroupCreator : BaseCreator
    {

        public GroupOutputModel AddGroup(string token)
        {
            var model = new GroupInputModel();
            return new GroupOutputModel();
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
            _endPoint = string.Format(GroupEndpoints.AddUserToGroupEndpoint, groupId, userId, Role.Student);
            var request = _requestHelper.CreatePostReferenceRequest(_endPoint, token);

            var actualResponce = _client.Execute(request);
        }

        public GroupFullOutputModel GetGroupById(int groupId, string adminToken)
        {
            _endPoint = string.Format(GroupEndpoints.GetGroupEndpoint, groupId);
            var getRequest = _requestHelper.CreateGetRequest(_endPoint, adminToken);
            var response = _client.Execute<GroupFullOutputModel>(getRequest);
            return response.Data;
        }

        public void DeleteTaskFromGroup(int groupId, string adminToken)
        {
            _endPoint = string.Format(GroupEndpoints.DeleteTaskFromGroupEndpoint, groupId);
            var getRequest = _requestHelper.CreateDeleteRequest(_endPoint, adminToken);
            var response = _client.Execute(getRequest);
        }
    }
}