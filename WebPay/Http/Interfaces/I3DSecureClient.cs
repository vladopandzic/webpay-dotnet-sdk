using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;
using WebPay.Response;

namespace WebPay.Http.Interfaces
{
    public interface I3DSecureClient
    {
        Response<PaymentResponse> FinishTransaction(SecureMessageRequest request);

        Task<Response<PaymentResponse>> FinishTransactionAsync(SecureMessageRequest request);
    }
}
