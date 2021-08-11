using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class MaterialInfoWithGroupsOutputModel : MaterialInfoOutputModel
    {
        public List<GroupInfoOutputModel> Groups { get; set; }
    }
}
