﻿using System;
using System.Web;
using System.Web.Services;

namespace M4_Project.Admin
{
    public partial class UpdateDarkModeSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isDarkMode = Convert.ToBoolean(Request.QueryString["isDarkMode"]);
            Session["DarkModeEnabled"] = isDarkMode;
        }
        [WebMethod]
        public static void UpdateDarkMode(bool isDarkMode)
        {
            HttpContext.Current.Session["DarkModeEnabled"] = isDarkMode;
        }
    }
}