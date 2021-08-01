﻿using System.ComponentModel.DataAnnotations;
using static DevEdu.Tests.Common.ValidationMessage;

namespace DevEdu.Tests.Models
{
    public class NotificationAddInputModel
    {

        public int? UserId { get; set; }
        [Required(ErrorMessage = TextRequired)]
        public string Text { get; set; }
        public int? RoleId { get; set; }
        public int? GroupId { get; set; }
    }
}