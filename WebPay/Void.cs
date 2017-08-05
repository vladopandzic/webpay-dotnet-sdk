using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Exceptions;
using WebPay.Http;
using WebPay.Http.Interfaces;
using WebPay.Request;
using WebPay.Request.Builder;
using WebPay.Request.Builder.Interfaces;
using WebPay.Validators;
using WebPay.Validators.Interfaces;

namespace WebPay
{
    public class Void:TransactionMessage
    {
        private WebPayIntegration wbpayIntegration;
        private IPaymentChangeRequestObjectBuilder requestBuilder { get;  }

        public Void(WebPayIntegration wbpayIntegration)
        {
            this.wbpayIntegration = wbpayIntegration;
            this.requestBuilder = new PaymentChangeRequestObjectBuilder();
        }
        public Void(WebPayIntegration wbpayIntegration, IPaymentChangeRequestObjectBuilder requestBuilder)
        {
            this.wbpayIntegration = wbpayIntegration;
            this.requestBuilder = requestBuilder;
        }

        public TransactionResult MakeTransaction(decimal amount,Currency currency,string orderNumber, Language language)
        {
            PaymentChangeRequest paymentRequest = requestBuilder.Build(amount,currency,orderNumber, wbpayIntegration, language);        
            return DoTransaction(paymentRequest, language,new  PaymentChangeRequestValidator(), new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl,TransactionType.Void));
        }
        public async Task<TransactionResult> MakeTransactionAsync(decimal amount, Currency currency, string orderNumber, Language language)
        {
            PaymentChangeRequest paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return await DoTransactionAsync(paymentRequest, language, new PaymentChangeRequestValidator(), new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl, TransactionType.Void));
        }
        public TransactionResult MakeTransaction(decimal amount, Currency currency, string orderNumber, Language language, IRequestValidator<PaymentChangeRequest> validator)
        {
            PaymentChangeRequest paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return DoTransaction(paymentRequest, language, validator, new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl,TransactionType.Void));
        }
        public async Task<TransactionResult> MakeTransactionAsync(decimal amount, Currency currency, string orderNumber, Language language, IRequestValidator<PaymentChangeRequest> validator)
        {
            PaymentChangeRequest paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return await DoTransactionAsync(paymentRequest, language, validator, new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl, TransactionType.Void));
        }
        public TransactionResult MakeTransaction(decimal amount, Currency currency, string orderNumber, Language language, IRequestValidator<PaymentChangeRequest> validator,IPaymentChangeClient client)
        {
            if (client.transactionType != TransactionType.Void) {
                throw new TransactionTypeMismatchException("Void transation cannot accept client that was initialized with transactionType"+client.transactionType);
            }
            PaymentChangeRequest paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return DoTransaction(paymentRequest, language, validator, client);
        }
        public async Task<TransactionResult> MakeTransactionAsync(decimal amount, Currency currency, string orderNumber, Language language, IRequestValidator<PaymentChangeRequest> validator, IPaymentChangeClient client)
        {
            if (client.transactionType != TransactionType.Void)
            {
                throw new TransactionTypeMismatchException("Void transation cannot accept client that was initialized with transactionType" + client.transactionType);
            }
            PaymentChangeRequest paymentRequest = requestBuilder.Build(amount, currency, orderNumber, wbpayIntegration, language);
            return await DoTransactionAsync(paymentRequest, language, validator, client);
        }
    }
}
