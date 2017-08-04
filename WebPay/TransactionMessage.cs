using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Core;
using WebPay.Interfaces;
using WebPay.Request;
using WebPay.Response;

namespace WebPay
{
    public abstract class TransactionMessage
    {

        //public PayingResult DoTransaction(PaymentRequest paymentRequest, Language language)
        //{
        //    return DoTransactionInternal(paymentRequest, language, new PaymentRequestValidator(), new PaymentClient());
        //}
        //public PayingResult DoTransaction(PaymentRequest paymentRequest, Language language, IValidator<PaymentRequest> validator)
        //{
        //    return DoTransactionInternal(paymentRequest, language, validator, new PaymentClient());
        //}
        protected TransactionResult DoTransaction(PaymentCommitRequest paymentRequest, Language language, IValidator validator, IPaymentCommitClient paymentClient)
        {
            return DoTransactionInternal(()=>paymentClient.Pay(paymentRequest),paymentRequest, language, validator);
        }
        protected TransactionResult DoTransaction(PaymentCommitRequest paymentRequest, Language language, IPaymentCommitClient paymentClient)
        {
            return DoTransactionInternal(()=> paymentClient.Pay(paymentRequest),paymentRequest, language, new PaymentCommitRequestValidator());
        }
        protected TransactionResult DoTransaction(PaymentChangeRequest paymentRequest, Language language, IValidator validator, IPaymentChangeClient paymentClient)
        {
            return DoTransactionInternal(() => paymentClient.Pay(paymentRequest), paymentRequest, language, validator);
        }
        //public TransactionResult DoTransaction(PaymentChangeRequest paymentRequest, Language language, IPaymentChangeClient paymentClient)
        //{
        //    return DoTransactionInternal(() => paymentClient.Pay(paymentRequest), paymentRequest, language, new PaymentChangeRequestValidator(), paymentClient);
        //}

        private TransactionResult DoTransactionInternal(Func<Response<PaymentResponse,SecureMessage>> action,object paymentRequest, Language language, IValidator paymentRequestValidator)
        {
            var payingResult = new TransactionResult();

            var validator = paymentRequestValidator;

            payingResult.RequestWasSuccessfullyValidated = validator == null ? true : validator.Validate(paymentRequest).IsValid;

            if (validator==null || validator.Validate(paymentRequest).IsValid)
            {
              

                var apiResult = action();// paymentClient.Pay(paymentRequest);
                if (apiResult?.Data != null)
                {
                    payingResult.PaymentResponse = apiResult.Data;

                }
                else if (apiResult?.AlternativeData != null)
                {
                    payingResult.SecureMessage = apiResult.AlternativeData;
                    payingResult.Has3DSecure = true;

                }
                else
                {
                    payingResult.WebPayErrors = apiResult?.Errors;
                }
            }
            else
            {
                ValidationResult results = validator.Validate(paymentRequest);
                payingResult.RequestValidationErrors = results.Errors;

            }
            return payingResult;
        }

    }
}
