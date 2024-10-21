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
    public partial class frmForgot : Form
    {
        public frmForgot()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Define the correct code
            string correctCode = "1234"; // Replace with your desired code

            // Check if the entered code is correct
            if (txtOTP.Text == correctCode)
            {
                // Unhide the controls
                lblPassword.Visible = true;
                txtPassword.Visible = true;
                btnSubmit.Visible = true;
            }
            else
            {
                // Optionally inform the user the code is incorrect
                MessageBox.Show("Incorrect code. Please try again.", "Invalid Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
