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
    public partial class frmLogin : Form
    {
        private bool isFirstImage = true;
        public frmLogin()
        {
            InitializeComponent();
            btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;  // Optional: to stretch the image to fit the button
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            frmForgot form1 = new frmForgot();
            form1.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Define the correct username and password
            string correctUsername = "admin";  // Change this to your desired username
            string correctPassword = "password"; // Change this to your desired password

            // Check if the entered username and password are correct
            if (txtUsername.Text == correctUsername && txtPassword.Text == correctPassword)
            {
                // Create an instance of frmControl
                frmDashboard frmDashboard = new frmDashboard();

                // Show frmControl
                frmDashboard.Show();

                // Close frmLogin
                this.Hide();
            }
            else
            {
                // Show a message box if credentials are incorrect
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (isFirstImage)
            {
                txtPassword.PasswordChar = '\0';
                // Change to the second image
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/view.png");
            }
            else
            {
                txtPassword.PasswordChar = '●';
                // Revert to the first image
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            }

            // Toggle the flag
            isFirstImage = !isFirstImage;


        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            lblForgotPassword.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable
        }
    }
}
