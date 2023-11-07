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



        /// <summary>
        ///     Initializes a new instance of the <see cref="StaffLoginSession"/> class.
        /// </summary>
        /// <param name="staffID">The staff ID.</param>
        /// <param name="role">The staff role.</param>
        public StaffLoginSession(int staffID, string role)
        {
            this.staffID = staffID;
            this.role = role;
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="StaffLoginSession"/> class.
        /// </summary>
        /// <param name="staffID">The staff ID.</param>
        /// <param name="staffImage">The staff image.</param>
        /// <param name="role">The staff role.</param>
        public StaffLoginSession(int staffID, byte[] staffImage, string role)
        {
            this.staffID = staffID;
            this.staffImage = staffImage;
            this.role = role;
        }
        /// <summary>
        ///     Initializes and returns an instance of the <see cref="StaffLoginSession"/> class
        /// if the username is found in the database and the passwords match with the one in the database; otherwise, it returns null.
        /// </summary>
        /// <param name="username">The username (email address).</param>
        /// <returns>A <see cref="StaffLoginSession"/> object if successful, otherwise null.</returns>
        public static StaffLoginSession GetSession(string username)
        {
            try {
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
                            if (reader["status"].ToString() != "Active")
                            {
                                HttpContext.Current.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
                                HttpContext.Current.Session["LoginStaff"] = null;
                                HttpContext.Current.Response.Redirect("/SystemAccess");
                                return null;
                            }
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
            catch (Exception ex) {
                SystemUtilities.LogError(ex);
                return null;
;            }
        }
        /// <summary>
        ///     Sets the session for the staff member.
        /// </summary>
        /// <returns>The staff login session.</returns>
        public static StaffLoginSession SetSession()
        {
            try
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
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error in SetSession: {ex.Message}");
                SystemUtilities.LogError(ex);
                return null; 
            }
        }
        /// <summary>
        ///     Checks if the staff account exists.
        /// </summary>
        /// <param name="email">The email address of the staff member.</param>
        /// <returns>True if the account exists, otherwise false.</returns>
        public static bool AccountExist(string email)
        {
            try
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
            catch (Exception ex)
            {
                SystemUtilities.LogError(ex);
                Console.WriteLine($"Error in AccountExist: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        ///     Updates the password for the staff member.
        /// </summary>
        /// <param name="email">The email address of the staff member.</param>
        /// <param name="newPassword">The new password.</param>
        public static void UpdatePassword(string email, string newPassword)
        {
            try
            {
                if (!AccountExist(email))
                    return;

                string query = "UPDATE [Staff] SET [password] = @NewPassword WHERE email_address = @Email;";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@NewPassword", newPassword); // Fix: Use the actual new password value.

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                RoleActions roleActions = new RoleActions();
                roleActions.AddUsertoRole("Admin", email, newPassword);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error in UpdatePassword: {ex.Message}");
                SystemUtilities.LogError(ex);
            }
        }
        /// <summary>
        ///     Checks if the staff member is a manager or supervisor.
        /// </summary>
        /// <returns>True if the staff member is a manager or supervisor, otherwise false.</returns>
        public bool IsManagerOrSupervisor()
        {
            return StaffRole.IsManager(role) || StaffRole.IsSupervisor(role);
        }
        /// <summary>
        ///     Checks if the staff member is a driver.
        /// </summary>
        /// <returns>True if the staff member is a driver, otherwise false.</returns>
        public bool IsDriver()
        {
            return StaffRole.IsDriver(role);
        }




        public static readonly string passToken = "M4-System";
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