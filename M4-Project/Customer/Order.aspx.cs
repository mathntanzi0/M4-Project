using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Customer
{
    public partial class Order : System.Web.UI.Page
    {
        protected Models.Customer customer;
        protected Models.Sales.Order order;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["Order"] == null)
                Response.Redirect("/");

            int orderID;
            if (!int.TryParse(Request.QueryString["Order"], out orderID))
                Response.Redirect("/");

            Models.Customer currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null)
                currentCustomer = Models.Customer.SetSession();
            
            order = Models.Sales.Order.GetOrder(orderID);
            if (order == null || order.Customer.CustomerID != currentCustomer.CustomerID)
                Response.Redirect("/");

            customer = order.Customer;
            ItemRepeater.DataSource = order.ItemLines;
            ItemRepeater.DataBind();
        }
    }
}