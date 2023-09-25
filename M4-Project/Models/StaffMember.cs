using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                "WHERE[staff_id] = @staffID";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@staffID", staffID);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count < 1)
                    return null;

                DataRow row = dt.Rows[0];
                return new StaffMember((int) row["staff_id"], row["first_name"].ToString(), row["last_name"].ToString(), row["gender"].ToString(), (decimal)row["pay_rate"], row["email_address"].ToString(), row["phone_number"].ToString(), (byte[])row["staff_image"], row["password"].ToString(), row["role"].ToString(), row["status"].ToString());
            }
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
                            return new StaffMember(
                                (int)reader["staff_id"],
                                reader["first_name"].ToString(),
                                reader["last_name"].ToString(),
                                reader["gender"].ToString(),
                                (decimal)reader["pay_rate"],
                                reader["email_address"].ToString(),
                                reader["phone_number"].ToString(),
                                (byte[])reader["staff_image"],
                                reader["password"].ToString(),
                                reader["role"].ToString(),
                                reader["status"].ToString()
                            );
                        }
                    }
                }
            }
            return null;
        }

        ///
        /// <summary>
        ///     Saves the attributes' values of the StaffMember instance into the database.
        /// </summary>
        public void AddStaffMember()
        {
            string query = "INSERT INTO [Staff] VALUES(@firstName, @lastName, @gender, @payRate, @emailAddress, @phoneNumber, @password, @role, @staffImage, @staffStatus);" +
              "SELECT SCOPE_IDENTITY() AS staff_id;";
            
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
                command.Parameters.AddWithValue("@staffImage", staffImage);
                command.Parameters.AddWithValue("@staffStatus", status);
                connection.Open();

                object insertedItemId = command.ExecuteScalar();
                this.staffID = Convert.ToInt32(insertedItemId);

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


        public int StaffID { get => staffID; set => staffID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Gender { get => gender; set => gender = value; }
        public decimal PayRate { get => payRate; set => payRate = value; }
        public string PayRateN2 { get => payRate.ToString("N2"); }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public byte[] StaffImage { get => staffImage; set => staffImage = value; }
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
        public readonly static string Deactivate = "Deactivate";
        /// <summary>
        ///     When a staff member is in an Archive status, they are permanently barred from accessing the system.
        /// </summary>
        public readonly static string Archive = "Archive";
    }
}