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
            var accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"); ;
            var authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

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