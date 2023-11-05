using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M4_Project.Models
{
    public class SystemUtilities
    {
        public static void LogError(Exception ex)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO [ErrorLog] (error_message, stack_trace, log_time) " +
                                   "VALUES (@ErrorMessage, @StackTrace, @LogTime)";

                    using (SqlCommand cmdLogError = new SqlCommand(query, connection))
                    {
                        cmdLogError.Parameters.AddWithValue("@ErrorMessage", ex.Message);
                        cmdLogError.Parameters.AddWithValue("@StackTrace", ex.StackTrace.ToString());
                        cmdLogError.Parameters.AddWithValue("@LogTime", DateTime.Now);

                        cmdLogError.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"Error occurred while logging error: {logEx.Message}");
            }
        }
    }
}