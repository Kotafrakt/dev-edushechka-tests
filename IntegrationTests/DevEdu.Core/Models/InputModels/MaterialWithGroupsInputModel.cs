using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class MaterialWithGroupsInputModel : MaterialWithTagsInputModel
    {
        [MinLength(1, ErrorMessage = GroupsRequired)]
        public List<int> GroupsIds { get; set; }
    }
}
