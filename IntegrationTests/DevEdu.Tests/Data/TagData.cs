using DevEdu.Core.Models;
using System;

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
    }
}