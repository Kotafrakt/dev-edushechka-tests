namespace DevEdu.Tests.Constants
{
    public class TaskPoints
    {
        public const string AddTaskByTeacherPoint = "api/task/teacher";
        public const string AddTaskByMethodistPoint = "api/task/methodist";
        public const string UpdateTaskByTeacherPoint = "api/task/{0}";
        public const string UpdateTaskByMethodistPoint = "api/task/{0}";
        public const string DeleteTaskPoint = "api/task/{0}";
        public const string GetTaskWithTagsPoint = "api/task/{0}";
        public const string GetTaskWithTagsAndCoursesPoint = "api/task/{0}/with-courses";
        public const string GetTaskWithTagsAndAnswersPoint = "api/task/{0}/with-answers";
        public const string GetTaskWithTagsAndGroupsPoint = "api/task/{0}/with-courses";
        public const string GetAllTasksWithTagsPoint = "api/task";
        public const string AddTagToTaskPoint = "api/task/{0}/tag/{1}";
        public const string DeleteTagFromTaskPoint = "api/task/{0}/tag/{1}";
        public const string AddStudentAnswerOnTaskPoint = "api/task/{0}/student/{1}";
        public const string GetAllStudentAnswersOnTaskPoint = "api/task/{0}/all-answers";
        public const string GetStudentAnswerOnTaskByTaskIdAndStudentIdPoint = "api/task/{0}/student/{1}";
        public const string UpdateStudentAnswerOnTaskPoint = "api/task/{0}/student/{1}";
        public const string DeleteStudentAnswerOnTaskPoint = "api/task/{0}/student/{1}";
        public const string UpdateStatusOfStudentAnswerPoint = "api/task/{0}/student/{1}/change-status/{2}";
        public const string GetAllAnswersByStudentIdPoint = "api/task/answer/by-user/{0}";
    }
}