﻿using DevEdu.Core.Models;
using System;

namespace DevEdu.Tests.Data
{
    public class CourseData : BaseData
    {
        public static CourseInputModel GetInvalidCourseInputModel()
        {
            return new()
            {
                Name = $"Course {DateTime.Now.ToString(_dateFormat)}",
                Description = "Negodyai back"
            };
        }
    }
}