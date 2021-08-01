﻿using System.Collections.Generic;
using System.ComponentModel;

namespace DevEdu.Tests.Models
{
    public class MaterialInfoOutputModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public List<TagOutputModel> Tags { get; set; }
    }
}