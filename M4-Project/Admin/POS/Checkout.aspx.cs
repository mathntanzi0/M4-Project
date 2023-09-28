using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.POS
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected decimal TotalCost = 0;
        protected Models.Sales.Sale sale;
        protected void Page_Load(object sender, EventArgs e)
        {
            sale = HttpContext.Current.Session["POS"] as Models.Sales.Sale;
            if (sale != null && sale.ItemLines.Count > 0)
            {
                var reversedItemLines = sale.ItemLines.ToList();
                reversedItemLines.Reverse();

                RepeaterItemLine.DataSource = reversedItemLines;
                RepeaterItemLine.DataBind();
                TotalCost = CalculateTotalCost(reversedItemLines);                
            } 
            else
                Response.Redirect("/Admin/POS/Cart");
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
            sale = HttpContext.Current.Session["POS"] as Models.Sales.Sale;
            if (sale.SaleType == Models.Sales.SaleType.Order)
                ProcessOrder((Models.Sales.Order)sale);
            else
                ProcessBooking((Models.Sales.Booking)sale);

        }
        private void ProcessOrder(Models.Sales.Order order)
        {
            if (!Context.User.Identity.IsAuthenticated)
                Response.Redirect("/Account/Login?ReturnUrl=/Admin/POS/Checkout");

            order.PaymentDate = DateTime.Now;
            order.PaymentMethod = "Card";
            order.StaffID = (Session["LoginStaff"] as Models.StaffLoginSession).StaffID;

            if (decimal.TryParse(tip_input.Text, out decimal tip))
                order.Tip = tip;
            else
                order.Tip = 0;
            order.PaymentAmount = TotalCost + tip;

            order.RecordSell();
            HttpContext.Current.Session["POS"] = null;
            Response.Redirect("/Admin/LiveOrders");
        }
        private void ProcessBooking(Models.Sales.Booking booking)
        {
            Models.Customer currentCustomer = new Models.Customer();

            if (!int.TryParse(Session["BookingCustomer"].ToString(), out int customerID))
                Response.Redirect("/Admin/POS/Customers");
            
            currentCustomer.CustomerID = customerID;

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

            booking.Customer = currentCustomer;
            booking.PaymentDate = DateTime.Now;
            booking.PaymentMethod = "Card";
            booking.PaymentAmount = TotalCost;

            booking.RecordSell();
            currentCustomer.UpdateLoyaltyPoints((int)Math.Floor(booking.PaymentAmount * Models.BusinessRules.Sale.LoyaltyPointsRatio));
            HttpContext.Current.Session["POS"] = null;
            Response.Redirect("/Admin/Bookings");
        }
    }
}