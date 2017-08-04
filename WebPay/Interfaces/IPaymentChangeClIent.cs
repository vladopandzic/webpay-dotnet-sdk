using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;
using WebPay.Response;

namespace WebPay.Interfaces
{
    public interface IPaymentChangeClient
    {
        Response<PaymentResponse, SecureMessage> Pay(PaymentChangeRequest paymentRequest);
    }
}
