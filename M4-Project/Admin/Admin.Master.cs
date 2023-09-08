using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DarkModeEnabled"] != null && Convert.ToBoolean(Session["DarkModeEnabled"]))
            {
                bodyTag.Attributes["class"] = "dark-mode";
            }
            else
            {
                bodyTag.Attributes["class"] = "";
            }
        }
    }
}