using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M4_Project.Models.Sales
{
    ///
    /// <summary>
    ///     Represent data for an order line or event line.
    /// </summary>
    public class ItemLine
    {
        private int itemID;
        private int itemQuantity;
        private decimal itemCost;
        private decimal totalSubCost;
        private string instructions;
        private string itemName;
        private byte[] image;

        private string itemCategory;


        /// <summary>
        ///      Initializes a new instance of the M4_System.Models.Sells.ItemLine class.
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="itemQuantity"></param>
        /// <param name="itemCost"></param>
        /// <param name="instructions"></param>
        /// <param name="itemName"></param>
        /// <param name="image"></param>
        public ItemLine(int itemID, int itemQuantity, decimal itemCost, string instructions, string itemName, byte[] image, string itemCategory)
        {
            this.itemID = itemID;
            this.itemQuantity = itemQuantity;
            this.itemCost = itemCost;
            this.totalSubCost = itemCost * itemQuantity;
            this.instructions = instructions;
            this.itemName = itemName;
            this.image = image;
            this.itemCategory = itemCategory;
        }
        public int ItemID { get => itemID; set => itemID = value; }
        public int ItemQuantity { get => itemQuantity; set { itemQuantity = value; totalSubCost = itemCost * itemQuantity; } }
        public decimal ItemCost { get => itemCost; set => itemCost = value; }
        public string ItemCostN2 { get => itemCost.ToString("N2");}
        public decimal TotalSubCost { get => totalSubCost; set => totalSubCost = value; }
        public string TotalSubCostN2 { get => totalSubCost.ToString("N2"); }
        public string Instructions { get => (instructions != null) ? instructions : ""; set => instructions = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public byte[] Image { get => image; set => image = value; }
        public string ItemCategory { get => itemCategory; set => itemCategory = value; }
    }
    public class CartItem
    {
        private int itemID;
        private int itemQuantity;
        private string instructions;
        public readonly static string OrderCart = "OrderCart";
        public readonly static string BookingCart = "BookingCart";
        public readonly static string BookingInfo = "BookingInfo";
        public CartItem(int itemID, int itemQuantity, string instructions)
        {
            this.itemID = itemID;
            this.itemQuantity = itemQuantity;
            this.instructions = instructions;
        }
        public int ItemID { get => itemID; set => itemID = value; }
        public int ItemQuantity { get => itemQuantity; set => itemQuantity = value; }
        public string Instructions { get => instructions; set => instructions = value; }
    }
    public class ItemSummary
    {
        public int RowsCount { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
    }
}