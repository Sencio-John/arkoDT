using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Add MySQL namespace
using arkoDT.Classes;

namespace arkoDT
{
    public partial class frmDashboard : Form
    {
        private frmLogin loginForm;
        private MySqlConnection connection; // MySQL connection object

        public frmDashboard(frmLogin login)
        {
            InitializeComponent();
            loginForm = login;

            // Set up MySQL connection
            string connectionString = "Server=localhost;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);

            // Check connection to the database
            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to connect to Database.");
            }
        }

        private void btnController_Click(object sender, EventArgs e)
        {
            frmControl form1 = new frmControl();
            form1.Show();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            frmMap form5 = new frmMap();
            form5.Show();
        }

        private void frmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the connection when the form is closing
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            Application.Exit();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            CountUsers();
            string user = loginForm.Name;
            string role = loginForm.Role;
            lblWelcome.Text = "Welcome, " + user;
            lblRole.Text = role;

            // Set up the Timer
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += new EventHandler(UpdateLabel); // Subscribe to Tick event
            timer.Start(); // Start the timer

            lblGoWaterLevel.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable
            lblGoUsers.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable
            lblDevices.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable
            lblProfile.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable

            if (role == "User")
            {
                pnlUsers.Visible = false;
                pnlDeviceCount.Visible = false;
            }
            else
            {
                pnlUsers.Visible = true;
                pnlDeviceCount.Visible = true;
            }
        }

        private void UpdateLabel(object sender, EventArgs e)
        {
            lblDay.Text = DateTime.Now.ToString("dddd");            // e.g., "Monday"
            lblDateNum.Text = DateTime.Now.Day.ToString();                    // e.g., "28"
            lblDate.Text = DateTime.Now.ToString("MMMM yyyy");       // e.g., "October 2024"
            lblTime.Text = DateTime.Now.ToString("hh:mm");                // e.g., "7:00"
            lblPeriod.Text = DateTime.Now.ToString("tt");
        }

        private void lblDevices_Click(object sender, EventArgs e)
        {
            frmDevices form2 = new frmDevices();
            form2.Show();
        }

        private void lblGoUsers_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure you close any previous DataReader or connections if any
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                // Create and show the frmUsers form
                frmUsers form4 = new frmUsers();
                form4.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void lblGoWaterLevel_Click(object sender, EventArgs e)
        {
            frmGraph form3 = new frmGraph();
            form3.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
            this.Hide();
        }

        public void RefreshDashboard()
        {
            CountUsers(); // Update user count label
        }

        public void CountUsers()
        {
            try
            {
                // Fetch user count from MySQL database
                string query = "SELECT COUNT(*) FROM users";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                label5.Text = $"User(s): {userCount}";
            }
            catch (Exception)
            {
                RetryFetchUserCount(); // Trigger a retry
            }
        }

        private void RetryFetchUserCount()
        {
            Timer retryTimer = new Timer();
            retryTimer.Interval = 1000; // Retry every 1 second
            retryTimer.Tick += (sender, e) =>
            {
                try
                {
                    // Attempt to fetch the user count again
                    string query = "SELECT COUNT(*) FROM users";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                    label5.Text = $"User(s): {userCount}";
                    ((Timer)sender).Stop(); // Stop retrying after success
                }
                catch
                {
                    // If it fails, do nothing; the timer will try again
                }
            };
            retryTimer.Start();
        }

        private async void lblProfile_Click(object sender, EventArgs e)
        {
            try
            {
                // Assuming the current user ID is stored as a class field or retrieved from login form
                string userId = loginForm.UserID;

                // Fetch user data from MySQL database
                string query = "SELECT first_Name, last_Name, email, role FROM users_info WHERE user_ID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string firstName = reader.GetString("first_Name");
                        string lastName = reader.GetString("last_Name");
                        string email = reader.GetString("email");
                        string role = reader.GetString("role");

                        frmProfile form1 = new frmProfile(firstName, lastName, email, role);
                        form1.Show();
                    }
                    else
                    {
                        MessageBox.Show("User details could not be found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Close the connection if open
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}

