using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected decimal TotalCost = 0;
        protected Models.Sales.Sale sale;
        protected void Page_Load(object sender, EventArgs e)
        {
            sale = HttpContext.Current.Session["sale"] as Models.Sales.Sale;
            if (sale != null && sale.ItemLines.Count > 0)
            {
                if (sale.SaleType == Models.Sales.SaleType.Order && ((Models.Sales.Order) sale).OrderType == Models.Sales.OrderType.Delivery)
                {
                    Models.Sales.Order order = (Models.Sales.Order) sale;
                    if (order.OrderType == Models.Sales.OrderType.Delivery && order.Delivery == null)
                        Response.Redirect("/SelectAddress");
                }
                var reversedItemLines = sale.ItemLines.ToList();
                reversedItemLines.Reverse();

                RepeaterItemLine.DataSource = reversedItemLines;
                RepeaterItemLine.DataBind();

                TotalCost = CalculateTotalCost(reversedItemLines);
                if (sale.SaleType == Models.Sales.SaleType.Order && (sale as Models.Sales.Order).OrderType == Models.Sales.OrderType.Delivery)
                    TotalCost += Models.BusinessRules.Delivery.DeliveryFee;
                else if (sale.SaleType == Models.Sales.SaleType.EventBooking)
                    TotalCost += Models.BusinessRules.Booking.BookingFee;
                if (sale.LoyaltyPoints != 0)
                    TotalCost -= sale.LoyaltyPoints * Models.BusinessRules.Sale.LoyaltyPointsCostRatio;
            } 
            else
                Response.Redirect("/Cart");
        }
        private decimal CalculateTotalCost(List<Models.Sales.ItemLine> itemLines)
        {
            decimal totalCost = 0;

            foreach (var itemLine in itemLines)
                totalCost += itemLine.TotalSubCost;


            return totalCost;
        }
        protected void btnPay_Click(object sender, EventArgs e)
        {
            sale = HttpContext.Current.Session["sale"] as Models.Sales.Sale;
            if (sale.SaleType == Models.Sales.SaleType.Order)
                ProcessOrder((Models.Sales.Order)sale);
            else
                ProcessBooking((Models.Sales.Booking)sale);

        }
        private void ProcessOrder(Models.Sales.Order order)
        {
            if (!Models.Sales.Order.online_order_available)
                Response.Redirect("/OrderUnavailable");
            if (!Context.User.Identity.IsAuthenticated)
                Response.Redirect("/Account/Login?ReturnUrl=/Checkout");
            
            Models.Customer currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null)
                currentCustomer = Models.Customer.SetSession("/Checkout");

            if (order.LoyaltyPoints > currentCustomer.LoyaltyPoints || order.LoyaltyPoints < 0)
            {
                string script = "alert('Invalid number of points');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            order.Customer = currentCustomer;
            order.PaymentDate = DateTime.Now;
            order.PaymentMethod = "Card";
            decimal tip;

            if (decimal.TryParse(tip_input.Text, out tip))
                order.Tip = tip;
            else
                order.Tip = 0;
            order.PaymentAmount = TotalCost + tip;

            order.RecordSell();
            if (order.OrderType == Models.Sales.OrderType.Delivery)
            {
                order.Delivery.OrderID = order.OrderID;
                order.Delivery.SaveDelivery();
            }
            if (order.LoyaltyPoints > 0)
                currentCustomer.UpdateLoyaltyPoints(-order.LoyaltyPoints);
            else
                currentCustomer.UpdateLoyaltyPoints((int) Math.Floor(order.PaymentAmount * Models.BusinessRules.Sale.LoyaltyPointsRatio));
            HttpContext.Current.Session["sale"] = null;
            HttpContext.Current.Session["liveOrder"] = order.OrderID;
            HttpCookie cartCookie = new HttpCookie(Models.Sales.CartItem.OrderCart);
            cartCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cartCookie);

            Response.Redirect("/Processing");
        }
        private void ProcessBooking(Models.Sales.Booking booking)
        {
            if (!Context.User.Identity.IsAuthenticated)
                Response.Redirect("/Account/Login?ReturnUrl=/Checkout");

            Models.Customer currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null)
                currentCustomer = Models.Customer.SetSession("/Checkout");

            if (string.IsNullOrEmpty(booking.EventAddress))
            {
                string script = "alert('Please provide event address');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }


            if (Models.Sales.Booking.isDateUnavailable(booking.EventDate) || !Models.Sales.Booking.dateInRange(booking.EventDate))
            {
                string script = "alert('Selected date is unavailable.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            if (booking.LoyaltyPoints > currentCustomer.LoyaltyPoints || booking.LoyaltyPoints < 0)
            {
                string script = "alert('Invalid number of points');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            booking.Customer = currentCustomer;
            booking.PaymentDate = DateTime.Now;
            booking.PaymentMethod = "Card";
            booking.PaymentAmount = TotalCost;

            booking.RecordSell();
            if (booking.LoyaltyPoints > 0)
                currentCustomer.UpdateLoyaltyPoints(-booking.LoyaltyPoints);
            else
                currentCustomer.UpdateLoyaltyPoints((int)Math.Floor(booking.PaymentAmount * Models.BusinessRules.Sale.LoyaltyPointsRatio));
            HttpContext.Current.Session["sale"] = null;
            HttpCookie cartCookie = new HttpCookie(Models.Sales.CartItem.BookingCart);
            cartCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cartCookie);

            Response.Redirect("/Customer/Bookings");
        }
    }
}