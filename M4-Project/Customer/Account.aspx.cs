using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using M4_Project.Models;
using Microsoft.AspNet.Identity;

namespace M4_Project.Customer
{
    public partial class Account : System.Web.UI.Page
    {
        public Models.Customer currentCustomer;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null)
                currentCustomer = Models.Customer.SetSession();
            
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session["Customer"] = null;
            Response.Redirect("~/");
        }

        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            
        }

    }
}