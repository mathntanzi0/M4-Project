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
                else
                    TotalCost += Models.BusinessRules.Booking.BookingFee;
                
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
            if (!Context.User.Identity.IsAuthenticated)
                Response.Redirect("/Account/Login?ReturnUrl=/Checkout");
            
            Models.Customer currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null)
                currentCustomer = Models.Customer.SetSession("/Checkout");

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
            currentCustomer.UpdateLoyaltyPoints((int) Math.Floor(order.PaymentAmount * Models.BusinessRules.Sale.LoyaltyPointsRatio));
            HttpContext.Current.Session["sale"] = null;
            HttpContext.Current.Session["liveOrder"] = order.OrderID;
            HttpCookie cartCookie = new HttpCookie(Models.Sales.CartItem.OrderCart);
            cartCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cartCookie);

            Response.Redirect("/Customer/Orders");
        }
        private void ProcessBooking(Models.Sales.Booking booking)
        {
            if (!Context.User.Identity.IsAuthenticated)
                Response.Redirect("/Account/Login?ReturnUrl=/Checkout");

            Models.Customer currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null)
                currentCustomer = Models.Customer.SetSession("/Checkout");

            booking.Customer = currentCustomer;
            booking.PaymentDate = DateTime.Now;
            booking.PaymentMethod = "Card";
        }
    }
}