using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M4_Project.Models.Sales
{
    /// <summary>
    ///     Represents sale types in a static class.
    /// </summary>
    public static class SaleType
    {
        /// <summary>
        ///     Represents an event booking.
        /// </summary>
        public readonly static int EventBooking = 0;

        /// <summary>
        ///     Represents an order.
        /// </summary>
        public readonly static int Order = 1;
    }

}