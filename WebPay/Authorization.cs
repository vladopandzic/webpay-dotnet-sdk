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
    public class Authorization:TransactionMessage
    {
        private WebPayIntegration wbpayIntegration;


        public Authorization(WebPayIntegration wbpayIntegration)
        {
            this.wbpayIntegration = wbpayIntegration;
        }
        public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language)
        {

            PaymentCommitRequest paymentRequest = new PaymentCommitRequestObjectBuilder()
                                            .Build(buyer, order, card, wbpayIntegration, TransactionType.Authorize, language);

            return DoTransaction(paymentRequest, language, new PaymentCommitClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl));
        }
        public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language, IValidator<PaymentCommitRequest> validator)
        {

            PaymentCommitRequest paymentRequest = new PaymentCommitRequestObjectBuilder()
                                            .Build(buyer, order, card, wbpayIntegration, TransactionType.Authorize, language);

            return DoTransaction(paymentRequest, language, validator, new PaymentCommitClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl));
        }
        public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language, IValidator<PaymentCommitRequest> validator, IPaymentCommitClient client)
        {

            PaymentCommitRequest paymentRequest = new PaymentCommitRequestObjectBuilder()
                                            .Build(buyer, order, card, wbpayIntegration, TransactionType.Authorize, language);

            return DoTransaction(paymentRequest, language, validator, client);
        }
    }
}
