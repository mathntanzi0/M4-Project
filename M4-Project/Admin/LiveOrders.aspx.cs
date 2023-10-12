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

        protected void LiveOrderRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"order?Order={orderID}");
            }
        }
        protected void NewOrderRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Accept" || e.CommandName == "Reject")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                string newOrderStatus = (e.CommandName == "Accept") ? Models.Sales.OrderState.Preparing : Models.Sales.OrderState.Rejected;
                
                if (Models.Sales.Order.ChangeStatus(orderID, newOrderStatus))
                {
                    Models.Sales.Order order = Models.Sales.Order.GetOrder(orderID);
                    order.Delivery = Models.Sales.Delivery.GetDelivery(orderID);
                    order.SendEmail();
                }

                Models.StaffLoginSession loginSession = Session["loginStaff"] as Models.StaffLoginSession;
                Models.StaffMember.SetOrderStaff(orderID, loginSession.StaffID);
                BindOrders();
            }
            if (e.CommandName == "View")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"order?Order={orderID}");
            }
        }
        private void BindOrders()
        {
            newOrders = Models.Sales.Order.GetPendingOrders();
            OrderRepeater.DataSource = newOrders;
            OrderRepeater.DataBind();
            liveOrders = Models.Sales.Order.GetLiveOrders();
            LiveOrderRepeater.DataSource = liveOrders;
            LiveOrderRepeater.DataBind();
        }
        protected void SelectOrderType_Changed(object sender, EventArgs e)
        {
            
            string selectedOrderType = select_order_type.SelectedValue;
            if (string.IsNullOrEmpty(selectedOrderType))
            {
                newOrders = Models.Sales.Order.GetPendingOrders();
                OrderRepeater.DataSource = newOrders;
                OrderRepeater.DataBind();
                liveOrders = Models.Sales.Order.GetLiveOrders();
                LiveOrderRepeater.DataSource = liveOrders;
                LiveOrderRepeater.DataBind();
                return;
            }
            liveOrders = Models.Sales.Order.GetLiveOrders(selectedOrderType);
            LiveOrderRepeater.DataSource = liveOrders;
            LiveOrderRepeater.DataBind();
        }


    }
}