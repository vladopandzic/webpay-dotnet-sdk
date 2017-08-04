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
    public class PaymentCommitRequestValidator : AbstractValidator<PaymentCommitRequest>
    {
        public PaymentCommitRequestValidator()
        {
            
            RuleFor(x => x.Transaction).NotNull();
            RuleFor(x => x.Transaction.FullName).Length(3, 30).WithErrorCode((int)PaymentRequestValidatorErrorCodes.BuyerFullNameInvalidLength);
            RuleFor(x => x.Transaction.Address).Length(3, 100).WithErrorCode((int)PaymentRequestValidatorErrorCodes.BuyerAddressLength);
            RuleFor(x => x.Transaction.City).Length(3, 30).WithErrorCode((int)PaymentRequestValidatorErrorCodes.BuyerCityLength);
            RuleFor(x => x.Transaction.ZIP).Length(3, 9).WithErrorCode((int)PaymentRequestValidatorErrorCodes.BuyerZIPLength);
            RuleFor(x => x.Transaction.Phone).Length(3, 30).WithErrorCode((int)PaymentRequestValidatorErrorCodes.BuyerPhoneLength);
            RuleFor(x => x.Transaction.Email).Length(3, 100).WithErrorCode((int)PaymentRequestValidatorErrorCodes.BuyerEmailLength).EmailAddress().WithErrorCode((int)PaymentRequestValidatorErrorCodes.BuyerEmail);

            RuleFor(x => x.Transaction.Pan).Length(14, 19).WithErrorCode((int)PaymentRequestValidatorErrorCodes.CardPanLength).CreditCard().WithErrorCode((int)PaymentRequestValidatorErrorCodes.CardPan);
            RuleFor(x => x.Transaction.CVV.ToString()).Length(3, 4).WithErrorCode((int)PaymentRequestValidatorErrorCodes.CardCVVLength);
            RuleFor(x => x.Transaction.ExpirationDate).Length(4).WithErrorCode((int)PaymentRequestValidatorErrorCodes.CardExpirationDate);

            RuleFor(x => x.Transaction.OrderInfo).Length(3, 100).WithErrorCode((int)PaymentRequestValidatorErrorCodes.OrderOrderInfoLength);
            RuleFor(x => x.Transaction.OrderNumber).Length(1, 40).WithErrorCode((int)PaymentRequestValidatorErrorCodes.OrderOrderNumberLength);
            RuleFor(x => x.Transaction.Amount.ToString()).Length(3, 11).WithErrorCode((int)PaymentRequestValidatorErrorCodes.OrderAmountLength);

            RuleFor(x => x.Transaction.Ip).Length(7, 15).WithErrorCode((int)PaymentRequestValidatorErrorCodes.ProcessingDataIpLength);
            RuleFor(x => x.Transaction.NumberOfInstallments).InclusiveBetween(2, 12).WithErrorCode((int)PaymentRequestValidatorErrorCodes.ProcessingDataNumberOfInstallmentsRange);

        }
        

    }
}
