using System;
using System.Data;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using MySql.Data.MySqlClient;

namespace arkoDT
{
    public partial class frmEdit : Form
    {
        private string userID; // To store the user_ID passed from the previous form
        private MySqlConnection connection; // MySQL connection
        private frmUsers Users;

        public frmEdit(string userID, frmUsers frmUsersInstance)
        {
            InitializeComponent();
            this.userID = userID;
            Users = frmUsersInstance;

            // Initialize MySQL connection
            string connectionString = "Server=localhost;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to connect to the database.");
            }
        }

        // When the form loads, fetch the user's current role and other data
        private void frmEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "SELECT role FROM users WHERE user_ID = @userID";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Display the current role in the ComboBox
                            cmbRole.Text = reader["role"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        // Save the updated role to MySQL when the user clicks 'Save'
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string newRole = cmbRole.Text;

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "UPDATE users SET role = @role WHERE user_ID = @userID";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@role", newRole);
                    cmd.Parameters.AddWithValue("@userID", userID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Role updated successfully!");
                        Users.LoadUsers(); // Refresh the users list in frmUsers
                        this.Close(); // Close the form after updating the role
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the role.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating the role: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        // Close the form without saving when the user clicks 'Back'
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}