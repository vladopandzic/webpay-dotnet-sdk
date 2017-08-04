﻿using FluentValidation;
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


        public Refund(WebPayIntegration wbpayIntegration)
        {
            this.wbpayIntegration = wbpayIntegration;
        }
        public TransactionResult MakeTransaction(decimal amount, Currency currency, string orderNumber, Language language)
        {

            PaymentChangeRequest paymentRequest = new PaymentChangeRequestObjectBuilder()
                                            .Build(amount, currency, orderNumber, wbpayIntegration, language);

            return DoTransaction(paymentRequest, language, new PaymentChangeRequestValidator(), new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl, TransactionType.Refund));
        }
        public TransactionResult MakeTransaction(decimal amount, Currency currency, string orderNumber, Language language, IValidator<PaymentChangeRequest> validator)
        {

            PaymentChangeRequest paymentRequest = new PaymentChangeRequestObjectBuilder()
                                             .Build(amount, currency, orderNumber, wbpayIntegration, language);

            return DoTransaction(paymentRequest, language, validator, new PaymentChangeClient(wbpayIntegration.ConfigurationSettings.WebPayRootUrl, TransactionType.Refund));
        }
        public TransactionResult MakeTransaction(decimal amount, Currency currency, string orderNumber, Language language, IValidator<PaymentCommitRequest> validator, IPaymentChangeClient client)
        {
            if (client.transactionType != TransactionType.Refund)
            {
                throw new TransactionTypeMismatchException("Capture transation cannot accept client that was initialized with transactionType" + client.transactionType);
            }
            PaymentChangeRequest paymentRequest = new PaymentChangeRequestObjectBuilder()
                                             .Build(amount, currency, orderNumber, wbpayIntegration, language);


            return DoTransaction(paymentRequest, language, validator, client);
        }
    }
}