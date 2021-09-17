using DevEdu.Core.Models;
using System;

namespace DevEdu.Tests.Data
{
    public class LessonData : BaseData
    {
        public static LessonInputModel GetValidLessonInputModel(int teacherId)
        {
            string _dateFormat1 = "dd.MM.yyyy";
            return new()
            {
                Date = DateTime.Now.ToString(_dateFormat1),
                TeacherComment = $"Comment {_random.Next(1, 1000)}",
                TeacherId = teacherId
            };
        }
    }
}