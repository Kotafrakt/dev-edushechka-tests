using System;

namespace DevEdu.Core.Models
{
    public class CommentDto : BaseDto
    {
        public string Text { get; set; }
        public UserDto User { get; set; }
        public DateTime Date { get; set; }
    }
}