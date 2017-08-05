# webpay-dotnet-sdk

This is <b>Unofficial WebPay .NET SDK</b>. It has some nice features like:


- Pluggable architecture
- Unit tested and unit testable
- Asynchronous actions support


Get Started by downloading it and creating new WebPay payment integration:

            WebPayIntegration wbpayIntegration = new WebPayIntegration(new Configuration
            {
             
                AuthenticityToken = "7db11ea5d4a1af32421b564c79b946d1ead3daf0",
                Key = "dasdsadsa",
                WebPayRootUrl = "https://ipg.webteh.hr",

            });

Parameterize root url, it is different in test and production enviroment.

If you want to make new <code>Purchase</code> <a target="_blank" href="https://ipg.webteh.hr/hr/documentation/direct#transactions-messages">
transaction message</a> it is simple as writing the following:

 	            Purchase payment = new Purchase(wbpayIntegration);
              TransactionResult payingResult = payment.MakeTransaction(buyer, order, card, Language.EN);
  or
             
              TransactionResult payingResult = await payment.MakeTransactionAsync(buyer, order, card, Language.EN);

<code>Buyer</code>, <code>Order</code> and <code>Card</code> objects need to be populated by you. If some of that data not meet requirements
no data will be sent to WebPay servers and list of validation messages will be returned to you (notice payingResult object - there is 
RequestValidationErrors property with all validation errors)

While still on payingResult variable take a look how whole TransactionResult object looks like:

     public class TransactionResult
    {
        public bool Has3DSecure { get; set; }

        public bool RequestWasSuccessfullyValidated { get; set; }

        public IList<ValidationError> RequestValidationErrors { get; set; }

        public ErrorsResponse WebPayErrors { get; set; }

        public Response.PaymentResponse PaymentResponse { get; set; }

        public SecureMessage SecureMessage { get; set; }

    }


<code>RequestValidationErrors</code> is already explained. One similar object is <code>WebPayErrors</code>. <code>WebPayErrors</code> 
list is populated when validation made upfront thinks everything is ok with request, but WebPay still returns error. To experience
this situation send wrong <code>AuthenticityToken</code> property (from <code>WebPayIntegration</code> class). You should get an error 
"digest is not ok".

When mentioned <code>RequestValidationErrors</code> and <code>WebPayErrors</code> happen to be  null or empty that means that either
<code>PaymentResponse</code> or  <code>SecureMessage</code> objects are populated. Those are populated in success scenario,
but keep in mind only one of them is populated. If card has <code>3DSecure</code> and response was successfull SecureMessage object will
be populated and ready for further processing (using <code>3DSecureHandler</code> class). For convenience <code>Has3DSecure</code>
flag is set to <code>true</code> in that case. If card doesn't have <code>3DSecure</code> and response was successfull, 
<code>PaymentResponse</code> object will be populated.

If you don't know what is 3d secure on credit card find out <a href="https://en.wikipedia.org/wiki/3-D_Secure">here</a>.

Already was mentioned <a href="https://ipg.webteh.hr/hr/documentation/direct#purchase">Purchase</a> transaction message. 
There are 5 transaction message supported by WebPay and this SDK supports them all:

<ul>
<li>Authorize</li>
<li>Purchase</li>
<li>Refund</li>
<li>Void</li>
<li>Capture</li>
</ul>

You can  read about them in offical WebPay <a href="https://ipg.webteh.hr/hr/documentation/direct">documentation</a>.
<code>Authorize</code> is completely similar to <code>Purchase</code>. It has same response. Response is handled in the same way. 
Request is the same, except the fact that <code>transaction_type</code> parametar (see documentation) is set to "authorize", 
rather than "purchase". You don't care about that. Everything is handled for you under the hood.


            Authorization authorizeTransaction = new Authorization(wbpayIntegration);
            TransactionResult result= authorizeTransaction.MakeTransaction(buyer, order, card, Language.EN);



