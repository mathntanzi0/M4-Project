using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class Cart : System.Web.UI.Page
    {
        protected decimal TotalCost = 0;
        protected Models.Customer currentCustomer;
        protected Models.Sales.Sale sale;
        protected void Page_Load(object sender, EventArgs e)
        {
            sale = HttpContext.Current.Session["sale"] as Models.Sales.Sale;
            if (sale != null)
            {
                var reversedItemLines = sale.ItemLines.ToList();
                reversedItemLines.Reverse();

                RepeaterItemLine.DataSource = reversedItemLines;
                RepeaterItemLine.DataBind();

                TotalCost = CalculateTotalCost(reversedItemLines);
            }
            if (!IsPostBack)
            {
                List<Models.MenuItem> menuItems = new List<Models.MenuItem>();
                menuItems = Models.MenuItem.GetRandomItems(6);
                PromoRepeater.DataSource = menuItems;
                PromoRepeater.DataBind();
                currentCustomer = Session["Customer"] as Models.Customer;
                if (currentCustomer != null)
                {
                    int costPoints = (int)(TotalCost / Models.BusinessRules.Sale.LoyaltyPointsCostRatio);
                    int maxPoints = Math.Min(costPoints, currentCustomer.LoyaltyPoints);
                    rvPoints.ErrorMessage = $"Please enter a value between 0 and {maxPoints}";
                    rvPoints.MaximumValue = maxPoints.ToString();
                    rvPoints1.ErrorMessage = rvPoints.ErrorMessage;
                    rvPoints1.MaximumValue = maxPoints.ToString();
                }
            }
        }

        private decimal CalculateTotalCost(List<Models.Sales.ItemLine> itemLines)
        {
            decimal totalCost = 0;

            foreach (var itemLine in itemLines)
                totalCost += itemLine.TotalSubCost;
            

            return totalCost;
        }
        protected void btnOrderCart_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["sale"] = null;
            Models.Sales.Order.SyncOrderCartWithCookieCart();
            if (HttpContext.Current.Session["sale"] == null)
                HttpContext.Current.Session["sale"] = new Models.Sales.Order();
            Response.Redirect("/Cart");
        }
        protected void btnBookingCart_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["sale"] = null;
            Models.Sales.Booking.SyncSessionWithCookies();

            if (HttpContext.Current.Session["sale"] == null)
                Response.Redirect("/MakeBooking");
            else
                Response.Redirect("/Cart");
        }
        protected void btnCheckout_Click(object sender, EventArgs e)
        {

            if (Session["sale"] != null)
            {
                Models.Sales.Sale sale = Session["sale"] as Models.Sales.Sale;
                if (sale.SaleType == Models.Sales.SaleType.Order )
                {
                    if (!Models.Sales.Order.online_order_available)
                        Response.Redirect("/OrderUnavailable");
                    Models.Sales.Order order = (Models.Sales.Order)sale;
                    if (select_category != null && select_category.Value == "D")
                        order.OrderType = Models.Sales.OrderType.Delivery;
                    else
                        order.OrderType = Models.Sales.OrderType.Collection;
                    sale = order;
                }
                string txt_points = (!string.IsNullOrEmpty(txtPoints.Text.Trim())) ? txtPoints.Text.Trim() : txtPoints1.Text.Trim();
                if (int.TryParse(txt_points, out int points))
                    sale.LoyaltyPoints = points;
                else
                    sale.LoyaltyPoints = 0;
                Session["sale"] = sale;
                Response.Redirect("/Checkout");
            }
            else
                Response.Redirect("/Cart");
        }
    }
}