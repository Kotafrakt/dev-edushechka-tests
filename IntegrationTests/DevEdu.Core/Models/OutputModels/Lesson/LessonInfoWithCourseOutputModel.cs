namespace DevEdu.Core.Models
{
    public class LessonInfoWithCourseOutputModel : LessonInfoOutputModel
    {
        public CourseInfoShortOutputModel Course { get; set; }
    }
}