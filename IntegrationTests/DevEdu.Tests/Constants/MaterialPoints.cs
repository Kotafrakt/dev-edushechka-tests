namespace DevEdu.Tests.Constants
{
    public class MaterialPoints
    {
        public const string AddMaterialWithGroupsPoint = "api/material/with-groups";
        public const string AddMaterialWithCoursesPoint = "api/material/with-courses";
        public const string GetAllMaterialsPoint = "api/material";
        public const string GetMaterialByIdWithCoursesAndGroupsPoint = "api/material/{0}/full-output-model";
        public const string GetMaterialByIdWithTagsPoint = "api/material/{0}/short-output-model";
        public const string UpdateMaterialPoint = "api/material/{0}";
        public const string DeleteMaterialPoint = "api/material/{0}/isDeleted/{1}";
        public const string AddTagToMaterialPoint = "api/material/{0}/tag/{1}";
        public const string DeleteTagFromMaterialPoint = "api/material/{0}/tag/{1}";
        public const string GetMaterialsByTagIdPoint = "api/material/by-tag/{0}";
    }
}