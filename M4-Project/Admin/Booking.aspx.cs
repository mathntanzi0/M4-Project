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
    }
}