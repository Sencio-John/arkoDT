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
using System.Data;

namespace arkoDT
{
    public partial class frmUC : Form
    {
        public event Action UserCreated;
        private MySqlConnection connection;
        private string generatedID;
        private bool isFirstImage = true;
        private frmUsers frmUsers;
        private frmDashboard frmDashboard;
        private string generatedOTP;

        public frmUC(frmUsers frmUsersInstance, frmDashboard frmDashboardInstance)
        {
            InitializeComponent();

            btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;  // Optional: to stretch the image to fit the button

            frmUsers = frmUsersInstance;
            frmDashboard = frmDashboardInstance;

            string connectionString = "Server=127.0.0.1;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to the database. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        // Close form on Back button click
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Generate a random unique ID when the form loads
        public async void frmUC_Load(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            generatedID = await GenerateUniqueID();
        }

        // Generate the random ID and check for uniqueness
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

        // Generate a unique random ID
        private async Task<string> GenerateUniqueID()
        {
            string newID;
            bool exists;
            int retryCount = 0;
            const int maxRetries = 30;

            do
            {
                newID = GenerateRandomID(8); // Generate an 8-character random ID
                exists = await IsIDExists(newID); // Check if the ID already exists in Firebase
                retryCount++;
            } while (exists && retryCount < maxRetries);

            if (retryCount >= maxRetries)
            {
                MessageBox.Show("Failed to generate a unique ID after multiple attempts.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            return newID;
        }

        // Method to generate a random alphanumeric string
        private string GenerateRandomID(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Check if the generated ID exists in Firebase
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

        // Check if the username already exists in Firebase
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
                MessageBox.Show($"An error occurred while checking username existence: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        // Check if the email already exists in Firebase
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
                MessageBox.Show($"An error occurred while checking email existence: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        // Validate the email format
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtConfirmPass.Text != txtPassword.Text)
                {
                    MessageBox.Show("Password does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                // Basic validation for empty fields
                if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Username and Email cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Password cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                // Check if username exists
                bool usernameExists = await IsUsernameExists(txtUsername.Text);
                if (usernameExists)
                {
                    MessageBox.Show("Username already exists. Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                // Check if email exists
                bool emailExists = await IsEmailExists(txtEmail.Text);
                if (emailExists)
                {
                    MessageBox.Show("Email already exists. Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                // Validate email format
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Invalid email format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                // Encrypt the password
                string encryptedPassword = PasswordHelper.EncryptPassword(txtPassword.Text);

                UserRegistration register = new UserRegistration
                {
                    UserID = "ARKO-" + generatedID,
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

                MessageBox.Show("New User has been successfully inserted into the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);




                UserCreated?.Invoke();
                frmUsers.LoadUsers();
                frmDashboard.CountUsers();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private void button1_Click(object sender, EventArgs e)
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

        private async void btnGetOTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            bool emailExists = await IsEmailExists(txtEmail.Text);
            if (emailExists)
            {
                MessageBox.Show("Email already exists. Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            generatedOTP = GenerateRandomOTP();
            if (await SendEmailAsync(txtEmail.Text, "Your OTP Code", $"Your OTP code is {generatedOTP}"))
            {
                MessageBox.Show("OTP sent to your email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Failed to send OTP. Please check your email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                MessageBox.Show("Failed to send email: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }
    }
}
