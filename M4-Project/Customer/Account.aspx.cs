using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M4_Project.Models;

namespace M4_Project.Customer
{
    public partial class Account : System.Web.UI.Page
    {
        public Models.Customer currentCustomer;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null)
                currentCustomer = Models.Customer.SetSession();
            
        }
    }
}