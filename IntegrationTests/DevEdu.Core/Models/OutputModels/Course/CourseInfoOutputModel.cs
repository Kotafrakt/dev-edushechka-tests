using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class CourseInfoOutputModel : CourseInfoBaseOutputModel
    {
        public string Description { get; set; }
        public List<CourseTopicOutputModel> CourseTopics { get; set; }
    }
}