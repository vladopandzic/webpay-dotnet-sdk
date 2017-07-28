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
        public WebPay.Response.Response<PaymentResponse> Pay(Buyer buyer,Order order,Card card)
        {


            var client = new Client();
            Request.PaymentRequest paymentRequest = new PaymentRequest();
            var transaction = new Transaction();
            transaction.Address = buyer.Address;
            transaction.Amount = order.Amount;
            transaction.AuthenticityToken = "7db11ea5d4a1af32421b564c79b946d1ead3daf0";//izvalidirat  ga
            transaction.City = buyer.City;
            transaction.Country = buyer.Country;
            transaction.Currency = order.Currency;
            transaction.CVV = card.CVV;
            transaction.Digest = "s";
            transaction.Email = buyer.Email;
            transaction.ExpirationDate = card.ExpirationDate;
            transaction.FullName = buyer.FullName;
            transaction.Ip = "sssss";
            transaction.Language = Language.EN;
            transaction.Moto = false;
            transaction.NumberOfInstallments = 0;
            transaction.OrderInfo = order.OrderInfo;
            transaction.Pan = card.Pan;
            transaction.TransactionType = TransactionType.Authorize;
            transaction.ZIP = buyer.ZIP;



            paymentRequest.Transaction = transaction;

            //Ode ide napredna validacija. Jer ako je nema, zapravo ako nisu sci parametri poslani webpay ne vraća dobar error response,
            //zapravo čini se da samo AuthenticityToken mora biti dobar
            return client.Pay(paymentRequest);
        }
    }
}
