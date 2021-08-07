using DevEdu.Core.Models;
using DevEdu.Tests.Fillings;

namespace DevEdu.Tests.Facades
{
    internal class CourseSub
    {
        private CourseFilling filling;
        public CourseSub() { filling = new CourseFilling(); }

        internal CourseInfoShortOutputModel CreateCourseCorrect(string token)
        {
            return filling.CreateCorrectCourse(token);
        }
    }
}