using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arkoDT
{
    public partial class frmProfile : Form
    {
        public frmProfile()
        {
            InitializeComponent();
        }

        private void MakeCircularPictureBox(PictureBox pictureBox)
        {

            // Create a graphics path to define a circular region
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);

            // Apply the circular region to the PictureBox
            pictureBox.Region = new Region(path);
        }

        private void MakeRoundedPanel(Panel panel, int borderRadius)
        {
            Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);
            GraphicsPath path = new GraphicsPath();

            int radius = borderRadius;
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);                        // Top-left
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);           // Top-right
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90); // Bottom-right
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);           // Bottom-left
            path.CloseFigure();

            panel.Region = new Region(path);
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            MakeRoundedPanel(pnlProfile, 100);
            MakeRoundedPanel(pnlContacts, 100);
            MakeRoundedPanel(panel1, 100);
            MakeCircularPictureBox(pbProfile);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditDetails_Click(object sender, EventArgs e)
        {
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtEmail.Enabled = true;
            txtOTP.Enabled = true;
            txtGender.Enabled = true;
            cmbRole.Enabled = true;
        }
    }
}
