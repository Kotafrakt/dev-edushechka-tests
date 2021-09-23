using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

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

        public List<CourseInfoShortOutputModel> CreateListOfCourses(string token, int count = 3)
        {
            var courses = new List<CourseInfoShortOutputModel>();
            for (int i = 0; i < count; i++)
            {
                courses.Add(_creator.AddCourse(token));
            }
            return courses;
        }
    }
}