Other 3 transaction message types ( <code>Refund,Void, Capture </code> ) have different request than <code>Authorize</code> and 
<code>Purchase</code>, but all three have same request when compared to each other (only url to which request is POST-ed is different)
Response for all 5 transaction type has same structure.




<code>Capture, Refund and Void</code> have similar way of creating request. Foe example,

            Capture capture = new Capture(wbpayIntegration);
            var result= capture.MakeTransaction(20.0m, Currency.EUR, "1254", Language.EN);
            
            or 
            var result=await capture.MakeTransactionAsync(20.0m, Currency.EUR, "1254", Language.EN);
            
Althought making transactions is very simple, SDK has very pluggable arhitecture. For example, you can create your own validation rules 
for validating objects or create your own http client that will make requests to WebPay's servers.

For example, if you want to put your own validation class into action you use another overload of MakeTransaction method,
or MakeTransactionAsync method.


            Purchase payment = new Purchase(wbpayIntegration);
            TransactionResult payingResult = payment.MakeTransaction(buyer, order, card, Language.EN, new MyCustomValidator());


where MyCustomValidator must extend  IRequestValidator<PaymentCommitRequest> or IRequestValidator<PaymentChangeRequest>, 
depending on what type of transaction message is.
If <code>Authorization</code> or <code>Purchase</code> IRequestValidator<PaymentCommitRequest> is used. 
In case of <code>Capture, Void, or Refund</code> IRequestValidator<PaymentChangeRequest> is used.

     public class MyCustomValidator : IRequestValidator<PaymentCommitRequest> {

        public MyCustomValidator()
        {
            
        }
         public bool IsValid(PaymentCommitRequest instance){
            return instance.Transation.City.Length>=3 && instance.Transation.City.Length<=30
         }
       public List<ValidationError> DoValidation(PaymentCommitRequest instance){
		         var list=new List<ValidationError>();
             if(instance.Transation.City.Length<3 || instance.Transation.City.Length>30){
		          	var error=new ValidationError();
			          error.ErrorMessage="City must be 3-30 characters long";
		         	list.Add(error);
            }
           return list;
       }     
    }

Althought  this example  doesn't make too much sense in real world (validation exists only for city, and WebPay will still returns errors
inside <code>WebPayError</code> property for other things) in some cases this pluggable arhitecture makes sense. For example, when WebPay
changes some rules on their side, and this library doesn't follow them with quick update, or simply when you want to make your own rules 
for validating credit cards or other objects.

Similary you can make your own web client that will make http requests.

Example of usage:

          Authorization authorizeTransaction = new Authorization(wbpayIntegration);
            TransactionResult result= authorizeTransaction.MakeTransaction(buyer, order, card, Language.EN, new PaymentCommitRequestValidator(),new MyWebClient());


<code>PaymentCommitRequestValidator</code> is default validator for validating <code>Authorization</code> and <code>Purchase</code>
transaction messages.
<code>Capture, Void, and Refund</code> transactions messages have <code>PaymentChangeRequestValidator</code> as default validator,
and also their custom web client must extend <code>IPaymentChangeClient</code> rather than <code>IPaymentCommitClient</code>
(because those have different request objects; naming comes from the fact that those Refund, Void, Capture transaction messages
change transaction is some way.
 
    public class MyWebClient:IPaymentCommitClient
    {
        
        public Response<PaymentResponse, SecureMessage> Send(PaymentCommitRequest paymentRequest){
		
		        var paymentRequestComesFirstAsParamater=new PaymentResponse();
			      var secureMessageComesSecondAsParameter=new SecureMessage();
			      return new Response(paymentRequestComesFirstAsParamater,secureMessageComesSecondAsParameter,null,HttpStatusCode.OK,null);
        }
}



Library is made with good intentions and author of SDK is not responsible for any problems that will potentially cause. You are using
at your own responsibility.

