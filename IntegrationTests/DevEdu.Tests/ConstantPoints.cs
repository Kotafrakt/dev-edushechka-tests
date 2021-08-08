namespace DevEdu.Tests
{
    public class ConstantPoints
    {
        //AuthorizationController
        public const string RegisterPoint = "register";
        public const string SignInPoint = "sign-in";
        //CommentController
        public const string GetCommentPoint = "api/comment/{0}";
        public const string AddCommentToLessonPoint = "api/comment/to-lesson/{0}";
        public const string AddCommentToStudentAnswerPoint = "api/comment/to-student-answer/{0}";
        public const string DeleteCommentPoint = "api/comment/{0}";
        public const string UpdateCommentPoint = "api/comment/{0}";
        //CourseController
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
        //GroupController
        public const string GetGroupPoint = "api/Group/{0}";
        public const string GetAllGroupsPoint = "api/Group";
        public const string AddGroupPoint = "api/Group";
        public const string DeleteGroupPoint = "api/Group";
        public const string UpdateGroupPoint = "api/Group";
        public const string ChangeGroupStatusPoint = "api/Group/{0}/change-status/{1}";
        public const string AddGroupToLessonPoint = "api/Group/{0}/lesson/{1}";
        public const string RemoveGroupFromLessonPoint = "api/Group/{0}/lesson/{1}";
        public const string AddGroupMaterialReferencePoint = "api/Group/{0}/material/{1}";
        public const string RemoveGroupMaterialReferencePoint = "api/Group/{0}/material/{1}";
        public const string AddUserToGroupPoint = "api/group/{0}/user/{1}/role/{0}";
        public const string DeleteUserFromGroupPoint = "api/group/{0}/user/{1}";
        //HomeworkController
        public const string GetHomeworkPoint = "api/homework/{0}";
        public const string GetHomeworkByGroupIdPoint = "api/homework/by-group/{0}";
        public const string GetHomeworkByTaskIdPoint = "api/homework/by-task/{0}";
        public const string AddHomeworkPoint = "api/homework/group/{0}/task/{1}";
        public const string DeleteHomeworkPoint = "api/homework/{0}";
        public const string UpdateHomeworkPoint = "api/homework/{0}";
        //LessonController
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
        //MaterialController
        public const string AddMaterialPoint = "api/material";
        public const string GetAllMaterialsPoint = "api/material";
        public const string GetMaterialPoint = "api/material/{0}";
        public const string UpdateMaterialPoint = "api/material/{0}";
        public const string DeleteMaterialPoint = "api/material/{0}/isDeleted/true";
        public const string AddTagToMaterialPoint = "api/material/{0}/tag/{1}";
        public const string DeleteTagFromMaterialPoint = "api/material/{0}/tag/{1}";
        public const string GetMaterialsByTagIdPoint = "api/material/by-tag/{0}";
        //NotificationController
        public const string GetNotificationPoint = "api/notification/{0}";
        public const string GetAllNotificationsByUserIdPoint = "api/notification/by-user/{0}";
        public const string GetAllNotificationsByGroupIdPoint = "api/notification/by-group/{0}";
        public const string GetAllNotificationsByRoleIdPoint = "api/notification/by-role/{0}";
        public const string AddNotificationPoint = "api/notification";
        public const string DeleteNotificationPoint = "api/notification/{0}";
        public const string UpdateNotificationPoint = "api/notification/{0}";
        //PaymentController
        public const string GetPaymentPoint = "api/payment/{0}";
        public const string SelectAllPaymentsByUserIdPoint = "api/payment/user/{0}";
        public const string AddPaymentPoint = "api/payment";
        public const string DeletePaymentPoint = "api/payment/{0}";
        public const string UpdatePaymentPoint = "api/payment/{0}";
        public const string AddPaymentsPoint = "api/payment/bulk";
        //RatingController
        public const string AddStudentRatingPoint = "api/rating";
        public const string DeleteStudentRatingPoint = "api/rating/{0}";
        public const string UpdateStudentRatingPoint = "api/rating/{0}/{1}/value/{2}";
        public const string GetAllStudentRatingsPoint = "api/rating";
        public const string GetStudentRatingByGroupIdPoint = "api/rating/by-group/{0}";
        public const string GetStudentRatingByUserIdPoint = "api/rating/by-user/{0}";
        //TagController
        public const string AddTagPoint = "api/tag";
        public const string DeleteTagPoint = "api/tag/{0}";
        public const string UpdateTagPoint = "api/tag/{0}";
        public const string GetAllTagsPoint = "api/tag";
        public const string GetTagByIdPoint = "api/tag/{0}";
        //TaskController
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
        //TopicController
        public const string GetTopicByIdPoint = "api/topic/{0}";
        public const string GetAllTopicsPoint = "api/topic";
        public const string AddTopicPoint = "api/topic";
        public const string DeleteTopicPoint = "api/topic/{0}";
        public const string UpdateTopicPoint = "api/topic/{0}";
        public const string AddTagToTopicPoint = "api/topic/{0}/tag/{1}";
        public const string DeleteTagFromTopicPoint = "api/topic/{0}/tag/{1}";
        //UserController
        public const string UpdateUserByIdPoint = "api/user/{0}";
        public const string GetUserByIdPoint = "api/user/{0}";
        public const string GetAllUsersPoint = "api/user";
        public const string DeleteUserPoint = "api/user/{0}";
        public const string AddRoleToUserPoint = "api/user/{0}/role/{1}";
        public const string DeleteRoleFromUserPoint = "api/user/{0}/role/{1}";
    }
}