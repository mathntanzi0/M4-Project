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
            return Models.Sales.BookingState.GetStatusColor(bookingStatus);
        }
    }
}