using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class PaymentControllerSub
    {
        private PaymentControllerCreator _creator;
        public PaymentControllerSub() { _creator = new PaymentControllerCreator(); }
    }
}