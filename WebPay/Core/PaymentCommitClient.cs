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
    public class PaymentCommitClient : Client, IPaymentCommitClient
    {
        private string rootUrl;

        public PaymentCommitClient(string rootUrl)
        {
            this.rootUrl = rootUrl;
        }
        public Response<PaymentResponse, SecureMessage> Send(PaymentCommitRequest paymentRequest)
        {
            RestRequest restsharpRequest;
            RestClient restClient;
            PrepareRestClient(paymentRequest, out restsharpRequest, out restClient);

            IRestResponse response = restClient.Execute(restsharpRequest);
            return HandleResponse<PaymentResponse, SecureMessage>(response, () => response.Content.Contains("<secure-message"));
        }
        public Task<Response<PaymentResponse, SecureMessage>> SendAsync(PaymentCommitRequest paymentRequest)
        {
            RestRequest restsharpRequest;
            RestClient restClient;
            var taskCompletionSource = new TaskCompletionSource<Response<PaymentResponse, SecureMessage>>();
            PrepareRestClient(paymentRequest, out restsharpRequest, out restClient);

            restClient.ExecuteAsync(restsharpRequest, (r) => 

                taskCompletionSource.SetResult(HandleResponse<PaymentResponse, SecureMessage>(r, () => r.Content.Contains("<secure-message")))
            );
            return taskCompletionSource.Task;

        }

        private void PrepareRestClient(PaymentCommitRequest paymentRequest, out RestRequest restsharpRequest, out RestClient restClient)
        {
            restsharpRequest = new RestRequest
            {
                Method = Method.POST,
                Resource = "/api",
                XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer(),
                RequestFormat = DataFormat.Xml
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            restsharpRequest.AddBody(paymentRequest.Transaction);
            restClient = new RestClient(rootUrl);
        }


    }
}
