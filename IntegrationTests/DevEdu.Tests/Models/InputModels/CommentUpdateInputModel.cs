using System.ComponentModel.DataAnnotations;
using static DevEdu.Tests.Common.ValidationMessage;

namespace DevEdu.Tests.Models
{
    public class CommentUpdateInputModel
    {
        [Required(ErrorMessage = TextRequired)]
        public string Text { get; set; }
    }
}