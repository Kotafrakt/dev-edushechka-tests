namespace DevEdu.Tests.Constants
{
    public class UserPoints
    {
        public const string UpdateUserByIdPoint = "api/user/{0}";
        public const string GetUserByIdPoint = "api/user/{0}";
        public const string GetAllUsersPoint = "api/user";
        public const string DeleteUserPoint = "api/user/{0}";
        public const string AddRoleToUserPoint = "api/user/{0}/role/{1}";
        public const string DeleteRoleFromUserPoint = "api/user/{0}/role/{1}";
    }
}