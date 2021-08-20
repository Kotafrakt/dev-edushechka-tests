namespace DevEdu.Tests.Constants
{
    public class PaymentEndpoints
    {
        public const string GetPaymentEndpoint = "api/payment/{0}";
        public const string SelectAllPaymentsByUserIdEndpoint = "api/payment/user/{0}";
        public const string AddPaymentEndpoint = "api/payment";
        public const string DeletePaymentEndpoint = "api/payment/{0}";
        public const string UpdatePaymentEndpoint = "api/payment/{0}";
        public const string AddPaymentsEndpoint = "api/payment/bulk";
    }
}