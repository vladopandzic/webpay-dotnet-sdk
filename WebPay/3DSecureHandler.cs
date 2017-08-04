using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebPay.Core;
using WebPay.Request;
using WebPay.Response;

namespace WebPay
{

    public class _3DSecureHandler
    {
        private SecureMessage _secureMessage;

        public WebPayIntegration _integation { get; private set; }

        public _3DSecureHandler(SecureMessage secureMessage, WebPayIntegration integation)
        {
            _secureMessage = secureMessage;
            _integation = integation;
        }
        public void SaveSecureMessageToSession(HttpContext httpContext)
        {
            httpContext.Session["WEBPAY_SESSION_DATA"] = _secureMessage;
        }
        public SecureMessage GetSecureMessageFromSession(HttpContext httpContext)
        {
            return httpContext.Session["WEBPAY_SESSION_DATA"] as SecureMessage;
        }
        public Response<PaymentResponse> FinishTransaction()
        {

            SecureMessageRequest smRequest = new SecureMessageRequest();
            smRequest.MD = _secureMessage.AuthenticityToken;
            smRequest.PaRes = _secureMessage.Pareq;

            var client = new _3DSecureClient(_integation.ConfigurationSettings.WebPayRootUrl);
            return client.FinishTransaction(smRequest);
        }

        public string GetBasicRedirectHtml(string term_url)
        {

            var html = @"<!DOCTYPE html>
                      <html>
                        <head>
                            <title> 3D Secure Verification</title>
                            <script language ='Javascript'>
                              function OnLoadEvent() { document.form.submit(); }
                           </script>
                       </head>
                        <body OnLoad ='OnLoadEvent();'>
                               Invoking 3 - D secure form, please wait...
   
                             <form name='form' action=" + _secureMessage.AcsUrl + @" method='post' >      
                                <input type='hidden' name='PaReq' value=" + _secureMessage.Pareq + @">
                                <input type='hidden' name='TermUr' value=" + term_url + @">
                                <input type='hidden' name='MD' value = " + _secureMessage.AuthenticityToken + @">
                                   <noscript>
                                       <p> Please click </ p >< input id'to-asc-button' type = 'submit' >
                                   </noscript >
                             </form>
                         </body>
                    </ html>";
            return html;
        }

    }
}
