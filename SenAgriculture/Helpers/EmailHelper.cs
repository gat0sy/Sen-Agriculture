using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace AppSenAgriculture.Helpers
{
    internal static class EmailHelper
    {
        public static void SendAccountCreatedEmail(string toEmail, string nom)
        {
            var fromAddress = new MailAddress("bot.67.csharp@gmail.com", "SenAgriculture");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "mpoq znzb ucjx bhnw";

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
                smtp.EnableSsl = true;

                using (var message = new MailMessage(fromAddress, toAddress))
                {
                    message.Subject = "Compte créé";
                    message.Body = $"Bonjour {nom},\n\nVotre compte a été créé avec succès.";
                    smtp.Send(message);
                }
            }
        }
        public static void SendAccountConnectionEmail(string toEmail, string nom)
        {
            var fromAddress = new MailAddress("bot.67.csharp@gmail.com", "SenAgriculture");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "mpoq znzb ucjx bhnw";

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
                smtp.EnableSsl = true;

                using (var message = new MailMessage(fromAddress, toAddress))
                {
                    message.Subject = "Compte créé";
                    message.Body = $"Bonjour {nom},\n\nVotre compte est vient de connecter.";
                    smtp.Send(message);
                }
            }
        }
    }
}