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

namespace arkoDT
{
    public partial class frmDashboard : Form
    {
        private frmLogin loginForm;  // Field to store the frmLogin instance

        public frmDashboard(frmLogin login)
        {
            InitializeComponent();
            loginForm = login;  // Store the frmLogin instance passed in the constructor
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
            string user = loginForm.Username;
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
            if(role == "User")
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
            frmUsers form4 = new frmUsers();
            form4.Show();
        }

        private void lblGoWaterLevel_Click(object sender, EventArgs e)
        {
            frmGraph form3 = new frmGraph();
            form3.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin loginForm = new frmLogin(); // Create a new instance of frmLogin
            loginForm.Show();                     // Show the new frmLogin form
            this.Hide();
        }
    }
}
