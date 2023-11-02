using System.Net;
using System.Net.Mail;

namespace M4_Project.Models
{
    public static class Emails
    {
        private static string smtpServer = "smtp.gmail.com";
        private static int smtpPort = 587;
        private static string senderEmail = "bytebusterstech@gmail.com";
        private static string senderPassword = "bjbwnlnvwmmkdgkc";


        /// <summary>
        /// Sends an email with HTML content.
        /// </summary>
        /// <param name="emailSubject">The subject of the email.</param>
        /// <param name="emailBody">The HTML body of the email.</param>
        /// <param name="receiverEmail">The email address of the recipient.</param>
        /// <param name="htmlView">The alternate view containing HTML content.</param>
        public static void SendMail(string emailSubject, string emailBody, string receiverEmail, AlternateView htmlView)
        {
            MailMessage mail = new MailMessage(senderEmail, receiverEmail);

            mail.Subject = emailSubject;
            mail.Body = emailBody;
            mail.AlternateViews.Add(htmlView);

            SmtpClient client = new SmtpClient(smtpServer, smtpPort);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);
            client.Send(mail);
        }
        /// <summary>
        /// Sends a plain text email.
        /// </summary>
        /// <param name="emailSubject">The subject of the email.</param>
        /// <param name="emailBody">The body of the email.</param>
        /// <param name="receiverEmail">The email address of the recipient.</param>
        public static void SendMail(string emailSubject, string emailBody, string receiverEmail)
        {
            MailMessage mail = new MailMessage(senderEmail, receiverEmail);

            mail.Subject = emailSubject;
            mail.Body = emailBody;

            SmtpClient client = new SmtpClient(smtpServer, smtpPort);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);
            client.Send(mail);
        }
    }
}