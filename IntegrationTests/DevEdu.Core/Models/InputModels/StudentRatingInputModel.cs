using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class StudentRatingInputModel
    {
        [Required(ErrorMessage = UserIdRequired)]
        public int UserId { get; set; }

        [Required(ErrorMessage = GroupIdRequired)]
        public int GroupId { get; set; }

        [Required(ErrorMessage = RatingTypeIdRequired)]
        public int RatingTypeId { get; set; }

        [Required(ErrorMessage = RatingRequired)]
        public int Rating { get; set; }

        [Required(ErrorMessage = ReportingPeriodNumberRequired)]
        public int ReportingPeriodNumber { get; set; }
    }
}