using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebPay.Interfaces;
using WebPay.Request;

namespace WebPay.Core
{
    public class PaymentRequestObjectBuilder
    {
        private IPaymentRequestValidator validator;

        public PaymentRequestObjectBuilder(IPaymentRequestValidator validator)
        {
            this.validator = validator;
        }
        public PaymentRequest Build(Buyer buyer, Order order, Card card, WebPayIntegration integration)
        {
            // an example of some validation
            //if (validator.IsValid(buyer, card, order))
            //{

            Request.PaymentRequest paymentRequest = new PaymentRequest();

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
            transaction.OrderNumber = "45445";
            transaction.OrderInfo = order.OrderInfo;
            transaction.Currency = order.Currency;
            //processing data
            transaction.Ip = "229.129.132.3";
            transaction.Language = Language.EN;
            transaction.TransactionType = TransactionType.Authorize;
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

        private string CreateDigest(string key, string orderNumber, int amount, Currency currency)
        {
            var hashBase = string.Format("{0}{1}{2}{3}", key, orderNumber, amount, currency.ToString());
            return CalculateHash(hashBase);//vraća hashBase kriptiran "
        }
        private string CalculateHash(string _hash_base)
        {
            UTF8Encoding Utf8enc = new UTF8Encoding();
            Byte[] ByteSourceText = Utf8enc.GetBytes(_hash_base);
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();

            //Make hash
            Byte[] ByteHash = SHA1.ComputeHash(ByteSourceText);

            StringBuilder sb = new StringBuilder(64);

            for (int i = 0; i < ByteHash.Length; i++)
                sb.Append(String.Format("{0:x2}", ByteHash[i]));

            return sb.ToString();
        }
    }
}
