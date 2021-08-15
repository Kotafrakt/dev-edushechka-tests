using DevEdu.Core.Models;

namespace DevEdu.Tests.Creators
{
    public class TopicControllerCreator
    {
        public TopicOutputModel AddTopic(string token)
        {
            var model = new TopicInputModel();
            return new TopicOutputModel();
        }

        public TopicOutputModel UpdateTopic(string token, int topicId)
        {
            var model = new TopicInputModel();
            return new TopicOutputModel();
        }

        public void AddTagToTopic(string token, int topicId, int tagId)
        {
        }
    }
}