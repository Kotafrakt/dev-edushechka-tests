using DevEdu.Core.Enums;
using System;

namespace DevEdu.Core.Models
{
    public class StudentAnswerOnTaskInfoOutputModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Answer { get; set; }
        public UserInfoShortOutputModel Student { get; set; }
    }
}