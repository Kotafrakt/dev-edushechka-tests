using System.ComponentModel.DataAnnotations;

namespace DevEdu.Core.Models
{
    public class NotificationUpdateInputModel
    {
        [Required]
        public string Text { get; set; }
    }
}