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

        private void btnDevices_Click(object sender, EventArgs e)
        {
            frmDevices form2 = new frmDevices();
            form2.Show();
        }

        private void btnGraph_Click(object sender, EventArgs e)
        {
            frmGraph form3 = new frmGraph();
            form3.Show();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            frmUC form4 = new frmUC();
            form4.Show();
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
        }

        private void UpdateLabel(object sender, EventArgs e)
        {
            // Assuming your label is named 'labelDateTime'
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy hh:mm tt");
        }
    }
}
