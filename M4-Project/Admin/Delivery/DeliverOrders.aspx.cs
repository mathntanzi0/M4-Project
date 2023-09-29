using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.Delivery
{
    public partial class DeliverOrders : System.Web.UI.Page
    {
        protected List<Models.Sales.Order> liveOrders;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                liveOrders = Models.Sales.Order.GetLiveDeliveryOrders();
                OrderRepeater.DataSource = liveOrders;
                OrderRepeater.DataBind();
            }
        }

        protected void OrderRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"order?Order={orderID}");
            }
        }
        private void BindOrders()
        {
            liveOrders = Models.Sales.Order.GetLiveDeliveryOrders();
            OrderRepeater.DataSource = liveOrders;
            OrderRepeater.DataBind();
        }
    }
}