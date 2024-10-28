using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace arkoDT
{
    public partial class frmLogin : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "9LIEv3oM8IkGzdbhvL6949CXXFAD86pu2v2ISD1r",
            BasePath = "https://arko-uno-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        IFirebaseClient client;


        private bool isFirstImage = true;
        public frmLogin()
        {
            InitializeComponent();
            btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;  // Optional: to stretch the image to fit the button

            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Failed to connect to Firebase.");
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            frmForgot form1 = new frmForgot();
            form1.Show();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username and password cannot be empty.");
                return;
            }

            bool isLoginSuccessful = await LoginUser(username, password);

            if (isLoginSuccessful)
            {
                this.Hide();
                new frmDashboard().Show();
                MessageBox.Show("Login successful!");
                // Proceed to the next form or action
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
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

        //Method for dehashing

        private async Task<bool> LoginUser(string username, string password)
        {
            try
            {
                // Fetch all users from Firebase
                FirebaseResponse response = await client.GetAsync("Users/");
                var users = response.ResultAs<Dictionary<string, UserRegistration>>();

                if (users == null)
                {
                    MessageBox.Show("No users found.");
                    return false;
                }

                // Find the user by username
                foreach (var user in users.Values)
                {
                    if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        // If the username matches, verify the password
                        bool isPasswordValid = PasswordHelper.VerifyPassword(password, user.Password);
                        return isPasswordValid; // Returns true or false based on password validation
                    }
                }

                // If we finish the loop and find no match
                MessageBox.Show("Username does not exist.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
        }

    }
}
