﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Tests.Models
{
    public class StudentAnswerOnTaskFullOutputModel : StudentAnswerOnTaskOutputModel
    {
        public UserInfoShortOutputModel User { get; set; }
    }
}
