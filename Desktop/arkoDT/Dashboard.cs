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

            int margin = 10;

            Panel pnlControls = new Panel();
            Panel pnlGraph = new Panel();
            Panel pnlReg = new Panel();
            Panel pnlMap = new Panel();

            pnlControls.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlControls.Name = "pnlControls";
            pnlControls.Location = new System.Drawing.Point(3, 3);
            pnlControls.Size = new System.Drawing.Size(441, 255);
            pnlControls.TabIndex = 0;

            pnlGraph.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlGraph.Name = "pnlGraph";
            pnlGraph.Location = new System.Drawing.Point(3, 3);
            pnlGraph.Size = new System.Drawing.Size(441, 255);
            pnlGraph.TabIndex = 1;

            pnlReg.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlReg.Name = "pnlReg";
            pnlReg.Location = new System.Drawing.Point(3, 3);
            pnlReg.Size = new System.Drawing.Size(441, 255);
            pnlReg.TabIndex = 2;

            pnlMap.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlMap.Name = "pnlMap";
            pnlMap.Location = new System.Drawing.Point(3, 3);
            pnlMap.Size = new System.Drawing.Size(441, 255);
            pnlMap.TabIndex = 3;

            flpCards.Controls.Add(pnlControls);
            flpCards.Controls.Add(pnlGraph);
            flpCards.Controls.Add(pnlReg);
            flpCards.Controls.Add(pnlMap);
        }

        private void UpdateLabel(object sender, EventArgs e)
        {
            // Assuming your label is named 'labelDateTime'
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy hh:mm tt");
        }

        public void UpdateDevicesAttributes()
        {
            Panel pnlDeviceCard = new Panel();
            Panel pnlDeviceHeader = new Panel();
            Label Title = new Label();
            Label Device_Name = new Label();
            Label Device_Status = new Label();

            Title.Dock = System.Windows.Forms.DockStyle.Fill;
            Title.Location = new System.Drawing.Point(0, 0);
            Title.Size = new System.Drawing.Size(285, 53);
            Title.TabIndex = 0;
            Title.Text = "Device";
            Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlDeviceHeader.BackColor = System.Drawing.SystemColors.ControlLight;
            pnlDeviceHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlDeviceHeader.Location = new System.Drawing.Point(0, 0);
            pnlDeviceHeader.Size = new System.Drawing.Size(285, 53);
            pnlDeviceHeader.TabIndex = 4;

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

            pnlDeviceCard.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlDeviceCard.Location = new System.Drawing.Point(3, 3);
            pnlDeviceCard.Size = new System.Drawing.Size(256, 100);
            pnlDeviceCard.TabIndex = 4;

            pnlDeviceHeader.Controls.Add(Title);
            pnlDeviceCard.Controls.Add(Device_Name);
            pnlDeviceCard.Controls.Add(Device_Status);
            pnlDeviceCard.Controls.Add(pnlDeviceHeader);
            flpDevices.Controls.Add(pnlDeviceCard);
        }
    }
}
