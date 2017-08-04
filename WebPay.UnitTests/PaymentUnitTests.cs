﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPay.Request;
using FluentValidation;
using WebPay.Interfaces;
using Moq;
using WebPay.Core;

namespace WebPay.UnitTests
{
    public class MyCustomValidator : AbstractValidator<PaymentCommitRequest> {


        public MyCustomValidator()
        {
            RuleFor(x => x.Transaction.City).Length(200, 250);
        }
    }

    [TestClass]
    public class PaymentUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(WebPay.Exceptions.IntergrationException))]
        public void Integration_Should_Fail_If_Authenticity_Token_Is_Not_In_Proper_Format()
        {
            WebPayIntegration wpi = new WebPayIntegration(new Configuration
            {
                AuthenticityToken = "a",
                Key = null,
                WebPayRootUrl = null
            });
        }
        [TestMethod]
        public void Should_Be_Able_To_Make_Custom_Validator__For_Payment_Request_If_For_Any_Reason_Neccessary()
        {
            WebPayIntegration wpi = new WebPayIntegration(new Configuration
            {
                AuthenticityToken = "7db11ea5d4a1af32421b564c79b946d1ead3daf0",
                Key = null,
                WebPayRootUrl = null
            });
            Buyer buyer; Order order; Card card;
            PrepareData(out buyer, out order, out card);
            TransactionResult result = new Purchase(wpi).MakeTransaction(buyer, order, card,Language.EN, new MyCustomValidator());
            Assert.AreEqual(result.RequestValidationErrors.Count, 1);
        }
        [TestMethod]
        public void Should_Not_Call_Api_If_Validation_Failed() {
            WebPayIntegration wpi = new WebPayIntegration(new Configuration
            {
                AuthenticityToken = "7db11ea5d4a1af32421b564c79b946d1ead3daf0",
                Key = null,
                WebPayRootUrl = null
            });
            Buyer buyer; Order order; Card card;
            PrepareData(out buyer, out order, out card);
            buyer.City = "7db11ea5d4a1af32421b564c79b946d1ead3daf07db11ea5d4a1af32421b564c79b946d1ead3daf07db11ea5d4a1af32421b564c79b946d1ead3daf07db11ea5d4a1af32421b564c79b946d1ead3daf07db11ea5d4a1af32421b564c79b946d1ead3daf0";
           
            Mock <IPaymentCommitClient> paymentClientMock= new Mock<IPaymentCommitClient>();
            Mock<IPaymentCommitClient> paymentClientMock2 = new Mock<IPaymentCommitClient>();
            PaymentCommitRequestObjectBuilder rb = new PaymentCommitRequestObjectBuilder();
         
            TransactionResult result = new Purchase(wpi).MakeTransaction(buyer, order, card, new MyCustomValidator(),paymentClientMock.Object);
            paymentClientMock.Verify(x => x.Pay(It.IsAny<PaymentCommitRequest>()), Times.Once);
            buyer.City = "7db11e";
            new Purchase(wpi).MakeTransaction(buyer, order, card,Language.EN, new MyCustomValidator(), paymentClientMock2.Object);
            paymentClientMock2.Verify(x => x.Pay(paymentRequest), Times.Never);
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