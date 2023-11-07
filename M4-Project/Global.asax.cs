using M4_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace M4_Project
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            M4_Project.Models.MenuItem.SetMenuCategories();
            //RoleActions roleActions = new RoleActions();
            //roleActions.AddUsertoRole("Admin", "thivar@email.com", "Th1v4r321@");
        }
        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 404)
                Response.Redirect("~/NotFound");
            else
            {
                Models.SystemUtilities.LogError(ex);
                Response.Redirect("~/Error");
            }
        }
    }
}