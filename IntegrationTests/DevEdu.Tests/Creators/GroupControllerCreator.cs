using DevEdu.Core.Enums;
using DevEdu.Core.Models;

namespace DevEdu.Tests.Creators
{
    public class GroupControllerCreator
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
        }
    }
}