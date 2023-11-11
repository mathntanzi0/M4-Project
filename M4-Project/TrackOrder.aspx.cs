using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class TrackOrder : System.Web.UI.Page
    {
        protected int orderID;
        protected Models.Sales.Order order;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["liveOrder"] == null)
                Response.Redirect("/");

            if (!int.TryParse(Session["liveOrder"].ToString(), out orderID))
                Response.Redirect("/");

            order = Models.Sales.Order.GetOrder(orderID);
            if (order == null)
            {
                Session["liveOrder"] = null;
                Response.Redirect("/");
            }
            if (order.OrderStatus == Models.Sales.OrderState.Pending)
                Response.Redirect("/Processing");
            

            if (Models.Sales.OrderState.IsFinalState(order.OrderStatus))
                Session["liveOrder"] = null;
            
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static bool[] GetActivation()
        {
            bool[] activation = GetCardActivation();
            return activation;
        }
        public static bool[] GetCardActivation()
        {
            if (HttpContext.Current.Session["liveOrder"] == null) 
                return  new bool[] {true, true, true, true};

            int orderID = (int)HttpContext.Current.Session["liveOrder"];
            Models.Sales.Order order = Models.Sales.Order.GetOrder_Short(orderID);
            if (order == null)
                return null;
            if (Models.Sales.OrderState.IsFinalState(order.OrderStatus))
            {
                HttpContext.Current.Session["liveOrder"] = null;
                if (order.OrderStatus != Models.Sales.OrderState.Collected || order.OrderStatus != Models.Sales.OrderState.Delivered)
                    return new bool[] {false};
                
            }


            bool[] activation = { false, false, false, false };
            int activationEnd;
            if (order.OrderStatus == Models.Sales.OrderState.Preparing)
                activationEnd = 1;
            else if (order.OrderStatus == Models.Sales.OrderState.Prepared)
                activationEnd = 2;
            else if (order.OrderStatus == Models.Sales.OrderState.OnTheWay)
                activationEnd = 3;
            else if (order.OrderStatus == Models.Sales.OrderState.Delivered || order.OrderStatus == Models.Sales.OrderState.Collected)
                activationEnd = 4;
            else
                activationEnd = -1;
            for (int i = 0; i < activationEnd; i++)
                activation[i] = true;
            
            return activation;
        }
    }
}