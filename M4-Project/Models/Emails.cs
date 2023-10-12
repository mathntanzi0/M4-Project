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

        /*
             System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 64.233.166.108:587
               at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
               at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
               --- End of inner exception stack trace ---
               at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
               at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
               at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
               at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
               at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
               at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
               at System.Net.Mail.SmtpClient.GetConnection()
               at System.Net.Mail.SmtpClient.Send(MailMessage message)
               --- End of inner exception stack trace ---
               at System.Net.Mail.SmtpClient.Send(MailMessage message)
               at M4_Project.Models.Emails.SendMail(String emailSubject, String emailBody, String receiverEmail) in D:\ISTN Project\M4-Project\M4-Project\Models\Emails.cs:line 52
               at M4_Project.Tests.UnitTest1.TestMethod1() in D:\ISTN Project\M4-Project\M4-Project.Tests\UnitTest1.cs:line 18
         */

    }
}