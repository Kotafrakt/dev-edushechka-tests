using System.Collections.Generic;

namespace DevEdu.Core.Models.Material
{
    public class MaterialInfoWithGroupsOutputModel : MaterialInfoOutputModel
    {
        public List<GroupInfoOutputModel> Groups { get; set; }
    }
}
