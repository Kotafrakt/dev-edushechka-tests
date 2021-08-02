using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Core.Models.Lesson
{
    public class FeedbackOutputModel
    {
        public string Feedback { get; set; }
        public UserInfoShortOutputModel User { get; set; }

    }
}
