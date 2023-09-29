using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace M4_Project.Models
{
    ///
    /// <summary>
    ///     Represents a staff member.
    /// </summary>
    public class StaffMember
    {
        private int staffID;
        private string firstName;
        private string lastName;
        private string gender;
        private decimal payRate;
        private string emailAddress;


        private string phoneNumber;
        private byte[] staffImage;
        private string password;
        private string role;
        private string status;

        ///
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.Sales.StaffMember class.
        /// </summary>
        public StaffMember()
        {
            staffID = -1;
        }
        ///
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.Sales.StaffMember class.
        /// </summary>
        public StaffMember(int staffID, string firstName, string lastName, string gender, decimal payRate, string emailAddress, string phoneNumber, byte[] staffImage, string password, string role, string status)
        {
            this.staffID = staffID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.payRate = payRate;
            this.emailAddress = emailAddress;
            this.phoneNumber = phoneNumber;
            this.staffImage = staffImage;
            this.password = password;
            this.role = role;
            this.status = status;
        }
        ///
        /// <summary>
        ///     Returns a list of staff members.
        /// </summary>
        public static List<StaffMember> GetStaff(int page, int maxListSize)
        {
            if (page < 1)
                page = 1;

            List<StaffMember> staff = new List<StaffMember>();
            //StaffMember staffMember = new StaffMember();
            string query = "SELECT [staff_id], [first_name], [last_name], [gender], [pay_rate], [email_address], [phone_number], [password], [role], [staff_image], [status] " +
                           "FROM [GroupPmb6].[dbo].[Staff]" +
                           "ORDER BY [first_name], [last_name] ASC" +
                           "OFFSET @page ROWS" +
                           "FETCH NEXT @maxCount ROWS ONLY;";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@page", (page - 1) * maxListSize);
                command.Parameters.AddWithValue("@maxCount", maxListSize);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    staff.Add(new StaffMember((int)row["staff_id"], (string)row["first_name"], (string)row["last_name"], (string)row["gender"], (decimal)row["pay_rate"], (string)row["email_address"], (string)row["phone_number"], (byte[])row["staff_image"], (string)row["password"], (string)row["role"], (string)row["status"]));
                }
                return staff;
            }
        }
        ///
        /// <summary>
        ///     Returns a staff member on the database using the staff identification number as parameter.
        /// </summary>
        public static StaffMember GetStaffMember(int staffID)
        {
            string query = "SELECT [staff_id], [first_name], [last_name], [gender], [pay_rate], [email_address], [phone_number], [password], [role], [staff_image], [status] " +
               "FROM [GroupPmb6].[dbo].[Staff] " +
               "WHERE [staff_id] = @staffID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staffID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            StaffMember staffMember = new StaffMember(
                                (int)reader["staff_id"],
                                reader["first_name"].ToString(),
                                reader["last_name"].ToString(),
                                reader["gender"].ToString(),
                                (decimal)reader["pay_rate"],
                                reader["email_address"].ToString(),
                                reader["phone_number"].ToString(),
                                (reader.IsDBNull(reader.GetOrdinal("staff_image"))) ? StaffSearch.GetDefaultImage() : (byte[])reader["staff_image"],
                                reader["password"].ToString(),
                                reader["role"].ToString(),
                                reader["status"].ToString()
                            );
                            if (reader.IsDBNull(reader.GetOrdinal("staff_image")))
                                staffMember.ImageIsDefault = true;

                            return staffMember;
                        }
                    }
                }
            }
            return null;
        }

        public static StaffMember GetStaffMember_short(int staffID)
        {
            StaffMember staffMember = null;

            string query = "SELECT staff_id, first_name, last_name, [role], phone_number, email_address FROM [Staff] WHERE staff_id = @staffID;";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staffID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            staffMember = new StaffMember
                            {
                                StaffID = (int)reader["staff_id"],
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                Role = reader["role"].ToString(),
                                EmailAddress = reader["email_address"].ToString(),
                                PhoneNumber = reader["phone_number"].ToString()
                            };
                        }
                    }
                }
            }
            return staffMember;
        }
        /// <summary>
        ///     Retrieves staff member information based on the provided email address.
        /// </summary>
        /// <param name="emailAddress">The email address of the staff member to retrieve.</param>
        /// <returns>A StaffMember object representing the staff member information, or null if not found.</returns>
        public static StaffMember GetStaffMember(string emailAddress)
        {
            string query = "SELECT [staff_id], [first_name], [last_name], [gender], [pay_rate], [email_address], [phone_number], [password], [role], [staff_image], [status] " +
                           "FROM [GroupPmb6].[dbo].[Staff] " +
                           "WHERE [email_address] = @emailAddress";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@emailAddress", emailAddress);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            StaffMember staffMember = new StaffMember(
                                (int)reader["staff_id"],
                                reader["first_name"].ToString(),
                                reader["last_name"].ToString(),
                                reader["gender"].ToString(),
                                (decimal)reader["pay_rate"],
                                reader["email_address"].ToString(),
                                reader["phone_number"].ToString(),
                                (reader.IsDBNull(reader.GetOrdinal("staff_image"))) ? StaffSearch.GetDefaultImage() : (byte[])reader["staff_image"],
                                reader["password"].ToString(),
                                reader["role"].ToString(),
                                reader["status"].ToString()
                            );
                            if (reader.IsDBNull(reader.GetOrdinal("staff_image")))
                                staffMember.ImageIsDefault = true;

                            return staffMember;
                        }
                    }
                }
            }
            return null;
        }
        public static List<StaffMember> GetBookingStaff(int bookingID)
        {
            List<StaffMember> staffMembers = new List<StaffMember>();

            string query = @"SELECT [Staff].[staff_id], [first_name], [last_name], [email_address], [phone_number], [role], [status], staff_image
                         FROM [Staff], [Event Staff]
                         WHERE [Staff].staff_id = [Event Staff].staff_id AND [Event Staff].booking_id = @bookingID";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookingID", bookingID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StaffMember staffMember = new StaffMember()
                            {
                                StaffID = Convert.ToInt32(reader["staff_id"]),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                EmailAddress = reader["email_address"].ToString(),
                                PhoneNumber = reader["phone_number"].ToString(),
                                Role = reader["role"].ToString(),
                                Status = reader["status"].ToString(),
                                StaffImage = (reader.IsDBNull(reader.GetOrdinal("staff_image"))) ? StaffSearch.GetDefaultImage() : (byte[])reader["staff_image"]
                            };
                            staffMembers.Add(staffMember);
                        }
                    }
                }
            }
            return staffMembers;
        }
        ///
        /// <summary>
        ///     Saves the attributes' values of the StaffMember instance into the database.
        /// </summary>
        public void AddStaffMember()
        {
            string query;
            if (staffImage != null && staffImage.Length > 0)
            {
               query = "INSERT INTO [Staff] VALUES(@firstName, @lastName, @gender, @payRate, @emailAddress, @phoneNumber, @password, @role, @staffImage, @staffStatus) " +
              "SELECT SCOPE_IDENTITY() AS staff_id;";
            }
            else
            {
               query = "INSERT INTO [Staff] VALUES(@firstName, @lastName, @gender, @payRate, @emailAddress, @phoneNumber, @password, @role, @staffStatus) " +
              "SELECT SCOPE_IDENTITY() AS staff_id;";
            }

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@payRate", payRate);
                command.Parameters.AddWithValue("@emailAddress", emailAddress);
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@staffStatus", status);

                if (staffImage != null &&  staffImage.Length > 0)
                    command.Parameters.AddWithValue("@staffImage", staffImage);
                connection.Open();
                staffID = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        public void UpdateStaffMember()
        {
            string query;
            if (staffImage != null &&  staffImage.Length > 0)
            {
                query = "UPDATE [Staff] SET [first_name] = @firstName, [last_name] = @lastName, [gender] = @gender, [pay_rate] = @payRate, " +
                        "[email_address] = @emailAddress, [phone_number] = @phoneNumber, [password] = @password, [role] = @role, " +
                        "[status] = @staffStatus, [staff_image] = @staffImage WHERE [staff_id] = @staffID;";
            }
            else
            {
                query = "UPDATE [Staff] SET [first_name] = @firstName, [last_name] = @lastName, [gender] = @gender, [pay_rate] = @payRate, " +
                        "[email_address] = @emailAddress, [phone_number] = @phoneNumber, [password] = @password, [role] = @role, " +
                        "[status] = @staffStatus WHERE [staff_id] = @staffID;";
            }

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@staffID", staffID);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@payRate", payRate);
                command.Parameters.AddWithValue("@emailAddress", emailAddress);
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@staffStatus", status);

                if (staffImage != null && staffImage.Length > 0)
                    command.Parameters.AddWithValue("@staffImage", staffImage);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        ///
        /// <summary>
        ///     Delete a staff member using a specific staff identification number.
        /// </summary>
        public static void Delete(int staffID)
        {
            string query = "DELETE [Staff] WHERE [staff_id] = @staff_id";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@staff_id", staffID);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public int GetNumberOfBookings()
        {
            string query = "SELECT COUNT(*) " +
                           "FROM [Event Staff] " +
                           "WHERE [staff_id] = @staffID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staffID);
                    connection.Open();

                    int numberOfBookings = (int)command.ExecuteScalar();

                    return numberOfBookings;
                }
            }
        }
        public int GetNumberOfOrders()
        {
            string query = "SELECT COUNT(*) " +
                           "FROM [Order] " +
                           "WHERE [staff_id] = @staffID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staffID);
                    connection.Open();

                    int numberOfOrders = (int)command.ExecuteScalar();

                    return numberOfOrders;
                }
            }
        }
        public static List<string> GetRoles()
        {
            List<string> roles = new List<string>();
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                connection.Open();
                string query = "SELECT DISTINCT Role FROM Staff";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            roles.Add(role);
                        }
                    }
                }
            }
            return roles;
        }
        public static void UpdateStaffStatus(int staffID, string status)
        {
            string query = "UPDATE [Staff] SET [status] = @status WHERE [staff_id] = @staffID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@staffID", staffID);
                command.Parameters.AddWithValue("@status", status);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public bool DeleteStaff()
        {
            string query = "DELETE FROM [Staff] WHERE [staff_id] = @staffID";
            int rowsAffected;

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@staffID", staffID);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected > 0;
        }
        public int StaffID { get => staffID; set => staffID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string FullName { get => firstName + " " + lastName; }
        public string Gender { get => gender; set => gender = value; }
        public decimal PayRate { get => payRate; set => payRate = value; }
        public string PayRateN2 { get => payRate.ToString("N2"); }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public byte[] StaffImage { get => staffImage; set => staffImage = value; }
        public bool ImageIsDefault { get; set; }
        public string Password { get => password; set => password = value; }
        public string Role { get => role; set => role = value; }
        public string Status { get => status; set => status = value; }
    }
    ///
    /// <summary>
    ///     Enumerates Potential Staff Member States.
    /// </summary>
    public static class StaffMemberState
    {
        /// <summary>
        ///     When a staff member is in an Active status, they are granted access to the system.
        /// </summary>
        public readonly static string Active = "Active";
        /// <summary>
        ///     When a staff member is in a Deactivate status, access to the system is denied.
        /// </summary>
        public readonly static string Deactivated = "Deactivated";
        /// <summary>
        ///     When a staff member is in an Archive status, they are permanently barred from accessing the system.
        /// </summary>
        public readonly static string Archive = "Archive";
    }
    public class StaffSearch
    {
        public string Query { get; private set; }
        public string RowCountQuery { get; private set; }
        public int StaffID { get; private set; }
        public string CustomerName { get; private set; }
        public int Page { get; private set; }
        public int MaxPerPage { get; private set; }
        public int MaxPage { get; private set; }
        private SqlCommand Command { get; set; }
        public List<StaffMember> StaffMembers { get; private set; }

        public StaffSearch(string pageString, string searchText, string role, int MaxPerPage)
        {
            this.MaxPerPage = MaxPerPage;
            Command = new SqlCommand();
            bool whereAdded = false;
            StringBuilder whereClause = WhereClause(searchText, role, ref whereAdded);

            if (RowCount(whereClause) < 1)
            {
                this.StaffMembers = new List<StaffMember>();
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
            queryBuilder.Append("SELECT [staff_id], [first_name], [last_name], [email_address], [phone_number], [role], [status], staff_image ");
            queryBuilder.Append("FROM [Staff] ");

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
            GetStaffMembers();
        }
        private void GetStaffMembers()
        {
            List<StaffMember> staffMembers = new List<StaffMember>();
            using (Database dbConnection = new Database(Command))
            {
                using (SqlDataReader reader = dbConnection.Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int staffID = Convert.ToInt32(reader["staff_id"]);
                        string firstName = reader["first_name"].ToString();
                        string lastName = reader["last_name"].ToString();
                        string emailAddress = reader["email_address"].ToString();
                        string phoneNumber = reader["phone_number"].ToString();
                        string role = reader["role"].ToString();
                        string status = reader["status"].ToString();
                        byte[] image = (reader.IsDBNull(reader.GetOrdinal("staff_image"))) ? GetDefaultImage() : (byte[])reader["staff_image"];

                        StaffMember staffMember = new StaffMember()
                        {
                            StaffID = staffID,
                            FirstName = firstName,
                            LastName = lastName,
                            EmailAddress = emailAddress,
                            PhoneNumber = phoneNumber,
                            Role = role,
                            StaffImage = image,
                            Status = status
                        };
                        staffMembers.Add(staffMember);

                        if (reader.IsDBNull(reader.GetOrdinal("staff_image")))
                            staffMember.ImageIsDefault = true;
                    }
                }
            }
            this.StaffMembers = staffMembers;
        }

        public static byte[] GetDefaultImage()
        {
            string defaultImagePath = HttpContext.Current.Server.MapPath("~/Assets/account_circle.png");
            try
            {
                using (FileStream fs = new FileStream(defaultImagePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, (int)fs.Length);
                    return imageData;
                }
            }
            catch
            {
                return new byte[0];
            }
        }

        private int RowCount(StringBuilder whereClause)
        {
            int rowCount = 0;

            StringBuilder query = new StringBuilder();
            RowCountQuery = "SELECT COUNT(staff_id) FROM Staff ";

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
        private StringBuilder WhereClause(string searchText, string role, ref bool whereAdded)
        {
            StringBuilder whereClause = new StringBuilder();

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause.Append(" (first_name + ' ' + last_name LIKE '%' + @search + '%' OR first_name + ' ' + last_name LIKE '%' + @search + '%') ");
                whereAdded = true;
                Command.Parameters.AddWithValue("@search", searchText);
            }

            if (!string.IsNullOrEmpty(role))
            {
                if (whereAdded)
                {
                    whereClause.Append(" AND ");
                }

                whereClause.Append(" [role] = @role ");
                whereAdded = true;
                Command.Parameters.AddWithValue("@role", role);
            }
            return whereClause;
        }

    }
}