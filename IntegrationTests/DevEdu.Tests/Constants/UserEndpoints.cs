namespace DevEdu.Tests.Constants
{
    public class UserEndpoints
    {
        public const string UpdateUserByIdEndpoint = "api/user/{0}";
        public const string GetUserByIdEndpoint = "api/user/{0}";
        public const string GetAllUsersEndpoint = "api/user";
        public const string DeleteUserEndpoint = "api/user/{0}";
        public const string AddRoleToUserEndpoint = "api/user/{0}/role/{1}";
        public const string DeleteRoleFromUserEndpoint = "api/user/{0}/role/{1}";
    }
}