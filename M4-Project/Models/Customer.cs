using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace M4_Project.Models
{
    /// <summary>
    /// A class that Represents a Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Private attribute declaration      
        /// </summary>

        private int customerID;
        private string firstName;
        private string lastName;
        private string emailAddress;
        private string phoneNumber;
        private string physicalAddress;
        private int loyaltyPoints;
        private string password;

        ///<summary>
        /// Initializing a new class instance of the customer.
        ///</summary>
        public Customer(int customerID, string firstName, string lastName, string emailAddress, string phoneNumber, string physicalAddress, int loyaltyPoints, string password)
        {
            this.customerID = customerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            this.phoneNumber = phoneNumber;
            this.physicalAddress = physicalAddress;
            this.loyaltyPoints = loyaltyPoints;
            this.password = password;
        }
        ///<summary>
        /// Initializing a new class instance of the customer.
        ///</summary>
        public Customer(int customerID, string firstName, string lastName, string emailAddress, string phoneNumber, string physicalAddress, int loyaltyPoints)
        {
            this.customerID = customerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            this.phoneNumber = phoneNumber;
            this.physicalAddress = physicalAddress;
            this.loyaltyPoints = loyaltyPoints;
        }
        ///<summary>
        /// Initializing a new class instance of the customer.
        ///</summary>
        public Customer(string firstName, string lastName)
        {
            this.customerID = -1;
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public Customer(string firstName, string lastName, string emailAddress, string phoneNumber, string physicalAddress)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.physicalAddress = physicalAddress;
            this.emailAddress = emailAddress;
        }

        ///<summary>
        /// Add Customer to the database.
        /// </summary>
        public void AddCustomer()
        {
            string query = "INSERT INTO Customer (first_name, last_name, email_address, phone_number, physical_address, loyalty_points) " +
                "VALUES (@firstName, @lastName, @emailAddress, @phoneNumber, @physicalAddress, @loyaltyPoints)";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@emailAddress", emailAddress);
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@physicalAddress", physicalAddress);
                command.Parameters.AddWithValue("@loyaltyPoints", loyaltyPoints);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        ///<summary>
        /// Removing a customer from the database.
        /// </summary>

        public void RemoveCustomer()
        {
            string query = "DELETE FROM Customer WHERE customer_id = @customerID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@customerID", this.customerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Update FirstName Method
        /// </summary>
        public void UpdateFirstName(string newName)
        {
            string query = "UPDATE Customer SET first_name = @NewName WHERE customer_id = @CustomerID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NewName", newName); // Bind the new first name as a parameter to prevent SQL injection
                command.Parameters.AddWithValue("@CustomerID", customerID);
                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Update Last Name
        /// </summary>
        public void UpdateLastName(string newName)
        {
            string query = "UPDATE Customer SET last_name = @NewName WHERE customer_id = @CustomerID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NewName", newName); // Bind the new last name as a parameter to prevent SQL injection
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();
                command.ExecuteNonQuery();

            }
        }
        /// <summary>
        /// Update Email Address
        /// </summary>
        public void UpdateEmailAdress(string newMail)
        {
            string query = "UPDATE Customer SET email_adress = @NewMail WHERE customer_id = @CustomerID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NewMail", newMail); // Bind the new email as a parameter to prevent SQL injection
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();
                command.ExecuteNonQuery();

            }
        }
        /// <summary>
        /// Update Phone Number
        /// </summary>
        public void UpdatePhoneNumber(string newPhone)
        {
            string query = "UPDATE Customer SET phone_number = @NewPhone WHERE customer_id = @CustomerID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NewPhone", newPhone); // Bind the new last name as a parameter to prevent SQL injection
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();
                command.ExecuteNonQuery();

            }
        }
        /// <summary>
        /// Update Physical Address
        /// </summary>
        public void UpdatePhysicalAdress(string newAddress)
        {
            string query = "UPDATE Customer SET physical_address = @NewAddress WHERE customer_id = @CustomerID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NewAddress", newAddress);
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();
                command.ExecuteNonQuery();

            }
        }
        /// <summary>
        /// Update Loyalty Points
        /// </summary>
        /// <param name="newPoints"></param>
        public void UpdateLoyaltyPoints(int newPoints)
        {
            string query = "UPDATE Customer SET loyalty_points += @NewPoints WHERE customer_id = @CustomerID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NewPoints", newPoints);
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="newPass"></param>
        public void UpdatePassword(string newPass)
        {
            string query = "UPDATE Customer SET last_name = @NewPass WHERE customer_id = @CustomerID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NewPass", newPass); // Bind the new last name as a parameter to prevent SQL injection
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();
                command.ExecuteNonQuery();

            }
        }
        /// <summary>
        /// Get Customer Details
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            string query = "SELECT customer_id, first_name, last_name, email_address, phone_number, physical_address, loyalty_points, password " +
               "FROM Customer WHERE customer_id = @CustomerID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Customer customer = new Customer(
                            (int)reader["customer_id"],
                            reader["first_name"].ToString(),
                            reader["last_name"].ToString(),
                            reader["email_address"].ToString(),
                            reader["phone_number"].ToString(),
                            reader["physical_address"].ToString(),
                            (int)reader["loyalty_points"],
                            reader["password"].ToString()
                        );
                        return customer;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Get List Of Customers
        /// </summary>
        /// <param name="page"></param>
        /// <param name="maxListSize"></param>
        /// <returns></returns>
        public System.Collections.Generic.List<Customer> GetCustomers(int page,int maxListSize)
        {
            List<Customer> customers = new List<Customer>();

            // Calculate the starting row for the given page and maxListSize
            int startRow = (page - 1) * maxListSize;

            string query = "SELECT customer_id, first_name, last_name, email_address, phone_number, physical_address, loyalty_points, password " +
                           "FROM Customer " +
                           "ORDER BY customer_id " +
                           $"OFFSET {startRow} ROWS FETCH NEXT @MaxListSize ROWS ONLY";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaxListSize", maxListSize);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        (
                             (int)reader["customer_id"],
                            reader["first_name"].ToString(),
                            reader["last_name"].ToString(),
                            reader["email_address"].ToString(),
                            reader["phone_number"].ToString(),
                            reader["physical_address"].ToString(),
                            (int)reader["loyalty_points"],
                            reader["password"].ToString()
                        );

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }
        /// <summary>
        /// Retrieves customer information based on the provided email address.
        /// </summary>
        /// <param name="emailAddress">The email address of the customer to retrieve.</param>
        /// <returns>A Customer object representing the customer information, or null if not found.</returns>
        public static Customer GetCustomer(string emailAddress)
        {
            string query = "SELECT customer_id, first_name, last_name, email_address, phone_number, physical_address, loyalty_points " +
                            "FROM Customer WHERE email_address = @emailAddress";
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@emailAddress", emailAddress);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Customer customer = new Customer(
                            (int)reader["customer_id"],
                            reader["first_name"].ToString(),
                            reader["last_name"].ToString(),
                            reader["email_address"].ToString(),
                            reader["phone_number"].ToString(),
                            reader["physical_address"].ToString(),
                            (int)reader["loyalty_points"]
                        );
                        return customer;
                    }
                }
            }
            return null;
        }
        public int GetNumberOfBooking()
        {
            int numberOfBookings = 0;

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM [Event Booking] WHERE customer_id = @customerId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@customerId", this.customerID);

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    numberOfBookings = Convert.ToInt32(result);
            }
            return numberOfBookings;
        }
        public int GetNumberOfOrders()
        {
            int numberOfOrders = 0;
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM [Order] WHERE customer_id = @customerId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@customerId", this.customerID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    numberOfOrders = Convert.ToInt32(result);
            }
            return numberOfOrders;
        }
        public static Customer SetSession()
        {
            Customer currentCustomer = Models.Customer.GetCustomer(HttpContext.Current.User.Identity.Name);
            if (currentCustomer != null)
                HttpContext.Current.Session["Customer"] = currentCustomer;
            else
                HttpContext.Current.Response.Redirect("/Customer/Profile");

            return currentCustomer;
        }
        public static Customer SetSession(string ReturnUrl)
        {
            Customer currentCustomer = Models.Customer.GetCustomer(HttpContext.Current.User.Identity.Name);
            if (currentCustomer != null)
                HttpContext.Current.Session["Customer"] = currentCustomer;
            else
                HttpContext.Current.Response.Redirect("/Customer/Profile?ReturnUrl="+ReturnUrl);

            return currentCustomer;
        }


        /// <summary>
        /// Getters and Setters
        /// </summary>
        public int CustomerID { get => customerID; set => customerID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string FullName { get => firstName + " " + lastName;}
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string PhysicalAddress { get => physicalAddress; set => physicalAddress = value; }
        public int LoyaltyPoints { get => loyaltyPoints; set => loyaltyPoints = value; }
        public string Password { get => password; set => password = value; }
    }
}