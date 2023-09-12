using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class SelectAddress : System.Web.UI.Page
    {
        protected double latitude = -29.621091867599112;
        protected double longitude = 30.394936263745304;
        protected void Page_Load(object sender, EventArgs e)
        {
            Models.Sales.Order sale = HttpContext.Current.Session["sale"] as Models.Sales.Order;

            if (sale != null)
            {
                if (sale.Delivery != null)
                {
                    latitude = sale.Delivery.DeliveryAddress.Latitude;
                    longitude = sale.Delivery.DeliveryAddress.Longitude;
                }
            }
        }

        [WebMethod]
        public static void SaveSelectedAddress(string addressName, double latitude, double longitude)
        {
            try
            {
                Models.Sales.Order sale = HttpContext.Current.Session["sale"] as Models.Sales.Order;

                if (sale != null)
                {
                    Models.Address address = new Models.Address(addressName, latitude, longitude);
                    Models.Sales.Delivery delivery = new Models.Sales.Delivery(Models.BusinessRules.Delivery.DeliveryFee, address);
                    sale.Delivery = delivery;
                    HttpContext.Current.Session["sale"] = sale;
                }
                else
                    throw new Exception("Sale object not found in session.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error processing the selected address: " + ex.Message);
            }
        }
    }
}