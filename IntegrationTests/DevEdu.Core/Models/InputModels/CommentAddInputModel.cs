﻿using DevEdu.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace DevEdu.Core.Models
{
    public class CommentAddInputModel
    {
        [Required(ErrorMessage = ValidationMessage.CommentUserIdRequired)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ValidationMessage.IdRequired)]
        public int UserId { get; set; }
        [Required(ErrorMessage = ValidationMessage.CommentTextRequired)]
        public string Text { get; set; }
    }
}