using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace M4_Project
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SendNowButton.Click += new EventHandler(SendNowButton_Click);
        }


        protected void SendNowButton_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string query = txtQuery.Text;


            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                connection.Open();

                // Check if any of the fields is empty before inserting
                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(query))
                {
                    string data_query = "INSERT INTO Queries (customer_name, customer_email, customer_query, query_date) VALUES (@Name, @Email, @Query, @Date)";
                    using (SqlCommand command = new SqlCommand(data_query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name.Trim());
                        command.Parameters.AddWithValue("@Email", email.Trim());
                        command.Parameters.AddWithValue("@Query", query.Trim());
                        command.Parameters.AddWithValue("@Date", DateTime.Now);

                        command.ExecuteNonQuery();
                    }

                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtQuery.Text = "";
                    // Response.Redirect("ThankYouPage.aspx");
                }

            }


        }
    }
}