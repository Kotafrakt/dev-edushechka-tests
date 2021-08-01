﻿namespace DevEdu.Tests.Models
{
    public class CommentInfoOutputModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public UserInfoOutPutModel User { get; set; }
        public string Date { get; set; }
        public bool IsDeleted { get; set; }
    }
}