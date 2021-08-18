using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class TopicControllerFacade
    {
        private TopicCreator _creator;
        public TopicControllerFacade() { _creator = new TopicCreator(); }
    }
}