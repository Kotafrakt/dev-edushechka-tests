using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Core.Models
{
    public class StudentAnswerOnTaskOutputModel
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public string CompletedDate { get; set; }
    }
}
