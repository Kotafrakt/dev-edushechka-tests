using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class TopicControllerCreator : BaseControllerCreator
    {
        public TopicOutputModel AddTopic(string token)
        {
            _endPoint = TopicPoints.AddTopicPoint;
            var cource = TopicData.GetValidTopicInputModel();

            var request = _requestHelper.CreatePost(_endPoint, cource, token);

            var response = _client.Execute<TopicOutputModel>(request);
            var result = response.Data;
            return result;
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