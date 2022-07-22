using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace UI.Utils
{
    public class MailSender
    {
        public static void SendEmail(string email, string subject, string message)
        {

            //Sender
            MailMessage sender = new MailMessage();
            sender.From = new MailAddress("emailexample581@gmail.com", "Password--2.");
            sender.To.Add(email);
            sender.Subject = subject;
            sender.Body = message;


            //Smtp
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("emailexample581@gmail.com", "Password--2.");
            smtp.Port = 587;
            smtp.Host = "smtp.google.com";
            smtp.EnableSsl = true;

            smtp.Send(sender);

        }
    }
}