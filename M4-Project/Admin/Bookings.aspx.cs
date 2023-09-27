using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class Bookings : System.Web.UI.Page
    {
        protected List<Models.Sales.Booking> events;
        protected string p_NotFound = "The system currently does not have any event booking in place.";
        protected int page = 1;
        protected int maxPage = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                events = GetEvents();
                EventRepeater.DataSource = events;
                EventRepeater.DataBind();

                string customerName = Request.QueryString["Customer"];
                string eventStatus = Request.QueryString["EventStatus"];
                bool customerNameNotEmpty = !string.IsNullOrEmpty(customerName);
                bool eventStatusNotEmpty = !string.IsNullOrEmpty(eventStatus);

                if (customerNameNotEmpty && eventStatusNotEmpty)
                {
                    search_bar.Text = customerName;
                    select_event_status.SelectedValue = eventStatus;
                    p_NotFound = "No booking for \"" + customerName + "\" with event status \"" + eventStatus + "\"";
                }
                else if (customerNameNotEmpty)
                {
                    search_bar.Text = customerName;
                    p_NotFound = "No booking for \"" + customerName + "\"";
                }
                else if (eventStatusNotEmpty)
                {
                    select_event_status.SelectedValue = eventStatus;
                    p_NotFound = "No booking with event status \"" + eventStatus + "\"";
                }
            }

        }
        protected void EventRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int bookingID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"Booking?Event={bookingID}");
            }
        }
        private List<Models.Sales.Booking> GetEvents()
        {
            string pageString = Request.QueryString["Page"];
            string bookingID = Request.QueryString["Event"];
            string staffID = Request.QueryString["Staff"];
            string customerName = Request.QueryString["Customer"];
            string eventStatus = Request.QueryString["EventStatus"];

            Models.Sales.BookingSearch search = new Models.Sales.BookingSearch(pageString, bookingID, staffID, customerName, eventStatus, 6);
            page = search.Page;
            maxPage = search.MaxPage;
            return search.Events;
        }
        protected void SelectEventStatus_Changed(object sender, EventArgs e)
        {
            string eventStatus = select_event_status.SelectedValue;
            string bookingID = Request.QueryString["Event"];
            string staffID = Request.QueryString["Staff"];
            string searchQuery = search_bar.Text;

            int booking_searchQuery;
            if (int.TryParse(searchQuery, out booking_searchQuery))
                Redirect("1", booking_searchQuery.ToString(), staffID, null, eventStatus);
            else
                Redirect("1", bookingID, staffID, searchQuery, eventStatus);
        }
        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string eventStatus = select_event_status.SelectedValue;
            string bookingID = Request.QueryString["Event"];
            string staffID = Request.QueryString["Staff"];
            string searchQuery = search_bar.Text;

            int booking_searchQuery;
            if (int.TryParse(searchQuery, out booking_searchQuery))
                Redirect("1", booking_searchQuery.ToString(), staffID, null, eventStatus);
            else
                Redirect("1", bookingID, staffID, searchQuery, eventStatus);
        }
        private void Redirect(string pageString, string bookingID, string staffID, string customerName, string eventState)
        {
            pageString = HttpUtility.UrlEncode(pageString);
            bookingID = HttpUtility.UrlEncode(bookingID);
            staffID = HttpUtility.UrlEncode(staffID);
            customerName = HttpUtility.UrlEncode(customerName);
            eventState = HttpUtility.UrlEncode(eventState);

            string redirectUrl = "/Admin/Bookings";
            bool hasParameters = false;

            if (!string.IsNullOrEmpty(pageString))
            {
                redirectUrl += "?Page=" + pageString;
                hasParameters = true;
            }

            if (!string.IsNullOrEmpty(bookingID))
            {
                if (hasParameters)
                {
                    redirectUrl += "&";
                }
                else
                {
                    redirectUrl += "?";
                    hasParameters = true;
                }

                redirectUrl += "Event=" + bookingID;
            }

            if (!string.IsNullOrEmpty(staffID))
            {
                if (hasParameters)
                {
                    redirectUrl += "&";
                }
                else
                {
                    redirectUrl += "?";
                    hasParameters = true;
                }

                redirectUrl += "Staff=" + staffID;
            }

            if (!string.IsNullOrEmpty(customerName))
            {
                if (hasParameters)
                {
                    redirectUrl += "&";
                }
                else
                {
                    redirectUrl += "?";
                    hasParameters = true;
                }

                redirectUrl += "Customer=" + customerName;
            }

            if (!string.IsNullOrEmpty(eventState))
            {
                if (hasParameters)
                {
                    redirectUrl += "&";
                }
                else
                {
                    redirectUrl += "?";
                }

                redirectUrl += "EventStatus=" + eventState;
            }

            Response.Redirect(redirectUrl);
        }
        protected void btnPreviousPage_Click(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;
            string eventStatus = select_event_status.SelectedValue;

            if (!int.TryParse(Request.QueryString["Page"], out int currentPage) || currentPage < 2)
                currentPage = 1;
            
            else
                currentPage--;
            
            Redirect(currentPage.ToString(), Request.QueryString["Event"], Request.QueryString["Staff"], searchQuery, eventStatus);
        }
        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;
            string eventStatus = select_event_status.SelectedValue;

            if (!int.TryParse(Request.QueryString["Page"], out int currentPage) || currentPage < 1)
                currentPage = 2;
            else
                currentPage++;
            Redirect(currentPage.ToString(), Request.QueryString["Event"], Request.QueryString["Staff"], searchQuery, eventStatus);
        }

    }
}