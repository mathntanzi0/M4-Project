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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetDriverLocation(int orderID)
        {
            Models.Sales.Delivery delivery = Models.Sales.Delivery.GetDelivery(orderID);

            Models.Address address = delivery.GetDriverLocation();

            string location = $"{address.Latitude}, {address.Longitude}";

            return location;
        }
    }
}