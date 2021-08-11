﻿using DevEdu.Core.Models;
using DevEdu.Tests.Data;
using System;

namespace DevEdu.Tests.Data
{
    public class TagData : BaseData
    {
        public static TagInputModel GetInvalidTagInputModel()
        {
            return new()
            {
                Name = $"Tag {DateTime.Now.ToString(_dateFormat)}",
            };
        }
    }
}