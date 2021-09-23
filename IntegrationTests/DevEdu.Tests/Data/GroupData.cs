using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using System;

namespace DevEdu.Tests.Data
{
    public class GroupData : BaseData
    {
        public static GroupInputModel GetValidGroup(int courseId, GroupStatus status = GroupStatus.Learning)
        {
            return new GroupInputModel()
            {
                Name = $"Name {_random.Next(0, 1000)}",
                CourseId = courseId,
                GroupStatusId = status,
                PaymentPerMonth =  _random.Next(2, 6)*500,
                StartDate = DateTime.Now.AddDays(_random.Next(-60, -10)).ToString(_validDateFormat),
                Timetable = $"Timetable {_random.Next(0, 1000)}"
            };
        }
    }
}