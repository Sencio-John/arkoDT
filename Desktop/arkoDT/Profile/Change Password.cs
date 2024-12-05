using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arkoDT.Profile
{
    public partial class frmChangePassword : Form
    {
        private bool isFirstImage = true;
        public frmChangePassword()
        {
            InitializeComponent();
            btnShowPass.BackgroundImage = Image.FromFile(Application.StartupPath + @"\..\..\Resources\hide.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (isFirstImage)
            {
                txtCurrentPassword.PasswordChar = '\0';
                txtNewPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/view.png");
            }
            else
            {
                txtCurrentPassword.PasswordChar = '●';
                txtNewPassword.PasswordChar = '●';
                txtConfirmPassword.PasswordChar = '●';
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            }

            isFirstImage = !isFirstImage;
        }
    }
}

