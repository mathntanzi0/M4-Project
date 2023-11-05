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
        protected Models.Sales.ItemSummary orderItemSummary;
        protected Models.Sales.ItemSummary bookingItemSummary;
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
            orderItemSummary = Models.Sales.Order.GetItemSummary(menuItem.ItemID);
            bookingItemSummary = Models.Sales.Booking.GetItemSummary(menuItem.ItemID);

            string base64String = Convert.ToBase64String(menuItem.ItemImage);
            string imageUrl = "data:image/jpeg;base64," + base64String;
            Image1.ImageUrl = imageUrl;

            if (!IsPostBack)
            {
                EditButton.CommandArgument = menuItem.ItemID.ToString();
                DeleteButton.CommandArgument = menuItem.ItemID.ToString();
                category_list.SelectedValue = menuItem.Status;
            }
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button editButton = (Button)sender;
            string itemID = editButton.CommandArgument;

            Response.Redirect("/Admin/AddItem?Item="+itemID);
        }
        protected void category_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = category_list.SelectedValue;

            if (!Models.MenuItemStatus.IsValidState(selectedValue))
                Response.Redirect("/");

            string str_itemID = Request.QueryString["Item"];
            if (string.IsNullOrEmpty(str_itemID))
                Response.Redirect("Menu");

            int itemID;
            if (!int.TryParse(str_itemID, out itemID))
                Response.Redirect("Menu");

            Models.MenuItem.ChangeState(itemID, selectedValue);

            Response.Redirect($"MenuItem?Item={itemID}");

        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button editButton = (Button)sender;
            string itemID = editButton.CommandArgument;

            if (!int.TryParse(itemID, out int int_itemID))
                Response.Redirect("/");

            Models.MenuItem.DeleteMenuItem(int_itemID);
            Response.Redirect("/Admin/Menu");
        }
    }
}