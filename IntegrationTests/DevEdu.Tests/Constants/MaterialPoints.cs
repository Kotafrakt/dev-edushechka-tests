namespace DevEdu.Tests.Constants
{
    public class MaterialPoints
    {
        public const string AddMaterialPoint = "api/material";
        public const string GetAllMaterialsPoint = "api/material";
        public const string GetMaterialPoint = "api/material/{0}";
        public const string UpdateMaterialPoint = "api/material/{0}";
        public const string DeleteMaterialPoint = "api/material/{0}/isDeleted/true";
        public const string AddTagToMaterialPoint = "api/material/{0}/tag/{1}";
        public const string DeleteTagFromMaterialPoint = "api/material/{0}/tag/{1}";
        public const string GetMaterialsByTagIdPoint = "api/material/by-tag/{0}";
    }
}
