using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Common.Resources
{
   public static class MailSender
    {
       public static bool MailSend(string toAddress, string subject, string body)
       {
           try
           {
               if (Settings.EnableMailSending)
               {
                   SmtpClient smtp = new SmtpClient
                   {
                       Host = "smtp.gmail.com",
                       Port = 587,
                       EnableSsl = true,
                       DeliveryMethod = SmtpDeliveryMethod.Network,
                       UseDefaultCredentials = true,
                       Credentials = new System.Net.NetworkCredential(Settings.SenderEmailId, Settings.SenderPassword),
                       Timeout = 30000,
                   };
                   MailMessage message = new MailMessage(Settings.SenderEmailId, toAddress, subject, body);
                   smtp.Send(message);
               }
               return true;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
   }
}
