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
        public string Username { get; set; }
        public string Role { get; set; }
        private IFirebaseClient client;
        private bool isFirstImage = true;

        public frmLogin()
        {
            InitializeComponent();
            this.FormClosing += frmLogin_FormClosing;
            btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;

            Firebase_Config firebaseConfig = new Firebase_Config();
            client = firebaseConfig.GetClient();

            if (client == null)
            {
                MessageBox.Show("Failed to connect to Database.");
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

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.");
                return;
            }

            var (Name, UserType) = await LoginUser(username, password);

            if (!string.IsNullOrEmpty(Name))
            {
                Username = Name;
                Role = UserType;
                this.Hide();
                new frmDashboard(this).Show();
                MessageBox.Show("Login successful!");
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
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/view.png");
            }
            else
            {
                txtPassword.PasswordChar = '●';
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            }

            isFirstImage = !isFirstImage;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            lblForgotPassword.Cursor = Cursors.Hand;
        }

        private async Task<(string username, string role)> LoginUser(string username, string password)
        {
            try
            {
                // Fetch all users from Firebase
                FirebaseResponse response = await client.GetAsync("Users/");
                var users = response.ResultAs<Dictionary<string, UserRegistration>>();

                if (users == null)
                {
                    MessageBox.Show("No users found.");
                    return (null, null);
                }

                // Find the user by username
                foreach (var user in users.Values)
                {
                    if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        // Decrypt the stored password
                        string decryptedPassword = PasswordHelper.DecryptPassword(user.Password);

                        // Verify the password
                        if (password == decryptedPassword)
                        {
                            return (user.Name, user.Role); // Returns the user's name if password is valid
                        }
                        else
                        {
                            MessageBox.Show("Invalid password.");
                            return (null, null);
                        }
                    }
                }

                // If we finish the loop and find no match
                MessageBox.Show("Username does not exist.");
                return (null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return (null, null);
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
