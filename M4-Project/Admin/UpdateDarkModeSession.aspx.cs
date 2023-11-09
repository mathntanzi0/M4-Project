using System;
using System.Web;
using System.Web.Services;

public partial class UpdateDarkModeSession : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static void UpdateDarkMode(bool isDarkMode)
    {
        HttpContext.Current.Session["DarkModeEnabled"] = isDarkMode;
    }
}