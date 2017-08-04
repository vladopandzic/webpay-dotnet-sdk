using System;
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
                //  AuthenticityToken = "f6c701631605eb9240e5d17d6604b0d819cc53bc",
                // Key = "$23p/fg#2",
                AuthenticityToken = "7db11ea5d4a1af32421b564c79b946d1ead3daf0",
                Key = "dasdsadsa",
                WebPayRootUrl = "https://ipg.webteh.hr",

            });
            Purchase payment = new Purchase(wbpayIntegration);
            Buyer buyer; Order order; Card card;



            PrepareData(out buyer, out order, out card);

            TransactionResult payingResult = payment.MakeTransaction(buyer, order, card, Language.EN);

            Capture capture = new Capture(wbpayIntegration);
            capture.MakeTransaction(20.0m, Currency.EUR, "1254", Language.EN);

            if (payingResult.Has3DSecure)
            {
                _3DSecureHandler _3dSecureHandler = new _3DSecureHandler(payingResult.SecureMessage, wbpayIntegration);
                var response = _3dSecureHandler.FinishTransaction();

            }


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
