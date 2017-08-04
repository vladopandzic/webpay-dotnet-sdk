using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Core;

namespace WebPay.UnitTests
{
    [TestClass]
    public class PaymentChangeRequestBuilderUnitTest
    {
        [TestMethod]
        public void Amount_Should_Be_Created_In_Corrent_Format()
        {
            WebPayIntegration wpi = new WebPayIntegration(new Configuration
            {
                AuthenticityToken = "7db11ea5d4a1af32421b564c79b946d1ead3daf0",
                Key = null,
                WebPayRootUrl = null
            });
            var response = new PaymentChangeRequestObjectBuilder().Build(20.23665m, Request.Currency.EUR, "s", wpi, Request.Language.EN);
            Assert.AreEqual(response.Transaction.Amount, 2024);
        }
    }
}
