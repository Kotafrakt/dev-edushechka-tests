using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class TaskInfoWithCoursesOutputModel : TaskInfoOutputModel
    {
        public List<CourseInfoShortOutputModel> Courses { get; set; }
    }
}