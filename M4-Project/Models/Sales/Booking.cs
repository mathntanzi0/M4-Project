using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace M4_Project.Models.Sales
{
    ///
    /// <summary>
    ///     Represents a booking for an event.
    /// </summary>
    public class Booking : Sale
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

            string query = "INSERT INTO [Event Booking] ([customer_id], [event_date], [event_duration], [event_setting], [event_address], [payment_amount], [payment_method], [payment_date], [status]) VALUES (@customer_id, @event_date, @event_duration, @event_setting, @event_address, @payment_amount, @payment_method, @payment_date, @status);" +
                "SELECT SCOPE_IDENTITY() AS booking_id;";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@customer_id", customer.CustomerID);
                command.Parameters.AddWithValue("@event_date", EventDate);
                command.Parameters.AddWithValue("@event_duration", EventDuration);
                command.Parameters.AddWithValue("@event_setting", EventDecorDescription);
                command.Parameters.AddWithValue("@event_address", EventAddress);
                command.Parameters.AddWithValue("@payment_method", PaymentMethod);
                command.Parameters.AddWithValue("@payment_date", PaymentDate);
                command.Parameters.AddWithValue("@status", "Pending");
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataRow row = dt.Rows[0];
                this.bookingID = (int) row["booking_id"];
            }
            RecordEventLine();
        }
        ///
        /// <summary>
        ///     Records the M4_System.Models.Sells.ItemLine attributes' values in the instance of M4_System.Models.Sales.Sale:Booking class.
        /// </summary>
        private void RecordEventLine()
        {
            foreach (ItemLine itemLine in ItemLines)
            {
                string query = "INSERT INTO [Event Line] ([item_id], [booking_id], [item_quantity], [sub_cost], [instructions]) VALUES (@item_id, @booking_id, @item_quantity, @sub_cost, @instructions);";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@item_id", itemLine.ItemID);
                    command.Parameters.AddWithValue("@booking_id", this.bookingID);
                    command.Parameters.AddWithValue("@item_quantity", itemLine.ItemQuantity);
                    command.Parameters.AddWithValue("@sub_cost", itemLine.TotalSubCost);
                    command.Parameters.AddWithValue("@instructions", itemLine.Instructions);
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                }
            }
        }
        ///
        /// <summary>
        ///     Change the status of an event booking on the database.
        /// </summary>
        public bool ChangeStatus(string bookingStatus)
        {
            if (BookingState.IsFinalState(bookingStatus))
                return false;

            if (!BookingState.IsValidState(bookingStatus))
                return false;

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
            string query = "UPDATE [Event Booking] SET [status] = @status WHERE[booking_id] = @bookingID";
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@bookingID", bookingID);
                command.Parameters.AddWithValue("@status", bookingStatus);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
            }
        }
        ///
        /// <summary>
        ///     Returns a event booking for a specific booking identification.
        /// </summary>
        public static Booking GetBooking(int bookingID)
        {
            string query = "SELECT * FROM [Event Booking] " +
                           "WHERE booking_id = @booking_id";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@booking_id", bookingID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count < 1)
                    return null;
                DataRow row = dt.Rows[0];

                Customer customer = Customer.GetCustomer((int)row["customer_id"]);
                Booking booking = new Booking((int)row["booking_id"], customer, (string)row["event_address"], (string)row["event_setting"], (DateTime)row["event_date"], (TimeSpan)row["event_duration"], (string)row["status"]);
                booking.ItemLines = GetEventLines(bookingID);
                return booking;
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
            string query = "SELECT [booking_id], [event_date], [event_address], [payment_amount], [status] " +
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
            List<Models.Sales.ItemLine> itemLines = new List<ItemLine>();
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@booking_id", bookingID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ItemLine itemLine = new ItemLine((int)row["item_id"], (int)row["item_quantity"], (decimal)row["item_price"], (string)row["instructions"], (string)row["item_name"], (byte[])row["item_image"], "");
                    itemLines.Add(itemLine);
                }
                return itemLines;
            }
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
                        //Customer customer, DateTime eventDate, decimal paymentAmount, string bookingStatus
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
        ///
        /// <summary>
        ///     Returns a list of event bookings using staff member's identification number.
        /// </summary>
        /*public static List<Booking> GetBookings(int page, int maxListSize, int staffID)
        {
            string query = "SELECT [Event Booking].[booking_id], [Event Booking].[customer_id], [Customer].first_name, [Customer].last_name, [event_date], [event_duration], [event_setting], [event_address], [payment_amount], [payment_method], [payment_date], [status] " +
              "FROM [Event Booking], [Event Staff], Customer " +
              "WHERE [Event Staff].staff_id = @staff_id AND [Event Staff].booking_id = [Event Booking].booking_id AND Customer.customer_id = [Event Booking].customer_id " +
              "ORDER BY [first_name], [last_name], [Event Booking].[booking_id] ASC " +
              "OFFSET @page ROW " +
              "FETCH NEXT @limit ROWS ONLY";

            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@staff_id", staffID);
                command.Parameters.AddWithValue("@page", page);
                command.Parameters.AddWithValue("@limit", maxListSize);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                //Waiting for customer class.
                return null;
            }
        }*/
        public int BookingID { get => bookingID; set => bookingID = value; }
        public string EventAddress { get => eventAddress; set => eventAddress = value; }
        public string EventDecorDescription { get => eventDecorDescription; set => eventDecorDescription = value; }
        public DateTime EventDate { get => eventDate; set => eventDate = value; }
        public TimeSpan EventDuration { get => eventDuration; set => eventDuration = value; }
        public string BookingStatus { get => bookingStatus; set => bookingStatus = value; }
        public Customer Customer { get => customer; set => customer = value; }
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
    }
}