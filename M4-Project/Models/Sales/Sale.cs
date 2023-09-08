using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M4_Project.Models.Sales
{
    ///
    /// <summary>
    ///     Abstract class that holds transaction details and performs transaction orientated function.
    ///     Extended by Booking and Order classes.
    /// </summary>
    /// 
    public abstract class Sale
    {
        private int sellID;
        private decimal totalAmountDue;
        private string paymentMethod;
        private decimal tip;
        private DateTime paymentDate;
        private List<ItemLine> itemLines;
        private Dictionary<int, int> cart;

        ///
        /// <summary>
        ///     Adds M4_System.Models.Sells.ItemLine to the itemLines list.
        /// </summary>
        public int AddItemLine(ItemLine itemLine)
        {
            if (cart.ContainsKey(itemLine.ItemID))
                return AddItemError.AlreadyAdded;
            if (ItemLines.Count >= BusinessRules.ItemLine.MaxMenuItems)
                return AddItemError.MaxItems;

            ItemLines.Add(itemLine);
            int index = ItemLines.Count - 1;
            cart.Add(ItemLines[index].ItemID, index);
            return AddItemError.NoError;
        }
        ///
        /// <summary>
        ///     Saves the Sales to the database.
        /// </summary>
        public abstract void RecordSell();

        
        private string SellTypeTitle { get; set; }
        private string SubTitle { get; set; }
        public int SellID { get => sellID; set => sellID = value; }
        public decimal TotalAmountDue { get => totalAmountDue; set => totalAmountDue = value; }
        public string TotalAmountDueN2 { get => totalAmountDue.ToString("N2");}
        public string PaymentMethod { get => paymentMethod; set => paymentMethod = value; }
        public decimal Tip { get => tip; set => tip = value; }
        public string TipN2 { get => tip.ToString("N2"); }
        public DateTime PaymentDate { get => paymentDate; set => paymentDate = value; }
        public List<ItemLine> ItemLines { get => itemLines; set => itemLines = value; }
        public Dictionary<int, int> Cart { get => cart; set => cart = value; }
        public abstract int SaleType { get; }

    }
    public static class AddItemError
    {
        public static readonly int MaxItems = 0;
        public static readonly int AlreadyAdded = 1;
        public static readonly int NoError = 5;
    }
}