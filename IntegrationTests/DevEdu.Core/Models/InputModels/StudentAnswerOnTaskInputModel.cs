﻿using DevEdu.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace DevEdu.Core.Models
{
    public class StudentAnswerOnTaskInputModel
    {
        [Required(ErrorMessage = ValidationMessage.StudentAnswerRequired)]
        public string Answer { get; set; }
    }
}