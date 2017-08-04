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

        public Response<T, U> HandleResponse<T, U>(IRestResponse response, Func<bool> whenToDeserializeToSecondGenericParam)
        {
            if (response.StatusCode == HttpStatusCode.NotAcceptable || (int)response.StatusCode == 422)
            {

                RestSharp.Deserializers.DotNetXmlDeserializer deserial = new RestSharp.Deserializers.DotNetXmlDeserializer();
                try
                {
                    var errors = deserial.Deserialize<ErrorsResponse>(response);
                    var headers = response.Headers.Select(x => new { Name = x.Name, Value = x.Value }).ToDictionary(t => t.Name, t => t.Value?.ToString());
                    return new Response<T, U>(default(T), default(U), errors, response.StatusCode, headers);
                }
                catch (Exception ex)
                {
                    var error = new ErrorsResponse(); error.Errors.Add("DESERALIZE EXCEPTION!");
                    return new Response<T, U>(default(T), default(U), error, response.StatusCode, null);
                }


            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.XmlDeserializer deserial = new RestSharp.Deserializers.XmlDeserializer();
                if (whenToDeserializeToSecondGenericParam())
                {

                    var deserialResponse = deserial.Deserialize<U>(response);
                    var headers = response.Headers.Select(x => new { Name = x.Name, Value = x.Value }).ToDictionary(t => t.Name, t => t.Value?.ToString());
                    return new Response<T, U>(default(T), deserialResponse, null, response.StatusCode, headers);
                }
                else
                {
                    var deserialResponse = deserial.Deserialize<T>(response);
                    var headers = response.Headers.Select(x => new { Name = x.Name, Value = x.Value }).ToDictionary(t => t.Name, t => t.Value?.ToString());
                    return new Response<T, U>(deserialResponse, default(U), null, response.StatusCode, headers);
                }
            }
            //HANDLE INTERBNAL SERVER ERROR, REPRODUCED BY SETTING WORONG AUTHENTICTY TOKEN
            return new Response<T, U>(default(T), default(U), null, response.StatusCode, null);
        }
        public Response<T> HandleResponse<T>(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.NotAcceptable || response.StatusCode.ToString() == "422")
            {

                RestSharp.Deserializers.DotNetXmlDeserializer deserial = new RestSharp.Deserializers.DotNetXmlDeserializer();
                try
                {
                    var errors = deserial.Deserialize<ErrorsResponse>(response);
                    return new Response<T>(default(T), errors, response.StatusCode);
                }
                catch (Exception ex)
                {
                    var error = new ErrorsResponse(); error.Errors.Add("DESERALIZE EXCEPTION!");
                    return new Response<T>(default(T), error, response.StatusCode);
                }


            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.XmlDeserializer deserial = new RestSharp.Deserializers.XmlDeserializer();
                var deserialResponse = deserial.Deserialize<T>(response);
                return new Response<T>(deserialResponse, null, response.StatusCode);
            }
            //HANDLE INTERBNAL SERVER ERROR, REPRODUCED BY SETTING WORONG AUTHENTICTY TOKEN
            return new Response<T>(default(T), null, response.StatusCode);
        }
    }



}
