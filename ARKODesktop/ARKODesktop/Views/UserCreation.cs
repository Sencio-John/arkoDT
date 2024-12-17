using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKODesktop.Views
{
    public partial class UserCreation : Form
    {
        public UserCreation()
        {
            InitializeComponent();
        }
        public void AddCardDevices()
        {

            Panel pnlUserCard = new Panel();
            Panel pnlHeader = new Panel();
            Panel pnlFooter = new Panel();
            Label lblName = new Label();
            Label lblUserRole = new Label();

            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(359, 32);
            pnlHeader.TabIndex = 0;
            if (lblUserRole.Text == "Admin")
            {
                pnlHeader.BackColor = System.Drawing.Color.FromArgb(128, 255, 255); // Admin color
            }
            else
            {
                pnlHeader.BackColor = System.Drawing.Color.FromArgb(192, 255, 192); // Default color
            }

            lblUserRole.Dock = System.Windows.Forms.DockStyle.Right;
            lblUserRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblUserRole.Location = new System.Drawing.Point(185, 32);
            lblUserRole.Size = new System.Drawing.Size(174, 134);
            lblUserRole.TabIndex = 3;
            lblUserRole.Text = "Role";
            lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            lblName.Dock = System.Windows.Forms.DockStyle.Left;
            lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblName.Location = new System.Drawing.Point(0, 32);
            lblName.Size = new System.Drawing.Size(179, 134);
            lblName.TabIndex = 2;
            lblName.Text = "Name";
            lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlFooter.Location = new System.Drawing.Point(0, 166);
            pnlFooter.Size = new System.Drawing.Size(359, 13);
            pnlFooter.TabIndex = 1;
            if (lblUserRole.Text == "Admin")
            {
                pnlFooter.BackColor = System.Drawing.Color.FromArgb(128, 255, 255); // Admin color
            }
            else
            {
                pnlFooter.BackColor = System.Drawing.Color.FromArgb(192, 255, 192); // Default color
            }

            pnlUserCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlUserCard.Location = new System.Drawing.Point(5, 5);
            pnlUserCard.Margin = new System.Windows.Forms.Padding(5);
            pnlUserCard.Size = new System.Drawing.Size(361, 181);
            pnlUserCard.TabIndex = 0;

            pnlUserCard.Controls.Add(lblUserRole);
            pnlUserCard.Controls.Add(lblName);
            pnlUserCard.Controls.Add(pnlFooter);
            pnlUserCard.Controls.Add(pnlHeader);
            flpUsers.Controls.Add(pnlUserCard);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddCardDevices();
        }
    }
}
