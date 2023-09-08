using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M4_Project.Models.Sales
{
    /// <summary>
    ///     Represents a delivery for an order.
    /// </summary>
    public class Delivery
    {
        private int deliveryID;
        private int orderID;
        private int driverID;
        private decimal deliveryFee;
        private Address deliveryAddress;
        private Address driverAddress;

        /// <summary>
        ///     Default constructor for a delivery.
        /// </summary>
        public Delivery(int deliveryID)
        {
            this.deliveryID = deliveryID;
        }
        /// <summary>
        ///     Constructor for a delivery with specific details.
        /// </summary>
        public Delivery(int orderID, decimal deliveryFee, Address deliveryAddress)
        {
            this.deliveryID = -1;
            this.orderID = orderID;
            this.deliveryFee = deliveryFee;
            this.deliveryAddress = deliveryAddress;
        }
        /// <summary>
        ///     Constructor for a delivery with specific details.
        /// </summary>
        public Delivery(int deliveryID, int orderID, decimal deliveryFee, Address deliveryAddress)
        {
            this.deliveryID = deliveryID;
            this.orderID = orderID;
            this.deliveryFee = deliveryFee;
            this.deliveryAddress = deliveryAddress;
        }
        /// <summary>
        ///     Saves the delivery information to the database.
        /// </summary>
        public void SaveDelivery()
        {
            string query = "INSERT INTO Delivery (order_id, destination_address, destination_latitude, destination_longitude, delivery_fee) " +
                "VALUES (@orderID, @address, @latitude, @longitude, @delivery_fee); " +
                "SELECT SCOPE_IDENTITY() AS deliveryID; ";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderID", orderID);
                command.Parameters.AddWithValue("@address", deliveryAddress.AddressName);
                command.Parameters.AddWithValue("@latitude", deliveryAddress.Latitude);
                command.Parameters.AddWithValue("@longitude", deliveryAddress.Longitude);
                command.Parameters.AddWithValue("@delivery_fee", deliveryFee);
                connection.Open();

                object insertedID = command.ExecuteScalar();
                this.deliveryID = Convert.ToInt32(insertedID);
                connection.Close();
            }
        }
        /// <summary>
        ///     Retrieves delivery information for a specific order.
        /// </summary>
        /// <param name="orderID">The ID of the order to retrieve delivery information for.</param>
        /// <returns>A Delivery object representing the delivery information, or null if not found.</returns>
        public static Delivery GetDelivery(int orderID)
        {
            string query = "SELECT TOP (1) [delivery_id], [order_id], [driver_id], [destination_address], [destination_latitude], [destination_longitude], [delivery_fee] " +
                           "FROM [Delivery] " +
                           "WHERE order_id = @orderID;";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderID", orderID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Address deliveryAddress = new Address(
                            addressName: reader["destination_address"].ToString(),
                            latitude: (double)reader["destination_latitude"],
                            longitude: (double)reader["destination_longitude"]
                        );

                        Delivery delivery = new Delivery(
                            deliveryID: (int)reader["delivery_id"],
                            orderID: (int)reader["order_id"],
                            deliveryFee: (decimal)reader["delivery_fee"],
                            deliveryAddress: deliveryAddress
                        );
                        return delivery;
                    }
                }
            }
            return null; 
        }
        /// <summary>
        ///     Retrieves the current driver's location for the delivery.
        /// </summary>
        /// <returns>The current driver's location as an Address object, or null if not found.</returns>
        public Address GetDriverLocation()
        {
            string query = "SELECT [delivery_id], [latitude], [longitude] " +
                           "FROM [Driver Location] " +
                           "WHERE delivery_id = @deliveryID; ";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@deliveryID", deliveryID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        double latitude = (double)reader["latitude"];
                        double longitude = (double)reader["longitude"];

                        Address driverLocation = new Address(latitude, longitude);
                        return driverLocation;
                    }
                }
            }
            return null;
        }
        /// <summary>
        ///     Sets the driver's current location for the delivery.
        /// </summary>
        /// <param name="driverAddress">The Address object representing the driver's location.</param>
        public void SetDriverLocation(Address driverAddress)
        {
            this.driverAddress = driverAddress;
            string query = "INSERT INTO [Driver Location] VALUES (@deliveryID, @latitude, @longitude)";
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.Parameters.AddWithValue("@deliveryID", deliveryID);
                command.Parameters.AddWithValue("@latitude", driverAddress.Latitude);
                command.Parameters.AddWithValue("@longitude", driverAddress.Longitude);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        /// <summary>
        ///     Updates the driver's location for the delivery.
        /// </summary>
        /// <param name="driverAddress">The Address object representing the updated driver's location.</param>
        public void UpdateDriverLocation(Address driverAddress)
        {
            this.driverAddress = driverAddress;
            string query = "UPDATE [Driver Location] SET [latitude] = @latitude, [longitude] = @longitude " +
                "WHERE[delivery_id] = @deliveryID; ";
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.Parameters.AddWithValue("@deliveryID", deliveryID);
                command.Parameters.AddWithValue("@latitude", driverAddress.Latitude);
                command.Parameters.AddWithValue("@longitude", driverAddress.Longitude);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        /// <summary>
        ///     Assigns a delivery driver to the delivery.
        /// </summary>
        /// <param name="staff">The StaffLoginSession represent the driver to be assigned.</param>
        /// <returns>True if the assignment was successful, otherwise false.</returns>
        public bool SetDeliveryDriver(StaffLoginSession staff)
        {
            if (!StaffRole.IsDriver(staff.Role))
                return false;
            string query = "UPDATE [Delivery] SET driver_id = @staffID " +
                "WHERE[delivery_id] = @deliveryID; ";
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.Parameters.AddWithValue("@deliveryID", deliveryID);
                command.Parameters.AddWithValue("@staffID", staff.StaffID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }

        public int DeliveryID { get => deliveryID; set => deliveryID = value; }
        public int OrderID { get => orderID; set => orderID = value; }
        public int DriverID { get => driverID; set => driverID = value; }
        public decimal DeliveryFee { get => deliveryFee; set => deliveryFee = value; }
        public Address DeliveryAddress { get => deliveryAddress; }
        public Address DriverAddress { get => driverAddress; }
    }
}
