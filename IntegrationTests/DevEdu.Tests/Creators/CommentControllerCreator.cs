using DevEdu.Core.Models;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class CommentControllerCreator : BaseControllerCreator
    {
        public CommentInfoOutputModel AddCommentToLesson(string token, int lessonId)
        {
            var comment = new CommentAddInputModel();
            return new CommentInfoOutputModel();
        }
        
        public CommentInfoOutputModel AddCommentToStudentAnswer(string token, int studentHomeworkId)
        {
            var comment = new CommentAddInputModel();
            return new CommentInfoOutputModel();
        }
        
        public CommentInfoOutputModel UpdateComment(string token, int commentId)
        {
            var comment = new CommentUpdateInputModel();
            return new CommentInfoOutputModel();
        }
    }
}