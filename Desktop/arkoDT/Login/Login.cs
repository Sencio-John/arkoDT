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
using arkoDT.Classes;

namespace arkoDT
{
    public partial class frmLogin : Form
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string UserID { get; set; }
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
                //MessageBox.Show("Failed to connect to Database.");
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

            // Call LoginUser and destructure its result
            var (userId, first_name, last_name, UserType) = await LoginUser(username, password);

            if (!string.IsNullOrEmpty(userId))
            {
                Username = first_name + " " + last_name;
                Role = UserType;
                UserID = userId;
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

        private async Task<(string userId, string firstName, string Lastname, string role)> LoginUser(string username, string password)
        {
            try
            {
                // Fetch all users from Firebase
                FirebaseResponse response = await client.GetAsync("Users/");
                var users = response.ResultAs<Dictionary<string, UserRegistration>>();

                if (users == null)
                {
                    MessageBox.Show("No users found.");
                    return (null, null, null, null);
                }

                // Find the user by username
                foreach (var user in users)
                {
                    string userId = user.Key; // This is the ID of the user
                    UserRegistration userData = user.Value;

                    if (userData.Username.Equals(username, StringComparison.Ordinal))
                    {
                        // Decrypt the stored password
                        string decryptedPassword = PasswordHelper.DecryptPassword(userData.Password);

                        // Verify the password
                        if (password == decryptedPassword)
                        {
                            return (userId, userData.First_Name, userData.Last_Name, userData.Role); // Return user ID, name, and role
                        }
                        else
                        {
                            MessageBox.Show("Invalid password.");
                            return (null, null, null, null);
                        }
                    }
                }

                MessageBox.Show("Username does not exist.");
                return (null, null, null, null);
            }
            catch (Exception)
            {
                //MessageBox.Show("Login Failed");
                return (null, null, null, null);
            }
        }


        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
