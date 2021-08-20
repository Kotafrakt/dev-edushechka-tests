using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class CourseFacade
    {
        private readonly CourseCreator _creator;
        public CourseFacade() { _creator = new CourseCreator(); }

        public CourseInfoShortOutputModel CreateCourse(string token)
        {
            return _creator.AddCourse(token);
        }
    }
}