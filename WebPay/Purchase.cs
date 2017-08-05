using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Core;
using WebPay.Interfaces;
using WebPay.Request;

namespace WebPay
{
    public class Purchase:TransactionMessage
    {
        private WebPayIntegration wbpayIntegration;
    

        public Purchase(WebPayIntegration wbpayIntegration)
        {
            this.wbpayIntegration = wbpayIntegration;
        }
        public TransactionResult MakeTransaction(Buyer buyer,Order order,Card card,Language language) {

            PaymentCommitRequest paymentRequest = new PaymentCommitRequestObjectBuilder()
                                            .Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);

          return DoTransaction(paymentRequest, language,new PaymentCommitClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl));
        }
        public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language, IRequestValidator<PaymentCommitRequest> validator)
        {

            PaymentCommitRequest paymentRequest = new PaymentCommitRequestObjectBuilder()
                                            .Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);

            return DoTransaction(paymentRequest, language,validator, new PaymentCommitClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl));
        }
        public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language, IRequestValidator<PaymentCommitRequest> validator,IPaymentCommitClient client)
        {

            PaymentCommitRequest paymentRequest = new PaymentCommitRequestObjectBuilder()
                                            .Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);

            return DoTransaction(paymentRequest, language, validator, client);
        }
    }
}
