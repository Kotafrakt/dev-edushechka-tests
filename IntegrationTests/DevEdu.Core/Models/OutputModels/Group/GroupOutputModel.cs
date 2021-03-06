using DevEdu.Core.Enums;

namespace DevEdu.Core.Models
{
    public class GroupOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseInfoBaseOutputModel Course { get; set; }
        public GroupStatus GroupStatus { get; set; }
        public string StartDate { get; set; }
        public string Timetable { get; set; }
        public decimal PaymentPerMonth { get; set; }
    }
}