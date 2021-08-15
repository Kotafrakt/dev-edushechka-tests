using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class TopicControllerSub
    {
        private TopicControllerCreator _creator;
        public TopicControllerSub() { _creator = new TopicControllerCreator(); }
    }
}