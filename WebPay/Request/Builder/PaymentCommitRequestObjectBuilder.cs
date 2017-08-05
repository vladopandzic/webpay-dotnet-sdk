using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebPay.Helpers;
using WebPay.Request;
using WebPay.Request.Builder.Interfaces;

namespace WebPay.Request.Builder
{
    public class PaymentCommitRequestObjectBuilder:RequestBuilder,IPaymentCommitRequestObjectBuilder
    {
       

        public PaymentCommitRequest Build(Buyer buyer, Order order, Card card, WebPayIntegration integration,TransactionType trancationType,Language language)
        {
          

            Request.PaymentCommitRequest paymentRequest = new PaymentCommitRequest();

            var transaction = new Transaction();
            //buyer
            transaction.FullName = buyer.FullName;
            transaction.Address = buyer.Address;
            transaction.City = buyer.City;
            transaction.Country = buyer.Country;
            transaction.Email = buyer.Email;
            transaction.ZIP = buyer.ZIP;
            transaction.Phone = buyer.Phone;
            //card
            transaction.Pan = card.Pan;
            transaction.CVV = card.CVV;
            transaction.ExpirationDate = card.ExpirationDate;
            //order
            transaction.Amount = order.Amount;
            transaction.OrderNumber = order.OrderNumber;
            transaction.OrderInfo = order.OrderInfo;
            transaction.Currency = order.Currency;
            //processing data
            transaction.Ip =NetworkHelper.GetIPAddress();
            transaction.Language = language;
            transaction.TransactionType = trancationType;
            transaction.AuthenticityToken = integration.ConfigurationSettings.AuthenticityToken;
            transaction.Digest = CreateDigest(integration.ConfigurationSettings.Key,order.OrderNumber,order.Amount,order.Currency);
            transaction.NumberOfInstallments = 2;
            transaction.Moto = false;

            paymentRequest.Transaction = transaction;
            return paymentRequest;

            //}
            //else
            //{
            //    return null;//privremeno
            //}

        }

       
    }
}
