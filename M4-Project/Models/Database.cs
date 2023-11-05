using System;
using System.Data;
using System.Data.SqlClient;

namespace M4_Project.Models
{
    /// <summary>
    /// Represents a static class for managing database connection configuration.
    /// </summary>
    public class Database : IDisposable
    {
        private static string server = "146.230.177.46";
        private static string catalog = "GroupPmb6";
        private static string persistSecurity = "True";
        private static string user = "GroupPmb6";
        private static string password = "m2daz8";

        /// <summary>
        /// Gets the connection string for the database.
        /// </summary>
        public static string ConnectionString =
            "Data Source=" + server +
            ";Initial Catalog=" + catalog +
            ";Persist Security Info=" + persistSecurity +
            ";User ID=" + user +
            ";Password=" + password;

        /// <summary>
        /// Initializes a new instance of the M4_Project.Models.Database class.
        /// </summary>
        public Database(SqlCommand command)
        {
            try
            {
                Connection = new SqlConnection(ConnectionString);
                command.Connection = Connection;
                Command = command;
                Connection.Open();
            }
            catch (Exception ex)
            {
                SystemUtilities.LogError(ex);
                Console.WriteLine($"Error in Database constructor: {ex.Message}");

            }
        }

        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }

        /// <summary>
        /// Dispose of the resources used by the Database class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    Command?.Dispose();
                    Connection?.Close();
                    Connection?.Dispose();
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Error in Dispose method: {ex.Message}");
                    SystemUtilities.LogError(ex);
                }
            }
        }
    }
}

