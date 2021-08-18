namespace DevEdu.Tests.Constants
{
    public class TopicEndpoints
    {
        public const string GetTopicByIdEndpoint = "api/topic/{0}";
        public const string GetAllTopicsEndpoint = "api/topic";
        public const string AddTopicEndpoint = "api/topic";
        public const string DeleteTopicEndpoint = "api/topic/{0}";
        public const string UpdateTopicEndpoint = "api/topic/{0}";
        public const string AddTagToTopicEndpoint = "api/topic/{0}/tag/{1}";
        public const string DeleteTagFromTopicEndpoint = "api/topic/{0}/tag/{1}";
    }
}