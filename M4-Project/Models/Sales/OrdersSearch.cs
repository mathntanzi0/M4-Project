using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

namespace M4_Project.Models.Sales
{
    public class OrdersSearch
    {
        public string Query { get; private set; }
        public string RowCountQuery { get; private set; }
        public int OrderID { get; private set; }
        public int StaffID { get; private set; }
        public string CustomerName { get; private set; }
        public string OrderType { get; private set; }
        public int Page { get; private set; }
        public int MaxPerPage { get; private set; }
        public int MaxPage { get; private set; }
        private SqlCommand Command { get; set; }
        public List<Order> Orders { get; private set; }


        private OrdersSearch()
        {
            Command = new SqlCommand();
            Query = "SELECT " +
                "ISNULL([Customer].first_name, 'Null') AS first_name, " +
                "ISNULL([Customer].last_name, 'Null') AS last_name, " +
                "[Order].order_id, " +
                "[Order].order_type, " +
                "[Order].order_state, " +
                "[Order].payment_date, " +
                "[Order].payment_amount " +
                "FROM [Order] " +
                "LEFT JOIN [Customer] ON [Order].customer_id = [Customer].customer_id " +
                "ORDER BY [Order].payment_date ASC, [Order].order_id ASC " +
                "OFFSET @page ROWS " +
                "FETCH NEXT @maxOrders ROWS ONLY;";
            Command.CommandText = Query;
            Page = 1;
            MaxPerPage = 10;
            GetOrders();
        }
        public OrdersSearch(string pageString, string orderID, string staffID, string customerName, string orderType, int MaxPerPage)
        {
            this.MaxPerPage = MaxPerPage;
            Command = new SqlCommand();
            bool whereAdded = false;
            StringBuilder whereClause = WhereClause(orderID, staffID, customerName, orderType, ref whereAdded);

            if (RowCount(whereClause) < 1)
            {
                this.Orders = new List<Order>();
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
            queryBuilder.Append("ISNULL([Customer].first_name, 'Null') AS first_name, ");
            queryBuilder.Append("ISNULL([Customer].last_name, 'Null') AS last_name, ");
            queryBuilder.Append("[Order].order_id, ");
            queryBuilder.Append("[Order].order_type, ");
            queryBuilder.Append("[Order].order_state, ");
            queryBuilder.Append("[Order].payment_date, ");
            queryBuilder.Append("[Order].payment_amount ");
            queryBuilder.Append("FROM [Order] ");
            queryBuilder.Append("LEFT JOIN [Customer] ON [Order].customer_id = [Customer].customer_id ");

            if (whereAdded)
            {
                queryBuilder.Append("WHERE ");
                queryBuilder.Append(whereClause.ToString());
            }

            queryBuilder.Append("ORDER BY [Order].payment_date ASC, [Order].order_id ASC ");
            queryBuilder.Append("OFFSET @page ROWS ");
            queryBuilder.Append("FETCH NEXT @maxOrders ROWS ONLY;");

            Query = queryBuilder.ToString();
            Command.CommandText = Query;
            GetOrders();
        }
        private StringBuilder WhereClause(string orderIDString, string staffIDString, string customerName, string orderType, ref bool whereAdded)
        {
            StringBuilder whereClause = new StringBuilder();

            int staffID;
            int orderID;
            if (!string.IsNullOrEmpty(orderIDString) && int.TryParse(orderIDString, out orderID))
            {
                Command.Parameters.AddWithValue("@orderID", orderID);
                whereClause.Append("order_id = @orderID ");
                whereAdded = true;
                return whereClause;
            }

            if (!string.IsNullOrEmpty(staffIDString) && int.TryParse(staffIDString, out staffID))
            {
                Command.Parameters.AddWithValue("@staffID", staffID);
                whereClause.Append("staff_id = @staffID ");
                whereAdded = true;
            }

            if (!string.IsNullOrEmpty(orderType))
            {
                if (whereAdded)
                    whereClause.Append("AND ");

                Command.Parameters.AddWithValue("@orderType", orderType);
                whereClause.Append("order_type = @orderType ");
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
            return whereClause;
        }
        private void GetOrders()
        {
            List<Order> orders = new List<Order>();
            using (Database dbConnection = new Database(Command))
            {
                using (SqlDataReader reader = dbConnection.Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader["first_name"].ToString();
                        string lastName = reader["last_name"].ToString();
                        int orderID = Convert.ToInt32(reader["order_id"]);
                        string orderType = reader["order_type"].ToString();
                        string orderState = reader["order_state"].ToString();
                        DateTime paymentDate = Convert.ToDateTime(reader["payment_date"]);
                        decimal paymentAmount = Convert.ToDecimal(reader["payment_amount"]);

                        Customer customer = (firstName == "Null" && lastName == "Null") ? null : new Customer(firstName, lastName);
                        Order order = new Order(orderID, customer, orderType, orderState, paymentDate, paymentAmount);
                        orders.Add(order);
                    }
                }
            }
            this.Orders = orders;
        }
        private int RowCount(StringBuilder whereClause)
        {
            int rowCount = 0;

            StringBuilder query = new StringBuilder();
            RowCountQuery = "SELECT " +
                "COUNT([Order].order_id) as RowsCount " +
                "FROM[Order] " +
                "LEFT JOIN[Customer] ON [Order].customer_id = [Customer].customer_id ";

            query.Append(RowCountQuery);
            if (whereClause.Length < 1)
                query.Append(';');
            else
            {
                query.Append("WHERE ");
                query.Append(whereClause);
            }

            Command.CommandText = query.ToString();
            using (Database dbConnection = new Database(Command))
                rowCount = (int) dbConnection.Command.ExecuteScalar();

            if (rowCount != 0)
                MaxPage = (int) Math.Ceiling((decimal)rowCount / (decimal)MaxPerPage);
            return rowCount;
        }
    }
}