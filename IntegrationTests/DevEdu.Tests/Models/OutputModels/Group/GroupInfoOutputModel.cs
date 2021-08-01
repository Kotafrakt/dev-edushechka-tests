﻿using System.ComponentModel;

namespace DevEdu.Tests.Models
{
    public class GroupInfoOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}