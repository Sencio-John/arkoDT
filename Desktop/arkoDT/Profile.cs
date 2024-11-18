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
        public frmProfile(string firstName, string lastName, string email, string role)
        {
            InitializeComponent();

            // Populate the fields
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
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
            MakeRoundedPanel(pnlProfile, 50);
            MakeRoundedPanel(pnlContacts, 50);
            MakeRoundedPanel(pnlLoginCred, 50);
            MakeCircularPictureBox(pbProfile);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditDetails_Click(object sender, EventArgs e)
        {
            if (btnEditDetails.Text == "Edit Details")
            {
                // Enable text fields
                txtFirstName.Enabled = true;
                txtLastName.Enabled = true;
                txtBirthDate.Enabled = true;
                txtAddress.Enabled = true;
                txtContactNum.Enabled = true;
                btnSave.Enabled = true;

                // Change button text to "Undo"
                btnEditDetails.Text = "Undo";
            }
            else
            {
                // Disable text fields
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                txtBirthDate.Enabled = false;
                txtAddress.Enabled = false;
                txtContactNum.Enabled = false;
                btnSave.Enabled = false;

                // Change button text back to "Edit Details"
                btnEditDetails.Text = "Edit Details";
            }
        }

        private void btnLoginCred_Click(object sender, EventArgs e)
        {
            if (btnLoginCred.Text == "Edit Credentials")
            {
                // Enable text fields
                txtUsername.Enabled = true;
                txtEmail.Enabled = true;
                txtOTP.Enabled = true;
                txtPassword.Enabled = true;
                txtConfirmPass.Enabled = true;
                btnSaveCred.Enabled = true;

                // Change button text to "Undo"
                btnLoginCred.Text = "Undo";
            }
            else
            {
                // Disable text fields
                txtUsername.Enabled = false;
                txtEmail.Enabled = false;
                txtOTP.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirmPass.Enabled = false;
                btnSaveCred.Enabled = false;

                // Change button text back to "Edit Credentials"
                btnLoginCred.Text = "Edit Credentials";
            }
        }
    }
}
