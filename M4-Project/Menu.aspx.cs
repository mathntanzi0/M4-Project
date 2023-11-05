using M4_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class Menu : System.Web.UI.Page
    {
        protected List<Models.MenuItem> menuItems = new List<Models.MenuItem>();
        protected static int SearchPage = 1;
        MenuSearch search;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["itemName"] != null || Request.QueryString["category"] != null)
                    search = new MenuSearch(Request.QueryString["itemName"], Request.QueryString["category"], true);
                else
                    search = new MenuSearch("", "", true);

                if (Request.QueryString["page"] != null)
                    if (int.TryParse(Request.QueryString["page"], out int requestedPage))
                    {
                        search.SetPage(requestedPage);
                        SearchPage = requestedPage;
                    }

                LoadItems(search);
                PopulateCategoryList();

                if (!string.IsNullOrEmpty(search.ItemName) || !string.IsNullOrEmpty(search.ItemType))
                    UpDateControl(search);
            }
        }
        private void UpDateControl(MenuSearch search)
        {
            if (!string.IsNullOrEmpty(search.ItemType))
                category_list.SelectedValue = search.ItemType;
            if (!string.IsNullOrEmpty(search.ItemName))
                itemName.Text = search.ItemName;
        }
        private void PopulateCategoryList()
        {
            category_list.Items.Clear();

            List<string> menuCategories = Models.MenuItem.menuCategories;
            category_list.Items.Add(new ListItem("Category", ""));
            foreach (string category in menuCategories)
            {
                category_list.Items.Add(new ListItem(category, category));
            }
        }
        private void LoadItems(MenuSearch search)
        {
            menuItems = Models.MenuItem.GetMenuItems(6, search);
            if (menuItems.Count < 1)
            {
                search.PreviousPage();
                SearchPage = search.Page;
                menuItems = Models.MenuItem.GetMenuItems(6, search);
                RepeaterMenuItem.DataSource = menuItems;
                RepeaterMenuItem.DataBind();
                return;
            }
            RepeaterMenuItem.DataSource = menuItems;
            RepeaterMenuItem.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string itemName = this.itemName.Text;
            string itemType = this.category_list.SelectedValue;
            Redirect(1, itemName, itemType);
        }
        protected void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (SearchPage < 2)
            {
                SearchPage = 1;
                Redirect(SearchPage, Request.QueryString["itemName"], Request.QueryString["category"]);
                return;
            }
            SearchPage -= 1;
            Redirect(SearchPage, Request.QueryString["itemName"], Request.QueryString["category"]);
        }
        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            SearchPage += 1;
            Redirect(SearchPage, Request.QueryString["itemName"], Request.QueryString["category"]);
        }
        private void Redirect(int page, string itemName, string category)
        {
            if (page < 0)
            {
                Response.Redirect("Default.aspx");
                return;
            }
            itemName = HttpUtility.UrlEncode(itemName);
            category = HttpUtility.UrlEncode(category);

            string redirectUrl = $"Menu?page={page}";

            if (!string.IsNullOrEmpty(itemName))
            {
                redirectUrl += $"&itemName={itemName}";
            }

            if (!string.IsNullOrEmpty(category))
            {
                redirectUrl += $"&category={category}";
            }

            Response.Redirect(redirectUrl);
        }

    }
}