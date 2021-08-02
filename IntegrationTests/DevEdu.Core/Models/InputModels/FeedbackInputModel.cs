using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class FeedbackInputModel
    {
        [Required(ErrorMessage = FeedbackRequired)]
        public string Feedback { get; set; }
    }
}