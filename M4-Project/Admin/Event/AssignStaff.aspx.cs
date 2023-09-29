using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.Event
{
    public partial class AssignStaff : System.Web.UI.Page
    {
        protected List<Models.StaffMember> staff;
        protected string p_NotFound = "The system currently does not have any staff members.";
        protected int page = 1;
        protected int maxPage = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                staff = GetStaff();
                StaffRepeater.DataSource = staff;
                StaffRepeater.DataBind();

                List<string> roles = Models.StaffMember.GetRoles();
                select_role.DataSource = roles;
                select_role.DataBind();

                select_role.Items.Insert(0, new ListItem("Role", ""));

                string searchText = Request.QueryString["Search"];
                string role = Request.QueryString["Role"];
                bool searchTextNotEmpty = !string.IsNullOrEmpty(searchText);
                bool roleNotEmpty = !string.IsNullOrEmpty(role);

                if (searchTextNotEmpty && roleNotEmpty)
                {
                    search_bar.Text = searchText;
                    select_role.SelectedValue = role;
                    p_NotFound = "No staff member found for search \"" + searchText + "\" with role \"" + role + "\"";
                }
                else if (searchTextNotEmpty)
                {
                    search_bar.Text = searchText;
                    p_NotFound = "No staff member found for search \"" + searchText + "\"";
                }
                else if (roleNotEmpty)
                {
                    select_role.SelectedValue = role;
                    p_NotFound = "No staff member found with role \"" + role + "\"";
                }
            }
        }

        protected void StaffRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AssignStaff")
            {
                int staffID = Convert.ToInt32(e.CommandArgument);
                if (!int.TryParse(Request.QueryString["Event"], out int bookingID))
                    Response.Redirect("/Admin/Bookings");


                if (Models.Sales.Booking.IsStaffAssignedToEvent(bookingID, staffID))
                {
                    string script = "alert('Staff member #" + staffID + " is already assigned to this event booking');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    staff = GetStaff();
                    StaffRepeater.DataSource = staff;
                    StaffRepeater.DataBind();
                    return;
                }

                Models.Sales.Booking.BookingDuration eventDate = Models.Sales.Booking.GetEventDate(bookingID);
                if (eventDate == null)
                    Response.Redirect("/Admin/Bookings");
                
                List<Models.Sales.Booking.BookingDuration> dates = Models.Sales.Booking.GetEventDatesForStaff(staffID, eventDate.EventDate.Year, eventDate.EventDate.Month, eventDate.EventDate.Day);

                foreach (Models.Sales.Booking.BookingDuration date in dates)
                {
                    if (Utilities.DateManager.SchedulesOverlap(eventDate.EventDate, eventDate.EventDuration, date.EventDate, date.EventDuration))
                    {
                        string script = "alert('Staff member #"+staffID+" is already assigned to an event at this time');";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                        staff = GetStaff();
                        StaffRepeater.DataSource = staff;
                        StaffRepeater.DataBind();
                        return;
                    }
                }
                Models.Sales.Booking.AddStaffToEvent(staffID, bookingID);
                Response.Redirect($"/Admin/Event/Staff?Event={bookingID}");
            }
        }
        protected void SelectRole_Changed(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;
            string orderType = select_role.SelectedValue;
            Redirect("1", searchQuery, orderType);
        }
        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;
            string orderType = select_role.SelectedValue;
            Redirect("1", searchQuery, orderType);
        }
        protected void btnPreviousPage_Click(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;
            string staffRole = select_role.SelectedValue;

            if (Request.QueryString["Page"] == null || !int.TryParse(Request.QueryString["Page"], out page))
                page = 1;

            if (page < 2)
            {
                page = 1;
                Redirect(page.ToString(), searchQuery, staffRole);
                return;
            }
            page -= 1;
            Redirect(page.ToString(), searchQuery, staffRole);
        }
        protected void btnNextPage_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["Page"] == null || !int.TryParse(Request.QueryString["Page"], out page))
                page = 2;
            else
                page += 1;
            string searchQuery = search_bar.Text;
            string staffRole = select_role.SelectedValue;
            Redirect(page.ToString(), searchQuery, staffRole);
        }
        private void Redirect(string page, string searchQuery, string staffRole)
        {
            StringBuilder redirectUrl = new StringBuilder("/Admin/Event/AssignStaff");

            bool hasQuery = false;

            if (!string.IsNullOrEmpty(page))
            {
                redirectUrl.Append("Page=" + Server.UrlEncode(page) + "&");
                hasQuery = true;
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                redirectUrl.Append("Search=" + Server.UrlEncode(searchQuery) + "&");
                hasQuery = true;
            }

            if (!string.IsNullOrEmpty(staffRole))
            {
                redirectUrl.Append("Role=" + Server.UrlEncode(staffRole) + "&");
                hasQuery = true;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Event"]))
            {
                redirectUrl.Append("Event=" + Server.UrlEncode(Request.QueryString["Event"]) + "&");
                hasQuery = true;
            }
            else
                Response.Redirect("/Admin/Bookings");
            

            if (hasQuery)
            {
                if (redirectUrl.ToString().EndsWith("&"))
                {
                    redirectUrl.Remove(redirectUrl.Length - 1, 1);
                }
                redirectUrl.Insert(redirectUrl.ToString().IndexOf("/Admin/Event/AssignStaff") + "/Admin/Event/AssignStaff".Length, "?");
            }
            Response.Redirect(redirectUrl.ToString());
        }
        private List<Models.StaffMember> GetStaff()
        {
            string pageString = Request.QueryString["Page"];
            string searchText = Request.QueryString["Search"];
            string role = Request.QueryString["Role"];
            Models.StaffSearch search = new Models.StaffSearch(pageString, searchText, role, 6);
            page = search.Page;
            maxPage = search.MaxPage;
            return search.StaffMembers;
        }

    }
}