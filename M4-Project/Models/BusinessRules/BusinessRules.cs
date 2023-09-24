using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M4_Project.Models.BusinessRules
{
    ///
    /// <summary>
    ///     The rules set limits for certain attributes of MenuItem.
    /// </summary>
    public static class MenuItem
    {
        ///
        /// <summary>
        ///     Set the maximum number of characters for item name.
        /// </summary>
        public static readonly int ItemNameCharLimit = 128;
    }
    ///
    /// <summary>
    ///     The rules set limits for certain attributes of ItemLine.
    /// </summary>
    public static class ItemLine
    {
        ///
        /// <summary>
        ///     Set the maximum number of items that can be added to a cart.
        /// </summary>
        public static readonly int MaxMenuItems = 32;
        ///
        /// <summary>
        ///     Set the minimum number of items on the cart that are needed for the transaction to be completed.
        /// </summary>
        public static readonly int MinMenuItems = 1;
        ///
        /// <summary>
        ///     Set the maximum number of characters for item line instructions.
        /// </summary>
        public static readonly int InstructionCharLimit = 256;
    }
    public static class Sale
    {
        public static decimal LoyaltyPointsRatio = 0.1M;
    }
    ///
    /// <summary>
    ///     The rules set limits for certain attributes of Booking.
    /// </summary>
    public static class Booking
    {
        ///
        /// <summary>
        ///     Set the minimum date a new event can take.
        /// </summary>
        public static readonly DateTime MinEventDate = DateTime.Now.AddDays(3);
        ///
        /// <summary>
        ///     Set the maximum duration a event can have.
        /// </summary>
        public static readonly TimeSpan MixEventDuration = new TimeSpan(4, 0, 0);
        ///
        /// <summary>
        ///     Set the maximum quantity of each items that an event line can have.
        /// </summary>
        public static readonly int MixItemQuantity = 25;
    }
    public static class Address
    {
        /// <summary>
        ///     Business Location
        /// </summary>
        public static readonly double centerLatitude = 0.0;
        public static readonly double centerLongitude = 0.0;
    }
    public static class Delivery
    {
        public static readonly decimal DeliveryFee = 50;
    }
}