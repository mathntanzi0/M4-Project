using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin.Reports
{
    public partial class CustomerQueries : System.Web.UI.Page
    {
        protected int page = 1;
        protected int queryCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["Page"], out page))
                page = 1;

            List<QueryData> queries = GetQueries(page, 6);
            queryCount = queries.Count;
            QueryRepeater.DataSource = queries;
            QueryRepeater.DataBind();
        }

        public List<QueryData> GetQueries(int pageNumber, int itemsPerPage)
        {
            List<QueryData> queries = new List<QueryData>();

            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                connection.Open();

                string query = "SELECT customer_name, customer_email, customer_query, query_date FROM Queries ORDER BY query_date DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                int offset = (pageNumber - 1) * itemsPerPage;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Offset", offset);
                    command.Parameters.AddWithValue("@PageSize", itemsPerPage);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QueryData queryData = new QueryData
                            {
                                Name = reader["customer_name"].ToString(),
                                Email = reader["customer_email"].ToString(),
                                Query = reader["customer_query"].ToString(),
                                Date = Convert.ToDateTime(reader["query_date"])
                            };

                            queries.Add(queryData);
                        }
                    }
                }
            }
            return queries;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["Page"], out page))
                page = 1;

            int pageSize = 6;
            int offset = (page - 1) * pageSize;
            DeleteQueries(offset, pageSize);
        }
        private void DeleteQueries(int offset, int pageSize)
        {
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                connection.Open();

                string deleteQuery = @"
                    DELETE FROM Queries
                    WHERE query_date IN (
                        SELECT query_date
                        FROM Queries
                        ORDER BY query_date
                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                    )
                ";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Offset", offset);
                    command.Parameters.AddWithValue("@PageSize", pageSize);

                    command.ExecuteNonQuery();
                }
            }
            Response.Redirect("/Admin/Reports/CustomerQueries");
        }
        protected void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (page < 2)
            {
                page = 1;
                Response.Redirect($"/Admin/Reports/CustomerQueries?Page={page}");
                return;
            }
            page -= 1;
            Response.Redirect($"/Admin/Reports/CustomerQueries?Page={page}");
        }
        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            page += 1;
            Response.Redirect($"/Admin/Reports/CustomerQueries?Page={page}");
        }
    }

    public class QueryData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Query { get; set; }
        public DateTime Date { get; set; }
    }

}