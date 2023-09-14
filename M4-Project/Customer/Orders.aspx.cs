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

            if (orderStatus == Models.Sales.OrderState.Pending)
                return "#F46036";
            else if (orderStatus == Models.Sales.OrderState.Preparing)
                return "#F5AF36";
            else if (orderStatus == Models.Sales.OrderState.Prepared)
                return "#1B998B";
            else if (orderStatus == Models.Sales.OrderState.Unsuccessful)
                return "#D7263D";
            else if (orderStatus == Models.Sales.OrderState.OnTheWay)
                return "#2E294E";
            else if (orderStatus == Models.Sales.OrderState.Collected)
                return "green";
            else if (orderStatus == Models.Sales.OrderState.Delivered)
                return "green";
            else if (orderStatus == Models.Sales.OrderState.Rejected)
                return "#D7263D";
            else
                return "#000000";
        }
    }
}