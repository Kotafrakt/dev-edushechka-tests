using DevEdu.Core.Models;
using System;

namespace DevEdu.Tests.Data
{
    public class HomeworkData : BaseData
    {
        public static StudentHomeworkInputModel GetValidStudentHomeworkInputModel()
        {
            return new StudentHomeworkInputModel()
            {
                Answer = "Answer" + DateTime.Now.ToString(_dateFormat)
            };
        }
    }
}