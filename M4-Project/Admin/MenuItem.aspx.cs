using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class MenuItem : System.Web.UI.Page
    {
        public Models.MenuItem menuItem;
        protected void Page_Load(object sender, EventArgs e)
        {
            string str_itemID = Request.QueryString["Item"];
            if (string.IsNullOrEmpty(str_itemID))
                Response.Redirect("Menu");

            int itemID;
            if (!int.TryParse(str_itemID, out itemID))
                Response.Redirect("Menu");

            menuItem = Models.MenuItem.GetMenuItem(itemID);
            if (menuItem == null)
                Response.Redirect("Menu");
            Page.Title = menuItem.ItemName;

            string base64String = Convert.ToBase64String(menuItem.ItemImage);
            string imageUrl = "data:image/jpeg;base64," + base64String;
            Image1.ImageUrl = imageUrl;
        }
    }
}