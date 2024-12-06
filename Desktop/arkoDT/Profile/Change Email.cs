using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace arkoDT.Profile
{
    public partial class frmChangeEmail : Form
    {
        private frmProfile profile;
        public string userID;
        private MySqlConnection connection;
        private string generatedOTP;
        public frmChangeEmail(frmProfile frmProfileInstance)
        {
            InitializeComponent();
            profile = frmProfileInstance;
            userID = profile.userID;
            string connectionString = "Server=localhost;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);

            if(connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            else
            {
                connection.Open();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlTransaction transaction = null;

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open(); // Open connection if not already open
                }
                transaction = connection.BeginTransaction();

                // Prepare the update query
                string updateQuery = "UPDATE users SET email = @Email WHERE user_ID = @UserID";
                MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                updateCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                updateCmd.Parameters.AddWithValue("@UserID", userID);

                // Execute the query within the transaction
                updateCmd.Transaction = transaction;

                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Commit the transaction if the query is successful
                    transaction.Commit();
                    MessageBox.Show("Email has been changed successfully.");
                    this.Close();
                }
                else
                {
                    // Rollback if no rows were affected
                    transaction.Rollback();
                    MessageBox.Show("Failed to change the Email.");
                }
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of any error
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Ensure that the connection is closed regardless of the outcome
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private async void btnSendCode_Click(object sender, EventArgs e)
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

            // Check if the entered email exists in the database
            bool emailExists = await CheckIfEmailExists(txtEmail.Text.Trim());

            if (emailExists)
            {
                MessageBox.Show("The entered email is an existing email in the system.");
                return;
            }

            generatedOTP = GenerateRandomOTP(6); // Generate a 6-digit OTP

            bool emailSent = await SendEmailOTP(txtEmail.Text.Trim(), generatedOTP);

            if (emailSent)
            {
                MessageBox.Show("OTP has been sent to the specified email address. Please check your inbox.");
                button2.Enabled = true;

            }
            else
            {
                MessageBox.Show("Failed to send OTP. Please check the email address and try again.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOTP.Text))
            {
                MessageBox.Show("Please enter the OTP sent to your email.");
                return;
            }

            if (txtOTP.Text == generatedOTP)
            {
                MessageBox.Show("OTP verified successfully.");
                btnSave.Enabled = true;
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.");
            }
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private string GenerateRandomOTP(int length)
        {
            const string chars = "0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

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

        private async Task<bool> CheckIfEmailExists(string email)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open(); // Open connection if not already open
                }

                // Check if the email exists in the database
                string query = "SELECT COUNT(*) FROM users WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);

                int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                connection.Close();

                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking email: {ex.Message}");
                return false;  // Consider the email as not found in case of an error
            }
        }
    }
}
