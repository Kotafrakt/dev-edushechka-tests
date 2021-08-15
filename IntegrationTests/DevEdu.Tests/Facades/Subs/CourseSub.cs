using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class CourseSub
    {
        private readonly CourseControllerCreator _creator;
        public CourseSub() { _creator = new CourseControllerCreator(); }

        internal CourseInfoShortOutputModel CreateCourse(string token)
        {
            return _creator.AddCourse(token);
        }
    }
}