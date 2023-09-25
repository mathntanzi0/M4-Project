using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class LiveOrders : System.Web.UI.Page
    {
        protected List<Models.Sales.Order> newOrders;
        protected List<Models.Sales.Order> liveOrders;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                newOrders = Models.Sales.Order.GetPendingOrders();
                OrderRepeater.DataSource = newOrders;
                OrderRepeater.DataBind();
                liveOrders = Models.Sales.Order.GetLiveOrders();
                LiveOrderRepeater.DataSource = liveOrders;
                LiveOrderRepeater.DataBind();
            }
        }
    }
}