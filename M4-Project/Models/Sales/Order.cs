﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M4_Project.Models.Sales
{
    ///
    /// <summary>
    ///     Represents an order.
    /// </summary>
    public class Order:Sale
    {
        private int orderID;
        private Customer customer;
        private string orderType;
        private string orderStatus;
        private int staffID;
        private Delivery delivery;

        public Order()
        {
            ItemLines = new List<ItemLine>();
            Cart = new Dictionary<int, int>();
            orderID = -1;
        }
        ///
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.Sales.Order class.
        /// </summary>
        public Order(int orderID, Customer customer, string orderType, string orderStatus, int staffID)
        {
            this.orderID = orderID;
            this.customer = customer;
            this.orderType = orderType;
            this.orderStatus = orderStatus;
            this.staffID = staffID;
        }
        ///
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.Sales.Order class.
        /// </summary>
        public Order(int orderID, Customer customer, string orderType, string orderStatus, DateTime paymentDate, decimal paymentAmount)
        {
            this.orderID = orderID;
            this.customer = customer;
            this.orderType = orderType;
            this.orderStatus = orderStatus;
            this.TotalAmountDue = paymentAmount;
            this.PaymentDate = paymentDate;
        }


        ///
        /// <summary>
        ///     Records the attributes' values of an instance of M4_System.Models.Sells.Order class to the database.
        /// </summary>
        public override void RecordSell()
        {
            if (ItemLines.Count < BusinessRules.ItemLine.MinMenuItems)
                return;
            if (this.OrderType != Sales.OrderType.InStore)
                RecordOrderOnline();
            else
                RecordOrderInStore();
        }
        ///
        /// <summary>
        ///     Records the attributes' values of an instance of M4_System.Models.Sells.Order class to the database without customer identification.
        /// </summary>
        private void RecordOrderInStore()
        {
            string query = "INSERT INTO [Order] ([staff_id], [order_type], [order_state], [payment_date], [payment_amount], [payment_method], [tip_amount]) VALUES (@staff_id, @order_type, @order_state, @payment_date, @payment_amount, @payment_method, @tip_amount);" +
                "SELECT SCOPE_IDENTITY() AS order_id;";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@staff_id", StaffID);
                command.Parameters.AddWithValue("@order_type", OrderType);
                command.Parameters.AddWithValue("@order_state", "Preparing");
                command.Parameters.AddWithValue("@payment_amount", TotalAmountDue);
                command.Parameters.AddWithValue("@payment_method", PaymentMethod);
                command.Parameters.AddWithValue("@payment_date", PaymentDate);
                command.Parameters.AddWithValue("@tip_amount", Tip);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataRow row = dt.Rows[0];
                this.OrderID = (int)row["order_id"];
            }
            RecordOrderLine();
        }
        ///
        /// <summary>
        ///     Records the attributes' values of an instance of M4_System.Models.Sells.Order class to the database with customer identification.
        /// </summary>
        private void RecordOrderOnline()
        {
            string query = "INSERT INTO [Order] ([staff_id], [customer_id], [order_type], [order_state], [payment_date], [payment_amount], [payment_method], [tip_amount]) VALUES (@staff_id, @order_type, @order_state, @payment_date, @payment_amount, @payment_method, @tip_amount);" +
                "SELECT SCOPE_IDENTITY() AS order_id;";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@staff_id", StaffID);
                command.Parameters.AddWithValue("@customer_id", customer.CustomerID);
                command.Parameters.AddWithValue("@order_type", OrderType);
                command.Parameters.AddWithValue("@order_state", "Preparing");
                command.Parameters.AddWithValue("@payment_amount", TotalAmountDue);
                command.Parameters.AddWithValue("@payment_method", PaymentMethod);
                command.Parameters.AddWithValue("@payment_date", PaymentDate);
                command.Parameters.AddWithValue("@tip_amount", Tip);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataRow row = dt.Rows[0];
                this.OrderID = (int)row["order_id"];
            }
            RecordOrderLine();
        }
        ///
        /// <summary>
        ///     Records the M4_System.Models.Sells.ItemLine attributes' values in the instance of M4_System.Models.Sells.Sales:Order class.
        /// </summary>
        private void RecordOrderLine()
        {
            foreach (ItemLine itemLine in ItemLines)
            {
                string query = "INSERT INTO [Order Line] ([item_id], [order_id], [item_quantity], [sub_cost], [instruction]) VALUES (@item_id, @order_id, @item_quantity, @sub_cost, @instruction);";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@item_id", itemLine.ItemID);
                    command.Parameters.AddWithValue("@order_id", this.OrderID);
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
        ///     Change the status of an order on the database.
        /// </summary>
        public bool ChangeStatus(string orderStatus)
        {
            if (OrderState.IsFinalState(orderStatus))
                return false;

            if (!OrderState.IsValidState(orderStatus))
                return false;

            this.OrderStatus = orderStatus;
            ChangeStatus(OrderID, orderStatus);
            return true;
        }
        ///
        /// <summary>
        ///     Change the status of an order on the database using the order identification as parameter.
        /// </summary>
        public static void ChangeStatus(int orderID, string orderStatus)
        {
            string query = "UPDATE [Order] SET [order_state] = @order_state WHERE[order_id] = @orderID";
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderID", orderID);
                command.Parameters.AddWithValue("@order_state", orderStatus);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
            }
        }
        ///
        /// <summary>
        ///     Returns an order with a specific order identification.
        /// </summary>
        public static Order GetOrder(int orderID)
        {
            string query = "SELECT TOP (1) customer_id, order_id, order_state, order_type, payment_amount, payment_date, payment_method, staff_id, tip_amount FROM [Order] WHERE (order_id = @orderID);";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderID", orderID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Order order = new Order(
                            (int)reader["order_id"],
                            Customer.GetCustomer((int)reader["customer_id"]),
                            reader["order_type"].ToString(),
                            reader["order_state"].ToString(),
                            (int)reader["staff_id"]
                        );
                        order.ItemLines = GetOrderLines(orderID);
                        return order;
                    }
                }
            }
            return null;
        }
        ///
        /// <summary>
        ///     Returns a order line for a specific order identification.
        /// </summary>
        public static List<ItemLine> GetOrderLines(int orderID)
        {
            string query = "SELECT [Order Line].item_id, [Menu Item].item_name, [Menu Item].item_price, [Menu Item].item_image, order_id, item_quantity, sub_cost, instruction " +
              "FROM[Order Line], [Menu Item]" +
              "WHERE order_id = @orderID AND [Order Line].item_id = [Menu Item].item_id; ";

            List<Models.Sales.ItemLine> itemLines = new List<ItemLine>();
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderID", orderID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ItemLine itemLine = new ItemLine(
                        (int)row["item_id"],
                        (int)row["item_quantity"],
                        (decimal)row["item_price"],
                        (string)row["instruction"],
                        (string)row["item_name"],
                        (byte[])row["item_image"],
                        ""
                    );
                    itemLines.Add(itemLine);
                }
                return itemLines;
            }
        }
        ///
        /// <summary>
        ///     Returns a list of orders.
        /// </summary>
        public static List<Order> GetOrders(int page, int maxListSize)
        {
            return null;
        }
        ///
        /// <summary>
        ///     Returns a list of orders with similar customer name or same customer name.
        /// </summary>
        public static List<Order> GetOrders(int page, int maxListSize, string customerName, string orderType)
        {

            List<Order> orders = new List<Order>();

            SqlCommand command = new SqlCommand();
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(orderType))
            {
                string query = "SELECT [Customer].first_name, [Customer].last_name, order_id, order_type, order_state, payment_date, payment_amount " +
                    "FROM[Customer], [Order] " +
                    "WHERE[Customer].customer_id = [Order].customer_id " +
                    "ORDER BY[Order].payment_date DESC, [Order].order_id DESC " +
                    "OFFSET @startRow ROWS " +
                    "FETCH NEXT @MaxListSize ROWS ONLY; ";
                command.CommandText = query;
            }
            else
            {
                string query = "SELECT [Customer].first_name, [Customer].last_name, order_id, order_type, order_state, payment_date, payment_amount " +
                    "FROM[Customer], [Order] " +
                    "WHERE[Customer].customer_id = [Order].customer_id " +
                    "AND [Customer].first_name + ' ' + [Customer].last_name LIKE '%'+@searchValue+'%' " +
                    "AND order_type = @orderType " +
                    "ORDER BY[Order].payment_date DESC, [Order].order_id DESC " +
                    "OFFSET @startRow ROWS " +
                    "FETCH NEXT @MaxListSize ROWS ONLY; ";
                command.CommandText = query;
                command.Parameters.AddWithValue("@searchValue", customerName);
                command.Parameters.AddWithValue("@orderType", orderType);
            }

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                command.Parameters.AddWithValue("@startRow", page);
                command.Parameters.AddWithValue("@MaxListSize", maxListSize);
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order(
                            (int)reader["order_id"],
                            new Customer((string) reader["first_name"], (string) reader["last_name"]),
                            reader["order_type"].ToString(),
                            reader["order_state"].ToString(),
                            (DateTime)reader["payment_date"],
                            (decimal)reader["payment_amount"]
                        );
                        orders.Add(order);
                    }
                }
            }
            return orders;
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public Customer Customer { get => customer; set => customer = value; }
        public string OrderType { get => orderType; set => orderType = value; }
        public string OrderStatus { get => orderStatus; set => orderStatus = value; }
        public int StaffID { get => staffID; set => staffID = value; }

        public override int SaleType => Sales.SaleType.Order;
    }
    /// <summary>
    ///     Enumerates Potential Order Types.
    /// </summary>
    public static class OrderType
    {
        /// <summary>
        ///     When an order is placed online and will be collected by the customer.
        /// </summary>
        public readonly static string Collection = "Collection";
        /// <summary>
        ///     When an order is placed online and will be delivered to the customer.
        /// </summary>
        public readonly static string Delivery = "Delivery";
        /// <summary>
        ///     When an order is placed in store.
        /// </summary>
        public readonly static string InStore = "In-Store";
    }
    /// <summary>
    ///     Enumerates Potential Order States.
    /// </summary>
    public static class OrderState
    {
        /// <summary>
        ///     When an order is submitted but remains pending acceptance by the business.
        /// </summary>
        public readonly static string Pending = "Pending";
        /// <summary>
        ///     When an order has been confirmed and accepted by the business.
        /// </summary>
        public readonly static string Preparing = "Preparing";
        /// <summary>
        ///     When the order has been processed and prepared for delivery/shipping.
        /// </summary>
        public readonly static string Prepared = "Prepared";
        /// <summary>
        ///     When the order has not been successfully delivered to the customer.
        /// </summary>
        public readonly static string Unsuccessful = "Unsuccessful";
        /// <summary>
        ///     When the driver is en route to deliver the order.
        /// </summary>
        public readonly static string OnTheWay = "On the way";


        //Final States
        /// <summary>
        ///     When the order has been collected by the customer.
        /// </summary>
        public readonly static string Collected = "Collected";
        /// <summary>
        ///     When the order has been successfully delivered to the customer.
        /// </summary>
        public readonly static string Delivered = "Delivered";
        /// <summary>
        ///     When the order is rejected or declined by the business.
        /// </summary>
        public readonly static string Rejected = "Rejected";


        /// <summary>
        ///     Checks if the given state is a final state in the sales process.
        /// </summary>
        /// <param name="state">The state to check.</param>
        /// <returns>True if the state is a final state, otherwise false.</returns>
        public static bool IsFinalState(string state)
        {
            return Collected == state || Delivered == state || Rejected == state;
        }
        /// <summary>
        ///     Checks if the given state is a valid state.
        /// </summary>
        /// <param name="state">The state to check.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        public static bool IsValidState(string state)
        {
            return state == Pending ||
                   state == Preparing ||
                   state == Prepared ||
                   state == Unsuccessful ||
                   state == OnTheWay ||
                   state == Collected ||
                   state == Delivered ||
                   state == Rejected;
        }
    }
}