using AccountManagement.API.Data.Context;
using AccountManagement.API.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AccountManagement.API.Service
{
    public interface IEmailService
    {
        void SendEmail(string type, string to, string company, string username);
    }
    public class EmailService : IEmailService
    {
        readonly AccountManagementContext _accountManagementContext;
        public IConfiguration Configuration { get; }
        public EmailService(AccountManagementContext accountManagementContext, IConfiguration configuration)
        {
            _accountManagementContext = accountManagementContext;
            Configuration = configuration;
        }

        public void SendEmail(string type, string to, string organization, string username)
        {
            EmailTemplate emailTemplate;
            switch (type)
            {
                case "organizationRegister":
                    emailTemplate = _accountManagementContext.EmailTemplates.FirstOrDefault(email => email.Type == type);
                    SendOrganizationRegisterEmail(emailTemplate, to, organization);
                    break;
                case "AppRegister":
                    emailTemplate = _accountManagementContext.EmailTemplates.FirstOrDefault(email => email.Type == type);
                    SendAppRegisterEmail(emailTemplate, to, organization);
                    break;
                case "accountRegister":
                    emailTemplate = _accountManagementContext.EmailTemplates.FirstOrDefault(email => email.Type == type);
                    SendAccountRegisterEmail(emailTemplate, to, username, organization);
                    break;
                case "licenseLock":
                    emailTemplate = _accountManagementContext.EmailTemplates.FirstOrDefault(email => email.Type == type);
                    SendLicenseLockEmail(emailTemplate, to, organization);
                    break;
                case "licenseFreed":
                    emailTemplate = _accountManagementContext.EmailTemplates.FirstOrDefault(email => email.Type == type);
                    SendLicenseFreedEmail(emailTemplate, to, organization);
                    break;
                case "suspiciousLogIn":
                    emailTemplate = _accountManagementContext.EmailTemplates.FirstOrDefault(email => email.Type == type);
                    SendSuspiciousLogInEmail(emailTemplate, to, organization);
                    break;
                case "licenseRenew":
                    emailTemplate = _accountManagementContext.EmailTemplates.FirstOrDefault(email => email.Type == type);
                    SendLicenseRenewEmail(emailTemplate, to, organization);
                    break;
            }
        }

        private void SendOrganizationRegisterEmail(EmailTemplate template, string to, string organization)
        {
            template.Body = template.Body.Replace("{Organization}", organization);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("noreply@jesseaivio.io", "noreply");
            message.To.Add(new MailAddress(to));
            message.Subject = template.Subject;
            message.Body = template.Body;
            smtp.Port = int.Parse(Configuration["EmailSettings.Port"]);
            smtp.Host = Configuration["EmailSettings.Host"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration["EmailSettings.Username"], Configuration["EmailSettings.Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        private void SendAppRegisterEmail(EmailTemplate template, string to, string organization)
        {
            template.Body = template.Body.Replace("{Organization}", organization);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("noreply@jesseaivio.io", "noreply");
            message.To.Add(new MailAddress(to));
            message.Subject = template.Subject;
            message.Body = template.Body;
            smtp.Port = int.Parse(Configuration["EmailSettings.Port"]);
            smtp.Host = Configuration["EmailSettings.Host"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration["EmailSettings.Username"], Configuration["EmailSettings.Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        private void SendAccountRegisterEmail(EmailTemplate template, string to, string username, string organization)
        {
            template.Body = template.Body.Replace("{Username}", username);
            template.Body = template.Body.Replace("{Organization}", organization);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("noreply@jesseaivio.io", "noreply");
            message.To.Add(new MailAddress(to));
            message.Subject = template.Subject;
            message.Body = template.Body;
            smtp.Port = int.Parse(Configuration["EmailSettings.Port"]);
            smtp.Host = Configuration["EmailSettings.Host"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration["EmailSettings.Username"], Configuration["EmailSettings.Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        private void SendLicenseLockEmail(EmailTemplate template, string to, string organization)
        {
            template.Body = template.Body.Replace("{Organization}", organization);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("noreply@jesseaivio.io", "noreply");
            message.To.Add(new MailAddress(to));
            message.Subject = template.Subject;
            message.Body = template.Body;
            smtp.Port = int.Parse(Configuration["EmailSettings.Port"]);
            smtp.Host = Configuration["EmailSettings.Host"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration["EmailSettings.Username"], Configuration["EmailSettings.Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        private void SendLicenseFreedEmail(EmailTemplate template, string to, string organization)
        {
            template.Body = template.Body.Replace("{Organization}", organization);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("noreply@jesseaivio.io", "noreply");
            message.To.Add(new MailAddress(to));
            message.Subject = template.Subject;
            message.Body = template.Body;
            smtp.Port = int.Parse(Configuration["EmailSettings.Port"]);
            smtp.Host = Configuration["EmailSettings.Host"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration["EmailSettings.Username"], Configuration["EmailSettings.Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        private void SendSuspiciousLogInEmail(EmailTemplate template, string to, string organization)
        {
            template.Body = template.Body.Replace("{Organization}", organization);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("noreply@jesseaivio.io", "noreply");
            message.To.Add(new MailAddress(to));
            message.Subject = template.Subject;
            message.Body = template.Body;
            smtp.Port = int.Parse(Configuration["EmailSettings.Port"]);
            smtp.Host = Configuration["EmailSettings.Host"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration["EmailSettings.Username"], Configuration["EmailSettings.Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        private void SendLicenseRenewEmail(EmailTemplate template, string to, string organization)
        {
            template.Body = template.Body.Replace("{Organization}", organization);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("noreply@jesseaivio.io", "noreply");
            message.To.Add(new MailAddress(to));
            message.Subject = template.Subject;
            message.Body = template.Body;
            smtp.Port = int.Parse(Configuration["EmailSettings.Port"]);
            smtp.Host = Configuration["EmailSettings.Host"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration["EmailSettings.Username"], Configuration["EmailSettings.Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
