using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using arkoDT.Classes;

namespace arkoDT
{
    public partial class frmDashboard : Form
    {
        private frmLogin loginForm;
        private IFirebaseClient client;// Field to store the frmLogin instance

        public frmDashboard(frmLogin login)
        {
            InitializeComponent();
            loginForm = login;
            Firebase_Config firebaseConfig = new Firebase_Config();
            client = firebaseConfig.GetClient();

            if (client == null)
            {
                MessageBox.Show("Failed to connect to Database.");
            }// Store the frmLogin instance passed in the constructor

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
            Application.Exit();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            CountUsers();
            string Username = loginForm.Name;
            string user = loginForm.Name;
            string role = loginForm.Role;
            //string user_ID = loginForm.UserID;
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
            frmUsers form4 = new frmUsers(this);
            form4.Show();
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
                          // Any other updates needed for the dashboard
        }

        public async void CountUsers()
        {
            try
            {
                // Fetch all users from the database
                FirebaseResponse response = await client.GetAsync("Users/");
                var users = response.ResultAs<Dictionary<string, UserRegistration>>();

                // Get the count and update label5
                int userCount = users?.Count ?? 0; // Use 0 if users is null
                label5.Text = $"User(s): {userCount}";
            }
            catch (Exception)
            {
                //MessageBox.Show("Failed to count users: " + ex.Message);
                RetryFetchUserCount(); // Trigger a retry
            }
        }

        private async void lblProfile_Click(object sender, EventArgs e)
        {
            try
            {
                // Assuming the current user ID is stored as a class field or retrieved from login form
                string userId = loginForm.UserID;

                // Fetch user data from Firebase
                FirebaseResponse response = await client.GetAsync($"Users/{userId}");
                UserRegistration user = response.ResultAs<UserRegistration>();

                if (user != null)
                {
                    frmProfile form1 = new frmProfile(
                        user.First_Name,
                        user.Last_Name,
                        user.Email,
                        user.Role
                    );
                    form1.Show();
                }
                else
                {
                    MessageBox.Show("User details could not be found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void RetryFetchUserCount()
        {
            Timer retryTimer = new Timer();
            retryTimer.Interval = 1000; // Retry every 5 seconds
            retryTimer.Tick += async (sender, e) =>
            {
                try
                {
                    // Attempt to fetch the user count again
                    FirebaseResponse response = await client.GetAsync("Users/");
                    var users = response.ResultAs<Dictionary<string, UserRegistration>>();

                    // Update the count and stop the timer if successful
                    int userCount = users?.Count ?? 0; // Use 0 if users is null
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

    }
}
