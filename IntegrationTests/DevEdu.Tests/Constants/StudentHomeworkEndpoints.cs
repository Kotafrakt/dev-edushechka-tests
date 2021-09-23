namespace DevEdu.Tests.Constants
{
    public class StudentHomeworkEndpoints
    {
        public const string AddStudentHomeworkEndpoint = "api/StudentHomework/{0}";
        public const string DeleteStudentHomeworkEndpoint = "api/StudentHomework/{0}";
        public const string UpdateStudentHomeworkEndpoint = "api/StudentHomework/{0}";
        public const string UpdateStatusOfStudentHomeworkEndpoint = "api/StudentHomework/{0}/change-status/{1}";
        public const string GetStudentHomeworkByIdEndpoint = "api/StudentHomework/{0}";
        public const string GetAllStudentHomeworkOnTaskEndpoint = "api/StudentHomework/{0}/answers";
        public const string GetAllStudentHomeworkByStudentIdEndpoint = "api/StudentHomework/{0}";
    }
}
