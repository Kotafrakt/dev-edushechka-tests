using System;

namespace DevEdu.Core.Exceptions
{
    public class ValidationError
    {
        public int Code { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }
    }
}