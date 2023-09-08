using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M4_Project.Models
{
    public class StaffLoginSession
    {
        private static StaffLoginSession loginSession;

        private int staffID;
        private string firstName;
        private string lastName;
        private string emailAddress;
        private string phoneNumber;
        private byte[] staffImage;
        private string role;

        public StaffLoginSession(int staffID, string role)
        {
            this.staffID = staffID;
            this.role = role;
        }
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.StaffLoginSession class.
        /// </summary>
        public StaffLoginSession(int staffID, string firstName, string lastName, string emailAddress, string phoneNumber, byte[] staffImage, string role)
        {
            this.staffID = staffID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            this.phoneNumber = phoneNumber;
            this.staffImage = staffImage;
            this.role = role;
        }
        ///
        /// <summary>
        ///     Returns a instance of the M4_System.Models.StaffLoginSession class.
        /// </summary>
        public static StaffLoginSession GetSession()
        {
            return loginSession;
        }
        ///
        /// <summary>
        ///     Initializes and Returns a instance of the M4_System.Models.StaffLoginSession class
        ///     if the username is found on the database and the passwords match with the one in the database, otherwise it returns null.
        /// </summary>
        public static StaffLoginSession GetSession(string username, string password)
        {
            string query = "SELECT TOP (1) [staff_id],[first_name],[last_name],[email_address],[phone_number],[password],[role],[staff_image],[status]" +
            "FROM [dbo].[Staff] " +
            "WHERE [email_address] = @username OR [phone_number] = @username;";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count < 1)
                    return null;
                DataRow row = dt.Rows[0];

                if (!Utilities.PasswordManager.PasswordMatch(password, (string)row["password"]))
                    return null;

                loginSession = new StaffLoginSession((int) row["staff_id"], (string)row["first_name"], (string)row["lastName"], (string)row["emailAddress"], (string)row["phoneNumber"], (byte[])row["staffImage"], (string)row["role"]);
            }
            return loginSession;
        }

        public int StaffID { get => staffID; set => staffID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public byte[] StaffImage { get => staffImage; set => staffImage = value; }
        public string Role { get => role; set => role = value; }
    }
    ///
    /// <summary>
    ///     Represents various roles within an organization, each having specific access privileges to designated parts of the system.
    /// </summary>
    public static class StaffRole
    {
        /// <summary>
        ///     Represents the role of a Manager.
        /// </summary>
        public readonly static string Manager = "Manager";
        /// <summary>
        ///     Represents the role of a Driver.
        /// </summary>
        public readonly static string Driver = "Driver";
        /// <summary>
        ///     Represents the role of a Supervisor.
        /// </summary>
        public readonly static string Supervisor = "Supervisor";
        /// <summary>
        ///     Checks if the given role is a Manager role.
        /// </summary>
        public static bool IsManager(string role)
        {
            return Manager == role;
        }
        /// <summary>
        ///     Checks if the given role is a Driver role.
        /// </summary>
        public static bool IsDriver(string role)
        {
            return Driver == role;
        }
        /// <summary>
        ///     Checks if the given role is a Supervisor role.
        /// </summary>
        public static bool IsSupervisor(string role)
        {
            return Supervisor == role;
        }
        /// <summary>
        ///     Checks if the given role has special access (Manager, Driver, or Supervisor).
        /// </summary>
        public static bool HasSpecialAccess(string role)
        {
            return role == Manager || role == Driver || role == Supervisor;
        }
    }
}