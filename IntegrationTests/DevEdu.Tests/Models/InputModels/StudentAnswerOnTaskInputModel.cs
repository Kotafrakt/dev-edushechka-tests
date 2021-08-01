using System.ComponentModel.DataAnnotations;
﻿using DevEdu.Tests.Common;

namespace DevEdu.Tests.Models
{
    public class StudentAnswerOnTaskInputModel
    {
        [Required(ErrorMessage = ValidationMessage.StudentAnswerRequired)]
        public string Answer { get; set; }
    }
}