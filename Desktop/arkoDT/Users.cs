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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        public void UpdateUsersCards()
        {
            Panel pnlCards = new Panel();
            Panel pnlHeader = new Panel();
            Label Title = new Label();
            Label lblUserName = new Label();
            Label lblRole = new Label();

            Title.Dock = System.Windows.Forms.DockStyle.Fill;
            Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Title.Location = new System.Drawing.Point(0, 0);
            Title.Size = new System.Drawing.Size(310, 41);
            Title.TabIndex = 0;
            Title.Text = "User";
            Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlHeader.AutoScroll = true;
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Size = new System.Drawing.Size(310, 41);
            pnlHeader.TabIndex = 1;

            lblRole.AutoSize = true;
            lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblRole.Location = new System.Drawing.Point(215, 90);
            lblRole.Size = new System.Drawing.Size(92, 31);
            lblRole.TabIndex = 3;
            lblRole.Text = "Role";

            lblUserName.AutoSize = true;
            lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblUserName.Location = new System.Drawing.Point(6, 90);
            lblUserName.Size = new System.Drawing.Size(177, 31);
            lblUserName.TabIndex = 2;
            lblUserName.Text = "Username";

            pnlCards.AutoScroll = true;
            pnlCards.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlCards.Location = new System.Drawing.Point(3, 3);
            pnlCards.Size = new System.Drawing.Size(310, 169);
            pnlCards.TabIndex = 0;

            pnlHeader.Controls.Add(Title);
            pnlCards.Controls.Add(lblRole);
            pnlCards.Controls.Add(lblUserName);
            pnlCards.Controls.Add(pnlHeader);
            flpUsers.Controls.Add(pnlCards);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmUC form1 = new frmUC(this);
            form1.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
