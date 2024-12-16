using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using arkoDT.Profile;
using MySql.Data.MySqlClient;

namespace arkoDT
{
    public partial class frmProfile : Form
    {
        private frmDashboard dashboard;
        public string userID;
        private MySqlConnection connection;
        public frmProfile(frmDashboard frmDashboardInstance)
        {
            InitializeComponent();
            dashboard = frmDashboardInstance;
            userID = dashboard.userID;
            string connectionString = "Server=127.0.0.1;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to connect to Database.");
            }
        }

        private void MakeCircularPictureBox(PictureBox pictureBox)
        {

            // Create a graphics path to define a circular region
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);

            // Apply the circular region to the PictureBox
            pictureBox.Region = new Region(path);
        }

        private void MakeRoundedPanel(Panel panel, int borderRadius)
        {
            Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);
            GraphicsPath path = new GraphicsPath();

            int radius = borderRadius;
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);                        // Top-left
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);           // Top-right
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90); // Bottom-right
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);           // Bottom-left
            path.CloseFigure();

            panel.Region = new Region(path);
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            MakeRoundedPanel(pnlProfile, 50);
            MakeRoundedPanel(pnlContacts, 50);
            MakeCircularPictureBox(pbProfile);
            LoadUserProfile();

            dtpBirthdate.Format = DateTimePickerFormat.Custom;
            dtpBirthdate.CustomFormat = "MM/dd/yyyy";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditDetails_Click(object sender, EventArgs e)
        {
            if (btnEditDetails.Text == "Edit Details")
            {
                Enabled();
            }
            else
            {
                LoadUserProfile();
                Disabled();
            }


        }

        private void btnChangeEmail_Click(object sender, EventArgs e)
        {
            frmChangeEmail form1 = new frmChangeEmail(this);
            form1.Show();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword form2 = new frmChangePassword(this);
            form2.Show();
        }

        private void LoadUserProfile()
        {
            try
            {
                // Open the database connection if not already open
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                // SQL query to retrieve user information
                string query = @"
                                SELECT first_Name, last_Name, address, birth_Date, contact 
                                FROM users_info 
                                WHERE user_ID = @userID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Safely handle nullable fields using DBNull.Value check
                            txtFirstName.Text = reader["first_Name"] != DBNull.Value ? reader["first_Name"].ToString() : "";
                            txtLastName.Text = reader["last_Name"] != DBNull.Value ? reader["last_Name"].ToString() : "";
                            txtAddress.Text = reader["address"] != DBNull.Value ? reader["address"].ToString() : "";
                            dtpBirthdate.Text = reader["birth_Date"] != DBNull.Value
                                ? Convert.ToDateTime(reader["birth_Date"]).ToString("MMMM dd, yyyy")
                                : "";
                            txtContactNum.Text = reader["contact"] != DBNull.Value ? reader["contact"].ToString() : "";
                        }
                        else
                        {
                            MessageBox.Show("User information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load user profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the connection is closed
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlTransaction transaction = null;

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open(); // Ensure the connection is open
                }

                if (!txtContactNum.Text.StartsWith("09"))
                {
                    MessageBox.Show("Phone number must start with 09.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Begin a transaction
                transaction = connection.BeginTransaction();

                // Prepare the SQL query
                DateTime selectedDate = dtpBirthdate.Value;
                string formattedDate = selectedDate.ToString("yyyy-MM-dd"); // Corrected date format
                string updateInfo = "UPDATE users_info SET first_Name = @first_name, last_Name = @last_name, " +
                                    "address = @address, birth_Date = @birth_date, contact = @contact WHERE user_ID = @userID";

                MySqlCommand userCmd = new MySqlCommand(updateInfo, connection, transaction);

                // Add parameters with values
                userCmd.Parameters.AddWithValue("@userID", userID); // Ensure userID is properly defined
                userCmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                userCmd.Parameters.AddWithValue("@last_name", txtLastName.Text);
                userCmd.Parameters.AddWithValue("@address", txtAddress.Text);
                userCmd.Parameters.AddWithValue("@birth_date", formattedDate);
                userCmd.Parameters.AddWithValue("@contact", txtContactNum.Text);

                // Execute the command
                userCmd.ExecuteNonQuery();

                // Commit the transaction
                transaction.Commit();

                MessageBox.Show("User information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Additional actions
                Disabled();
                LoadUserProfile();
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of error
                transaction?.Rollback();
                MessageBox.Show($"Error updating user information: {ex.Message}");
            }
            finally
            {
                // Close the connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void Enabled()
        {
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            dtpBirthdate.Enabled = true;
            txtAddress.Enabled = true;
            txtContactNum.Enabled = true;
            btnSave.Enabled = true;

            // Change button text to "Undo"
            btnEditDetails.Text = "Undo";
        }

        private void Disabled()
        {
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            dtpBirthdate.Enabled = false;
            txtAddress.Enabled = false;
            txtContactNum.Enabled = false;
            btnSave.Enabled = false;

            // Change button text back to "Edit Details"
            btnEditDetails.Text = "Edit Details";
        }
    }
}
