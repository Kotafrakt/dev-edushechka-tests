using System;
using DevEdu.Core.Enums;

namespace DevEdu.Core.Models
    
{
    public class NotificationDto : BaseDto
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public Role? Role { get; set; }
        public UserDto User { get; set; }
        public GroupDto Group { get; set; }       
    }
}