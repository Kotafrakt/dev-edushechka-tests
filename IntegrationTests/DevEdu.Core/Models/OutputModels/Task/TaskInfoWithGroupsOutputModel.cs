using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class TaskInfoWithGroupsOutputModel : TaskInfoOutputModel
    {
        public List<GroupInfoOutputModel> Groups { get; set; }
    }
}