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

    public class PaymentChangeRequestValidator : AbstractValidator<PaymentChangeRequest>,IRequestValidator<PaymentChangeRequest>
    {
        public PaymentChangeRequestValidator()
        {
            
            RuleFor(x => x.Transaction).NotNull();

            RuleFor(x => x.Transaction.OrderNumber).Length(1, 40).WithErrorCode((int)PaymentRequestValidatorErrorCodes.OrderOrderNumberLength);
            RuleFor(x => x.Transaction.Amount.ToString()).Length(3, 11).WithErrorCode((int)PaymentRequestValidatorErrorCodes.OrderAmountLength);

        }
        public bool IsValid(PaymentChangeRequest instance)
        {
            return this.DoValidation(instance).Any();
        }

        public List<ValidationError> DoValidation(PaymentChangeRequest instance)
        {
            return this.Validate(instance).Errors.ToValidationErrors();
        }

      
    }
}
