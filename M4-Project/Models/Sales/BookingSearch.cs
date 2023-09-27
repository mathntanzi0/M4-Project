using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace M4_Project.Models.Sales
{
    public class BookingSearch
    {
        public string Query { get; private set; }
        public string RowCountQuery { get; private set; }
        public int BookingID { get; private set; }
        public int StaffID { get; private set; }
        public string CustomerName { get; private set; }
        public string EventStatus { get; private set; }
        public int Page { get; private set; }
        public int MaxPerPage { get; private set; }
        public int MaxPage { get; private set; }
        private SqlCommand Command { get; set; }
        public List<Booking> Events { get; private set; }


        private BookingSearch()
        {
            Command = new SqlCommand();
            Query = "SELECT " +
                "[Customer].first_name, " +
                "[Customer].last_name, " +
                "[Event Booking].booking_id, " +
                "[Event Booking].status, " +
                "[Event Booking].event_date, " +
                "[Event Booking].payment_amount " +
                "FROM [Event Booking], [Customer] " +
                "WHERE [Event Booking].customer_id = [Customer].customer_id " +
                "ORDER BY [Event Booking].event_date DESC, [Event Booking].booking_id DESC " +
                "OFFSET @page ROWS " +
                "FETCH NEXT @maxOrders ROWS ONLY;";

            Command.CommandText = Query;
            Page = 1;
            MaxPerPage = 10;
            GetBookings();
        }
        public BookingSearch(string pageString, string bookingID, string staffID, string customerName, string eventState, int MaxPerPage)
        {
            this.MaxPerPage = MaxPerPage;
            Command = new SqlCommand();
            bool whereAdded = false;
            StringBuilder fromClause = FromClause(staffID, bookingID);
            StringBuilder whereClause = WhereClause(bookingID, staffID, customerName, eventState, ref whereAdded);

            if (RowCount(whereClause, fromClause) < 1)
            {
                this.Events = new List<Booking>();
                return;
            }
            int page;
            if (!string.IsNullOrEmpty(pageString) && int.TryParse(pageString, out page))
            {
                if (page > MaxPage)
                    Page = MaxPage;
                else if (page < 0)
                    Page = 1;
                else
                    Page = page;
            }
            else
            {
                Page = 1;
            }
            Command.Parameters.AddWithValue("@page", (Page - 1) * MaxPerPage);
            Command.Parameters.AddWithValue("@maxOrders", MaxPerPage);

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("SELECT ");
            queryBuilder.Append("[Customer].first_name, ");
            queryBuilder.Append("[Customer].last_name, ");
            queryBuilder.Append("[Event Booking].booking_id, ");
            queryBuilder.Append("[Event Booking].status, ");
            queryBuilder.Append("[Event Booking].event_date, ");
            queryBuilder.Append("[Event Booking].payment_amount ");
            queryBuilder.Append(fromClause.ToString());
            queryBuilder.Append("WHERE [Event Booking].customer_id = [Customer].customer_id ");

            if (whereAdded)
            {
                queryBuilder.Append("AND ");
                queryBuilder.Append(whereClause.ToString());
            }

            queryBuilder.Append("ORDER BY [Event Booking].event_date DESC, [Event Booking].booking_id DESC ");
            queryBuilder.Append("OFFSET @page ROWS ");
            queryBuilder.Append("FETCH NEXT @maxOrders ROWS ONLY;");

            Query = queryBuilder.ToString();
            Command.CommandText = Query;
            GetBookings();
        }
        private StringBuilder FromClause(string staffID, string bookingID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("From ");
            stringBuilder.Append("[Event Booking], ");
            stringBuilder.Append("[Customer]");
            int int_staffID;
            if (string.IsNullOrEmpty(bookingID) && !string.IsNullOrEmpty(staffID) && int.TryParse(staffID, out int_staffID))
                stringBuilder.Append(", [Event Staff]");
            return stringBuilder;
        }
        private StringBuilder WhereClause(string bookingIDString, string staffIDString, string customerName, string eventState, ref bool whereAdded)
        {
            StringBuilder whereClause = new StringBuilder();

            int bookingID;
            int staffID;
            if (!string.IsNullOrEmpty(bookingIDString) && int.TryParse(bookingIDString, out bookingID))
            {
                Command.Parameters.AddWithValue("@bookingID", bookingID);
                whereClause.Append("[Event Booking].booking_id = @bookingID ");
                whereAdded = true;
                return whereClause;
            }

            if (!string.IsNullOrEmpty(staffIDString) && int.TryParse(staffIDString, out staffID))
            {
                if (whereAdded)
                    whereClause.Append("AND ");

                Command.Parameters.AddWithValue("@staffID", staffID);
                whereClause.Append("[Event Booking].booking_id = [Event Staff].booking_id AND [Event Staff].staff_id = @staffID ");
                whereAdded = true;
            }

            if (!string.IsNullOrEmpty(customerName))
            {
                if (whereAdded)
                    whereClause.Append("AND ");

                Command.Parameters.AddWithValue("@customerName", customerName);
                whereClause.Append("[Customer].first_name + [Customer].last_name LIKE '%' + @customerName + '%' ");
                whereAdded = true;
            }

            if (!string.IsNullOrEmpty(eventState))
            {
                if (whereAdded)
                    whereClause.Append("AND ");

                Command.Parameters.AddWithValue("@eventState", eventState);
                whereClause.Append("[Event Booking].status = @eventState ");
                whereAdded = true;
            }

            return whereClause;
        }
        private void GetBookings()
        {
            List<Booking> bookings = new List<Booking>();
            using (Database dbConnection = new Database(Command))
            {
                using (SqlDataReader reader = dbConnection.Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader["first_name"].ToString();
                        string lastName = reader["last_name"].ToString();
                        int bookingID = Convert.ToInt32(reader["booking_id"]);
                        string bookingStatus = reader["status"].ToString();
                        DateTime eventDate = Convert.ToDateTime(reader["event_date"]);
                        decimal paymentAmount = Convert.ToDecimal(reader["payment_amount"]);

                        Customer customer = (firstName == "Null" && lastName == "Null") ? null : new Customer(firstName, lastName);
                        Booking booking = new Booking(bookingID, customer, eventDate, paymentAmount, bookingStatus);
                        bookings.Add(booking);
                    }
                }
            }
            this.Events = bookings;
        }

        private int RowCount(StringBuilder whereClause, StringBuilder fromClause)
        {
            int rowCount = 0;

            StringBuilder query = new StringBuilder();

            RowCountQuery = "SELECT " +
                "COUNT([Event Booking].booking_id) AS RowsCount ";

            query.Append(RowCountQuery);
            query.Append(fromClause);
            query.Append("WHERE [Event Booking].customer_id = [Customer].customer_id ");

            if (whereClause.Length > 0)
            {
                query.Append("AND");
                query.Append(whereClause);
            }

            Command.CommandText = query.ToString();
            using (Database dbConnection = new Database(Command))
                rowCount = (int)dbConnection.Command.ExecuteScalar();

            if (rowCount != 0)
                MaxPage = (int)Math.Ceiling((decimal)rowCount / (decimal)MaxPerPage);
            return rowCount;
        }
    }
}