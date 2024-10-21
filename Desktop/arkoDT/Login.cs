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
        public frmLogin()
        {
            InitializeComponent();
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

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            lblForgotPassword.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable
            frmForgot form1 = new frmForgot();
            form1.Show();
        }
    }
}
