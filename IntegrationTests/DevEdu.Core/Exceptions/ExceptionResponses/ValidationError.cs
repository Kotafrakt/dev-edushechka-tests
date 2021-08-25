using System;

namespace DevEdu.Core.Exceptions
{
    public class ValidationError
    {
        public int Code { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ValidationError error &&
                   Code == error.Code &&
                   Field == error.Field &&
                   Message == error.Message;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Field, Message);
        }
    }
}