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
    public class _3DSecureClient : Client, I3DSecureClient
    {

        public string rootUrl { get; private set; }

        public _3DSecureClient(string rootUrl)
        {
            this.rootUrl = rootUrl;
        }


        public Response<PaymentResponse> FinishTransaction(SecureMessageRequest request)
        {
            var restsharpRequest = new RestRequest
            {
                Method = Method.POST,
                Resource = "/pares",
                XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer(),
                RequestFormat = DataFormat.Xml
            };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            restsharpRequest.AddBody(request);
            var restClient = new RestClient(rootUrl);

            IRestResponse response = restClient.Execute(restsharpRequest);

            return HandleResponse<PaymentResponse>(response);
        }
    }
}
