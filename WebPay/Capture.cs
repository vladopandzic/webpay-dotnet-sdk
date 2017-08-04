using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Core;
using WebPay.Request;

namespace WebPay
{
    public class Capture:TransactionMessage
    {
        private WebPayIntegration wbpayIntegration;


        public Capture(WebPayIntegration wbpayIntegration)
        {
            this.wbpayIntegration = wbpayIntegration;
        }
        public TransactionResult MakeTransaction(decimal amount,Currency currency,string orderNumber, Language language)
        {

            PaymentChangeRequest paymentRequest = new PaymentChangeRequestObjectBuilder()
                                            .Build(amount,currency,orderNumber, wbpayIntegration, language);
            
            return DoTransaction(paymentRequest, language,null, new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl,TransactionType.Capture));
        }
        //public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language, IValidator<PaymentCommitRequest> validator)
        //{

        //    PaymentCommitRequest paymentRequest = new PaymentCommitRequestObjectBuilder()
        //                                    .Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);

        //    return DoTransaction(paymentRequest, language, validator, new PaymentCommitClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl));
        //}
        //public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language, IValidator<PaymentCommitRequest> validator, IPaymentCommitClient client)
        //{

        //    PaymentCommitRequest paymentRequest = new PaymentCommitRequestObjectBuilder()
        //                                    .Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);

        //    return DoTransaction(paymentRequest, language, validator, client);
        //}
    }
}
