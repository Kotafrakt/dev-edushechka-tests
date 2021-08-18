using DevEdu.Core.Common;
using DevEdu.Core.Exceptions;
using DevEdu.Core.Models;
using System;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class TagData : BaseData
    {
        public static TagInputModel GetValidTagInputModel()
        {
            return new()
            {
                Name = $"Tag {DateTime.Now.ToString(_dateFormat)}",
            };
        }

        public static TagInputModel GetTagInputModel_UpdatedModel()
        {
            return new()
            {
                Name = "Zloo is bad"
            };
        }

        public static TagInputModel GetInValidTagInputModel()
        {
            return new();
        }

        public static ValidationExceptionResponse GetValidationExceptionResponse()
        {
            return new ValidationExceptionResponse
            {
                Code = ValidationExceptionResponse.ValidationCode,
                Message = ValidationExceptionResponse.MessageValidation,
                Errors = new List<ValidationError>
                {
                    new ValidationError
                    {
                        Code = 422,
                        Field = nameof(TagInputModel.Name),
                        Message = ValidationMessage.NameRequired
                    }
                }
            };
        }
    } 
} 