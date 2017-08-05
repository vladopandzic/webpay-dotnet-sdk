using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Http.Interfaces;
using WebPay.Request;
using WebPay.Response;
using WebPay.Validators;
using WebPay.Validators.Interfaces;

namespace WebPay
{
    public abstract class TransactionMessage
    {
        #region Authorization and Purchase
        protected  TransactionResult DoTransaction(PaymentCommitRequest paymentRequest, Language language, IRequestValidator<PaymentCommitRequest> validator, IPaymentCommitClient paymentClient)
        {
            return DoTransactionInternal(()=>Task.FromResult(paymentClient.Send(paymentRequest)),paymentRequest, language, validator).Result;
        }
        protected Task<TransactionResult> DoTransactionAsync(PaymentCommitRequest paymentRequest, Language language, IRequestValidator<PaymentCommitRequest> validator, IPaymentCommitClient paymentClient)
        {
            return DoTransactionInternal(() => paymentClient.SendAsync(paymentRequest), paymentRequest, language, validator);
        }
        protected TransactionResult DoTransaction(PaymentCommitRequest paymentRequest, Language language, IPaymentCommitClient paymentClient)
        {
            return DoTransactionInternal(()=> Task.FromResult(paymentClient.Send(paymentRequest)),paymentRequest, language, new PaymentCommitRequestValidator()).Result;
        }

        protected  Task<TransactionResult> DoTransactionAsync(PaymentCommitRequest paymentRequest, Language language, IPaymentCommitClient paymentClient)
        {
            return DoTransactionInternal(() => paymentClient.SendAsync(paymentRequest), paymentRequest, language, new PaymentCommitRequestValidator());
        }
        #endregion

        #region Refund, Capture and Void
        protected TransactionResult DoTransaction(PaymentChangeRequest paymentRequest, Language language, IRequestValidator<PaymentChangeRequest> validator, IPaymentChangeClient paymentClient)
        {
            return DoTransactionInternal(() => Task.FromResult(paymentClient.Send(paymentRequest)), paymentRequest, language, validator).Result;
        }
        protected Task<TransactionResult> DoTransactionAsync(PaymentChangeRequest paymentRequest, Language language, IRequestValidator<PaymentChangeRequest> validator, IPaymentChangeClient paymentClient)
        {
            return DoTransactionInternal(() => paymentClient.SendAsync(paymentRequest), paymentRequest, language, validator);
        }
        protected TransactionResult DoTransaction(PaymentChangeRequest paymentRequest, Language language, IPaymentChangeClient paymentClient)
        {
            return DoTransactionInternal(() => Task.FromResult(paymentClient.Send(paymentRequest)), paymentRequest, language, new PaymentChangeRequestValidator()).Result;
        }
        protected Task<TransactionResult> DoTransactionAsync(PaymentChangeRequest paymentRequest, Language language, IPaymentChangeClient paymentClient)
        {
            return DoTransactionInternal(() => paymentClient.SendAsync(paymentRequest), paymentRequest, language, new PaymentChangeRequestValidator());
        }
        #endregion


        private async Task<TransactionResult> DoTransactionInternal<T>(Func<Task<Response<PaymentResponse,SecureMessage>>> action,T paymentRequest, Language language, IRequestValidator<T> paymentRequestValidator)
        {
            var payingResult = new TransactionResult();

            var validator = paymentRequestValidator;

            payingResult.RequestWasSuccessfullyValidated = validator == null ? true : validator.IsValid(paymentRequest);

            if (validator==null || validator.IsValid(paymentRequest))
            {

                  
                var apiResult = await action();
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
                //ValidationResult results = 
                payingResult.RequestValidationErrors = validator.DoValidation(paymentRequest).ToList();//results.Errors;

            }
            return payingResult;
        }

    }
}
