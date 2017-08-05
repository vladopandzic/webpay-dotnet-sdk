using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Http;
using WebPay.Http.Interfaces;
using WebPay.Request;
using WebPay.Request.Builder;
using WebPay.Request.Builder.Interfaces;
using WebPay.Validators.Interfaces;

namespace WebPay
{
    public class Purchase : TransactionMessage
    {
        private WebPayIntegration wbpayIntegration;
        private IPaymentCommitRequestObjectBuilder requestBuilder { get; }

        public Purchase(WebPayIntegration wbpayIntegration)
        {
            this.wbpayIntegration = wbpayIntegration;
            this.requestBuilder = new PaymentCommitRequestObjectBuilder();
        }
        public Purchase(WebPayIntegration wbpayIntegration, IPaymentCommitRequestObjectBuilder requestBuilder)
        {
            this.wbpayIntegration = wbpayIntegration;
            this.requestBuilder = requestBuilder;
        }

        public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language)
        {
            var paymentRequest = requestBuilder.Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);
            return DoTransaction(paymentRequest, language, new PaymentCommitClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl));
        }
        public async Task<TransactionResult> MakeTransactionAsync(Buyer buyer, Order order, Card card, Language language)
        {
            var paymentRequest = requestBuilder.Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);
            return await DoTransactionAsync(paymentRequest, language, new PaymentCommitClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl));
        }
        public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language, IRequestValidator<PaymentCommitRequest> validator)
        {
            var paymentRequest = requestBuilder.Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);
            return DoTransaction(paymentRequest, language, validator, new PaymentCommitClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl));
        }
        public async Task<TransactionResult> MakeTransactionAsync(Buyer buyer, Order order, Card card, Language language, IRequestValidator<PaymentCommitRequest> validator)
        {
            var paymentRequest = requestBuilder.Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);
            return await DoTransactionAsync(paymentRequest, language, validator, new PaymentCommitClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl));
        }
        public TransactionResult MakeTransaction(Buyer buyer, Order order, Card card, Language language, IRequestValidator<PaymentCommitRequest> validator, IPaymentCommitClient client)
        {
            var paymentRequest = requestBuilder.Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);
            return DoTransaction(paymentRequest, language, validator, client);
        }
        public async Task<TransactionResult> MakeTransactionAsync(Buyer buyer, Order order, Card card, Language language, IRequestValidator<PaymentCommitRequest> validator, IPaymentCommitClient client)
        {
            var paymentRequest = requestBuilder.Build(buyer, order, card, wbpayIntegration, TransactionType.Purchase, language);
            return await DoTransactionAsync(paymentRequest, language, validator, client);
        }
    }
}
