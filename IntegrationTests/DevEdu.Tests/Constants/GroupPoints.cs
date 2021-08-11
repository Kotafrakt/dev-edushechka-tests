namespace DevEdu.Tests.Constants
{
    public class GroupPoints
    {
        public const string GetGroupPoint = "api/Group/{0}";
        public const string GetAllGroupsPoint = "api/Group";
        public const string AddGroupPoint = "api/Group";
        public const string DeleteGroupPoint = "api/Group";
        public const string UpdateGroupPoint = "api/Group";
        public const string ChangeGroupStatusPoint = "api/Group/{0}/change-status/{1}";
        public const string AddGroupToLessonPoint = "api/Group/{0}/lesson/{1}";
        public const string RemoveGroupFromLessonPoint = "api/Group/{0}/lesson/{1}";
        public const string AddGroupMaterialReferencePoint = "api/Group/{0}/material/{1}";
        public const string RemoveGroupMaterialReferencePoint = "api/Group/{0}/material/{1}";
        public const string AddUserToGroupPoint = "api/group/{0}/user/{1}/role/{0}";
        public const string DeleteUserFromGroupPoint = "api/group/{0}/user/{1}";
    }
}