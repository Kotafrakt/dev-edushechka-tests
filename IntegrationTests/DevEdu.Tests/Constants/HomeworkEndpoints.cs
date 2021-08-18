namespace DevEdu.Tests.Constants
{
    public class HomeworkEndpoints
    {
        public const string GetHomeworkEndpoint = "api/homework/{0}";
        public const string GetHomeworkByGroupIdEndpoint = "api/homework/by-group/{0}";
        public const string GetHomeworkByTaskIdEndpoint = "api/homework/by-task/{0}";
        public const string AddHomeworkEndpoint = "api/homework/group/{0}/task/{1}";
        public const string DeleteHomeworkEndpoint = "api/homework/{0}";
        public const string UpdateHomeworkEndpoint = "api/homework/{0}";
    }
}