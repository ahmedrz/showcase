using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace EManifestServices.Core
{
    public class EmailSender
    {
        const string uploadSubject = "IQMAN manifest upload status";
        public static Task SendNotificationEmail(Notifications notification, string emailTo)
        {
            return SendEmail(emailTo, uploadSubject, notification.Header, notification.Details);
        }
        public static async Task SendEmail(string emailTo, string subject, string header, string details)
        {
            var body = "<p>Email From: {0}</p><p>Message:</p><p>{1}</p> Details:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(emailTo));  // replace with valid value 
            message.From = new MailAddress("info@iraq-seas.com");  // replace with valid value
            message.Subject = subject;
            message.Body = string.Format(body, "IQMAN", header, details);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }
        }

    }
}