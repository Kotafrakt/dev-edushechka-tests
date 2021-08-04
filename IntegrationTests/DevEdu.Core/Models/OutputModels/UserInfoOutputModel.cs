﻿using System.Collections.Generic;
using DevEdu.Core.Enums;

namespace DevEdu.Core.Models
{
    public class UserInfoOutPutModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public List<Role> Roles { get; set; }
    }
}