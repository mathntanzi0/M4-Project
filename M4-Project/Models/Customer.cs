using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

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
        public Customer()
        {
            customerID = -1;
            physicalAddress = "";
        }
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
        public void UpdateCustomer()
        {
            string query = "UPDATE Customer " +
                           "SET first_name = @firstName, " +
                           "    last_name = @lastName, " +
                           "    email_address = @emailAddress, " +
                           "    phone_number = @phoneNumber, " +
                           "    physical_address = @physicalAddress " +
                           "WHERE customer_id = @customerId";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@customerId", customerID);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@emailAddress", emailAddress);
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@physicalAddress", physicalAddress);

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
        public System.Collections.Generic.List<Customer> GetCustomers(int page, int maxListSize)
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
                HttpContext.Current.Response.Redirect("/Customer/Profile?ReturnUrl=" + ReturnUrl);

            return currentCustomer;
        }
        public static bool DeleteCustomer(int customerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string query = "UPDATE [GroupPmb6].[dbo].[Customer] " +
                                           "SET [first_name] = 'deleted', " +
                                           "[last_name] = '', " +
                                           "[phone_number] = '', " +
                                           "[physical_address] = '', " +
                                           "[password] = '' " +
                                           "WHERE [customer_id] = @customerID";

                            using (SqlCommand cmdDeleteCustomer = new SqlCommand(query, connection, transaction))
                            {
                                cmdDeleteCustomer.Parameters.AddWithValue("@customerID", customerId);
                                cmdDeleteCustomer.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public static void UpdateDetails(string firstName, string lastName, string phoneNumber, string physicalAddress, int customerId)
        {
            string updateQuery = "UPDATE [Customer] " +
                                 "SET first_name = @FirstName, last_name = @LastName, " +
                                 "phone_number = @PhoneNumber, physical_address = @PhysicalAddress " +
                                 "WHERE customer_id = @CustomerId";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@PhysicalAddress", physicalAddress);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        // Handle the exception, e.g., log or display an error message
                    }
                }
            }
        }


        public int CustomerID { get => customerID; set => customerID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string FullName { get => firstName + " " + lastName; }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string PhysicalAddress { get => physicalAddress; set => physicalAddress = value; }
        public int LoyaltyPoints { get => loyaltyPoints; set => loyaltyPoints = value; }
        public string Password { get => password; set => password = value; }
    }
    public class CustomerSearch
    {
        public string Query { get; private set; }
        public string RowCountQuery { get; private set; }
        public string CustomerName { get; private set; }
        public int Page { get; private set; }
        public int MaxPerPage { get; private set; }
        public int MaxPage { get; private set; }
        private SqlCommand Command { get; set; }
        public List<Customer> Customers { get; private set; }

        public CustomerSearch(string pageString, string searchText, int MaxPerPage)
        {
            this.MaxPerPage = MaxPerPage;
            Command = new SqlCommand();
            bool whereAdded = false;
            StringBuilder whereClause = WhereClause(searchText, ref whereAdded);

            if (RowCount(whereClause) < 1)
            {
                this.Customers = new List<Customer>();
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
            queryBuilder.Append("SELECT [customer_id], [first_name], [last_name], [email_address], [phone_number], [loyalty_points] ");
            queryBuilder.Append("FROM [Customer] ");

            if (whereAdded)
            {
                queryBuilder.Append("WHERE ");
                queryBuilder.Append(whereClause.ToString());
            }
            queryBuilder.Append("ORDER BY (first_name + ' ' + last_name) ASC ");
            queryBuilder.Append("OFFSET @page ROWS ");
            queryBuilder.Append("FETCH NEXT @maxOrders ROWS ONLY;");

            Query = queryBuilder.ToString();
            Command.CommandText = Query;
            GetCustomers();
        }
        private void GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (Database dbConnection = new Database(Command))
            {
                using (SqlDataReader reader = dbConnection.Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int customerID = Convert.ToInt32(reader["customer_id"]);
                        string firstName = reader["first_name"].ToString();
                        string lastName = reader["last_name"].ToString();
                        string emailAddress = reader["email_address"].ToString();
                        string phoneNumber = reader["phone_number"].ToString();
                        int loyaltyPoints = Convert.ToInt32(reader["loyalty_points"]);

                        Customer customer = new Customer()
                        {
                            CustomerID = customerID,
                            FirstName = firstName,
                            LastName = lastName,
                            EmailAddress = emailAddress,
                            PhoneNumber = phoneNumber,
                            LoyaltyPoints = loyaltyPoints
                        };

                        customers.Add(customer);
                    }
                }
            }
            this.Customers = customers;
        }
        private int RowCount(StringBuilder whereClause)
        {
            int rowCount = 0;

            StringBuilder query = new StringBuilder();
            RowCountQuery = "SELECT COUNT([customer_id]) FROM [Customer] ";

            query.Append(RowCountQuery);
            if (whereClause.Length < 1)
                query.Append(';');
            else
            {
                query.Append("WHERE ");
                query.Append(whereClause.ToString());
            }

            Command.CommandText = query.ToString();
            using (Database dbConnection = new Database(Command))
                rowCount = (int)dbConnection.Command.ExecuteScalar();

            if (rowCount != 0)
                MaxPage = (int)Math.Ceiling((decimal)rowCount / (decimal)MaxPerPage);
            return rowCount;
        }
        private StringBuilder WhereClause(string searchText, ref bool whereAdded)
        {
            StringBuilder whereClause = new StringBuilder();

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause.Append(" (first_name + ' ' + last_name LIKE '%' + @search + '%') ");
                whereAdded = true;
                Command.Parameters.AddWithValue("@search", searchText);
            }
            return whereClause;
        }
    }
}