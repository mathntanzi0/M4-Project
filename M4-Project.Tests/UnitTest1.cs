using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace M4_Project.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ///<summary>
            /// Email sending test.
            ///</summary>

            /*try
            {
                M4_Project.Models.Emails.SendMail("Hello Testing", "Unit testing", "ntanzi033@gmail.com");
                //M4_Project.Models.Emails.SendTest();
                Console.WriteLine("Passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }*/
            SendEmail();
        }
        public bool SendEmail()
        {
            string emailBody = GetEmailBody();

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, MediaTypeNames.Text.Html);

            string imagePath = @"D:\ISTN Project\M4-Project\M4-Project\Assets\logo.png";
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

            LinkedResource itemImage = new LinkedResource(new MemoryStream(imageBytes), MediaTypeNames.Image.Jpeg);
            itemImage.ContentId = "logo";
            htmlView.LinkedResources.Add(itemImage);

            try
            {
                Models.Emails.SendMail("Verify Email Address", emailBody, "nsz3n4@gmail.com", htmlView);
                Console.WriteLine("Sent");
                return true;
            }
            catch
            {
                Console.WriteLine("Not Sent");
                return false;
            }
        }
        public string GetEmailBody()
        {
            return $@"
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Email Template</title>
            </head>
            <body style='font-family: Arial, sans-serif; margin: 0; padding: 8px; text-align: center;'>

                <div style='background-color: #496970; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); padding: 20px; margin: 60px auto; max-width: 400px;'>

                    <img src='cid:logo' alt='Logo' style='width: 124px; height: auto;'>

                    <h2 style='color: #fff; font-weight: bold;'>Friends & Family</h2>

                    <p style='color: #fff; line-height: 1.6; padding:24px 0;'>Welcome to the team Sthe! Before accessing the administrative system, please ensure to verify your email. Thank you.</p>

                    <a href='#' style='display: inline-block; margin-top: 20px; padding: 10px 32px; background-color: #262626; color: #fff; text-decoration: none; border-radius: 8px;'>Verify Email</a>

                </div>

            </body>
            </html>";
        }
    }
}
