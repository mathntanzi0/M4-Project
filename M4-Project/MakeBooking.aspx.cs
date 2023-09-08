using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class MakeBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Value;
            string decorDescription = txtDecorDescription.Value;
            DateTime date;
            TimeSpan duration;

            if (DateTime.TryParse(datePicker.Value, out date)) { }
            else
                date = DateTime.Now.AddDays(1);
            if (TimeSpan.TryParse(ddlDuration.Value, out duration)){}
            else
                duration = TimeSpan.Zero;

            Models.Sales.Booking booking = new Models.Sales.Booking(address, decorDescription, date, duration);
            Session["sale"] = booking;
            Response.Redirect("/Menu");
        }

    }
}