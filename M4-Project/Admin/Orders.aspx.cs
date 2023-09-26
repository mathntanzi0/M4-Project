using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        protected List<Models.Sales.Order> orders;
        protected string p_NotFound = "The system currently does not have any orders in place.";
        protected int page;
        protected int maxPage = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page = 1;
                orders = GetOrders();
                OrderRepeater.DataSource = orders;
                OrderRepeater.DataBind();


                string customerName = Request.QueryString["Customer"];
                string orderType = Request.QueryString["OrderType"];
                bool customerNameNotEmpty = !string.IsNullOrEmpty(customerName);
                bool orderTypeNotEmpty = !string.IsNullOrEmpty(orderType);


                if (customerNameNotEmpty && orderTypeNotEmpty)
                {
                    search_bar.Text = customerName;
                    select_order_type.SelectedValue = orderType;
                    p_NotFound = "No order for \"" + customerName + "\" of type \"" + orderType + "\"";
                }
                else if (customerNameNotEmpty)
                {
                    search_bar.Text = customerName;
                    p_NotFound = "No order for \"" + customerName + "\"";

                }
                else if (orderTypeNotEmpty)
                {
                    select_order_type.SelectedValue = orderType;
                    p_NotFound = "No order of type \"" + orderType + "\"";
                }
            }
        }
        protected void OrderRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"order?Order={orderID}");
            }
        }
        protected void SelectOrderType_Changed(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;
            string orderType = select_order_type.SelectedValue;

            int order_searchQuery = -1;
            if (int.TryParse(searchQuery, out order_searchQuery))
                Redirect("1", searchQuery, Request.QueryString["Staff"], null, orderType);
            else
                Redirect("1", Request.QueryString["Order"], Request.QueryString["Staff"], searchQuery, orderType);
            
        }
        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;
            string orderType = select_order_type.SelectedValue;


            int order_searchQuery = -1;
            if (int.TryParse(searchQuery, out order_searchQuery))
                Redirect("1", searchQuery, Request.QueryString["Staff"], null, orderType);
            else
                Redirect("1", Request.QueryString["Order"], Request.QueryString["Staff"], searchQuery, orderType);
        }
        private void Redirect(string pageString, string orderID, string staffID, string customerName, string orderType)
        {
            pageString = HttpUtility.UrlEncode(pageString);
            orderID = HttpUtility.UrlEncode(orderID);
            staffID = HttpUtility.UrlEncode(staffID);
            customerName = HttpUtility.UrlEncode(customerName);
            orderType = HttpUtility.UrlEncode(orderType);

            string redirectUrl = "/Admin/Orders";
            bool hasParameters = false;

            if (!string.IsNullOrEmpty(pageString))
            {
                redirectUrl += "?Page=" + pageString;
                hasParameters = true;
            }

            if (!string.IsNullOrEmpty(orderID))
            {
                if (hasParameters)
                {
                    redirectUrl += "&";
                }
                else
                {
                    redirectUrl += "?";
                    hasParameters = true;
                }

                redirectUrl += "Order=" + orderID;
            }

            if (!string.IsNullOrEmpty(staffID))
            {
                if (hasParameters)
                {
                    redirectUrl += "&";
                }
                else
                {
                    redirectUrl += "?";
                    hasParameters = true;
                }

                redirectUrl += "Staff=" + staffID;
            }

            if (!string.IsNullOrEmpty(customerName))
            {
                if (hasParameters)
                {
                    redirectUrl += "&";
                }
                else
                {
                    redirectUrl += "?";
                    hasParameters = true;
                }

                redirectUrl += "Customer=" + customerName;
            }

            if (!string.IsNullOrEmpty(orderType))
            {
                if (hasParameters)
                {
                    redirectUrl += "&";
                }
                else
                {
                    redirectUrl += "?";
                }

                redirectUrl += "OrderType=" + orderType;
            }

            Response.Redirect(redirectUrl);
        }
        private List<Models.Sales.Order> GetOrders()
        {
            string pageString = Request.QueryString["Page"];
            string orderID = Request.QueryString["Order"];
            string staffID = Request.QueryString["Staff"];
            string customerName = Request.QueryString["Customer"];
            string orderType = Request.QueryString["OrderType"];
            Models.Sales.OrdersSearch search = new Models.Sales.OrdersSearch(pageString, orderID, staffID, customerName, orderType, 6);
            page = search.Page;
            maxPage = search.MaxPage;
            return search.Orders;
        }
        protected void btnPreviousPage_Click(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;
            string orderType = select_order_type.SelectedValue;

            if (Request.QueryString["Page"] == null || !int.TryParse(Request.QueryString["Page"], out page))
                page = 1;

            if (page < 2)
            {
                page = 1;
                Redirect(page.ToString(), Request.QueryString["Order"], Request.QueryString["Staff"], searchQuery, orderType);
                return;
            }
            page -= 1;
            Redirect(page.ToString(), Request.QueryString["Order"], Request.QueryString["Staff"], searchQuery, orderType);
        }
        protected void btnNextPage_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["Page"] == null || !int.TryParse(Request.QueryString["Page"], out page))
                page = 2;
            else
                page += 1;


            string searchQuery = search_bar.Text;
            string orderType = select_order_type.SelectedValue;
            Redirect(page.ToString(), Request.QueryString["Order"], Request.QueryString["Staff"], searchQuery, orderType);
        }
    }
}