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
            if (!IsPostBack) {
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
                    btnAddCustomer.Text = "Edit Customer";
                }
            }
        }
        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string address = txtAddress.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtNumber.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
            {
                string script = "alert('Please fill in all required fields.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            if (!IsValidEmail(email))
            {
                string script = "alert('Invalid email format. Please enter a valid email address.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            if (!IsValidPhoneNumber(phoneNumber))
            {
                string script = "alert('Invalid phone number format. Please enter a valid 10-digit phone number.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            Models.Customer customer = new Models.Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                PhysicalAddress = address,
                EmailAddress = email,
                PhoneNumber = phoneNumber,
                LoyaltyPoints = 0
            };
            if (Request.QueryString["Customer"] != null && int.TryParse(Request.QueryString["Customer"], out int customerID))
            {
                customer.CustomerID = customerID;
                customer.UpdateCustomer();
            }
            else
                customer.AddCustomer();

            Response.Redirect("/Admin/POS/Customers");
        }
        protected void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            int customerID = 0;
            if (Request.QueryString["Customer"] == null || !int.TryParse(Request.QueryString["Customer"], out customerID)) 
            {
                string script = "alert('Failed to delete customer.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Response.Redirect("/Admin/POS/Customers");
            }

            if (Models.Customer.DeleteCustomer(customerID))
            {
                string script = "alert('Customer deleted successfully.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Response.Redirect("/Admin/POS/Customers");
            }
            else
            {
                string script = "alert('Failed to delete customer.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }


        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string phonePattern = @"^\d{10}$";
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, phonePattern);
        }
    }
}