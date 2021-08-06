﻿namespace DevEdu.Tests.Facades
{
    public class Facade
    {
        private readonly CommentSub _commentSub = new();
        private readonly CourseSub _courseSub = new();
        private readonly CourseMaterialSub _courseMaterialSub = new();
        private readonly CourseTaskSub _courseTaskSub = new();
        private readonly CourseTopicSub _courseTopicSub = new();
        private readonly GroupSub _groupSub = new();
        private readonly GroupLessonSub _groupLessonSub = new();
        private readonly GroupMaterialSub _groupMaterialSub = new();
        private readonly HomeworkSub _homeworkSub = new();
        private readonly LessonSub _lessonSub = new();
        private readonly LessonTopicSub _lessonTopicSub = new();
        private readonly MaterialSub _materialSub = new();
        private readonly NotificationSub _notificationSub = new();
        private readonly PaymentSub _paymentSub = new();
        private readonly RatingTypeSub _ratingTypeSub = new();
        private readonly StudentLessonSub _studentLessonSub = new();
        private readonly StudentRatingSub _studentRatingSub = new();
        private readonly TagSub _tagSub = new();
        private readonly TagMaterialSub _tagMaterialSub = new();
        private readonly TagTaskSub _tagTaskSub = new();
        private readonly TagTopicSub _tagTopicSub = new();
        private readonly TaskSub _taskSub = new();
        private readonly TaskStudentSub _taskStudentSub = new();
        private readonly TaskStatusSub _taskStatusSub = new();
        private readonly TopicSub _topicSub = new();
        private readonly UserSub _userSub = new();
        private readonly UserGroupSub _userGroupSub = new();
        private readonly UserRoleSub _userRoleSub = new();
    }
}