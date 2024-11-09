using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arkoDT
{
    public partial class frmDevices : Form
    {
        public frmDevices()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAdd form1 = new frmAdd(this);
            form1.Show();
        }

        public void UpdateDeviceCards()
        {
            Panel pnlCards = new Panel();
            Panel pnlHeader = new Panel();
            Label Title = new Label();
            Label lblDeviceName = new Label();
            Label lblStatus = new Label();
            Button btnRemove = new Button();
            Button btnChangeName = new Button();

            Title.Dock = System.Windows.Forms.DockStyle.Fill;
            Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Title.Location = new System.Drawing.Point(0, 0);
            Title.Size = new System.Drawing.Size(310, 41);
            Title.TabIndex = 0;
            Title.Text = "Devices";
            Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlHeader.AutoScroll = true;
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Size = new System.Drawing.Size(310, 41);
            pnlHeader.TabIndex = 1;

            btnRemove.Location = new System.Drawing.Point(3, 143);
            btnRemove.Size = new System.Drawing.Size(75, 23);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;

            btnChangeName.Location = new System.Drawing.Point(221, 143);
            btnChangeName.Size = new System.Drawing.Size(86, 23);
            btnChangeName.TabIndex = 3;
            btnChangeName.Text = "Change Name";
            btnChangeName.UseVisualStyleBackColor = true;

            lblStatus.AutoSize = true;
            lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblStatus.Location = new System.Drawing.Point(215, 90);
            lblStatus.Size = new System.Drawing.Size(92, 31);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Status";

            lblDeviceName.AutoSize = true;
            lblDeviceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblDeviceName.Location = new System.Drawing.Point(6, 90);
            lblDeviceName.Size = new System.Drawing.Size(177, 31);
            lblDeviceName.TabIndex = 2;
            lblDeviceName.Text = "Device Name";

            pnlCards.AutoScroll = true;
            pnlCards.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlCards.Location = new System.Drawing.Point(3, 3);
            pnlCards.Size = new System.Drawing.Size(310, 169);
            pnlCards.TabIndex = 0;

            pnlHeader.Controls.Add(Title);
            pnlCards.Controls.Add(btnRemove);
            pnlCards.Controls.Add(btnChangeName);
            pnlCards.Controls.Add(lblStatus);
            pnlCards.Controls.Add(lblDeviceName);
            pnlCards.Controls.Add(pnlHeader);
            flpDevices.Controls.Add(pnlCards);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
