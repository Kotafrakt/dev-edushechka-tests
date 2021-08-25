using System;

namespace DevEdu.Core.Exceptions
{
    public class ExceptionResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ExceptionResponse response &&
                   Code == response.Code &&
                   Message == response.Message &&
                   Description == response.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Message, Description);
        }
    }
}