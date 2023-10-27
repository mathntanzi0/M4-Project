using Newtonsoft.Json;
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
        public static readonly string title = "Live Delivery Orders";
        protected List<Models.Sales.Order> liveOrders;
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = title;
            if (!IsPostBack)
            {
                if (Session["Delivery"] != null || HttpContext.Current.Request.Cookies["Delivery"] != null)
                {
                    Response.Redirect("~/Admin/Delivery/Deliver");
                    return;
                }

                liveOrders = Models.Sales.Order.GetLiveDeliveryOrders();
                OrderRepeater.DataSource = liveOrders;
                OrderRepeater.DataBind();
            }
        }

        protected void OrderRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Deliver")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                Models.Sales.Order order = Models.Sales.Order.GetOrder_Short(orderID);
                Models.Sales.Delivery delivery = Models.Sales.Delivery.GetDelivery(orderID);

                Models.StaffLoginSession driver = Session["LoginStaff"] as Models.StaffLoginSession;

                if (!delivery.SetDeliveryDriver(driver))
                {
                    string script = "alert('Unable to assign');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    liveOrders = Models.Sales.Order.GetLiveDeliveryOrders();
                    OrderRepeater.DataSource = liveOrders;
                    OrderRepeater.DataBind();
                    return;
                }

                Models.Address address = new Models.Address(Models.BusinessRules.Address.centerLatitude, Models.BusinessRules.Address.centerLongitude);
                delivery.SetDriverLocation(address);
                order.Delivery = delivery;
                order.ChangeStatus(Models.Sales.OrderState.OnTheWay);

                string orderJSON = JsonConvert.SerializeObject(order);
                HttpCookie orderCookie = new HttpCookie("Delivery")
                {
                    Value = orderJSON,
                    Expires = DateTime.Now.AddDays(30)
                };
                HttpContext.Current.Response.Cookies.Add(orderCookie);

                Session["Delivery"] = order;
                Response.Redirect($"Deliver");
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