using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class Processing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static int IsPending()
        {
            if (HttpContext.Current.Session["liveOrder"] == null)
                return 0;

            int orderID = (int)HttpContext.Current.Session["liveOrder"];
            Models.Sales.Order order = Models.Sales.Order.GetOrder_Short(orderID);
            if (order.OrderStatus == Models.Sales.OrderState.Pending)
                return 1;

            return 0;
        }
    }
}