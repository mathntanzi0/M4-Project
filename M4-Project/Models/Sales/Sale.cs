﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
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
        ///     Saves the Sales to the database.
        /// </summary>
        public abstract void RecordSell();
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
        /// <summary>
        ///     Attaches images to an AlternateView for use in an email.
        /// </summary>
        /// <param name="htmlView">The AlternateView to which the images will be attached.</param>
        protected void AttachImages(ref AlternateView htmlView)
        {
            for (int i = 0; i < itemLines.Count; i++)
            {
                ItemLine itemLine = itemLines[i];
                byte[] imageBytes = itemLine.Image;

                LinkedResource itemImage = new LinkedResource(new MemoryStream(imageBytes), MediaTypeNames.Image.Jpeg);
                itemImage.ContentId = "item_line" + itemLine.ItemID;
                htmlView.LinkedResources.Add(itemImage);
            }
        }




        private string SellTypeTitle { get; set; }
        private string SubTitle { get; set; }
        public int LoyaltyPoints { get; set; }
        public int SellID { get => sellID; set => sellID = value; }
        public decimal PaymentAmount { get => totalAmountDue; set => totalAmountDue = value; }
        public string TotalAmountDueN2 { get => totalAmountDue.ToString("N2");}
        public string PaymentMethod { get => paymentMethod; set => paymentMethod = value; }
        public decimal Tip { get => tip; set => tip = value; }
        public string TipN2 { get => tip.ToString("N2"); }
        public DateTime PaymentDate { get => paymentDate; set => paymentDate = value; }
        public string Str_PaymentDate { get => paymentDate.ToString("dd MMM yyyy HH:mm"); }
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