using DevEdu.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace DevEdu.Core.Models
{
    public class CommentAddInputModel
    {
        [Required(ErrorMessage = ValidationMessage.CommentTextRequired)]
        public string Text { get; set; }
    }
}