﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.POS
{
    public partial class MenuItem : System.Web.UI.Page
    {
        protected Models.MenuItem menuItem;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Session["POS"] as Models.Sales.Booking) != null)
                    LoadItemQuantityOptions();

                if (Request.Form["itemID"] == null)
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

                    if (Session["POS"] != null && (Session["POS"] as M4_Project.Models.Sales.Sale).Cart.ContainsKey(menuItem.ItemID))
                    {
                        Models.Sales.Sale sales = (Session["POS"] as M4_Project.Models.Sales.Sale);
                        int index = sales.Cart[menuItem.ItemID];
                        Models.Sales.ItemLine itemLine = sales.ItemLines[index];
                        itemQuantity.SelectedValue = itemLine.ItemQuantity.ToString();
                        txtInstructions.Text = itemLine.Instructions;
                    }
                }
            }
        }
        private void LoadItemQuantityOptions()
        {
            itemQuantity.Items.Clear();
            for (int i = 1; i <= Models.BusinessRules.Booking.MixItemQuantity; i++)
                itemQuantity.Items.Add(new ListItem(i.ToString(), i.ToString()));
            
        }
        [WebMethod]
        public static void AddItem(int itemID, int qty, string instructions)
        {
            Models.Sales.Sale sale;
            if (HttpContext.Current.Session["POS"] != null)
                sale = HttpContext.Current.Session["POS"] as Models.Sales.Sale;
            else
                sale = new Models.Sales.Order();

            Models.MenuItem item = Models.MenuItem.GetMenuItem(itemID);
            Models.Sales.ItemLine itemLine = new Models.Sales.ItemLine(itemID, qty, item.ItemPrice, instructions, item.ItemName, item.ItemImage, item.ItemCategory);
            sale.AddItemLine(itemLine);

            HttpContext.Current.Session["POS"] = sale;
        }

        [WebMethod]
        public static void EditItem(int itemID, int qty, string instructions)
        {
            if (HttpContext.Current.Session["POS"] is null)
                return;

            Models.Sales.Sale order = HttpContext.Current.Session["POS"] as Models.Sales.Sale;
            if (order.Cart.TryGetValue(itemID, out int index) && index < order.ItemLines.Count)
            {
                Models.Sales.ItemLine itemLine = order.ItemLines[index];
                itemLine.Instructions = instructions;
                itemLine.ItemQuantity = qty;
                HttpContext.Current.Session["POS"] = order;
            }
        }

        [WebMethod]
        public static void RemoveItem(int itemID)
        {
            if (HttpContext.Current.Session["POS"] is null)
                return;

            Models.Sales.Sale order = HttpContext.Current.Session["POS"] as Models.Sales.Sale;
            if (order.Cart.TryGetValue(itemID, out int index) && index < order.ItemLines.Count)
            {
                order.ItemLines.RemoveAt(index);
                order.Cart.Remove(itemID);

                foreach (var keyValuePair in order.Cart.ToList())
                    if (keyValuePair.Value > index)
                        order.Cart[keyValuePair.Key] = keyValuePair.Value - 1;

                if (order.ItemLines.Count < 1)
                    HttpContext.Current.Session["POS"] = null;
                else
                    HttpContext.Current.Session["POS"] = order;
            }
        }

    }
}