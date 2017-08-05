using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;

namespace WebPay.Interfaces
{
    public interface IPaymentCommitRequestObjectBuilder
    {
        PaymentCommitRequest Build(Buyer buyer, Order order, Card card, WebPayIntegration integration, TransactionType trancationType, Language language);
    }
}
