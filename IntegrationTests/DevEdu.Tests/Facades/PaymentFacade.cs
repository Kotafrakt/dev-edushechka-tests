using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class PaymentFacade
    {
        private PaymentCreator _creator;
        public PaymentFacade() { _creator = new PaymentCreator(); }
    }
}