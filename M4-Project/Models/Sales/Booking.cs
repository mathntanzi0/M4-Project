using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace M4_Project.Models.Sales
{
    ///
    /// <summary>
    ///     Represents a booking for an event.
    /// </summary>
    public class Booking : Sale, IComparable<Booking>
    {
        private int bookingID;
        private Customer customer;
        private string eventAddress;
        private string eventDecorDescription;
        private DateTime eventDate;
        private TimeSpan eventDuration;
        private string bookingStatus;

        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.Sales.Booking class.
        /// </summary>
        public Booking(int bookingID, Customer customer, DateTime eventDate, decimal paymentAmount, string bookingStatus)
        {
            this.BookingID = bookingID;
            this.customer = customer;
            this.eventDate = eventDate;
            this.PaymentAmount = paymentAmount;
            this.bookingStatus = bookingStatus;
        }
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.Sales.Booking class.
        /// </summary>
        public Booking(int bookingID, Customer customer, string eventAddress, string eventDecorDescription, DateTime eventDate, TimeSpan eventDuration, string bookingStatus)
        {
            this.bookingID = bookingID;
            this.eventAddress = eventAddress;
            this.customer = customer;
            this.eventDecorDescription = eventDecorDescription;
            this.eventDate = eventDate;
            this.eventDuration = eventDuration;
            this.bookingStatus = bookingStatus;
        }
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.Sales.Booking class.
        /// </summary>
        public Booking(string eventAddress, string eventDecorDescription, DateTime eventDate, TimeSpan eventDuration)
        {
            ItemLines = new List<ItemLine>();
            Cart = new Dictionary<int, int>();
            this.eventAddress = eventAddress;
            this.eventDecorDescription = eventDecorDescription;
            this.eventDate = eventDate;
            this.eventDuration = eventDuration;
        }
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.Sales.Booking class.
        /// </summary>
        public Booking(string eventAddress, DateTime eventDate, decimal paymentAmount, string bookingStatus)
        {
            this.eventAddress = eventAddress;
            this.eventDate = eventDate;
            this.PaymentAmount = paymentAmount;
            this.bookingStatus = bookingStatus;
        }
        ///
        /// <summary>
        ///     Records the attributes' values of an instance of M4_System.Models.Sales.Booking class to the database.
        /// </summary>
        public override void RecordSell()
        {
            if (ItemLines.Count < BusinessRules.ItemLine.MinMenuItems || customer.CustomerID < 1)
                return;

            string query = "INSERT INTO [Event Booking] ([customer_id], [event_date], [event_duration], [event_setting], [event_address], [payment_amount], [payment_method], [payment_date], [status]) " +
                           "VALUES (@customer_id, @event_date, @event_duration, @event_setting, @event_address, @payment_amount, @payment_method, @payment_date, @status);" +
                           "SELECT CAST(SCOPE_IDENTITY() AS INT) AS booking_id;";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@customer_id", customer.CustomerID);
                command.Parameters.AddWithValue("@event_date", EventDate);
                command.Parameters.AddWithValue("@event_duration", EventDuration);
                command.Parameters.AddWithValue("@event_setting", EventDecorDescription);
                command.Parameters.AddWithValue("@event_address", EventAddress);
                command.Parameters.AddWithValue("@payment_amount", PaymentAmount);
                command.Parameters.AddWithValue("@payment_method", PaymentMethod);
                command.Parameters.AddWithValue("@payment_date", PaymentDate);
                command.Parameters.AddWithValue("@status", BookingState.Pending);
                connection.Open();

                this.bookingID = (int)command.ExecuteScalar();
            }
            RecordEventLine();
        }
        /// <summary>
        ///     Checks if a given event date falls on an unavailable date.
        /// </summary>
        /// <param name="eventDate">The DateTime representing the event date to be checked.</param>
        /// <returns>
        ///   <c>true</c> if the provided event date falls on an unavailable date; otherwise, <c>false</c>.
        /// </returns>
        public static bool isDateUnavailable(DateTime eventDate)
        {
            List<DateTime> unavailableDates = UnavailableDates();
            return (unavailableDates.Contains(eventDate.Date));
        }
        ///
        /// <summary>
        ///     Records the M4_System.Models.Sells.ItemLine attributes' values in the instance of M4_System.Models.Sales.Sale:Booking class.
        /// </summary>
        private void RecordEventLine()
        {
            string query = "INSERT INTO [Event Line] ([item_id], [booking_id], [item_quantity], [sub_cost], [instructions]) VALUES (@item_id, @booking_id, @item_quantity, @sub_cost, @instructions);";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                connection.Open();

                foreach (ItemLine itemLine in ItemLines)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@item_id", itemLine.ItemID);
                    command.Parameters.AddWithValue("@booking_id", this.bookingID);
                    command.Parameters.AddWithValue("@item_quantity", itemLine.ItemQuantity);
                    command.Parameters.AddWithValue("@sub_cost", itemLine.TotalSubCost);
                    command.Parameters.AddWithValue("@instructions", itemLine.Instructions);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Update(int bookingID, string address, DateTime date, TimeSpan duration, string decorDescription)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    connection.Open();

                    string query = "UPDATE [Event Booking] " +
                                   "SET [event_address] = @Address, " +
                                   "[event_date] = @EventDate, " +
                                   "[event_duration] = @EventDuration, " +
                                   "[event_setting] = @DecorDescription " +
                                   "WHERE [booking_id] = @BookingID";

                    using (SqlCommand cmdUpdateEvent = new SqlCommand(query, connection))
                    {
                        cmdUpdateEvent.Parameters.AddWithValue("@BookingID", bookingID);
                        cmdUpdateEvent.Parameters.AddWithValue("@Address", address);
                        cmdUpdateEvent.Parameters.AddWithValue("@EventDate", date);
                        cmdUpdateEvent.Parameters.AddWithValue("@EventDuration", duration);
                        cmdUpdateEvent.Parameters.AddWithValue("@DecorDescription", decorDescription);

                        cmdUpdateEvent.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log or display an error message
            }
        }


        ///
        /// <summary>
        ///     Change the status of an event booking on the database.
        /// </summary>
        public bool ChangeStatus(string bookingStatus)
        {
            if (this.BookingStatus == bookingStatus)
                return false;

            if (BookingState.IsFinalState(this.BookingStatus) && !BookingState.IsFinalState(bookingStatus))
                return false;

            if (this.BookingStatus == Models.Sales.BookingState.Pending)
                SendEmail();

            this.BookingStatus = bookingStatus;
            ChangeStatus(BookingID, bookingStatus);
            return true;
        }
        ///
        /// <summary>
        ///     Change the status of an event booking on the database using the booking identification number as parameter.
        /// </summary>
        public static void ChangeStatus(int bookingID, string bookingStatus)
        {
            if (!BookingState.IsValidState(bookingStatus))
                return;


            string query = "UPDATE [Event Booking] SET [status] = @status WHERE[booking_id] = @bookingID";
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@bookingID", bookingID);
                command.Parameters.AddWithValue("@status", bookingStatus);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }
        ///
        /// <summary>
        ///     Returns a event booking for a specific booking identification.
        /// </summary>
        public static Booking GetBooking(int bookingID, bool getEventLine)
        {
            string query = "SELECT * FROM [Event Booking] WHERE booking_id = @booking_id";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@booking_id", bookingID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return null;

                    reader.Read();

                    Customer customer = Customer.GetCustomer(reader.GetInt32(reader.GetOrdinal("customer_id")));
                    Booking booking = new Booking(
                        reader.GetInt32(reader.GetOrdinal("booking_id")),
                        customer,
                        reader.GetString(reader.GetOrdinal("event_address")),
                        (reader.IsDBNull(reader.GetOrdinal("event_setting")) || string.IsNullOrEmpty(reader["event_setting"].ToString())) ? "none" : reader.GetString(reader.GetOrdinal("event_setting")),
                        reader.GetDateTime(reader.GetOrdinal("event_date")),
                        reader.GetTimeSpan(reader.GetOrdinal("event_duration")),
                        reader.GetString(reader.GetOrdinal("status"))
                    );
                    booking.PaymentDate = reader.GetDateTime(reader.GetOrdinal("payment_date"));
                    booking.PaymentAmount = reader.GetDecimal(reader.GetOrdinal("payment_amount"));
                    booking.PaymentMethod = reader.GetString(reader.GetOrdinal("payment_method"));

                    if (getEventLine)
                        booking.ItemLines = GetEventLines(bookingID);

                    return booking;
                }
            }
        }
        /// <summary>
        ///     Retrieves a list of bookings made by a customer.
        /// </summary>
        /// <param name="customerID">The ID of the customer whose bookings are to be retrieved.</param>
        /// <returns>A list of Booking objects representing the customer's bookings.</returns>
        public static List<Booking> GetCustomerBookings(int customerID)
        {
            List<Booking> bookings = new List<Booking>();
            string query = "SELECT TOP(10) [booking_id], [event_date], [event_address], [payment_amount], [status] " +
                            "FROM [Event Booking] " +
                            "WHERE [customer_id] = @customerID " +
                            "ORDER BY [event_date] DESC";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@customerID", customerID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int bookingID = (int)reader["booking_id"];
                        string eventAddress = reader["event_address"].ToString();
                        DateTime eventDate = (DateTime)reader["event_date"];
                        decimal paymentAmount = (decimal)reader["payment_amount"];
                        string bookingStatus = reader["status"].ToString();

                        Booking booking = new Booking(eventAddress, eventDate, paymentAmount, bookingStatus);
                        booking.BookingID = bookingID;
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }
        ///
        /// <summary>
        ///     Returns a event line for a specific booking identification.
        /// </summary>
        public static List<ItemLine> GetEventLines(int bookingID)
        {
            string query = "SELECT [Event Line].item_id, [Menu Item].item_name, [Menu Item].item_price, [Menu Item].item_image, booking_id, item_quantity, sub_cost, instructions " +
                           "FROM dbo.[Event Line], [Menu Item] " +
                           "WHERE booking_id = @bookingID AND [Event Line].item_id = [Menu Item].item_id;";
            List<ItemLine> itemLines = new List<ItemLine>();

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@bookingID", bookingID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ItemLine itemLine = new ItemLine(
                            reader.GetInt32(reader.GetOrdinal("item_id")),
                            reader.GetInt32(reader.GetOrdinal("item_quantity")),
                            reader.GetDecimal(reader.GetOrdinal("item_price")),
                            reader.IsDBNull(reader.GetOrdinal("instructions")) ? "" : reader.GetString(reader.GetOrdinal("instructions")),
                            reader.GetString(reader.GetOrdinal("item_name")),
                            (byte[])reader["item_image"],
                            ""
                        );

                        itemLines.Add(itemLine);
                    }
                }
            }

            return itemLines;
        }
        ///
        /// <summary>
        ///     Returns a list of event bookings with similar customer name or same customer name.
        /// </summary>
        public static List<Booking> GetBookings(int page, int maxListSize, string customerName)
        {
            string query = "SELECT booking_id, [Customer].first_name, [Customer].last_name, event_date, payment_amount, [status] " +
                "FROM[Event Booking], [Customer] " +
                "WHERE[Customer].customer_id = [Event Booking].customer_id AND[Customer].first_name + ' ' + [Customer].last_name LIKE '%' + @searchValue + '%' " +
                "ORDER BY[Customer].first_name + ' ' + [Customer].last_name ASC, event_date DESC " +
                "OFFSET @page ROWS " +
                "FETCH NEXT @limit ROWS ONLY; ";

            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@searchValue", customerName);
                command.Parameters.AddWithValue("@page", ((page - 1) * maxListSize));
                command.Parameters.AddWithValue("@limit", maxListSize);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking(
                            (int) reader["booking_id"],
                            new Customer(reader["first_name"].ToString(), reader["last_name"].ToString()),
                            (DateTime) reader["event_date"],
                            (decimal) reader["payment_amount"],
                            reader["status"].ToString()
                        );
                        bookings.Add(booking);
                    }
                    connection.Close();
                }
                return bookings;
            }
        }
        /// <summary>
        ///     Synchronizes the session with cookies for booking information and cart items.
        /// </summary>
        public static void SyncSessionWithCookies()
        {
            if (HttpContext.Current.Request.Cookies[Models.Sales.CartItem.BookingInfo] != null)
            {
                var bookingInfoCookie = HttpContext.Current.Request.Cookies[Models.Sales.CartItem.BookingInfo];
                string address = bookingInfoCookie.Values["Address"];
                string decorDescription = bookingInfoCookie.Values["DecorDescription"];
                DateTime date;
                TimeSpan duration;

                if (DateTime.TryParse(bookingInfoCookie.Values["Date"], out date)) { }
                else
                    date = DateTime.Now.AddDays(1);
                if (TimeSpan.TryParse(bookingInfoCookie.Values["Duration"], out duration)) { }
                else
                    duration = TimeSpan.Zero;

                Models.Sales.Sale sale = new Models.Sales.Booking(address, decorDescription, date, duration);
                HttpContext.Current.Session["sale"] = sale;
            }
            else
                return;

            if (HttpContext.Current.Request.Cookies[Models.Sales.CartItem.BookingCart] != null)
            {
                var bookingCartCookie = HttpContext.Current.Request.Cookies[Models.Sales.CartItem.BookingCart];
                var cartJSON = bookingCartCookie.Value;
                var cookieCart = JsonConvert.DeserializeObject<List<Models.Sales.CartItem>>(cartJSON);

                Models.Sales.Sale sale;
                if (HttpContext.Current.Session["sale"] != null)
                    sale = HttpContext.Current.Session["sale"] as Models.Sales.Sale;
                else
                    return;

                foreach (var cookieCartItem in cookieCart)
                {
                    Models.MenuItem item = Models.MenuItem.GetMenuItem(cookieCartItem.ItemID);
                    sale.AddItemLine(new Models.Sales.ItemLine(
                        cookieCartItem.ItemID,
                        cookieCartItem.ItemQuantity,
                        item.ItemPrice,
                        cookieCartItem.Instructions,
                        item.ItemName,
                        item.ItemImage,
                        item.ItemCategory));
                }
                HttpContext.Current.Session["sale"] = sale;
            }
        }
        /// <summary>
        ///     Retrieves a list of dates that are unavailable due to the maximum number of events booked per day being exceeded.
        /// </summary>
        /// <returns>A list of DateTime objects representing unavailable dates.</returns>
        public static List<DateTime> UnavailableDates()
        {
            List<DateTime> unavailableDates = new List<DateTime>();
            string query = "SELECT year, month, day " +
                           "FROM ( " +
                           "    SELECT COUNT(booking_id) as count, YEAR([Event Booking].event_date) as year, MONTH([Event Booking].event_date) as month, DAY([Event Booking].event_date) as day " +
                           "    FROM [Event Booking] " +
                           "    GROUP BY YEAR([Event Booking].event_date), MONTH([Event Booking].event_date), DAY([Event Booking].event_date) " +
                           "    HAVING COUNT(booking_id) > @maxPerDay " +
                           ") AS subquery ";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maxPerDay", (BusinessRules.Booking.MaxEvents - 1));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int year = reader.GetInt32(0);
                            int month = reader.GetInt32(1);
                            int day = reader.GetInt32(2);
                            unavailableDates.Add(new DateTime(year, month, day));
                        }
                    }
                }
            }
            return unavailableDates;
        }
        public static ItemSummary GetItemSummary(int itemID)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            StringBuilder query = new StringBuilder();
            query.Append("SELECT COUNT([item_id]) AS RowsCount, SUM([Event Line].item_quantity) AS TotalQty, SUM(sub_cost) AS TotalAmount");
            query.Append(" FROM [Event Line], [Event Booking]");
            query.Append(" WHERE [Event Booking].booking_id = [Event Line].booking_id");
            query.Append(" AND MONTH([Event Booking].payment_date) = @month");
            query.Append(" AND YEAR([Event Booking].payment_date) = @year");
            query.Append(" AND item_id = @itemID;");

            ItemSummary itemSummary = new ItemSummary();
            using (SqlCommand command = new SqlCommand(query.ToString()))
            {
                using (Database dbConnection = new Database(command))
                {
                    dbConnection.Command.Parameters.AddWithValue("@month", month);
                    dbConnection.Command.Parameters.AddWithValue("@year", year);
                    dbConnection.Command.Parameters.AddWithValue("@itemID", itemID);

                    using (SqlDataReader reader = dbConnection.Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            itemSummary.RowsCount = reader["RowsCount"] != DBNull.Value ? (int)reader["RowsCount"] : 0;
                            itemSummary.TotalQty = reader["TotalQty"] != DBNull.Value ? (int)reader["TotalQty"] : 0;
                            itemSummary.TotalAmount = reader["TotalAmount"] != DBNull.Value ? (decimal)reader["TotalAmount"] : 0;
                        }
                    }
                }
            }
            return itemSummary;
        }
        /// <summary>
        ///     Checks if a given DateTime falls within a specified date range defined by minimum and maximum dates.
        /// </summary>
        /// <param name="date">The DateTime value to be checked.</param>
        /// <returns>
        ///   <c>true</c> if the provided date falls within the defined date range; otherwise, <c>false</c>.
        /// </returns>
        public static bool dateInRange(DateTime date)
        {
            return (date >= BusinessRules.Booking.MinEventDate) && (date <= BusinessRules.Booking.MaxEventDate);
        }
        public int CompareTo(Booking other)
        {
            if (this.BookingStatus == BookingState.Pending && other.BookingStatus != BookingState.Pending)
                return -1;
            else if (this.BookingStatus != BookingState.Pending && other.BookingStatus == BookingState.Pending)
                return 1;
            else
                return 0;
        }
        public static List<BookingDuration> GetEventDatesForStaff(int staffID, int year, int month, int day)
        {
            List<BookingDuration> eventDates = new List<BookingDuration>();

            string query = @"SELECT [Event Booking].event_date, event_duration
                         FROM [Event Booking]
                         INNER JOIN [Event Staff] ON [Event Booking].booking_id = [Event Staff].booking_id
                         WHERE YEAR([Event Booking].event_date) = @year
                         AND MONTH([Event Booking].event_date) = @month
                         AND DAY([Event Booking].event_date) = @day
                         AND [Event Staff].staff_id = @staffID";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staffID);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                    command.Parameters.AddWithValue("@day", day);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookingDuration bookingDuration = new BookingDuration()
                            {
                                EventDate = Convert.ToDateTime(reader["event_date"]),
                                EventDuration = (TimeSpan) reader["event_duration"]
                            };
                            eventDates.Add(bookingDuration);
                        }
                    }
                }
            }
            return eventDates;
        }
        public static BookingDuration GetEventDate(int bookingID)
        {
            DateTime eventDate = DateTime.MinValue;
            BookingDuration bookingDuration = null;

            string query = "SELECT [Event Booking].event_date, event_duration FROM [Event Booking] WHERE booking_id = @bookingID";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookingID", bookingID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bookingDuration = new BookingDuration()
                            {
                                EventDate = Convert.ToDateTime(reader["event_date"]),
                                EventDuration = (TimeSpan) reader["event_duration"]
                            };
                        }
                    }
                }
            }
            return bookingDuration;
        }
        public class BookingDuration
        {
            public DateTime EventDate { get; set; }
            public TimeSpan EventDuration { get; set; }
        }
        public static bool IsStaffAssignedToEvent(int bookingID, int staffID)
        {
            bool isAssigned = false;

            string query = "SELECT COUNT(*) FROM [Event Staff] WHERE booking_id = @bookingID AND staff_id = @staffID";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookingID", bookingID);
                    command.Parameters.AddWithValue("@staffID", staffID);

                    connection.Open();

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                        isAssigned = true;
                }
            }
            return isAssigned;
        }
        public static void AddStaffToEvent(int staffID, int bookingID)
        {
            string query = "INSERT INTO [Event Staff] (staff_id, booking_id) VALUES (@staffID, @bookingID)";
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staffID);
                    command.Parameters.AddWithValue("@bookingID", bookingID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    
                }
            }
        }
        public static void RemoveStaffFromEvent(int staffID, int bookingID)
        {
            string query = "DELETE FROM [Event Staff] WHERE staff_id = @staffID AND booking_id = @bookingID";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staffID);
                    command.Parameters.AddWithValue("@bookingID", bookingID);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }

        public bool SendEmail()
        {
            string emailBody = GetEmailBody();

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, MediaTypeNames.Text.Html);

            byte[] imageBytes = Utilities.Images.GetImage("~/Assets/logo.png");
            LinkedResource itemImage = new LinkedResource(new MemoryStream(imageBytes), MediaTypeNames.Image.Jpeg);
            itemImage.ContentId = "logo";
            htmlView.LinkedResources.Add(itemImage);
            AttachImages(ref htmlView);

            try
            {
                Emails.SendMail("Booking Confirmation", emailBody, customer.EmailAddress, htmlView);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string GetEmailBody()
        {
            StringBuilder emailBodyBuilder = new StringBuilder();


            emailBodyBuilder.AppendLine(@"
            <html>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Booking Email</title>
            </head>
            <body style='font-family: Arial, sans-serif; background-color: #fff; margin: 0; padding: 0;'>
                <div style='background-color: #dde7df; border-radius: 8px; padding: 20px; margin: 20px auto; max-width: 600px; text-align: center;'>
                    <img src='cid:logo' alt='Logo' style='width: 100%; max-width: 124px; height: auto; margin-bottom: 15px;'>
                    <h1 style='color: #fff; font-weight: 700;'>Friends & Family</h1>
                </div>");


            emailBodyBuilder.AppendLine($@"
            <div style='background - color: #ffffff; border-radius: 8px; padding: 20px; margin: 20px auto; max-width: 600px; text-align: left;'>
                <div>
                    <h2> Hello {customer.FullName}!</h2>
                    <p>Your event booking has been approved. Our team will contact you and provide updates on the preparation progress.</p>
                </div>");

            string hoursString = (eventDuration.Hours == 1) ? "hour" : "hours";
            string durationString = $"{eventDuration.Hours} {hoursString} and {eventDuration.Minutes} minutes";
            emailBodyBuilder.AppendLine($@"
                <div>
                    <h2>Booking Summary</h2>
                    <p>Booking number: #{bookingID}</p>
                    <p>Event date: {eventDate.ToString("dd MMMM yyyy HH:mm")}</p>
                    <p>Duration: {durationString}</p>
                    <p>Event Address: {eventAddress}</p>
                    <p>Payment date: {PaymentDate.ToString("dd MMMM yyyy HH:mm")}</p>
                    <p>Payment type: {PaymentMethod}</p>");
            emailBodyBuilder.AppendLine(@"
                </div> 
            </div>
            <div style='background-color: #ffffff; border-radius: 8px; padding: 20px; margin: 20px auto; max-width: 600px; text-align: left;'>");



            foreach (Sales.ItemLine itemLine in ItemLines)
            {
                emailBodyBuilder.AppendLine($@"
                <div>
                    <img src='cid:item_line{itemLine.ItemID}' alt='{itemLine.ItemName}' style='max-width: 100%;'>
                    <p style = 'margin:0; margin-top: 8px;' > {itemLine.ItemName} </p>
                    <p style = 'margin:0;'>Price: R {itemLine.ItemCostN2}</p>
                    <p style = 'margin:0;'>Qty: {itemLine.ItemQuantity}</p>
                    <p style = 'margin:0; margin-bottom: 32px;'>Amount: R {itemLine.TotalSubCostN2}</p>
                </div>");
            }

            emailBodyBuilder.AppendLine($@"</div>
            <div style='background-color: #ffffff; border-radius: 8px; padding: 20px; margin: 20px auto; max-width: 600px; text-align:left;'>
                <p>Sub Total: R {PaymentAmount - BusinessRules.Booking.BookingFee}</p>
                <p>Booking Fee: R {BusinessRules.Booking.BookingFee.ToString("N2")}</p>
                <p style='font-size: 26px; font-weight: bold;'>Total: R {(PaymentAmount).ToString("N2")}</p>
            </div>");

            emailBodyBuilder.AppendLine(@"
            <div style='margin-top: 40px; color: #555555; background-color: #dde7df; padding: 10px; text-align: center;'>
                <h2 style = 'margin-bottom: 16px; font-size: 16px; color: #fff;'> Have any queries? Please reply to this email </h2>
                <p style = 'font-size: 12px; color: #fff;'> 2023 Friends & Family.All rights reserved. <br/> 10th floor, 323 Cornland, Foreshore, KwaZulu-Natal.</p>
                <p style = 'font-size: 12px; color: #fff;'> Friends & Family(Pty) Ltd, Reg 2002 / 02020 / 08 <br/> VAT number: 4343 4834 438.</ p >   
            </div> ");

            emailBodyBuilder.AppendLine(@"</body>");
            emailBodyBuilder.AppendLine(@"</html>");

            return emailBodyBuilder.ToString();
        }

        public int BookingID { get => bookingID; set => bookingID = value; }
        public string EventAddress { get => eventAddress; set => eventAddress = value; }
        public string EventDecorDescription { get => eventDecorDescription; set => eventDecorDescription = value; }
        public DateTime EventDate { get => eventDate; set => eventDate = value; }
        public string Str_EventDate { get => eventDate.ToString("dd MMM yyyy HH:mm"); }
        public TimeSpan EventDuration { get => eventDuration; set => eventDuration = value; }
        public string BookingStatus { get => bookingStatus; set => bookingStatus = value; }
        public Customer Customer { get => customer; set => customer = value; }
        public string CustomerName { get => (customer != null) ? customer.FirstName + " " + customer.LastName : "none"; }
        public override int SaleType => Sales.SaleType.EventBooking;
    }
    /// <summary>
    ///     Enumerates Potential Booking States.
    /// </summary>
    public class BookingState
    {
        /// <summary>
        ///     When an event booking is submitted but remains pending acceptance by the business.
        /// </summary>
        public readonly static string Pending = "Pending";
        /// <summary>
        ///     When an event booking has been confirmed and accepted by the business.
        /// </summary>
        public readonly static string UpComing = "Up coming";
        /// <summary>
        ///     When an event is currently underway.
        /// </summary>
        public readonly static string InProgress = "In progress";


        //Final States
        /// <summary>
        ///     When an event has concluded.
        /// </summary>
        public readonly static string Completed = "Completed";
        /// <summary>
        ///     When an event booking is canceled.
        /// </summary>
        public readonly static string Canceled = "Canceled";
        /// <summary>
        ///     When the booking is rejected or declined by the business.
        /// </summary>
        public readonly static string Rejected = "Rejected";
        /// <summary>
        ///     Checks if the given state is a final state in the sales process.
        /// </summary>
        /// <param name="state">The state to check.</param>
        /// <returns>True if the state is a final state, otherwise false.</returns>
        public static bool IsFinalState(string state)
        {
            return Completed == state || Canceled == state || Rejected == state;
        }
        /// <summary>
        ///     Checks if the given state is a valid state.
        /// </summary>
        /// <param name="state">The state to check.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        public static bool IsValidState(string state)
        {
            return state == Pending ||
                   state == UpComing ||
                   state == InProgress ||
                   state == Completed ||
                   state == Canceled ||
                   state == Rejected;
        }
        public static string GetStatusColor(string status)
        {
            if (status == Pending)
                return "#F46036";
            else if (status == UpComing)
                return "#F5AF36";
            else if (status == InProgress)
                return "#1B998B";
            else if (status == Completed)
                return "green";
            else if (status == Canceled)
                return "#D7263D";
            else if (status == Rejected)
                return "#D7263D";
            else
                return "#000000";
        }
        public static string GetCorrectState(string status, DateTime eventDate, TimeSpan duration)
        {
            if (eventDate > DateTime.Now)
                return status;

            if (Pending == status)
                return Rejected;
            DateTime dateTime = eventDate;

            if (UpComing == status && DateTime.Now < (dateTime.Add(duration)))
                return InProgress;
            else if (UpComing == status || InProgress == status)
                return Completed;
            else
                return status;
        }
    }
}