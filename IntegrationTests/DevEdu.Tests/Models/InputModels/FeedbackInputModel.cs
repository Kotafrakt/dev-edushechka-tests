using System.ComponentModel.DataAnnotations;
using static DevEdu.Tests.Common.ValidationMessage;

namespace DevEdu.Tests.Models
{
    public class FeedbackInputModel
    {
        [Required(ErrorMessage = FeedbackRequired)]
        public string Feedback { get; set; }
    }
}