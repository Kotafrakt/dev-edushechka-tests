using System;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class TagInputModel
    {
        [Required(ErrorMessage = NameRequired)]
        public string Name { get; set; }
    }
}