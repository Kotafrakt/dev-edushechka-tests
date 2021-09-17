using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class LessonFacade
    {
        private LessonCreator _creator;
        public LessonFacade() { _creator = new LessonCreator(); }
        internal LessonInfoOutputModel AddLesson(string token, int id)
        {
            return _creator.AddLesson(token, id);
        }

        internal void AddTopicToLesson(string token, int teacherId, int lessonId, int topicId)
        {
            _creator.AddTopicToLesson(token, teacherId, lessonId, topicId);
        }
    }
}