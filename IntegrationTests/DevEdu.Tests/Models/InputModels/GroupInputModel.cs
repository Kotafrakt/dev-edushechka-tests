using DevEdu.Tests.Common;
using DevEdu.Tests.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Tests.Common.ValidationMessage;

namespace DevEdu.Tests.Models
{
    public class GroupInputModel
    {
        [Required(ErrorMessage = NameRequired)]
        public string Name { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required(ErrorMessage = GroupStatusIdRequired)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = WrongFormatGroupStatusId)]
        public GroupStatus? GroupStatusId { get; set; }
        [Required(ErrorMessage = DateRequired)]
        [CustomDateFormatAttribute(ErrorMessage = WrongFormatStartDate)]
        public string StartDate { get; set; }
        [Required(ErrorMessage = TimetableRequired)]
        public string Timetable { get; set; }
        [Required(ErrorMessage = PaymentPerMonthRequired)]
        public decimal PaymentPerMonth { get; set; }
    }
}