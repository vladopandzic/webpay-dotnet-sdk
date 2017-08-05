using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebPay.Http.Interfaces;
using WebPay.Request;
using WebPay.Response;

namespace WebPay.Http
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
            RestRequest restsharpRequest;
            RestClient restClient;
            PrepareRestClient(request, out restsharpRequest, out restClient);

            IRestResponse response = restClient.Execute(restsharpRequest);

            return HandleResponse<PaymentResponse>(response);
        }

        private void PrepareRestClient(SecureMessageRequest request, out RestRequest restsharpRequest, out RestClient restClient)
        {
            restsharpRequest = new RestRequest
            {
                Method = Method.POST,
                Resource = "/pares",
                XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer(),
                RequestFormat = DataFormat.Xml
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            restsharpRequest.AddBody(request);
            restClient = new RestClient(rootUrl);
        }

        public Task<Response<PaymentResponse>> FinishTransactionAsync(SecureMessageRequest request)
        {
            RestRequest restsharpRequest;
            RestClient restClient;
            PrepareRestClient(request, out restsharpRequest, out restClient);

            IRestResponse response = restClient.Execute(restsharpRequest);
            var taskCompletionSource = new TaskCompletionSource<Response<PaymentResponse>>();

            restClient.ExecuteAsync(restsharpRequest, (r) =>

                taskCompletionSource.SetResult(HandleResponse<PaymentResponse>(r))
            );
            return taskCompletionSource.Task;

        }
    }
}
