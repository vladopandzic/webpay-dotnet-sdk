using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Http.Interfaces;
using WebPay.Request;
using WebPay.Request.Builder.Interfaces;

namespace WebPay.UnitTests
{
    [TestClass]
    public class AuthorizationUnitTests
    {
        [TestMethod]
        public void Should_Have_Trancation_Type_Authorize() {

            WebPayIntegration wpi = new WebPayIntegration(new Configuration
            {
                AuthenticityToken = "7db11ea5d4a1af32421b564caaa946d1ead3daf0",
                Key = null,
                WebPayRootUrl = null
            });
            Buyer buyer; Order order; Card card;
            PrepareData(out buyer, out order, out card);
           
            Mock<IPaymentCommitClient> pc = new Mock<IPaymentCommitClient>();
            Mock<IPaymentCommitRequestObjectBuilder> pcr = new Mock<IPaymentCommitRequestObjectBuilder>();
            Authorization auth = new Authorization(wpi,pcr.Object);
            auth.MakeTransaction(buyer, order, card, Language.EN, null, pc.Object);
            pcr.Verify(x => x.Build(buyer, order, card, wpi,It.Is<TransactionType>(p=>p.HasFlag(TransactionType.Authorize)), Language.EN));
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
