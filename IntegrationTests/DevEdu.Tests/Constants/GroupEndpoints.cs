namespace DevEdu.Tests.Constants
{
    public class GroupEndpoints
    {
        public const string GetGroupEndpoint = "api/Group/{0}";
        public const string GetAllGroupsEndpoint = "api/Group";
        public const string AddGroupEndpoint = "api/Group";
        public const string DeleteGroupEndpoint = "api/Group";
        public const string UpdateGroupEndpoint = "api/Group";
        public const string ChangeGroupStatusEndpoint = "api/Group/{0}/change-status/{1}";
        public const string AddGroupToLessonEndpoint = "api/Group/{0}/lesson/{1}";
        public const string RemoveGroupFromLessonEndpoint = "api/Group/{0}/lesson/{1}";
        public const string AddGroupMaterialReferenceEndpoint = "api/Group/{0}/material/{1}";
        public const string RemoveGroupMaterialReferenceEndpoint = "api/Group/{0}/material/{1}";
        public const string AddUserToGroupEndpoint = "api/group/{0}/user/{1}/role/{0}";
        public const string DeleteUserFromGroupEndpoint = "api/group/{0}/user/{1}";
    }
}