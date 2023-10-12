using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            
            try
            {
                M4_Project.Models.Emails.SendMail("Hello Testing", "Unit testing", "ntanzi033@gmail.com");
                //M4_Project.Models.Emails.SendTest();
                Console.WriteLine("Passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
