﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;

namespace WebPay.Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebPayIntegration wbpayIntegration = new WebPayIntegration(new Configuration
            {

                AuthenticityToken = "hkfjdghfgdsfsdfsd5f4sd4fs4dfsd5",
                WebPayRootUrl = "https://ipg.webteh.hr/api"
            });
            WebPay.Payment payment = new Payment();

            Buyer buyer = new Buyer();
            buyer.FullName = "John Doe";
            buyer.City = "Knoxville";
            buyer.Address = "Elm street 22";
            buyer.Country = "Tennessee";
            buyer.Email = "email@example.com";
            buyer.Phone = "phone";
            buyer.ZIP = "123456789";



            Order order = new Order();
            order.Amount = 54321;
            order.OrderNumber = "abcdef";
            order.OrderInfo = "1 x SnowMaster 3000";

            Card card = new Card();
            card.CVV = 265;
            card.Pan = "4111111111111111";
            card.ExpirationDate = "0118";
            var response = payment.Pay(buyer,order,card);
        }
    }
}