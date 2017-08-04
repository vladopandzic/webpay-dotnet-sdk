using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebPay.Interfaces;
using WebPay.Request;
using WebPay.Response;

namespace WebPay.Core
{
    public class PaymentChangeClient : Client,IPaymentChangeClient
    {
        private string rootUrl;
      

        public PaymentChangeClient(string rootUrl,TransactionType transactionType)
        {
            this.rootUrl = rootUrl;
            this.transactionType = transactionType;
        }

        public TransactionType transactionType { get; set; }

        public Response<PaymentResponse, SecureMessage> Pay(PaymentChangeRequest paymentRequest)
        {
            var typeOfTransactionPartOfUrl = "";
            if (transactionType == TransactionType.Capture) {

                typeOfTransactionPartOfUrl = "capture";

            } else if (transactionType == TransactionType.Refund)
            {
                typeOfTransactionPartOfUrl = "refund";

            } else if (transactionType == TransactionType.Void)
            {
                typeOfTransactionPartOfUrl = "void";
            }
            var restsharpRequest = new RestRequest
            {
                Method = Method.POST,
                Resource = "/transactions/"+ paymentRequest.Transaction.OrderNumber+ "/"+typeOfTransactionPartOfUrl+".xml",
                XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer(),
                RequestFormat = DataFormat.Xml
            };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            restsharpRequest.AddBody(paymentRequest.Transaction);
            var restClient = new RestClient(rootUrl);  //bez onog api

            IRestResponse response = restClient.Execute(restsharpRequest);
            return HandleResponse<PaymentResponse, SecureMessage>(response, () => response.Content.Contains("<secure-message"));

        }
    }
}
