using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class MaterialInfoWithCoursesAndGroupsOutputModel : MaterialInfoOutputModel
    {
        public List<CourseInfoBaseOutputModel> Courses { get; set; }
        public List<GroupInfoOutputModel> Groups { get; set; }
    }
}