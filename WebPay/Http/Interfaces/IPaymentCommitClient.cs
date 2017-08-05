using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;
using WebPay.Response;

namespace WebPay.Http.Interfaces
{
    public interface IPaymentCommitClient
    {
        Response<PaymentResponse, SecureMessage> Send(PaymentCommitRequest paymentRequest);

        Task<Response<PaymentResponse, SecureMessage>> SendAsync(PaymentCommitRequest paymentRequest);
    }
}
