using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using MailKit.Net.Smtp;
using MimeKit;

namespace UI.Utils
{
    public class MailSender
    {
        public static void SendEmail(string email, string subject, string message)
        {

            //Sender
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("emailexample581@gmail.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message
            };

            //Smtp
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(
                    host :"smtp.gmail.com",
                    port: 587,
                    options: MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("emailexample581@gmail.com", "dfalgoibxsqfmsof");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }

        }
    }
}