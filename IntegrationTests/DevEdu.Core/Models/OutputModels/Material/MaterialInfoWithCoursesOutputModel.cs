using System.Collections.Generic;

namespace DevEdu.Core.Models.Material
{
    public class MaterialInfoWithCoursesOutputModel : MaterialInfoOutputModel
    {
        public List<CourseInfoBaseOutputModel> Courses { get; set; }
    }
}