using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Customer
{
    public partial class Update : System.Web.UI.Page
    {
        public Models.Customer currentCustomer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                currentCustomer = Session["Customer"] as Models.Customer;
                if (currentCustomer == null)
                    currentCustomer = Models.Customer.SetSession();

                FirstName.Text = currentCustomer.FirstName;
                LastName.Text = currentCustomer.LastName;
                PhoneNumber.Text = currentCustomer.PhoneNumber;
                Address.Text = currentCustomer.PhysicalAddress;
            }
        }
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string phoneNumber = PhoneNumber.Text;
            string address = Address.Text;

            currentCustomer = Session["Customer"] as Models.Customer;

            if (currentCustomer != null)
            {
                Models.Customer.UpdateDetails(firstName, lastName, phoneNumber, address, currentCustomer.CustomerID);
                currentCustomer = Models.Customer.GetCustomer(currentCustomer.CustomerID);
                Session["Customer"] = currentCustomer;
                Response.Redirect("/Customer/Account");
            }
        }


    }
}