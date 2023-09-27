using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Customer
{
    public partial class Booking : System.Web.UI.Page
    {
        protected Models.Customer customer;
        protected Models.Sales.Booking booking;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Event"] == null)
                    Response.Redirect("/");

                if (!int.TryParse(Request.QueryString["Event"], out int bookingID))
                    Response.Redirect("/");

                Models.Customer currentCustomer = Session["Customer"] as Models.Customer;
                if (currentCustomer == null)
                    currentCustomer = Models.Customer.SetSession();

                booking = Models.Sales.Booking.GetBooking(bookingID);
                if (booking == null || booking.Customer.CustomerID != currentCustomer.CustomerID)
                    Response.Redirect("/");

                customer = booking.Customer;

                ItemRepeater.DataSource = booking.ItemLines;
                ItemRepeater.DataBind();
            }
        }
    }
}