using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BCrypt.Net;
using MySql.Data.MySqlClient;

namespace arkoDT
{
    public partial class frmForgot : Form
    {
        private MySqlConnection connection;
        private string generatedOTP; // Stores the generated OTP for validation
        private string enteredEmail; // Stores the email entered by the user
        private bool isFirstImage = true;

        public frmForgot()
        {
            InitializeComponent();
            txtPassword.Visible = false; // Show password field
            txtConfirm.Visible = false; // Show confirm password field
            btnChange.Visible = false; // Show change password button
            lblPassword.Visible = false;
            lblConfirmPass.Visible = false;
            btnShowPass.Visible = false;

            // Set up MySQL connection
            string connectionString = "Server=localhost;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);

            btnShowPass.BackgroundImage = Image.FromFile(Application.StartupPath + @"\..\..\Resources\hide.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;

            if (connection == null)
            {
                MessageBox.Show("Failed to connect to Database.");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Not required in the current context; can be removed or repurposed.
        }

        // Email validation using regex
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Generate a random OTP
        private string GenerateRandomOTP(int length)
        {
            const string chars = "0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Send OTP email
        private async Task<bool> SendEmailOTP(string email, string otp)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("arkoVessel@gmail.com", "jtcz lyxq gwjt qcuo"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("arkoVessel@gmail.com"),
                    Subject = "Your OTP Code",
                    Body = $"Your OTP for password reset is: {otp}",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Handle Get OTP Button Click
        private async void btnSendOTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter an email address.");
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            enteredEmail = txtEmail.Text.Trim(); // Store the entered email

            // Check if the entered email exists in the database
            bool emailExists = await CheckIfEmailExists(enteredEmail);

            if (!emailExists)
            {
                MessageBox.Show("The entered email does not exist in the system.");
                return;
            }

            generatedOTP = GenerateRandomOTP(6); // Generate a 6-digit OTP

            bool emailSent = await SendEmailOTP(enteredEmail, generatedOTP);

            if (emailSent)
            {
                MessageBox.Show("OTP has been sent to the specified email address. Please check your inbox.");
                btnVerify.Enabled = true;

            }
            else
            {
                MessageBox.Show("Failed to send OTP. Please check the email address and try again.");
            }
        }

        // Handle Verify OTP Button Click
        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOTP.Text))
            {
                MessageBox.Show("Please enter the OTP sent to your email.");
                return;
            }

            if (txtOTP.Text == generatedOTP)
            {
                MessageBox.Show("OTP verified successfully.");
                txtPassword.Visible = true; // Show password field
                txtConfirm.Visible = true; // Show confirm password field
                btnChange.Visible = true; // Show change password button
                lblPassword.Visible = true;
                lblConfirmPass.Visible = true;
                btnShowPass.Visible = true;
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.");
            }
        }

        // Handle Change Password Button Click
        private async void btnChange_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtConfirm.Text))
            {
                MessageBox.Show("Password fields cannot be empty.");
                return;
            }

            if (txtPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return;
            }

            string newPassword = txtPassword.Text;

            try
            {
                // Fetch user from MySQL using the email as key
                string query = "SELECT * FROM users WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", enteredEmail);

                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    MessageBox.Show("No user found with the specified email address.");
                    connection.Close();
                    return;
                }

                reader.Read();
                string userId = reader["user_ID"].ToString(); // Assuming UserID is the primary key
                connection.Close();

                // Update the user's password
                string updateQuery = "UPDATE users SET password = @Password WHERE user_ID = @UserID";
                MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                updateCmd.Parameters.AddWithValue("@Password", PasswordHelper.EncryptPassword(newPassword)); // Encrypt the password
                updateCmd.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                int rowsAffected = updateCmd.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password has been changed successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to change the password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async Task<bool> CheckIfEmailExists(string email)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();

                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking email: {ex.Message}");
                return false;  // Consider the email as not found in case of an error
            }
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (isFirstImage)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirm.PasswordChar = '\0';
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/view.png");
            }
            else
            {
                txtPassword.PasswordChar = '●';
                txtConfirm.PasswordChar = '●';
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            }

            isFirstImage = !isFirstImage;
        }
    }
}
