﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class Order : System.Web.UI.Page
    {
        protected Models.Customer customer;
        protected Models.StaffMember staffMember;
        protected Models.Sales.Order order;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["Order"] == null)
            Response.Redirect("/");

            int orderID;
            if (!int.TryParse(Request.QueryString["Order"], out orderID))
                Response.Redirect("/");

            order = Models.Sales.Order.GetOrder(orderID);
            if (order == null)
                Response.Redirect("/");

            customer = order.Customer;
            staffMember = Models.StaffMember.GetStaffMember_short(order.StaffID);

            ItemRepeater.DataSource = order.ItemLines;
            ItemRepeater.DataBind();

            if (Models.Sales.OrderState.IsFinalState(order.OrderStatus))
            {
                ListItem item = new ListItem(order.OrderStatus);
                try
                {
                    select_order_type.SelectedItem.Selected = false;
                }
                catch { }
                item.Selected = true;
                select_order_type.Items.Add(item);
                return;
            }
            string[] orderStatuses = {
                Models.Sales.OrderState.Preparing,
                Models.Sales.OrderState.Prepared,
                Models.Sales.OrderState.Collected,
                Models.Sales.OrderState.Rejected,
                Models.Sales.OrderState.Delivered,
                Models.Sales.OrderState.Unsuccessful
            };

            foreach (string status in orderStatuses)
            {
                ListItem item = new ListItem(status, order.OrderID + "|" + status);
                select_order_type.Items.Add(item);
            }

            foreach (ListItem item in select_order_type.Items)
            {
                if (item.Value == order.OrderStatus)
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        protected void SelectOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string selectedValue = ddl.SelectedValue;

            
            string[] values = selectedValue.Split('|');

            if (values.Length != 2)
                Response.Redirect("/");


            int orderID = -1;

            try
            {
                orderID = Convert.ToInt32(values[0]);
            } catch
            {
                Response.Redirect("/");
            }

            string selectedStatus = values[1];
            if (!Models.Sales.OrderState.IsValidState(selectedStatus))
                Response.Redirect("/");

            Models.Sales.Order.ChangeStatus(orderID, selectedStatus);
            Response.Redirect("/Admin/Order?Order=" + orderID);
        }

    }
}