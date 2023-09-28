using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.POS
{
    public partial class MakeBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["BookingCustomer"] == null)
                    Response.Redirect("/Admin/POS/Customers");

                datePicker.Attributes["min"] = Models.BusinessRules.Booking.MinEventDate.ToString("yyyy-MM-dd");
                datePicker.Attributes["max"] = Models.BusinessRules.Booking.MaxEventDate.ToString("yyyy-MM-dd");
                if (Session["POS"] != null)
                {
                    Models.Sales.Booking booking = Session["POS"] as Models.Sales.Booking;
                    string address = booking.EventAddress;
                    string decorDescription = booking.EventDecorDescription;
                    DateTime date = booking.EventDate;
                    TimeSpan duration = booking.EventDuration;
                    
                    txtAddress.Value = address;
                    txtDecorDescription.Value = decorDescription;
                    datePicker.Value = date.ToString("yyyy-MM-dd");
                    ddlDuration.Value = duration.ToString(@"hh\:mm");
                    ddlTimeHour.Value = date.Hour.ToString();
                    ddlTimeMin.Value = date.Minute.ToString();
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Value;
            string decorDescription = txtDecorDescription.Value;
            DateTime date;
            TimeSpan duration;
            int hour;
            int minutes;

            if (!DateTime.TryParse(datePicker.Value, out date)) 
            {
                string script = "alert('Please choose a valid date');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            if (!TimeSpan.TryParse(ddlDuration.Value, out duration)) 
            {
                string script = "alert('Please choose a valid duration');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            if (!int.TryParse(ddlTimeHour.Value, out hour) && !(hour > 5 && hour < 20))
            {
                string script = "alert('Please choose a valid time');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
            if (!int.TryParse(ddlTimeMin.Value, out minutes))
            {
                string script = "alert('Please choose a valid time');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            if (string.IsNullOrEmpty(address))
            {
                string script = "alert('Please provide event address');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }


            if (Models.Sales.Booking.isDateUnavailable(date) || !Models.Sales.Booking.dateInRange(date))
            {
                string script = "alert('Selected date is unavailable.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
            date = new DateTime(date.Year, date.Month, date.Day, hour, minutes, 0);

            Models.Sales.Booking booking;

            if (Session["POS"] != null && Session["POS"] is Models.Sales.Booking)
            {
                booking = (Models.Sales.Booking)Session["POS"];
                booking.EventAddress = address;
                booking.EventDecorDescription = decorDescription;
                booking.EventDate = date;
                booking.EventDuration = duration;
            }
            else
                booking = new Models.Sales.Booking(address, decorDescription, date, duration);

            Session["POS"] = booking;

            if (Request.QueryString["ReturnUrl"] != null)
                Response.Redirect(Request.QueryString["ReturnUrl"]);
            else
                Response.Redirect("/Admin/POS/Menu");
        }
        protected string GetUnavailableDatesAsJavaScriptArray()
        {
            List<DateTime> unavailableDates = Models.Sales.Booking.UnavailableDates();
            return "[" + string.Join(",", unavailableDates.Select(date => $"\"{date.ToString("yyyy-MM-dd")}\"")) + "]";
        }

    }
}