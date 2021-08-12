using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class MaterialWithCoursesInputModel : MaterialWithTagsInputModel
    {
        [MinLength(1, ErrorMessage = CoursesRequired)]
        public List<int> CoursesIds { get; set; }
    }
}
