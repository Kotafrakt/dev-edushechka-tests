namespace DevEdu.Tests.Constants
{
    public class RatingEndpoints
    {
        public const string AddStudentRatingEndpoint = "api/rating";
        public const string DeleteStudentRatingEndpoint = "api/rating/{0}";
        public const string UpdateStudentRatingEndpoint = "api/rating/{0}/{1}/value/{2}";
        public const string GetAllStudentRatingsEndpoint = "api/rating";
        public const string GetStudentRatingByGroupIdEndpoint = "api/rating/by-group/{0}";
        public const string GetStudentRatingByUserIdEndpoint = "api/rating/by-user/{0}";
    }
}