﻿namespace DevEdu.Core.Models
{
    public class StudentLessonOutputModel
    {
        public int Id { get; set; }
        public UserInfoShortOutputModel User { get; set; }
        public string Feedback { get; set; }
        public bool IsPresent { get; set; }
        public string AbsenceReason { get; set; }
    }
}