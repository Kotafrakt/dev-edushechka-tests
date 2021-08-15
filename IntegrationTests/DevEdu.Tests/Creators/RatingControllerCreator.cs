using DevEdu.Core.Models;

namespace DevEdu.Tests.Creators
{
    public class RatingControllerCreator : BaseControllerCreator
    {
        public StudentRatingOutputModel AddStudentRating(string token)
        {
            var model = new StudentRatingInputModel();
            return new StudentRatingOutputModel();
        }

        public StudentRatingOutputModel UpdateStudentRating(string token, int userId, int value, int periodNumber)
        {
            return new StudentRatingOutputModel();
        }
    }
}