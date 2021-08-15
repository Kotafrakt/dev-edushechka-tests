using DevEdu.Core.Models;
using DevEdu.Tests.Data;
using System.Collections.Generic;

namespace DevEdu.Tests.Creators
{
    public class PaymentControllerCreator : BaseControllerCreator
    {
        public PaymentOutputModel AddPayment(string token)
        {
            var model = new PaymentInputModel();
            return new PaymentOutputModel();
        }

        public PaymentOutputModel UpdatePayment(string token, int paymentId)
        {
            var model = new PaymentUpdateInputModel();
            return new PaymentOutputModel();
        }

        public List<PaymentOutputModel> AddPayments(string token)
        {
            var model = new List<PaymentInputModel>();
            return new List<PaymentOutputModel>();
        }
    }
}