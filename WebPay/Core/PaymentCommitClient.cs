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
    public class PaymentCommitClient:Client,IPaymentCommitClient
    {
        private string rootUrl;

        public PaymentCommitClient(string rootUrl)
        {
            this.rootUrl = rootUrl;
        }
        public Response<PaymentResponse, SecureMessage> Pay(PaymentCommitRequest paymentRequest)
        {

            var restsharpRequest = new RestRequest
            {
                Method = Method.POST,
                Resource = "/api",
                XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer(),
                RequestFormat = DataFormat.Xml
            };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            restsharpRequest.AddBody(paymentRequest.Transaction);
            var restClient = new RestClient(rootUrl);

            IRestResponse response = restClient.Execute(restsharpRequest);
            return HandleResponse<PaymentResponse, SecureMessage>(response, () => response.Content.Contains("<secure-message"));

        }
    }
}
