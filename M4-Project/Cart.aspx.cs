using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project
{
    public partial class Cart : System.Web.UI.Page
    {
        protected decimal TotalCost = 0;
        protected Models.Sales.Sale sale;
        protected void Page_Load(object sender, EventArgs e)
        {
            sale = HttpContext.Current.Session["sale"] as Models.Sales.Sale;
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

        protected void btnCheckout_Click(object sender, EventArgs e)
        {

            if (Session["sale"] != null)
            {
                Models.Sales.Sale sale = Session["sale"] as Models.Sales.Sale;
                if (sale.SaleType == Models.Sales.SaleType.Order )
                {
                    Models.Sales.Order order = (Models.Sales.Order)sale;
                    if (select_category != null && select_category.Value == "D")
                        order.OrderType = Models.Sales.OrderType.Delivery;
                    else
                        order.OrderType = Models.Sales.OrderType.Collection;
                } 
                Response.Redirect("/Checkout");
            }
            else
                Response.Redirect("/Cart");
        }
    }
}