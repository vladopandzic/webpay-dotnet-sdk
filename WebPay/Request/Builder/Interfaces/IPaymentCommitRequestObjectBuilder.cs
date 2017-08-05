using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;

namespace WebPay.Request.Builder.Interfaces
{
    public interface IPaymentCommitRequestObjectBuilder
    {
        PaymentCommitRequest Build(Buyer buyer, Order order, Card card, WebPayIntegration integration, TransactionType trancationType, Language language);
    }
}
