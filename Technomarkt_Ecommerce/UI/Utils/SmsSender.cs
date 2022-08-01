using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace UI.Utils
{
    public class SmsSender
    {
        public static string SendSms(string msg, string phoneNumber)
        {

            var accountSid = "AC31c009caac6408fbed3899753204fc47";
            var authToken = "223e3c4450773f8a71dfd3bc05ad6442";

            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(phoneNumber);
            var from = new PhoneNumber("+12393560151");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: msg);

            return message.Sid;

        }
    }
}