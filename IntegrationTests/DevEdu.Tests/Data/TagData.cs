using DevEdu.Core.Models;
using DevEdu.Tests.Data;
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
    }
}