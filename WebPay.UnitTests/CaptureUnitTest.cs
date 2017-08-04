using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Core;
using WebPay.Interfaces;
using WebPay.Request;

namespace WebPay.UnitTests
{
    [TestClass]
    public class CaptureUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(WebPay.Exceptions.TransactionTypeMismatchException))]
        public void Should_Throw_Exception_If_Custom_Client_Doesnt_have_Capture_TransactionType() {

            WebPayIntegration wpi = new WebPayIntegration(new Configuration
            {
                AuthenticityToken = "7db11ea5d4a1af32421b564c79b946d1ead3daf0",
                Key = null,
                WebPayRootUrl = null
            });
            Buyer buyer; Order order; Card card;
            PrepareData(out buyer, out order, out card);
            
            Mock<IPaymentChangeClient> paymentClientMock = new Mock<IPaymentChangeClient>();
            paymentClientMock.Setup(x => x.transactionType).Returns(TransactionType.Purchase);
            PaymentCommitRequestObjectBuilder rb = new PaymentCommitRequestObjectBuilder();

            new Capture(wpi).MakeTransaction(20.0m, Currency.EUR, "s", Language.EN, null, paymentClientMock.Object);
        }

        private static void PrepareData(out Buyer buyer, out Order order, out Card card)
        {
            buyer = new Buyer();
            buyer.FullName = "John Doe";
            buyer.City = "Knoxville";
            buyer.Address = "Elm street 22";
            buyer.Country = "Tennessee";
            buyer.Email = "email@example.com";
            buyer.Phone = "phone";
            buyer.ZIP = "123456789";

            order = new Order();
            order.Amount = 54321;
            order.OrderNumber = "abcdef";
            order.OrderInfo = "1 x SnowMaster 3000";

            card = new Card();
            card.CVV = 265;
            card.Pan = "4111111111111111";
            card.ExpirationDate = "1811";
        }
    }
}
