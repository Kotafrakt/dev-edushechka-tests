using System.Collections.Generic;

namespace DevEdu.Tests.Models
{
    public class TaskInfoWithCoursesOutputModel : TaskInfoOutputModel
    {
        public List<CourseInfoShortOutputModel> Courses { get; set; }
    }
}