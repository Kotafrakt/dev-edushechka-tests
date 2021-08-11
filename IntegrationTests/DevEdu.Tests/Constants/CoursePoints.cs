namespace DevEdu.Tests.Constants
{
    public class CoursePoints
    {
        public const string GetCourseSimplePoint = "api/course/{0}/simple";
        public const string GetCourseFullPoint = "api/course/{0}/full";
        public const string GetAllCoursesWithGroupsPoint = "api/course";
        public const string AddCoursePoint = "api/course";
        public const string DeleteCoursePoint = "api/course/{0}";
        public const string UpdateCoursePoint = "api/course/{0}";
        public const string AddCourseMaterialReferencePoint = "api/course/{0}/Material/{1}";
        public const string RemoveCourseMaterialReferencePoint = "api/course/{0}/Material/{1}";
        public const string AddTaskToCoursePoint = "api/course/{0}/task/{1}";
        public const string RemoveTaskFromCoursePoint = "api/course/{0}/task/{0}";
        public const string AddTopicToCoursePoint = "api/course/{0}/topic/{1}";
        public const string AddTopicsToCoursePoint = "api/course/{0}/add-topics";
        public const string DeleteTopicFromCoursePoint = "api/course/{0}/topics/{1}";
        public const string SelectAllTopicsByCourseIdPoint = "api/course/{0}/topics";
        public const string UpdateCourseTopicsByCourseIdPoint = "api/course/{0}/program";
    }
}
