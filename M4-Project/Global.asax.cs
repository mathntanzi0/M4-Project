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
            //roleActions.AddRole("Admin"); //run once only
            //roleActions.AddUsertoRole("Admin", "amal@email.com", "Am4l321@");
        }
    }
}