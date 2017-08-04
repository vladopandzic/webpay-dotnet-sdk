using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;

namespace WebPay.Core
{
    public class PaymentChangeRequestObjectBuilder:RequestBuilder
    {
        public PaymentChangeRequest Build(decimal amount, Currency currency, string orderNumber, WebPayIntegration integration,Language language)
        {


            Request.PaymentChangeRequest paymentRequest = new PaymentChangeRequest();

            var transaction = new TransactionSimple();
            //buyer
          
            //order
            transaction.Amount = (int)amount;
            transaction.OrderNumber = orderNumber;
            transaction.Currency = currency;
            transaction.AuthenticityToken = integration.ConfigurationSettings.AuthenticityToken;
            transaction.Digest = CreateDigest(integration.ConfigurationSettings.Key, orderNumber,(int)amount, currency);
            paymentRequest.Transaction = transaction;
            return paymentRequest;

            //}
            //else
            //{
            //    return null;//privremeno
            //}

        }


    }
}
