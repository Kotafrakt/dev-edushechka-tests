namespace DevEdu.Tests.Constants
{
    public class CourseEndpoints
    {
        public const string GetCourseSimpleEndpoint = "api/course/{0}/simple";
        public const string GetCourseFullEndpoint = "api/course/{0}/full";
        public const string GetAllCoursesWithGroupsEndpoint = "api/course";
        public const string AddCourseEndpoint = "api/course";
        public const string DeleteCourseEndpoint = "api/course/{0}";
        public const string UpdateCourseEndpoint = "api/course/{0}";
        public const string AddCourseMaterialReferenceEndpoint = "api/course/{0}/Material/{1}";
        public const string RemoveCourseMaterialReferenceEndpoint = "api/course/{0}/Material/{1}";
        public const string AddTaskToCourseEndpoint = "api/course/{0}/task/{1}";
        public const string RemoveTaskFromCourseEndpoint = "api/course/{0}/task/{0}";
        public const string AddTopicToCourseEndpoint = "api/course/{0}/topic/{1}";
        public const string AddTopicsToCourseEndpoint = "api/course/{0}/add-topics";
        public const string DeleteTopicFromCourseEndpoint = "api/course/{0}/topics/{1}";
        public const string SelectAllTopicsByCourseIdEndpoint = "api/course/{0}/topics";
        public const string UpdateCourseTopicsByCourseIdEndpoint = "api/course/{0}/program";
    }
}