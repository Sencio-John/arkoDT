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
            frmAdd form2 = new frmAdd(this);
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

        public void UpdateDevicesAttributes()
        {
            Panel pnlCard = new Panel();
            Panel pnlHeader = new Panel();
            Label Title = new Label();
            Label Device_Name = new Label();
            Label Device_Status = new Label();

            Title.Dock = System.Windows.Forms.DockStyle.Fill;
            Title.Location = new System.Drawing.Point(0, 0);
            Title.Size = new System.Drawing.Size(285, 53);
            Title.TabIndex = 0;
            Title.Text = "Device";
            Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlHeader.BackColor = System.Drawing.SystemColors.ControlLight;
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Size = new System.Drawing.Size(285, 53);
            pnlHeader.TabIndex = 4;

            Device_Status.Dock = System.Windows.Forms.DockStyle.Right;
            Device_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Device_Status.Location = new System.Drawing.Point(156, 53);
            Device_Status.Size = new System.Drawing.Size(100, 47);
            Device_Status.TabIndex = 6;
            Device_Status.Text = "Status";
            Device_Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            Device_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            Device_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Device_Name.Location = new System.Drawing.Point(0, 53);
            Device_Name.Size = new System.Drawing.Size(256, 47);
            Device_Name.TabIndex = 5;
            Device_Name.Text = "Device Name";
            Device_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            pnlCard.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlCard.Location = new System.Drawing.Point(3, 3);
            pnlCard.Size = new System.Drawing.Size(256, 100);
            pnlCard.TabIndex = 4;

            pnlHeader.Controls.Add(Title);
            pnlCard.Controls.Add(Device_Name);
            pnlCard.Controls.Add(Device_Status);
            pnlCard.Controls.Add(pnlHeader);
            flpDevices.Controls.Add(pnlCard);
        }

        public void UpdateUserAttributes()
        {
            Panel pnlCard = new Panel();
            Panel pnlHeader = new Panel();
            Label Title = new Label();
            Label Device_Name = new Label();
            Label Device_Status = new Label();

            Title.Dock = System.Windows.Forms.DockStyle.Fill;
            Title.Location = new System.Drawing.Point(0, 0);
            Title.Size = new System.Drawing.Size(285, 53);
            Title.TabIndex = 0;
            Title.Text = "Device";
            Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlHeader.BackColor = System.Drawing.SystemColors.ControlLight;
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Size = new System.Drawing.Size(285, 53);
            pnlHeader.TabIndex = 4;

            Device_Status.Dock = System.Windows.Forms.DockStyle.Right;
            Device_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Device_Status.Location = new System.Drawing.Point(156, 53);
            Device_Status.Size = new System.Drawing.Size(100, 47);
            Device_Status.TabIndex = 6;
            Device_Status.Text = "Status";
            Device_Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            Device_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            Device_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Device_Name.Location = new System.Drawing.Point(0, 53);
            Device_Name.Size = new System.Drawing.Size(256, 47);
            Device_Name.TabIndex = 5;
            Device_Name.Text = "Device Name";
            Device_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            pnlCard.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlCard.Location = new System.Drawing.Point(3, 3);
            pnlCard.Size = new System.Drawing.Size(256, 100);
            pnlCard.TabIndex = 4;

            pnlHeader.Controls.Add(Title);
            pnlCard.Controls.Add(Device_Name);
            pnlCard.Controls.Add(Device_Status);
            pnlCard.Controls.Add(pnlHeader);
            flpDevices.Controls.Add(pnlCard);
        }
    }
}
