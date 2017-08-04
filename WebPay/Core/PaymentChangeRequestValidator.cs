using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;

namespace WebPay.Core
{
   
    public class PaymentChangeRequestValidator : AbstractValidator<PaymentChangeRequest>
    {
        public PaymentChangeRequestValidator()
        {

            RuleFor(x => x.Transaction).NotNull();
           
            RuleFor(x => x.Transaction.OrderNumber).Length(1, 40).WithErrorCode((int)PaymentRequestValidatorErrorCodes.OrderOrderNumberLength);
            RuleFor(x => x.Transaction.Amount.ToString()).Length(3, 11).WithErrorCode((int)PaymentRequestValidatorErrorCodes.OrderAmountLength);

        }


    }
}
