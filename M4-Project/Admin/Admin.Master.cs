﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected Models.StaffLoginSession loginStaff;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DarkModeEnabled"] != null && Convert.ToBoolean(Session["DarkModeEnabled"]))
                bodyTag.Attributes["class"] = "dark-mode";
            else
                bodyTag.Attributes["class"] = "";

            loginStaff = Session["LoginStaff"] as Models.StaffLoginSession;
            if (loginStaff == null)
            {
                loginStaff = Models.StaffLoginSession.GetSession(Context.User.Identity.Name);
                if (loginStaff == null)
                    Response.Redirect("/Contact");
                else
                    Session["LoginStaff"] = loginStaff;
            }
            if (loginStaff.IsDriver() && (Page.Title != Delivery.DeliverOrders.title && Page.Title != Delivery.Deliver.title && !DriverViewablePage()))
                Response.Redirect("/Admin/Delivery/DeliverOrders");

            if (loginStaff.StaffImage != null)
            {
                string base64String = Convert.ToBase64String(loginStaff.StaffImage);
                string imageUrl = "data:image/jpeg;base64," + base64String;
                Image1.ImageUrl = imageUrl;
                Image2.ImageUrl = imageUrl;
            }
        }
        private bool DriverViewablePage()
        {
            string targetPageURL = "/Admin/StaffMember";
            string targetPageURL1 = "/Admin/AddStaff";
            return Request.Url.AbsolutePath.Equals(targetPageURL, StringComparison.OrdinalIgnoreCase) || Request.Url.AbsolutePath.Equals(targetPageURL1, StringComparison.OrdinalIgnoreCase);
        }


    }
}