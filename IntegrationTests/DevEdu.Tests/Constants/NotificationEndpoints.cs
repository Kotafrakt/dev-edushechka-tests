namespace DevEdu.Tests.Constants
{
    public class NotificationEndpoints
    {
        public const string GetNotificationEndpoint = "api/notification/{0}";
        public const string GetAllNotificationsByUserIdEndpoint = "api/notification/by-user/{0}";
        public const string GetAllNotificationsByGroupIdEndpoint = "api/notification/by-group/{0}";
        public const string GetAllNotificationsByRoleIdEndpoint = "api/notification/by-role/{0}";
        public const string AddNotificationEndpoint = "api/notification";
        public const string DeleteNotificationEndpoint = "api/notification/{0}";
        public const string UpdateNotificationEndpoint = "api/notification/{0}";
    }
}