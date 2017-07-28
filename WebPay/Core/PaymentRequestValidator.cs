using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Interfaces;
using WebPay.Request;

namespace WebPay.Core
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>,IPaymentRequestValidator
    {
        public PaymentRequestValidator()
        {
            
            RuleFor(x => x.Transaction).NotNull();
            RuleFor(x => x.Transaction.FullName).Length(3, 30).WithErrorCode((int)AdriagateValidatorErrorCodes.BuyerFullNameInvalidLength);
        }
        

        //public bool IsValid(Buyer buyer, Card card, Order order)
        //{
            
        //    return true;
        //}
    }
}
