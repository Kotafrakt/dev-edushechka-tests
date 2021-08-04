using System.ComponentModel.DataAnnotations;
﻿using DevEdu.Core.Common;

namespace DevEdu.Core.Models
{
    public class StudentAnswerOnTaskInputModel
    {
        [Required(ErrorMessage = ValidationMessage.StudentAnswerRequired)]
        public string Answer { get; set; }
    }
}