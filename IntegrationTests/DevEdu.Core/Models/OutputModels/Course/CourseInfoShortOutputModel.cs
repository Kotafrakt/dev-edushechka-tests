using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class CourseInfoShortOutputModel : CourseInfoBaseOutputModel
    {
        public string Description { get; set; }
        public List<GroupOutputMiniModel> Groups { get; set; }
    }
}