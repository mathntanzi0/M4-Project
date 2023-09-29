using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.Event
{
    public partial class Staff : System.Web.UI.Page
    {
        protected List<Models.StaffMember> staff;
        protected int bookingID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int bookingID = 0;
                if (Request.QueryString["Event"] == null || !int.TryParse(Request.QueryString["Event"], out bookingID))
                    Response.Redirect("/Admin/Bookings");
                this.bookingID = bookingID;
                staff = Models.StaffMember.GetBookingStaff(bookingID);
                StaffRepeater.DataSource = staff;
                StaffRepeater.DataBind();
            }
        }
        protected void StaffRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int staffID = Convert.ToInt32(e.CommandArgument);
            if (!int.TryParse(Request.QueryString["Event"], out int bookingID))
                Response.Redirect("/Admin/Bookings");

            Models.Sales.Booking.RemoveStaffFromEvent(staffID, bookingID);
            Response.Redirect("/Admin/Event/Staff?Event="+bookingID);
        }
    }
}