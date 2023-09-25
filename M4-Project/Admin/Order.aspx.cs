using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class Order : System.Web.UI.Page
    {
        Models.Customer customer;
        Models.StaffMember staffMember;
        Models.Sales.Order order;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ReturnUrl"] == null)
                Response.Redirect("/");

            int orderID;
            if (!int.TryParse(Request.QueryString["ReturnUrl"], out orderID))
                Response.Redirect("/");

            order = Models.Sales.Order.GetOrder(orderID);
            if (order == null)
                Response.Redirect("/");

            customer = order.Customer;
            staffMember = Models.StaffMember.GetStaffMember(order.StaffID);
        }
    }
}