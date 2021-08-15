using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class MaterialWithTagsInputModel : MaterialInputModel
    {
        public List<int> TagsIds { get; set; }
    }
}