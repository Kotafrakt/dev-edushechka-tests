using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class CourseControllerFacade
    {
        private readonly CourseCreator _creator;
        public CourseControllerFacade() { _creator = new CourseCreator(); }

        public CourseInfoShortOutputModel CreateCourse(string token)
        {
            return _creator.AddCourse(token);
        }
    }
}