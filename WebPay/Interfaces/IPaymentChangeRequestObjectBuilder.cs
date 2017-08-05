using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;

namespace WebPay.Interfaces
{
    public interface IPaymentChangeRequestObjectBuilder
    {
        PaymentChangeRequest Build(decimal amount, Currency currency, string orderNumber, WebPayIntegration integration, Language language);
    }
}
