namespace DevEdu.Tests.Constants
{
    public class LessonPoints
    {
        public const string AddLessonPoint = "api/lesson";
        public const string DeleteLessonPoint = "api/lesson/{0}";
        public const string UpdateLessonPoint = "api/lesson/{0}";
        public const string GetAllLessonsByGroupIdPoint = "api/lesson/groupId/{0}";
        public const string GetAllLessonsByTeacherIdPoint = "api/lesson/teacherId/{0}";
        public const string GetLessonByIdPoint = "api/lesson/{0}";
        public const string GetAllLessonsWithCommentsPoint = "api/lesson/{0}/with-comments";
        public const string GetAllLessonsWithStudentsAndCommentsPoint = "api/lesson/{0}/full-info";
        public const string DeleteTopicFromLessonPoint = "api/lesson/{0}/topic/{1}";
        public const string AddTopicToLessonPoint = "api/lesson/{0}/topic/{1}";
        public const string AddStudentToLessonPoint = "api/lesson/{0}/user/{1}";
        public const string DeleteStudentFromLessonPoint = "api/lesson/{0}/user/{1}";
        public const string UpdateStudentFeedbackForLessonPoint = "api/lesson/{0}/user/{1}/feedback";
        public const string UpdateStudentAbsenceReasonOnLessonPoint = "api/lesson/{0}/user/{1}/absenceReason";
        public const string UpdateStudentAttendanceOnLessonPoint = "api/lesson/{0}/user/{1}/attendance";
        public const string GetAllFeedbackByLessonIdPoint = "api/lesson/{0}/feedback";
    }
}
