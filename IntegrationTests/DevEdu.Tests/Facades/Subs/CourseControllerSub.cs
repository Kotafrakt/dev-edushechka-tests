using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class CourseControllerSub
    {
        private readonly CourseControllerCreator _creator;
        public CourseControllerSub() { _creator = new CourseControllerCreator(); }

        internal CourseInfoShortOutputModel CreateCourse(string token)
        {
            return _creator.AddCourse(token);
        }
    }
}