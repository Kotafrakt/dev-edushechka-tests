using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class LessonCreator : BaseCreator
    {
        public LessonInfoOutputModel AddLesson(string token, int teacherId)
        {
            _endPoint = LessonEndpoints.AddLessonEndpoint;
            var postData = LessonData.GetValidLessonInputModel(teacherId);
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, token);
            var response = _client.Execute<LessonInfoOutputModel>(request);
            var result = response.Data;
            return result;
        }

        public LessonInfoOutputModel LessonInfoOutputModel(string token, int lessonId)
        {
            var model = new LessonUpdateInputModel();
            return new LessonInfoOutputModel();
        }

        public TopicOutputModel AddTopicToLesson(string token, int teacherId, int lessonId, int topicId)
        {
            _endPoint = string.Format(LessonEndpoints.AddTopicToLessonEndpoint,lessonId,topicId);
            var request = _requestHelper.CreatePostReferenceRequest(_endPoint, token);
            var response = _client.Execute<TopicOutputModel>(request);
            return response.Data;
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