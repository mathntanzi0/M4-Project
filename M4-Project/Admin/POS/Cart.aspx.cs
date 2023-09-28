using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.POS
{
    public partial class Cart : System.Web.UI.Page
    {
        protected decimal TotalCost = 0;
        protected Models.Sales.Sale sale;
        protected void Page_Load(object sender, EventArgs e)
        {
            sale = HttpContext.Current.Session["POS"] as Models.Sales.Sale;
            if (sale != null)
            {
                var reversedItemLines = sale.ItemLines.ToList();
                reversedItemLines.Reverse();

                RepeaterItemLine.DataSource = reversedItemLines;
                RepeaterItemLine.DataBind();

                TotalCost = CalculateTotalCost(reversedItemLines);
            }
        }

        private decimal CalculateTotalCost(List<Models.Sales.ItemLine> itemLines)
        {
            decimal totalCost = 0;

            foreach (var itemLine in itemLines)
                totalCost += itemLine.TotalSubCost;
            

            return totalCost;
        }
        protected void btnOrderCart_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["POS"] = new Models.Sales.Order();
            Response.Redirect("/Admin/POS/Menu");
        }
        protected void btnBookingCart_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["POS"] = null;
            Response.Redirect("/Admin/POS/MakeBooking");
        }
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["POS"] != null)
            {
                Models.Sales.Sale sale = Session["POS"] as Models.Sales.Sale;
                if (sale.SaleType == Models.Sales.SaleType.Order)
                {
                    Models.Sales.Order order = (Models.Sales.Order)sale;
                    order.OrderType = Models.Sales.OrderType.InStore;
                } 
                Response.Redirect("/Admin/POS/Checkout");
            }
            else
                Response.Redirect("/Admin/POS/Cart");
        }
    }
}