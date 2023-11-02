using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class Booking : System.Web.UI.Page
    {
        protected Models.Customer customer;
        protected Models.Sales.Booking booking;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                datePicker.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                datePicker.Attributes["max"] = Models.BusinessRules.Booking.MaxEventDate.ToString("yyyy-MM-dd");
                if (Request.QueryString["Event"] == null)
                    Response.Redirect("/");

                if (!int.TryParse(Request.QueryString["Event"], out int bookingID))
                    Response.Redirect("/");

                booking = Models.Sales.Booking.GetBooking(bookingID);
                if (booking == null)
                    Response.Redirect("/");

                customer = booking.Customer;

                ItemRepeater.DataSource = booking.ItemLines;
                ItemRepeater.DataBind();

                txtAddress.Value = booking.EventAddress;
                txtDecorDescription.Value = booking.EventDecorDescription;
                datePicker.Value = booking.EventDate.ToString("yyyy-MM-dd");
                ddlDuration.Value = booking.EventDuration.ToString(@"hh\:mm");
                ddlTimeHour.Value = booking.EventDate.Hour.ToString();
                ddlTimeMin.Value = (booking.EventDate.Minute != 0) ? booking.EventDate.Minute.ToString() : "00";

                if (Models.Sales.BookingState.IsFinalState(booking.BookingStatus))
                {
                    ListItem item = new ListItem(booking.BookingStatus);
                    try
                    {
                        select_event_status.SelectedItem.Selected = false;
                    }
                    catch { }
                    item.Selected = true;
                    select_event_status.Items.Add(item);
                    if (booking.BookingStatus == Models.Sales.BookingState.Completed)
                    {
                        ListItem item1 = new ListItem(Models.Sales.BookingState.Canceled);
                        select_event_status.Items.Add(item1);
                    }
                    else
                    {
                        ListItem item1 = new ListItem(Models.Sales.BookingState.Completed);
                        select_event_status.Items.Add(item1);
                    }
                    return;
                }
                string[] eventStatuses = {
                    Models.Sales.BookingState.UpComing,
                    Models.Sales.BookingState.InProgress,
                    Models.Sales.BookingState.Completed,
                    Models.Sales.BookingState.Canceled,
                    Models.Sales.BookingState.Rejected
                };

                foreach (string status in eventStatuses)
                {
                    ListItem item = new ListItem(status, booking.BookingID + "|" + status);
                    select_event_status.Items.Add(item);
                }
                
                foreach (ListItem item in select_event_status.Items)
                {
                    if (item.Text == booking.BookingStatus)
                    {
                        if (item.Selected == true)
                            break;
                        select_event_status.SelectedValue = item.Value;
                        break;
                    }
                }


            }
        }
        protected void SelectEventStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string selectedValue = ddl.SelectedValue;


            string[] values = selectedValue.Split('|');

            if (values.Length != 2)
                Response.Redirect("/");


            int bookingID = -1;

            try
            {
                bookingID = Convert.ToInt32(values[0]);
            }
            catch
            {
                Response.Redirect("/");
            }

            string selectedStatus = values[1];
            if (!Models.Sales.BookingState.IsValidState(selectedStatus) && selectedStatus == Models.Sales.BookingState.Pending)
                Response.Redirect("/");

            Models.Sales.Booking.ChangeStatus(bookingID, selectedStatus);
            Response.Redirect("/Admin/Booking?Event=" + bookingID);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int bookingID = 0;
            if (Request.QueryString["Event"] == null || !int.TryParse(Request.QueryString["Event"], out bookingID) || bookingID < 1)
            {
                string script = "alert('Invalid booking');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Response.Redirect("/Admin");
                return;
            }

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
                Response.Redirect("/Admin");
                return;
            }

            if (!TimeSpan.TryParse(ddlDuration.Value, out duration))
            {
                string script = "alert('Please choose a valid duration');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Response.Redirect("/Admin");
                return;
            }

            if (!int.TryParse(ddlTimeHour.Value, out hour) && !(hour > 5 && hour < 20))
            {
                string script = "alert('Please choose a valid time');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Response.Redirect("/Admin");
                return;
            }
            if (!int.TryParse(ddlTimeMin.Value, out minutes))
            {
                string script = "alert('Please choose a valid time');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Response.Redirect("/Admin");
                return;
            }

            if (string.IsNullOrEmpty(address))
            {
                string script = "alert('Please provide event address');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Response.Redirect("/Admin");
                return;
            }


            if (Models.Sales.Booking.isDateUnavailable(date) || date < DateTime.Now)
            {
                string script = "alert('Selected date is unavailable.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Response.Redirect("/Admin");
                return;
            }
            date = new DateTime(date.Year, date.Month, date.Day, hour, minutes, 0);

            Models.Sales.Booking.Update(bookingID, address, date, duration, decorDescription);

            Response.Redirect("/Admin/Booking?Event="+bookingID);
        }
        protected string GetUnavailableDatesAsJavaScriptArray()
        {
            List<DateTime> unavailableDates = Models.Sales.Booking.UnavailableDates();
            return "[" + string.Join(",", unavailableDates.Select(date => $"\"{date.ToString("yyyy-MM-dd")}\"")) + "]";
        }
    }
}