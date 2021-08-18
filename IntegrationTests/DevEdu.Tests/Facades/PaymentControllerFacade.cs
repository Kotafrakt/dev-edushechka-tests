using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class PaymentControllerFacade
    {
        private PaymentCreator _creator;
        public PaymentControllerFacade() { _creator = new PaymentCreator(); }
    }
}