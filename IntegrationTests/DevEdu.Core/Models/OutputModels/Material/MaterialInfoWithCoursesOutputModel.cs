using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class MaterialInfoWithCoursesOutputModel : MaterialInfoOutputModel
    {
        public List<CourseInfoBaseOutputModel> Courses { get; set; }
    }
}