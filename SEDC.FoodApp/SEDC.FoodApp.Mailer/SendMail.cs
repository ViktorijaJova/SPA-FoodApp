using SEDC.FoodApp.Mailer.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SEDC.FoodApp.Mailer
{
    public static class SendMail
    {
        public static void Execute(Email email) 
        {
            var message = new MailMessage();

            message.To.Add(email.To);
            message.Subject = email.Subject;
            message.Body = email.Body;

            message.From = new MailAddress("sedcfoodapp@gmail.com");
            message.IsBodyHtml = true;

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("sedcfoodapp@gmail.com", "sedcapp123");

            smtp.Send(message);

            Console.WriteLine("Message Sent");
        }
    }

    //https://myaccount.google.com/u/0/lesssecureapps
}
