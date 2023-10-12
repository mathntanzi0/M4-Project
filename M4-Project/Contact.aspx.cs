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
            // Retrieve values from input controls
            string name = txtName.Text; // Assuming txtName is the ID of the name textbox
            string email = txtEmail.Text;  // Assuming txtEmail is the ID of the email textbox
            string query = txtQuery.Text;  // Assuming txtQuery is the ID of the query textarea


            // Insert into the database using SQL
            using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
            {
                connection.Open();

                // Check if any of the fields is empty before inserting
                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(query))
                {
                    string data_query = "INSERT INTO Queries (customer_name, customer_email, customer_query) VALUES (@Name, @Email, @Query)";
                    using (SqlCommand command = new SqlCommand(data_query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name.Trim());
                        command.Parameters.AddWithValue("@Email", email.Trim());
                        command.Parameters.AddWithValue("@Query", query.Trim());

                        command.ExecuteNonQuery();
                    }

                    // Clear the input fields after successful insertion
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtQuery.Text = "";

                    // Optionally, you can redirect the user to another page after the data is inserted
                    // Response.Redirect("ThankYouPage.aspx");
                }

            }


        }
    }
}