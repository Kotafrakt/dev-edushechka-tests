using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class MaterialInputModel
    {
        [Required(ErrorMessage = ContentRequired)]
        public string Content { get; set; }
    }
}
