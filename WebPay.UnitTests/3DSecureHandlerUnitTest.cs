using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Interfaces;
using WebPay.Response;

namespace WebPay.UnitTests
{
    [TestClass]
    public class _3DSecureHandlerUnitTest
    {
        [TestMethod]
        public void Should_Correcntly_Populate_Request()
        {
            WebPayIntegration wpi = new WebPayIntegration(new Configuration
            {
                AuthenticityToken = "7db11ea5d4a1af32421b564c79b946d1ead3daf0",
                Key = null,
                WebPayRootUrl = null
            });
            var secureMessage = new SecureMessage
            {
                AcsUrl = "dsjdsj",
                AuthenticityToken = "aa",
                Id = "nn",
                Pareq = "cc"
            };
            _3DSecureHandler _3dSecureHandler = new _3DSecureHandler(secureMessage, wpi);
            Mock<I3DSecureClient> _3dSecureClient = new Mock<I3DSecureClient>();
           // _3dSecureClient.Setup(x => x.FinishTransaction(secureMessage).
            var response = _3dSecureHandler.FinishTransaction(_3dSecureClient.Object);
            Assert.AreEqual(_3dSecureHandler.smRequest.PaRes, secureMessage.Pareq);
            Assert.AreEqual(_3dSecureHandler.smRequest.MD, secureMessage.AuthenticityToken);
        }
    }
}
