//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Twilio;
//using Twilio.AspNet.Common;
//using Twilio.AspNet.Mvc;
//using Twilio.Rest.Api.V2010.Account;
//using System.Configuration;
//using Twilio.Types;

//namespace Simit_Saray.Controllers
//{
//    public class SMSController : TwilioController 
//    {
//        // GET: SMS
//        public ActionResult SendSms(SmsRequest incomingMessage)
//        {
//            var accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
//            var authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
//            TwilioClient.Init(accountSid, authToken);
//            var from = new PhoneNumber("+1 0 888 4874");
//            var to = new PhoneNumber("+994558744364");
//            var message = MessageResource.Create(
//                to: to,
//                from: from,
//                body: "Simit Sarayi"
//                );
//            return Content(message.Sid);
//        }

//        [HttpPost]
//        public ActionResult ReceiveSms(SmsRequest request)
//        {

//            return Content(request.Body);
//        }
//    }
//}
