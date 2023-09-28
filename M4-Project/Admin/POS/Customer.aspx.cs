using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.POS
{
    public partial class Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Customer"] != null && int.TryParse(Request.QueryString["Customer"], out int custID))
            {
                Models.Customer customer = Models.Customer.GetCustomer(custID);
                if (customer == null)
                    Response.Redirect("/Admin/POS/Customers");

                txtFirstName.Text = customer.FirstName;
                txtLastName.Text = customer.LastName;
                txtEmail.Text = customer.EmailAddress;
                txtAddress.Text = customer.PhysicalAddress;
                txtNumber.Text = customer.PhoneNumber;
                btnAddCustomer.InnerText = "Edit Customer";
            }
        }
    }
}