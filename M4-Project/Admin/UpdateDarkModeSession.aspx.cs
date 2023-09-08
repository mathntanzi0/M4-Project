using System;

public partial class UpdateDarkModeSession : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isDarkMode = Convert.ToBoolean(Request.QueryString["isDarkMode"]);
        Session["DarkModeEnabled"] = isDarkMode;
    }
}