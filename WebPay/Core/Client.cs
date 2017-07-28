using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;
using WebPay.Response;

namespace WebPay.Core
{
    public class Client
    {
        public Response<PaymentResponse> Pay(PaymentRequest paymentRequest)
        {

            var restsharpRequest = new RestRequest
            {
                Method = Method.POST,
                Resource = "/",
                XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer(),
                RequestFormat = DataFormat.Xml
            };
         
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            restsharpRequest.AddBody(paymentRequest.Transaction);
            var restClient = new RestClient("https://ipg.webteh.hr/api");
          
            IRestResponse response = restClient.Execute(restsharpRequest);
            return HandleResponse<PaymentResponse>(response);

        }


        public Response<T> HandleResponse<T>(IRestResponse response)
        {
            if (response.StatusCode ==HttpStatusCode.NotAcceptable || response.StatusCode.ToString()=="422")
            {

                RestSharp.Deserializers.DotNetXmlDeserializer deserial = new RestSharp.Deserializers.DotNetXmlDeserializer();
                try
                {
                    var errors = deserial.Deserialize<ErrorsResponse>(response);
                    return new Response<T>(default(T), errors, response.StatusCode);
                }
                catch (Exception ex)
                {
                    var error = new ErrorsResponse();error.Errors.Add("DESERALIZE EXCEPTION!");
                    return new Response<T>(default(T),error, response.StatusCode);
                }


            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.XmlDeserializer deserial = new RestSharp.Deserializers.XmlDeserializer();
                var deserialResponse = deserial.Deserialize<T>(response);
                return new Response<T>(deserialResponse, null, response.StatusCode);
            }

            return new Response<T>(default(T),null,  response.StatusCode);
        }
    }
}
