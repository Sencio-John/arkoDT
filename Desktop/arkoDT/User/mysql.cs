using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using BCrypt.Net;
using System.Net;
using System.Net.Mail;
using arkoDT.Classes;
using MySql.Data.MySqlClient;

namespace arkoDT.User
{
    public partial class frmSQL : Form
    {
        public event Action UserCreated;
        private MySqlConnection connection;
        private string generatedID;
        private bool isFirstImage = true;
        private frmUsers frmUsers;
        private frmDashboard frmDashboard;
        private string generatedOTP;

        public frmSQL()
        {
            InitializeComponent();
            btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;  // Optional: to stretch the image to fit the button

            string connectionString = "Server=localhost;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to the database. " + ex.Message);
            }
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (isFirstImage)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirmPass.PasswordChar = '\0';
                // Change to the second image
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/view.png");
            }
            else
            {
                txtPassword.PasswordChar = '●';
                txtConfirmPass.PasswordChar = '●';
                // Revert to the first image
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            }

            // Toggle the flag
            isFirstImage = !isFirstImage;
        }

        public async Task GenerateRandomIDOnLoad()
        {
            generatedID = await GenerateUniqueID();
            if (generatedID != null)
            {
                MessageBox.Show("Generated Unique ID: " + generatedID);
            }
            else
            {
                MessageBox.Show("Could not generate a unique ID. Please try again.");
            }
        }

        private async Task<string> GenerateUniqueID()
        {
            string newID;
            bool exists;
            int retryCount = 0;
            const int maxRetries = 30;

            do
            {
                newID = GenerateRandomID(8); // Generate an 8-character random ID
                exists = await IsIDExists(newID); // Check if the ID already exists in the MySQL database
                retryCount++;
            } while (exists && retryCount < maxRetries);

            if (retryCount >= maxRetries)
            {
                MessageBox.Show("Failed to generate a unique ID after multiple attempts.");
                return null;
            }

            return newID;
        }

        private string GenerateRandomID(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> IsIDExists(string id)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM users WHERE user_id = @user_id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user_id", id);

                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result) > 0; // If count > 0, ID exists
            }
            catch (Exception)
            {
                return false; // Assume ID does not exist in case of error
            }
        }

        private async Task<bool> IsUsernameExists(string username)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM users WHERE username = @username";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);

                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result) > 0; // If count > 0, username exists
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking username existence: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> IsEmailExists(string email)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM users WHERE email = @email";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@email", email);

                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result) > 0; // If count > 0, email exists
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking email existence: {ex.Message}");
                return false;
            }
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private async void btnGetOTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            generatedOTP = GenerateRandomOTP();
            if (await SendEmailAsync(txtEmail.Text, "Your OTP Code", $"Your OTP code is {generatedOTP}"))
            {
                MessageBox.Show("OTP sent to your email.");
            }
            else
            {
                MessageBox.Show("Failed to send OTP. Please check your email address.");
            }
        }

        private string GenerateRandomOTP()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString(); // 6-digit OTP
        }

        private async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("arkoVessel@gmail.com", "jtcz lyxq gwjt qcuo"),
                    EnableSsl = true
                };
                MailMessage mail = new MailMessage("youremail@gmail.com", toEmail, subject, body);
                await smtp.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send email: " + ex.Message);
                return false;
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtOTP.Text.Trim() == generatedOTP)
            {
                MessageBox.Show("OTP verified successfully!");
                btnCreate.Enabled = true; // Enable the 'Create' button once OTP is verified
            }
            else
            {
                MessageBox.Show("Incorrect OTP. Please try again.");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtConfirmPass.Text != txtPassword.Text)
                {
                    MessageBox.Show("Password does not match");
                    return;
                }

                // Basic validation for empty fields
                if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Username and Email cannot be empty.");
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Password cannot be empty");
                    return;
                }

                // Check if username exists
                bool usernameExists = await IsUsernameExists(txtUsername.Text);
                if (usernameExists)
                {
                    MessageBox.Show("Username already exists. Please choose another.");
                    return;
                }

                // Check if email exists
                bool emailExists = await IsEmailExists(txtEmail.Text);
                if (emailExists)
                {
                    MessageBox.Show("Email already exists. Please choose another.");
                    return;
                }

                // Validate email format
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Invalid email format.");
                    return;
                }

                // Encrypt the password
                string encryptedPassword = PasswordHelper.EncryptPassword(txtPassword.Text);

                UserRegistration register = new UserRegistration
                {
                    UserID = generatedID,
                    Username = txtUsername.Text,
                    Password = encryptedPassword,  // Use the encrypted password
                    Email = txtEmail.Text,
                    Role = cbRole.Text,
                    Status = "Active",
                    First_Name = txtFirstName.Text,
                    Last_Name = txtLastName.Text,
                };
                // Insert into `users` table
                string userQuery = "INSERT INTO users (user_ID, username, password, email, status, role) " +
                                   "VALUES (@user_ID, @username, @password, @email, @status, @role)";

                MySqlCommand userCmd = new MySqlCommand(userQuery, connection);
                userCmd.Parameters.AddWithValue("@user_ID", register.UserID);
                userCmd.Parameters.AddWithValue("@username", register.Username);
                userCmd.Parameters.AddWithValue("@password", register.Password);
                userCmd.Parameters.AddWithValue("@email", register.Email);
                userCmd.Parameters.AddWithValue("@status", "Active");
                userCmd.Parameters.AddWithValue("@role", register.Role);

                userCmd.ExecuteNonQuery(); // Execute the insert query for users

                // Insert into `users_info` table
                string userInfoQuery = "INSERT INTO users_info (first_Name, last_Name, user_ID) " +
                                       "VALUES (@first_Name, @last_Name, @user_ID)";

                MySqlCommand userInfoCmd = new MySqlCommand(userInfoQuery, connection);
                userInfoCmd.Parameters.AddWithValue("@first_Name", register.First_Name);
                userInfoCmd.Parameters.AddWithValue("@last_Name", register.Last_Name);
                userInfoCmd.Parameters.AddWithValue("@user_ID", register.UserID);

                userInfoCmd.ExecuteNonQuery(); // Execute the insert query for user info

                MessageBox.Show("New User has been successfully inserted into the database.");
                connection.Close();
                this.Close();
                //UserCreated?.Invoke();
                //frmUsers.LoadUsers();
                //frmDashboard.CountUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private async void frmSQL_Load(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            generatedID = await GenerateUniqueID();
        }
    }
}
