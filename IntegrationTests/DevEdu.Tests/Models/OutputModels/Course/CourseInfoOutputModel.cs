using System.Collections.Generic;

namespace DevEdu.Tests.Models
{
    public class CourseInfoOutputModel : CourseInfoBaseOutputModel
    {
        public string Description { get; set; }
        public List<CourseTopicOutputModel> CourseTopics { get; set; }
    }
}