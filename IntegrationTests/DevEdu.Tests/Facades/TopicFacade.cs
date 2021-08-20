using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class TopicFacade
    {
        private TopicCreator _creator;
        public TopicFacade() { _creator = new TopicCreator(); }
    }
}