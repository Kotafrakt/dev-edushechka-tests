using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class CommentUpdateInputModel
    {
        [Required(ErrorMessage = TextRequired)]
        public string Text { get; set; }
    }
}