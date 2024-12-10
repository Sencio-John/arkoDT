using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using arkoDT.Classes;
using System.Text.Json;
using MySql.Data.MySqlClient;

namespace arkoDT
{
    public partial class frmLogin : Form
    {
        private const string LogFilePath = @"C:\Users\SENCIO\Documents\app_log.txt";
        public string UserID { get; set; }
        public new string Name { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }  // Added property to store the username

        private const string LockoutFilePath = "lockout.json"; // File to store lockout info
        private const int MaxLoginAttempts = 5;               // Maximum allowed attempts
        private const int LockoutDurationMinutes = 3;         // Lockout duration in minutes
        private Timer lockoutTimer;
        private bool isFirstImage = true;
        string connectionString = "Server=127.0.0.1;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
        public frmLogin()
        {
            InitializeComponent();
            this.FormClosing += frmLogin_FormClosing;
            string imagePath = Path.Combine(Application.StartupPath, @"Resources\hide.png");

            if (File.Exists(imagePath))
            {
                btnShowPass.BackgroundImage = Image.FromFile(imagePath);
            }
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void LogMessage(string message)
        {
            try
            {
                string logMessage = $"{DateTime.Now}: {message}{Environment.NewLine}";
                File.AppendAllText(LogFilePath, logMessage);
            }
            catch (Exception ex)
            {
                // If logging fails, show an error message but continue execution
                MessageBox.Show($"Error writing to log file: {ex.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class LockoutInfo
        {
            public int FailedAttempts { get; set; } = 0;
            public DateTime? LockoutUntil { get; set; } = null;
        }

        private LockoutInfo LoadLockoutInfo()
        {
            if (File.Exists(LockoutFilePath))
            {
                string json = File.ReadAllText(LockoutFilePath);
                return JsonSerializer.Deserialize<LockoutInfo>(json);
            }

            return new LockoutInfo();
        }

        private void SaveLockoutInfo(LockoutInfo info)
        {
            string json = JsonSerializer.Serialize(info);
            File.WriteAllText(LockoutFilePath, json);
        }

        private void ClearLockoutInfo()
        {
            if (File.Exists(LockoutFilePath))
            {
                File.Delete(LockoutFilePath);
            }
        }

        private bool IsLockedOut()
        {
            LockoutInfo info = LoadLockoutInfo();

            if (info.LockoutUntil.HasValue && info.LockoutUntil.Value > DateTime.Now)
            {
                MessageBox.Show($"Account is locked. Try again after {info.LockoutUntil.Value:hh:mm tt}.", "Account Locked");
                LockUI(info.LockoutUntil.Value);
                return true;
            }
            else if (info.LockoutUntil.HasValue && info.LockoutUntil.Value <= DateTime.Now)
            {
                ResetFailedAttempts();
                UnlockUI();
            }

            return false;
        }

        private void IncrementFailedAttempts()
        {
            LockoutInfo info = LoadLockoutInfo();
            info.FailedAttempts++;

            if (info.FailedAttempts >= MaxLoginAttempts)
            {
                info.LockoutUntil = DateTime.Now.AddMinutes(LockoutDurationMinutes);
                LockUI(info.LockoutUntil.Value);
            }

            SaveLockoutInfo(info);
        }

        private void ResetFailedAttempts()
        {
            ClearLockoutInfo();
        }

        private void LockUI(DateTime lockoutEndTime)
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Enabled = false;
            lblForgotPassword.Enabled = false;
            MessageBox.Show($"Device is locked out. Try again at {lockoutEndTime:hh:mm tt}.", "Device Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void UnlockUI()
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            btnLogin.Enabled = true;
            lblForgotPassword.Enabled = true;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (IsLockedOut())
            {
                return;
            }

            var userInfo = await Task.Run(() => AuthenticateUser(username, password));

            if (userInfo != null)
            {
                if (userInfo.Status == "Inactive")
                {
                    MessageBox.Show("Account is inactive. Please contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                ResetFailedAttempts();
                UnlockUI();
                UserID = userInfo.UserID;
                Role = userInfo.Role;

                // Save the username in the Username property
                Username = userInfo.Username;  // Save the username here

                // Fetch the first and last name after login
                var name = await Task.Run(() => GetUserFullName(userInfo.UserID));
                Name = name;

                this.Hide();
                new frmDashboard(this).Show();
            }
            else
            {
                IncrementFailedAttempts();
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private UserRegistration AuthenticateUser(string username, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    user_ID, username, password, role, status
                FROM 
                    users
                WHERE 
                    BINARY username = @username";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Fetch the data from the reader
                                string dbPassword = reader.GetString("password");
                                string userId = reader.GetString("user_ID");
                                string role = reader.GetString("role");
                                string status = reader.GetString("status");

                                // Decrypt the password stored in the database
                                string decryptedPassword = PasswordHelper.DecryptPassword(dbPassword);

                                // Check if the decrypted password matches the entered password
                                if (password == decryptedPassword)
                                {
                                    return new UserRegistration
                                    {
                                        UserID = userId,
                                        Username = username,  // Save username in the returned user object
                                        Role = role,
                                        Status = status
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return null;
        }

        private string GetUserFullName(string userId)
        {
            MySqlConnection connection = null;

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = @"
                    SELECT 
                        first_Name, last_Name
                    FROM 
                        users_info
                    WHERE 
                        user_ID = @userId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader.GetString("first_Name");
                            string lastName = reader.GetString("last_Name");
                            return $"{firstName} {lastName}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to fetch user info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                // Ensure the connection is always closed
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return null;
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lockoutTimer != null)
            {
                lockoutTimer.Stop();
            }
            Application.Exit();
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
            try
            {
                LogMessage("frmLogin_Load started.");

                // Simulate checking lockout or other initial operations
                LockoutInfo info = LoadLockoutInfo();
                if (info.LockoutUntil.HasValue && info.LockoutUntil.Value > DateTime.Now)
                {
                    // Lock UI initially if the user is locked out
                    LockUI(info.LockoutUntil.Value);
                    LogMessage($"User locked out until {info.LockoutUntil.Value}");
                }
                else
                {
                    // If lockout expired, reset failed attempts and unlock UI
                    ResetFailedAttempts();
                    UnlockUI();
                }

                LogMessage("frmLogin_Load completed.");
            }
            catch (Exception ex)
            {
                LogMessage($"Error in frmLogin_Load: {ex.Message}");
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            frmForgot form1 = new frmForgot();
            form1.Show();
        }

        private void LockoutTimer_Tick(object sender, EventArgs e)
        {
            LockoutInfo info = LoadLockoutInfo();

            // If the lockout duration is over, unlock the UI
            if (info.LockoutUntil.HasValue && info.LockoutUntil.Value <= DateTime.Now)
            {
                // Unlock the UI
                UnlockUI();
                // Stop the timer as it's no longer needed
                lockoutTimer.Stop();
                // Clear lockout info as it's expired
                ResetFailedAttempts();
            }
        }
    }
}
