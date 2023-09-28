using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.POS
{
    public partial class Customers : System.Web.UI.Page
    {
        protected List<Models.Customer> customers;
        protected string p_NotFound = "The system currently does not have any customers";
        protected int page = 1;
        protected int maxPage = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                customers = GetCustomers();
                CustomerRepeater.DataSource = customers;
                CustomerRepeater.DataBind();

                string customerName = Request.QueryString["Customer"];
                bool customerNameNotEmpty = !string.IsNullOrEmpty(customerName);

                if (customerNameNotEmpty)
                {
                    search_bar.Text = customerName;
                    p_NotFound = "No customer found for search \"" + customerName + "\"";
                }
            }
        }
        protected void CustomerRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int customerID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edit")
                Response.Redirect($"Customer?Customer={customerID}");
            if (e.CommandName == "MakeBooking")
            {
                Session["BookingCustomer"] = customerID;
                Response.Redirect("/Admin/POS/MakeBooking");
            }
        }
        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;
            Redirect("1", searchQuery);
        }
        private void Redirect(string pageString, string searchText)
        {
            pageString = HttpUtility.UrlEncode(pageString);
            searchText = HttpUtility.UrlEncode(searchText);

            string redirectUrl = "/Admin/POS/Customers";
            bool hasParameters = false;

            if (!string.IsNullOrEmpty(pageString))
            {
                redirectUrl += "?Page=" + pageString;
                hasParameters = true;
            }

            if (!string.IsNullOrEmpty(searchText))
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

                redirectUrl += "Customer=" + searchText;
            }

            Response.Redirect(redirectUrl);
        }
        private List<Models.Customer> GetCustomers()
        {
            string pageString = Request.QueryString["Page"];
            string customerName = Request.QueryString["Customer"];
            Models.CustomerSearch search = new Models.CustomerSearch(pageString, customerName, 6);
            page = search.Page;
            maxPage = search.MaxPage;
            return search.Customers;
        }
        protected void btnPreviousPage_Click(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;

            if (!int.TryParse(Request.QueryString["Page"], out int currentPage) || currentPage < 2)
                currentPage = 1;

            else
                currentPage--;

            Redirect(currentPage.ToString(), searchQuery);
        }
        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            string searchQuery = search_bar.Text;

            if (!int.TryParse(Request.QueryString["Page"], out int currentPage) || currentPage < 1)
                currentPage = 2;
            else
                currentPage++;

            Redirect(currentPage.ToString(), searchQuery);
        }
    }
}