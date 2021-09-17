using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class TopicFacade
    {
        private readonly TopicCreator _creator;
        public TopicFacade() { _creator = new TopicCreator(); }

        public TopicOutputModel AddTopic(string token)
        {
            var result = _creator.AddTopic(token);
            return result;
        }
    }
}