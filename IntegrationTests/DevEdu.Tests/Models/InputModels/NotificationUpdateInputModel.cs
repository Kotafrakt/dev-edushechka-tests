using System.ComponentModel.DataAnnotations;

namespace DevEdu.Tests.Models
{
    public class NotificationUpdateInputModel
    {
        [Required]
        public string Text { get; set; }
    }
}