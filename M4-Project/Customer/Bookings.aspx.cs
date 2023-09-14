using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Customer
{
    public partial class Bookings : System.Web.UI.Page
    {
        public Models.Customer currentCustomer;
        protected List<Models.Sales.Booking> bookings;

        protected void Page_Load(object sender, EventArgs e)
        {
            currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null)
                currentCustomer = Models.Customer.SetSession();

            bookings = Models.Sales.Booking.GetCustomerBookings(currentCustomer.CustomerID);
            bookingRepeater.DataSource = bookings;
            bookingRepeater.DataBind();
        }

        protected string GetBookingStatusColor(object status)
        {
            string bookingStatus = status.ToString();

            if (bookingStatus == Models.Sales.BookingState.Pending)
                return "#F46036";
            else if (bookingStatus == Models.Sales.BookingState.UpComing)
                return "#F5AF36";
            else if (bookingStatus == Models.Sales.BookingState.InProgress)
                return "#1B998B";
            else if (bookingStatus == Models.Sales.BookingState.Completed)
                return "green";
            else if (bookingStatus == Models.Sales.BookingState.Canceled)
                return "#D7263D";
            else if (bookingStatus == Models.Sales.BookingState.Rejected)
                return "#D7263D";
            else
                return "#000000";
        }
    }
}