using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Http.Interfaces;
using WebPay.Request;

namespace WebPay.Validators.Interfaces
{
    public interface ITransactionMessage
    {
         TransactionResult DoTransaction(PaymentCommitRequest paymentRequest, Language language, IValidator validator, IPaymentCommitClient paymentClient);

        TransactionResult DoTransaction(PaymentCommitRequest paymentRequest, Language language, IPaymentCommitClient paymentClient);

        TransactionResult DoTransaction(PaymentChangeRequest paymentRequest, Language language, IValidator validator, IPaymentChangeClient paymentClient);

        TransactionResult DoTransaction(PaymentChangeRequest paymentRequest, Language language, IPaymentChangeClient paymentClient);

    }
}
