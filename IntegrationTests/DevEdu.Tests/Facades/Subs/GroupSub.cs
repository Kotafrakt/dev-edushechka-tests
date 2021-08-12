using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class GroupSub
    {
        private GroupCreator _creator;

        public GroupSub() { _creator = new GroupCreator(); }

        public GroupOutputModel CreateValidGroup(string token, int courseId = 0)
        {
            if (courseId == 0)
            {
                var courseCreator = new CourseSub();
                courseId = courseCreator.CreateValidCourse(token).Id;
            }
            return _creator.CreateValidGroup(token, courseId);
        }
        
    }
}