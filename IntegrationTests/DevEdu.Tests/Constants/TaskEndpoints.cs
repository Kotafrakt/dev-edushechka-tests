namespace DevEdu.Tests.Constants
{
    public class TaskEndpoints
    {
        public const string AddTaskByTeacherEndpoint = "api/task/teacher";
        public const string AddTaskByMethodistEndpoint = "api/task/methodist";
        public const string UpdateTaskByTeacherEndpoint = "api/task/teacher/{0}";
        public const string UpdateTaskByMethodistEndpoint = "api/task/methodist/{0}";
        public const string DeleteTaskEndpoint = "api/task/{0}";
        public const string GetTaskWithTagsEndpoint = "api/task/{0}";
        public const string GetTaskWithTagsAndCoursesEndpoint = "api/task/{0}/with-courses";
        public const string GetTaskWithTagsAndAnswersEndpoint = "api/task/{0}/with-answers";
        public const string GetTaskWithTagsAndGroupsEndpoint = "api/task/{0}/with-groups";
        public const string GetAllTasksWithTagsEndpoint = "api/task";
        public const string AddTagToTaskEndpoint = "api/task/{0}/tag/{1}";
        public const string DeleteTagFromTaskEndpoint = "api/task/{0}/tag/{1}";
        public const string AddStudentAnswerOnTaskEndpoint = "api/task/{0}/student/{1}";
        public const string GetAllStudentAnswersOnTaskEndpoint = "api/task/{0}/all-answers";
        public const string GetStudentAnswerOnTaskByTaskIdAndStudentIdEndpoint = "api/task/{0}/student/{1}";
        public const string UpdateStudentAnswerOnTaskEndpoint = "api/task/{0}/student/{1}";
        public const string DeleteStudentAnswerOnTaskEndpoint = "api/task/{0}/student/{1}";
        public const string UpdateStatusOfStudentAnswerEndpoint = "api/task/{0}/student/{1}/change-status/{2}";
        public const string GetAllAnswersByStudentIdEndpoint = "api/task/answer/by-user/{0}";
    }
}