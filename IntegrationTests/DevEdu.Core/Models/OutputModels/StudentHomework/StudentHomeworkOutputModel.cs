﻿using DevEdu.Core.Enums;

namespace DevEdu.Core.Models
{
    public class StudentHomeworkOutputModel
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public string CompletedDate { get; set; }
        public UserInfoShortOutputModel User { get; set; }
    }
}