using System.Collections.Generic;

namespace DevEdu.Core.Models.Material
{
    public class MaterialInfoFullOutputModel : MaterialInfoOutputModel
    {
        public List <CourseInfoBaseOutputModel> Courses { get; set; }
        public List <GroupInfoOutputModel> Groups { get; set; }
    }
}