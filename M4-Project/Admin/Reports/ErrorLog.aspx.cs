using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.Reports
{
    public partial class ErrorLog : System.Web.UI.Page
    {
        protected int page = 1;
        protected int errorLogCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["Page"], out page))
                page = 1;

            List<ErrorLogEntry> queries = GetErrorLogEntries(page, 6);
            errorLogCount = queries.Count;
            ErrorLogRepeater.DataSource = queries;
            ErrorLogRepeater.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["Page"], out page))
                page = 1;
            DeleteErrorLogs(page, 6);
        }

        private void DeleteErrorLogs(int pageNumber, int itemsPerPage)
        {
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                connection.Open();

                string deleteQuery = $@"
                    DELETE FROM ErrorLog
                    WHERE log_time IN (
                        SELECT log_time
                        FROM (
                            SELECT [error_message], [stack_trace], [log_time],
                                   ROW_NUMBER() OVER (ORDER BY log_time) AS RowNum
                            FROM ErrorLog
                        ) AS ErrorLogs
                        WHERE RowNum BETWEEN {((pageNumber - 1) * itemsPerPage) + 1} AND {pageNumber * itemsPerPage}
                    )
                ";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            Response.Redirect("/Admin/Reports/ErrorLog");
        }

        private List<ErrorLogEntry> GetErrorLogEntries(int pageNumber, int itemsPerPage)
        {
            List<ErrorLogEntry> entries = new List<ErrorLogEntry>();

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                connection.Open();

                string query = $@"
                SELECT [error_message], [stack_trace], [log_time]
                FROM [ErrorLog]
                ORDER BY log_time DESC
                OFFSET {((pageNumber - 1) * itemsPerPage)} ROWS
                FETCH NEXT {itemsPerPage} ROWS ONLY";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ErrorLogEntry entry = new ErrorLogEntry
                            {
                                ErrorMessage = reader["error_message"].ToString(),
                                StackTrace = reader["stack_trace"].ToString(),
                                LogTime = Convert.ToDateTime(reader["log_time"])
                            };
                            entries.Add(entry);
                        }
                    }
                }
            }

            return entries;
        }

        protected void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (page < 2)
            {
                page = 1;
                Response.Redirect($"/Admin/Reports/ErrorLog?Page={page}");
                return;
            }
            page -= 1;
            Response.Redirect($"/Admin/Reports/ErrorLog?Page={page}");
        }
        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            page += 1;
            Response.Redirect($"/Admin/Reports/ErrorLog?Page={page}");
        }
    }
    public class ErrorLogEntry
    {
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime LogTime { get; set; }
    }
}