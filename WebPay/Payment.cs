using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Core;
using WebPay.Request;
using WebPay.Response;

namespace WebPay
{
    public class Payment
    {
        private WebPayIntegration wbpayIntegration;

        public Payment(WebPayIntegration wbpayIntegration)
        {
            this.wbpayIntegration = wbpayIntegration;
        }

        public WebPay.Response.Response<PaymentResponse> Pay(Buyer buyer, Order order, Card card)
        {
            var validator = new PaymentRequestValidator();
            PaymentRequestObjectBuilder paymentRequestBuilder = new PaymentRequestObjectBuilder(validator);
            var paymentRequest = paymentRequestBuilder.Build(buyer, order, card, wbpayIntegration);

            if (validator.Validate(paymentRequest).IsValid)
            {

                var client = new Client();
                return client.Pay(paymentRequest);
            }
            else {
                ValidationResult results = validator.Validate(paymentRequest);
                return null;//privremeno
            }
        }
    }
}
