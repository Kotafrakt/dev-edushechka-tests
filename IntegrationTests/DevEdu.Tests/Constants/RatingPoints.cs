namespace DevEdu.Tests.Constants
{
    public class RatingPoints
    {
        public const string AddStudentRatingPoint = "api/rating";
        public const string DeleteStudentRatingPoint = "api/rating/{0}";
        public const string UpdateStudentRatingPoint = "api/rating/{0}/{1}/value/{2}";
        public const string GetAllStudentRatingsPoint = "api/rating";
        public const string GetStudentRatingByGroupIdPoint = "api/rating/by-group/{0}";
        public const string GetStudentRatingByUserIdPoint = "api/rating/by-user/{0}";
    }
}