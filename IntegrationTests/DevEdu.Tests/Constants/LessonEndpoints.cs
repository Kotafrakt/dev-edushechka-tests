namespace DevEdu.Tests.Constants
{
    public class LessonEndpoints
    {
        public const string AddLessonEndpoint = "api/lesson";
        public const string DeleteLessonEndpoint = "api/lesson/{0}";
        public const string UpdateLessonEndpoint = "api/lesson/{0}";
        public const string GetAllLessonsByGroupIdEndpoint = "api/lesson/groupId/{0}";
        public const string GetAllLessonsByTeacherIdEndpoint = "api/lesson/teacherId/{0}";
        public const string GetLessonByIdEndpoint = "api/lesson/{0}";
        public const string GetAllLessonsWithCommentsEndpoint = "api/lesson/{0}/with-comments";
        public const string GetAllLessonsWithStudentsAndCommentsEndpoint = "api/lesson/{0}/full-info";
        public const string DeleteTopicFromLessonEndpoint = "api/lesson/{0}/topic/{1}";
        public const string AddTopicToLessonEndpoint = "api/lesson/{0}/topic/{1}";
        public const string AddStudentToLessonEndpoint = "api/lesson/{0}/user/{1}";
        public const string DeleteStudentFromLessonEndpoint = "api/lesson/{0}/user/{1}";
        public const string UpdateStudentFeedbackForLessonEndpoint = "api/lesson/{0}/user/{1}/feedback";
        public const string UpdateStudentAbsenceReasonOnLessonEndpoint = "api/lesson/{0}/user/{1}/absenceReason";
        public const string UpdateStudentAttendanceOnLessonEndpoint = "api/lesson/{0}/user/{1}/attendance";
        public const string GetAllFeedbackByLessonIdEndpoint = "api/lesson/{0}/feedback";
    }
}