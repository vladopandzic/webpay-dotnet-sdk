using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebPay.Response;

namespace WebPay.Response
{
    public class Response<T>
    {
        public Response(T data, ErrorsResponse errorResponse, HttpStatusCode statusCode)
        {
            this.Data = data;
            this.Errors = errorResponse;
            this.StatusCode = statusCode;
        }
        public System.Net.HttpStatusCode StatusCode { get; set; }

        public ErrorsResponse Errors { get; set; }

        public T Data { get; set; }
    }
    public class Response<T,U>
    {
        public Response(T data,U alternativeData, ErrorsResponse errorResponse, HttpStatusCode statusCode,Dictionary<string,string> headers)
        {
            this.Data = data;
            this.Errors = errorResponse;
            this.StatusCode = statusCode;
            this.Headers = headers;
        }
        public System.Net.HttpStatusCode StatusCode { get; set; }

        public ErrorsResponse Errors { get; set; }

        public T Data { get; set; }

        public U AlternativeData { get; set; }
        public Dictionary<string, string> Headers { get; private set; }
    }
}
