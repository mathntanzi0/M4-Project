using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected decimal TotalCost = 0;
        protected Models.Sales.Sale sale;
        protected void Page_Load(object sender, EventArgs e)
        {
            sale = HttpContext.Current.Session["sale"] as Models.Sales.Sale;
            if (sale != null && sale.ItemLines.Count > 0)
            {
                if (sale.SaleType == Models.Sales.SaleType.Order && ((Models.Sales.Order) sale).OrderType == Models.Sales.OrderType.Delivery)
                {
                    Models.Sales.Order order = (Models.Sales.Order) sale;
                    if (order.OrderType == Models.Sales.OrderType.Delivery && order.Delivery == null)
                        Response.Redirect("/SelectAddress");
                }
                var reversedItemLines = sale.ItemLines.ToList();
                reversedItemLines.Reverse();

                RepeaterItemLine.DataSource = reversedItemLines;
                RepeaterItemLine.DataBind();

                TotalCost = CalculateTotalCost(reversedItemLines);
                if (sale.SaleType == Models.Sales.SaleType.Order && (sale as Models.Sales.Order).OrderType == Models.Sales.OrderType.Delivery)
                    TotalCost += Models.BusinessRules.Delivery.DeliveryFee;
            } 
            else
                Response.Redirect("/Cart");
        }
        private decimal CalculateTotalCost(List<Models.Sales.ItemLine> itemLines)
        {
            decimal totalCost = 0;

            foreach (var itemLine in itemLines)
                totalCost += itemLine.TotalSubCost;


            return totalCost;
        }
    }
}