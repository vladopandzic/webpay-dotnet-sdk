using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request.Builder;

namespace WebPay.UnitTests
{
    [TestClass]
    public class RequestBuilderUnitTest
    {
        [TestMethod]
        public void Digest_Should_Be_Created_Correctly()
        {
            WebPayIntegration wpi = new WebPayIntegration(new Configuration
            {
                AuthenticityToken = "7db11ea5d4a1af32421b564c79b946d1ead3daf0",
                Key = "qwert123",
                WebPayRootUrl = null
            });
            var requestBuilder = new RequestBuilder();
            var digest = requestBuilder.CreateDigest(wpi.ConfigurationSettings.Key, "abcdef", 543.21m, Request.Currency.EUR);
            Assert.AreEqual(digest, "16e943d2b84546ce4271de51679abc3bf1eb163b");
        }
    }
}
