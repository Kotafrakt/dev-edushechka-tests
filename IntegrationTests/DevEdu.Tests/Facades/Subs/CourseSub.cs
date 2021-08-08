using DevEdu.Core.Models;
using DevEdu.Tests.Fillings;

namespace DevEdu.Tests.Facades
{
    internal class CourseSub
    {
        private readonly CourseFilling _filling;
        public CourseSub() { _filling = new CourseFilling(); }

        internal CourseInfoShortOutputModel CreateCourse(string token)
        {
            return _filling.CreateCourse(token);
        }
    }
}