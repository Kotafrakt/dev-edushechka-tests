using DevEdu.Core.Models;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class LessonControllerCreator : BaseControllerCreator
    {
        public LessonInfoOutputModel AddLesson(string token)
        {
            var model = new LessonInputModel();
            return new LessonInfoOutputModel();
        }
        
        public LessonInfoOutputModel LessonInfoOutputModel(string token, int lessonId)
        {
            var model = new LessonUpdateInputModel();
            return new LessonInfoOutputModel();
        }

        public void AddTopicToLesson(string token, int topicId)
        {
        }

        public StudentLessonOutputModel AddStudentToLesson(string token, int lessonId, int studentId)
        {
            return new StudentLessonOutputModel();
        }

        public StudentLessonOutputModel UpdateStudentFeedbackForLesson(string token, int lessonId, int studentId)
        {
            var model = new  FeedbackInputModel();
            return new StudentLessonOutputModel();
        }

        public StudentLessonOutputModel UpdateStudentAbsenceReasonOnLesson  (string token, int lessonId, int studentId)
        {
            var model = new AbsenceReasonInputModel();
            return new StudentLessonOutputModel();
        }

        public StudentLessonOutputModel UpdateStudentAttendanceOnLesson(string token, int lessonId, int studentId)
        {
            var model = new AttendanceInputModel();
            return new StudentLessonOutputModel();
        }
    }
}