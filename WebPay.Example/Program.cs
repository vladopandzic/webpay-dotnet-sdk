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
        static void Main()
        {
            Task t = MainAsync();
            t.Wait();
            var a = "b";
        }

        static async Task MainAsync()
        {
            WebPayIntegration wbpayIntegration = new WebPayIntegration(new Configuration
            {
             
                AuthenticityToken = "7db11ea5d4a1af32421b564caaa946d1ead3daf0",
                Key = "dasdsadsa",
                WebPayRootUrl = "https://ipg.webteh.hr",

            });

            Buyer buyer; Order order; Card card;
            PrepareData(out buyer, out order, out card);

            Authorization authorizeTransaction = new Authorization(wbpayIntegration);
            Task<TransactionResult> result =  authorizeTransaction.MakeTransactionAsync(buyer, order, card, Language.EN);

            Purchase payment = new Purchase(wbpayIntegration);
            TransactionResult payingResult = payment.MakeTransaction(buyer, order, card, Language.EN);
          

            //Capture capture = new Capture(wbpayIntegration);
            //capture.MakeTransaction(20.0m, Currency.EUR, "1254", Language.EN);

            if (payingResult.Has3DSecure)
            {
                _3DSecureHandler _3dSecureHandler = new _3DSecureHandler(payingResult.SecureMessage, wbpayIntegration);
                var response = _3dSecureHandler.FinishTransaction();

            }
            var a = await result;
            var b = "c";


        }

        private static void PrepareData(out Buyer buyer, out Order order, out Card card)
        {
            buyer = new Buyer();
            buyer.FullName = "Jo";
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
