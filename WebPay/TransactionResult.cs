using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Response;

namespace WebPay
{
    public class TransactionResult
    {
        public bool Has3DSecure { get; set; }

        public bool RequestWasSuccessfullyValidated { get; set; }

        public IList<ValidationFailure> RequestValidationErrors { get; set; }

        public ErrorsResponse WebPayErrors { get; set; }

        public Response.PaymentResponse PaymentResponse { get; set; }

        public SecureMessage SecureMessage { get; set; }

    }
}
