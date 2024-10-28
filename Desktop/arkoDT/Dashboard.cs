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
        public frmDashboard()
        {
            InitializeComponent();
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

            // Set up the Timer
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += new EventHandler(UpdateLabel); // Subscribe to Tick event
            timer.Start(); // Start the timer

            lblGoWaterLevel.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable
            lblGoUsers.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable
            lblDevices.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable
        }

        private void UpdateLabel(object sender, EventArgs e)
        {
            lblDay.Text = DateTime.Now.ToString("dddd");            // e.g., "Monday"
            lblDateNum.Text = DateTime.Now.Day.ToString();                    // e.g., "28"
            lblDate.Text = DateTime.Now.ToString("MMMM yyyy");       // e.g., "October 2024"
            lblTime.Text = DateTime.Now.ToString("hh:mm");                // e.g., "7:00"
            lblPeriod.Text = DateTime.Now.ToString("tt");
        }

        private void lblGoWaterLevel_Click(object sender, EventArgs e)
        {
            frmGraph form3 = new frmGraph();
            form3.Show();
        }

        private void lblGoUsers_Click(object sender, EventArgs e)
        {
            frmUC form4 = new frmUC();
            form4.Show();
        }

        private void lblDevices_Click(object sender, EventArgs e)
        {
            frmDevices form2 = new frmDevices();
            form2.Show();
        }
    }
}
