using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class GroupFacade
    {
        private readonly GroupCreator _creator;
        public GroupFacade() { _creator = new GroupCreator(); }

        public void AddUserToGroup(string token, int groupId, int userId, Role roleId)
        {
            _creator.AddUserToGroup(token, groupId, userId, roleId);
        }

        public GroupFullOutputModel GetGroupById(int groupId, string adminToken)
        {
            var response = _creator.GetGroupById(groupId, adminToken);
        public GroupOutputModel CreateValidGroup(string token, int courseId = 0)
        {
            if (courseId == 0)
            {
                var courseFacade = new CourseFacade();
                courseId = courseFacade.CreateCourse(token).Id;
            }
            return _creator.AddGroup(token, courseId);
        }

        public void AddUserToGroup(string token, int groupId, int userId, Role role)
        {
            _creator.AddUserToGroup(token, groupId, userId, role);
        }
    }
}