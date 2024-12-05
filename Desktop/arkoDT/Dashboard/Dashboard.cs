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
        public string userID;

        public frmDashboard(frmLogin login)
        {
            InitializeComponent();
            loginForm = login;
            userID = loginForm.UserID;

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
                frmUsers form4 = new frmUsers(this);
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

        public void CountUsers()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open(); // Ensure the connection is open
                }

                string query = "SELECT COUNT(*) FROM users";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                    label5.Text = $"User(s): {userCount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to count users: " + ex.Message);
                RetryFetchUserCount(); // Trigger retry logic
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close(); // Ensure the connection is closed
                }
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

        private void lblProfile_Click(object sender, EventArgs e)
        {
            frmProfile form1 = new frmProfile(this);
            form1.Show();
        }
    }
}

