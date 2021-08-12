using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class CourseSub
    {
        private readonly CourseCreator _creator;
        public CourseSub() { _creator = new CourseCreator(); }

        internal CourseInfoShortOutputModel CreateValidCourse(string token)
        {
            return _creator.CreateValidCourse(token);
        }
    }
}