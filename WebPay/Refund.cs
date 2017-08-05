using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Core;
using WebPay.Exceptions;
using WebPay.Interfaces;
using WebPay.Request;

namespace WebPay
{
    public class Refund : TransactionMessage
    {
        private WebPayIntegration wbpayIntegration;
        private IPaymentChangeRequestObjectBuilder requestBuilder { get;  }

        public Refund(WebPayIntegration wbpayIntegration)
        {
            this.wbpayIntegration = wbpayIntegration;
            this.requestBuilder = new PaymentChangeRequestObjectBuilder();
        }
        public Refund(WebPayIntegration wbpayIntegration, IPaymentChangeRequestObjectBuilder requestBuilder)
        {
            this.wbpayIntegration = wbpayIntegration;
            this.requestBuilder = requestBuilder;
        }

        public TransactionResult MakeTransaction(decimal amount, Currency currency, string orderNumber, Language language)
        {
            var paymentRequest = requestBuilder .Build(amount, currency, orderNumber, wbpayIntegration, language);
            return DoTransaction(paymentRequest, language, new PaymentChangeRequestValidator(), new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl, TransactionType.Refund));
        }
        public async Task<TransactionResult> MakeTransactionAsync(decimal amount, Currency currency, string orderNumber, Language language)
        {
            var paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return await DoTransactionAsync(paymentRequest, language, new PaymentChangeRequestValidator(), new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl, TransactionType.Refund));
        }
        public TransactionResult MakeTransaction(decimal amount, Currency currency, string orderNumber, Language language, IRequestValidator<PaymentChangeRequest> validator)
        {
            var paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return DoTransaction(paymentRequest, language, validator, new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl, TransactionType.Refund));
        }
        public async Task<TransactionResult> MakeTransactionAsync(decimal amount, Currency currency, string orderNumber, Language language, IRequestValidator<PaymentChangeRequest> validator)
        {
            var paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return await DoTransactionAsync(paymentRequest, language, validator, new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl, TransactionType.Refund));
        }
        public TransactionResult MakeTransaction(decimal amount, Currency currency, string orderNumber, Language language, IRequestValidator<PaymentChangeRequest> validator, IPaymentChangeClient client)
        {
            if (client.transactionType != TransactionType.Refund)
            {
                throw new TransactionTypeMismatchException("Capture transation cannot accept client that was initialized with transactionType" + client.transactionType);
            }
            var paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return DoTransaction(paymentRequest, language, validator, client);
        }
        public async Task<TransactionResult> MakeTransactionAsync(decimal amount, Currency currency, string orderNumber, Language language, IRequestValidator<PaymentChangeRequest> validator, IPaymentChangeClient client)
        {
            if (client.transactionType != TransactionType.Refund)
            {
                throw new TransactionTypeMismatchException("Capture transation cannot accept client that was initialized with transactionType" + client.transactionType);
            }
            var paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return await DoTransactionAsync(paymentRequest, language, validator, client);
        }
    }
}
