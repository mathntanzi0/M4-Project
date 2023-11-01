using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Customer
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Models.Customer currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer != null)
                Response.Redirect("/Customer/Account");


            if (User.IsInRole("Admin"))
            {
                Response.Redirect("/Admin");
            }
        }
        protected void ValidatePhysicalAddress(object source, ServerValidateEventArgs args)
        {
            string physicalAddress = args.Value;
            if (string.IsNullOrEmpty(physicalAddress) || physicalAddress.Length < 10)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void CreateCustomer(object sender, EventArgs e)
        {
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string phoneNumber = PhoneNumber.Text;
            string physicalAddress = PhysicalAddress.Text;
            Models.Customer customer = new Models.Customer(firstName, lastName, Context.User.Identity.Name, phoneNumber, physicalAddress);
            customer.AddCustomer();
            if (Request.QueryString["ReturnUrl"] != null)
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            else
                Response.Redirect("Account");
        }
    }
}