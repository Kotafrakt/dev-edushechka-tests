using DevEdu.Core.Common;
using DevEdu.Core.Exceptions;
using DevEdu.Core.Models;
using System;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class TaskData : BaseData
    {
        public static TaskByTeacherUpdateInputModel GetValidTaskByTeacherUpdateInputModel()
        {
            return new TaskByTeacherUpdateInputModel()
            {
                Name = $"Name{_random.Next(1, 1000)}",
                Description = $"Description{_random.Next(1, 1000)}",
                Links = $"Link{_random.Next(1, 1000)}",
                IsRequired = _random.Next(0, 1) == 0 ? false : true
            };
        }
        public static TaskByTeacherUpdateInputModel GetInvalidTaskByTeacherUpdateInputModel()
        {
            return new TaskByTeacherUpdateInputModel();
        }
        public static TaskByTeacherInputModel GetValidTaskByTeacherWithHomework(int groupId, List<int> tagIds = default )
        {
            if (tagIds == default)
            {
                tagIds = new List<int>();
            }
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
            if (tagIds == default)
            {
                tagIds = new List<int>();
            }
            return new TaskByTeacherInputModel()
            {
                Name = $"Name {_random.Next(1, 1000)}",
                Description = $"Description {_random.Next(1, 1000)}",
                Links = $"Link {_random.Next(1, 1000)}",
                IsRequired = _random.Next(0, 1) == 0 ? false : true,
                Tags = tagIds
            };
        }

        public static TaskByMethodistInputModel GetValidTaskByMethodist(List<int> courseIds = default, List<int> tagIds = default)
        {
            if (tagIds == default)
            {
                tagIds = new List<int>();
            }
            return new TaskByMethodistInputModel()
            {
                Name = $"Name{_random.Next(1, 1000)}",
                Description = $"Description{_random.Next(1, 1000)}",
                Links = $"Link{_random.Next(1, 1000)}",
                IsRequired = _random.Next(0, 1) == 0 ? false : true,
                Tags = tagIds,
                CourseIds = courseIds
            };
        }

        public static TaskByMethodistInputModel GetInvalidTaskByMethodist()
        {
            return new TaskByMethodistInputModel();
        }

        public static TaskByTeacherInputModel GetInvalidTaskByTeacher()
        {
            return new TaskByTeacherInputModel();
        }

        public static ValidationExceptionResponse GetValidationExceptionResponse()
        {
            var validationExceptionResponse = new ValidationExceptionResponse();
            validationExceptionResponse.Code = ValidationExceptionResponse.ValidationCode;
            validationExceptionResponse.Message = ValidationExceptionResponse.MessageValidation;
            validationExceptionResponse.AddError(422, "Name", ValidationMessage.NameRequired);
            validationExceptionResponse.AddError(422, "Description", ValidationMessage.DescriptionRequired);
            validationExceptionResponse.AddError(422, "Links", ValidationMessage.LinkToRecordIdRequired);
            return validationExceptionResponse;
        }
    }
}