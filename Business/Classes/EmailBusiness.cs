using EzPay.DTO;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Classes
{
    public interface IEmailBusines
    {
        void Send(string from, string to, string subject, string html, string cc = "", string bcc = "");
    }
    public class EmailBusiness : IEmailBusines
    {
        private readonly AppSettings _appSettings;

        public EmailBusiness(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public void Send(string from, string to, string subject, string html, string cc = "", string bcc = "")
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));

            if (Boolean.Parse(_appSettings.IsEmailSendActive))
                email.To.Add(MailboxAddress.Parse(to));
            else
                email.To.Add(MailboxAddress.Parse(_appSettings.dummyAdminEmail));

            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            if (!String.IsNullOrEmpty(cc))
            {
                string[] ccEmailString = cc.Split(';');
                foreach (var item in ccEmailString)
                {
                    email.Cc.Add(MailboxAddress.Parse(item));
                }
            }

            if (!String.IsNullOrEmpty(bcc))
            {
                string[] bccEmailString = bcc.Split(';');
                foreach (var item in bccEmailString)
                {
                    email.Bcc.Add(MailboxAddress.Parse(item));
                }
            }

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
