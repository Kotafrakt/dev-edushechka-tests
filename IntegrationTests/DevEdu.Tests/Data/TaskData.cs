using DevEdu.Core.Models;
using System;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class TaskData : BaseData
    {
        public static TaskByTeacherInputModel GetValidTaskByTeacherWithHomework(int groupId, List<int> tagIds = default )
        {
            return new TaskByTeacherInputModel()
            {
                Name = $"Name{_random.Next(1, 1000)}",
                Description = $"Description{_random.Next(1, 1000)}",
                Links = $"Link{_random.Next(1, 1000)}",
                IsRequired = _random.Next(0, 1) == 0 ? false : true,
                GroupId = groupId,
                Tags = tagIds,
                Homework = new HomeworkInputModel()
                {
                    StartDate = DateTime.Now.ToString(_validDateFormat),
                    EndDate = DateTime.Now.AddDays(_random.Next(1, 20)).ToString(_validDateFormat),
                }
            };
        }

        public static TaskByTeacherInputModel GetValidTaskByTeacherWithoutHomework(List<int> tagIds = default)
        {
            return new TaskByTeacherInputModel()
            {
                Name = $"Name {_random.Next(1, 1000)}",
                Description = $"Description {_random.Next(1, 1000)}",
                Links = $"Link {_random.Next(1, 1000)}",
                IsRequired = _random.Next(0, 1) == 0 ? false : true,
                Tags = tagIds
            };
        }
    }
}