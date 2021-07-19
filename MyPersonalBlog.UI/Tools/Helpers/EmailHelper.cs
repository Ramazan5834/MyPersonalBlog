using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyPersonalBlog.UI.Areas.Member.Models;
using MyPersonalBlog.UI.Tools.AppClasses;

namespace MyPersonalBlog.UI.Tools.Helpers
{
    public class EmailHelper
    {
        //public bool SendEmail(SendMessageModel sendMessageModel)
        //{
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.From = new MailAddress("rblogcom@gmail.com");
        //    mailMessage.To.Add(new MailAddress("iscananramazan@gmail.com"));

        //    mailMessage.Subject = "Ziyaretci Mesajı";
        //    //mailMessage.IsBodyHtml = true;
        //    mailMessage.Body = $"Kişi Adı:{sendMessageModel.SenderName}   Kişi Mail Adresi: {sendMessageModel.SenderEmail}" + Environment.NewLine + $"Mesaj:{sendMessageModel.MessageBody}"; ;
        //    SmtpClient smtpClient = new SmtpClient();
        //    smtpClient.Host = "smtp.gmail.com";
        //    smtpClient.Port = 587;
        //    //smtpClient.UseDefaultCredentials = false;
        //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpClient.Credentials = new NetworkCredential("rblogcom@gmail.com", "rblog.com");
        //    smtpClient.EnableSsl = true;

        //    try
        //    {
        //        smtpClient.Send(mailMessage);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return false;
        //    }
        //}
        public EmailHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public bool SendEmail(SendMessageModel sendMessageModel)
        {
            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailConfig.From);
            mailMessage.To.Add(new MailAddress("blog@ramazaniscanan.com"));
            mailMessage.Subject = "Ziyaretci Mesajı";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"Kişi Adı:{sendMessageModel.SenderName} Kişi Mail Adresi: {sendMessageModel.SenderEmail}" + Environment.NewLine + $"Mesaj:{sendMessageModel.MessageBody}"; ;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = emailConfig.SmtpServer;
            smtpClient.Port = emailConfig.Port;
            smtpClient.Credentials = new NetworkCredential(emailConfig.UserName, emailConfig.Password);
            smtpClient.EnableSsl = emailConfig.SslState;

            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailConfig.From);
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.Subject = "Parola Sıfırlama";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = emailConfig.SmtpServer;
            smtpClient.Port = emailConfig.Port;
            smtpClient.Credentials = new NetworkCredential(emailConfig.UserName, emailConfig.Password);
            smtpClient.EnableSsl = emailConfig.SslState;

            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
