using System.Collections.Generic;

namespace DevEdu.Tests.Models
{
    public class TopicDto : BaseDto
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public List<CourseTopicDto> CourseTopics { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}