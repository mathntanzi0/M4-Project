using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using M4_Project.Models;

namespace M4_Project.Customer
{
    public partial class Password : Page
    {
        protected string email;
        protected void Page_Load(object sender, EventArgs e)
        {
            email = Request.QueryString["Email"];

            if (email == null || string.IsNullOrEmpty(email))
            {
                Response.Redirect("/");
                return;
            }

            if (!StaffLoginSession.AccountExist(email))
            {
                Response.Redirect("/");
                return;
            }
        }

        protected void Create(object sender, EventArgs e)
        {
            string email = Request.QueryString["Email"];
            if (email == null || string.IsNullOrEmpty(email))
            {
                Response.Redirect("/");
                return;
            }
            if (!StaffLoginSession.AccountExist(email))
            {
                Response.Redirect("/");
                return;
            }
            StaffLoginSession.UpdatePassword(email, PasswordText.Text);
        }
    }
}