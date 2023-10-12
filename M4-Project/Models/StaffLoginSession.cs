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
        private int staffID;
        private byte[] staffImage;
        private string role;

        public StaffLoginSession(int staffID, string role)
        {
            this.staffID = staffID;
            this.role = role;
        }

        public StaffLoginSession(int staffID, byte[] staffImage, string role)
        {
            this.staffID = staffID;
            this.staffImage = staffImage;
            this.role = role;
        }
        ///
        /// <summary>
        ///     Initializes and Returns a instance of the M4_System.Models.StaffLoginSession class
        ///     if the username is found on the database and the passwords match with the one in the database, otherwise it returns null.
        /// </summary>
        public static StaffLoginSession GetSession(string username)
        {
            StaffLoginSession loginSession = null;

            string query = "SELECT TOP (1) [staff_id], [role], [staff_image], [status] " +
                           "FROM [dbo].[Staff] " +
                           "WHERE [email_address] = @username";

            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {  
                        loginSession = new StaffLoginSession(
                            (int)reader["staff_id"],
                            (reader.IsDBNull(reader.GetOrdinal("staff_image"))) ? StaffSearch.GetDefaultImage() : (byte[])reader["staff_image"],
                            (string)reader["role"]
                        );
                    }
                }
            }

            return loginSession;
        }
        public static StaffLoginSession SetSession()
        {
            StaffLoginSession loginStaff = HttpContext.Current.Session["LoginStaff"] as Models.StaffLoginSession;
            if (loginStaff == null)
            {
                loginStaff = Models.StaffLoginSession.GetSession(HttpContext.Current.User.Identity.Name);
                if (loginStaff == null)
                    HttpContext.Current.Response.Redirect("/Contact");
                else
                    HttpContext.Current.Session["LoginStaff"] = loginStaff;
            }
            return loginStaff;
        }

        public static bool AccountExist(string email)
        {
            string query = "SELECT COUNT(*) FROM [Staff] WHERE email_address = @Email AND [password] = @Password";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", passToken);

                connection.Open();

                int count = (int)command.ExecuteScalar();

                connection.Close();

                return count > 0;
            }
        }
        public static readonly string passToken = "M4-System";
        public static void UpdatePassword(string email, string newPassword)
        {

            if (!AccountExist(email))
                return;


            string query = "UPDATE [Staff] SET [password] = @NewPassword WHERE email_address = @Email;";

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@NewPassword", -1);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            RoleActions roleActions = new RoleActions();
            roleActions.AddUsertoRole("Admin", email, newPassword);
        }

        public int StaffID { get => staffID; set => staffID = value; }
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