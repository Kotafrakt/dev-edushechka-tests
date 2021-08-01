namespace DevEdu.Tests.Models
{
    public class LessonInfoWithCourseOutputModel : LessonInfoOutputModel
    {
        public CourseInfoShortOutputModel Course { get; set; }
    }
}