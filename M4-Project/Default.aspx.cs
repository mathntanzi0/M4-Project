using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Models.MenuItem> menuItems = new List<Models.MenuItem>();
                menuItems = Models.MenuItem.GetRandomItems(6);
                PromoRepeater.DataSource = menuItems;
                PromoRepeater.DataBind();
            }
        }
    }
}