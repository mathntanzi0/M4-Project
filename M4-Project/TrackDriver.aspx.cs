using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class TrackDriver : System.Web.UI.Page
    {
        protected int orderID;
        protected Models.StaffMember driver;
        protected Models.Sales.Order order;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["liveOrder"] == null || !int.TryParse(Session["liveOrder"].ToString(), out orderID))
            {
                Response.Redirect("/");
                return;
            }

            order = Models.Sales.Order.GetOrder_Short(orderID);
            Models.Sales.Delivery delivery = Models.Sales.Delivery.GetDelivery(orderID);
            order.Delivery = delivery;
            if (delivery == null)
            {
                Response.Redirect("/");
                return;
            }


            driver = Models.StaffMember.GetStaffMember_Short(delivery.DriverID);
        }

        [WebMethod]
        public static string GetDriverLocation(int orderID)
        {
            Models.Sales.Delivery delivery = Models.Sales.Delivery.GetDelivery(orderID);
            Models.Sales.Order order = Models.Sales.Order.GetOrder_Short(orderID);

            if (Models.Sales.OrderState.IsFinalState(order.OrderStatus))
                return $"{1000}, {1000}";
            
            if (delivery == null)
                return $"{Models.BusinessRules.Address.centerLatitude}, {Models.BusinessRules.Address.centerLongitude}";
            
            Models.Address address = delivery.GetDriverLocation();
            string location = $"{address.Latitude}, {address.Longitude}";

            return location;
        }
    }
}