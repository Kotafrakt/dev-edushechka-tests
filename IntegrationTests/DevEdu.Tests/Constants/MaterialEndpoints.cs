namespace DevEdu.Tests.Constants
{
    public class MaterialEndpoints
    {
        public const string AddMaterialWithGroupsEndpoint = "api/material/with-groups";
        public const string AddMaterialWithCoursesEndpoint = "api/material/with-courses";
        public const string GetAllMaterialsEndpoint = "api/material";
        public const string GetMaterialByIdWithCoursesAndGroupsEndpoint = "api/material/{0}/full-output-model";
        public const string GetMaterialByIdWithTagsEndpoint = "api/material/{0}/short-output-model";
        public const string UpdateMaterialEndpoint = "api/material/{0}";
        public const string DeleteMaterialEndpoint = "api/material/{0}/isDeleted/{1}";
        public const string AddTagToMaterialEndpoint = "api/material/{0}/tag/{1}";
        public const string DeleteTagFromMaterialEndpoint = "api/material/{0}/tag/{1}";
        public const string GetMaterialsByTagIdEndpoint = "api/material/by-tag/{0}";
    }
}