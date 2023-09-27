using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Customer
{
    public partial class Orders : System.Web.UI.Page
    {
        public Models.Customer currentCustomer;
        protected List<Models.Sales.Order> orders;

        protected void Page_Load(object sender, EventArgs e)
        {
            currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null)
                currentCustomer = Models.Customer.SetSession();

            orders = Models.Sales.Order.GetCustomerOrders(currentCustomer.CustomerID);
            orderRepeater.DataSource = orders;
            orderRepeater.DataBind();
        }

        protected string GetStatusColor(object status)
        {
            string orderStatus = status.ToString();
            return Models.Sales.OrderState.GetStatusColor(orderStatus);
        }
    }
}