using System;

namespace DevEdu.Core.Models
{
    public class PaymentDto:BaseDto
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public UserDto User { get; set; }
        public bool IsPaid { get; set; }        
    }
}