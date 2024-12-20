using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKODesktop.Views
{
    public partial class UserCreation : Form
    {
        private string generatedOTP;
        private string connectionString = $@"Data Source={Application.StartupPath}\ArkoDB.db;Version=3;";
        public UserCreation()
        {
            InitializeComponent();
        }
        public void AddCardDevices()
        {

            Panel pnlUserCard = new Panel();
            Panel pnlHeader = new Panel();
            Panel pnlFooter = new Panel();
            Label lblName = new Label();
            Label lblUserRole = new Label();

            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(359, 32);
            pnlHeader.TabIndex = 0;
            if (lblUserRole.Text == "Admin")
            {
                pnlHeader.BackColor = System.Drawing.Color.FromArgb(128, 255, 255); // Admin color
            }
            else
            {
                pnlHeader.BackColor = System.Drawing.Color.FromArgb(192, 255, 192); // Default color
            }

            lblUserRole.Dock = System.Windows.Forms.DockStyle.Right;
            lblUserRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblUserRole.Location = new System.Drawing.Point(185, 32);
            lblUserRole.Size = new System.Drawing.Size(174, 134);
            lblUserRole.TabIndex = 3;
            lblUserRole.Text = "Role";
            lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            lblName.Dock = System.Windows.Forms.DockStyle.Left;
            lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblName.Location = new System.Drawing.Point(0, 32);
            lblName.Size = new System.Drawing.Size(179, 134);
            lblName.TabIndex = 2;
            lblName.Text = "Name";
            lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlFooter.Location = new System.Drawing.Point(0, 166);
            pnlFooter.Size = new System.Drawing.Size(359, 13);
            pnlFooter.TabIndex = 1;
            if (lblUserRole.Text == "Admin")
            {
                pnlFooter.BackColor = System.Drawing.Color.FromArgb(128, 255, 255); // Admin color
            }
            else
            {
                pnlFooter.BackColor = System.Drawing.Color.FromArgb(192, 255, 192); // Default color
            }

            pnlUserCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlUserCard.Location = new System.Drawing.Point(5, 5);
            pnlUserCard.Margin = new System.Windows.Forms.Padding(5);
            pnlUserCard.Size = new System.Drawing.Size(361, 181);
            pnlUserCard.TabIndex = 0;

            pnlUserCard.Controls.Add(lblUserRole);
            pnlUserCard.Controls.Add(lblName);
            pnlUserCard.Controls.Add(pnlFooter);
            pnlUserCard.Controls.Add(pnlHeader);
            flpUsers.Controls.Add(pnlUserCard);
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtConfirmPassword.Text != txtPassword.Text)
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
                string encryptedPassword = EncryptDecrypt.EncryptPassword(txtPassword.Text);


                //AddCardDevices();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool AddUser()
        {
            MessageBox.Show($"Database Path: {connectionString}", "Debug Path", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string firstName = "";
            string lastName = "";
            string middleName = "";
            string role = "";
            string username = "";
            string password = "";
            string email = "";
            string dateCreated = DateTime.Now.ToString("yyyy-MM-dd");
            string timeCreated = DateTime.Now.ToString("HH:mm:ss tt");


            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    string addUser = "INSERT INTO Users" +
                                        "([f_name], [l_name], [m_name], [role], [username], [password], [email], [time_created], [date_created]) VALUES" +
                                        "(@FirstName, @LastName, @MiddleName, @Role, @Username, @Password, @Email, @TimeCreated, @DateCreated)";

                    using (SQLiteCommand cmd = new SQLiteCommand(addUser, con))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@MiddleName", middleName);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@TimeCreated", timeCreated);
                        cmd.Parameters.AddWithValue("@TimeCreated", dateCreated);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task<bool> IsUsernameExists(string username)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    await con.OpenAsync(); // Open the connection asynchronously
                    string query = "SELECT COUNT(*) FROM Users WHERE username = @username";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        // ExecuteScalarAsync retrieves the first column of the first row in the result set
                        object result = await cmd.ExecuteScalarAsync();

                        return Convert.ToInt32(result) > 0; // If count > 0, username exists
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking username existence: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task<bool> IsEmailExists(string username)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    await con.OpenAsync(); // Open the connection asynchronously
                    string query = "SELECT COUNT(*) FROM Users WHERE email = @email";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@email", username);

                        // ExecuteScalarAsync retrieves the first column of the first row in the result set
                        object result = await cmd.ExecuteScalarAsync();

                        return Convert.ToInt32(result) > 0; // If count > 0, username exists
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking username existence: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool IsValidEmail(string email)
        {

            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
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

        private async void btnOTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(""))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            bool emailExists = await IsEmailExists("");
            if (emailExists)
            {
                MessageBox.Show("Email already exists. Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            generatedOTP = GenerateRandomOTP();
            if (await SendEmailAsync("", "Your OTP Code", $"Your OTP code is {generatedOTP}"))
            {
                MessageBox.Show("OTP sent to your email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Failed to send OTP. Please check your email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtOTP.Text.Trim() == generatedOTP)
            {
                MessageBox.Show("OTP verified successfully!");
                btnAddUser.Enabled = true; // Enable the 'Create' button once OTP is verified
            }
            else
            {
                MessageBox.Show("Incorrect OTP. Please try again.");
            }
        }
    }
}
