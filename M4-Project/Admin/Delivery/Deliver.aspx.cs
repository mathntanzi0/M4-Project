using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.Delivery
{
    public partial class Deliver : System.Web.UI.Page
    {
        protected Models.Sales.Order order;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Session["Delivery"] == null && HttpContext.Current.Request.Cookies["Delivery"] != null)
                {
                    var orderJSON = HttpContext.Current.Request.Cookies["Delivery"].Value;
                    order = JsonConvert.DeserializeObject<Models.Sales.Order>(orderJSON);
                    order.Delivery = Models.Sales.Delivery.GetDelivery(order.OrderID);
                    Session["Delivery"] = order;
                } 
                else if (Session["Delivery"] != null)
                {
                    order = Session["Delivery"] as Models.Sales.Order;
                }
                else
                {
                    Response.Redirect("~/Admin/Delivery/DeliverOrders");
                    return;
                }

                order = Session["Delivery"] as Models.Sales.Order;
                order.Customer = Models.Customer.GetCustomer(order.Customer.CustomerID);
            }
        }
        [WebMethod]
        public static void UpdateDriverLocation(double latitude, double longitude)
        {
            Models.Sales.Order order = HttpContext.Current.Session["Delivery"] as Models.Sales.Order;
            Models.Address newDriverLocation = new Models.Address(latitude, longitude);
            order.Delivery.UpdateDriverLocation(newDriverLocation);
        }
    }
}