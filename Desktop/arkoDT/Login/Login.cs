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
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using arkoDT.Classes;
using System.Text.Json;

namespace arkoDT
{
    public partial class frmLogin : Form
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string UserID { get; set; }
        private IFirebaseClient client;
        private bool isFirstImage = true;

        private const string LockoutFilePath = "lockout.json"; // File to store lockout info
        private const int MaxLoginAttempts = 5;               // Maximum allowed attempts
        private const int LockoutDurationMinutes = 3;         // Lockout duration in minutes
        private Timer lockoutTimer;

        public frmLogin()
        {
            InitializeComponent();
            this.FormClosing += frmLogin_FormClosing;
            btnShowPass.BackgroundImage = Image.FromFile(Application.StartupPath + @"\..\..\Resources\hide.png");
            //btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;

            Firebase_Config firebaseConfig = new Firebase_Config();
            client = firebaseConfig.GetClient();

            if (client == null)
            {
                // MessageBox.Show("Failed to connect to Database.");
            }
        }

        // Lockout Management
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
                // Lockout is still active
                MessageBox.Show($"Account is locked. Try again after {info.LockoutUntil.Value.ToString("T")}.", "Account Locked");
                LockUI(info.LockoutUntil.Value);
                return true;
            }
            else if (info.LockoutUntil.HasValue && info.LockoutUntil.Value <= DateTime.Now)
            {
                // Lockout period has expired
                ResetFailedAttempts(); // Reset the failed attempts
                UnlockUI(); // Re-enable the UI controls
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
                //MessageBox.Show($"Account locked due to too many failed attempts. Try again after {info.LockoutUntil.Value.ToString("T")}.", "Account Locked");
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
            MessageBox.Show($"Device is locked out. Try again at {lockoutEndTime}.", "Device Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void UnlockUI()
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            btnLogin.Enabled = true;
            lblForgotPassword.Enabled = true;
        }

        // Event Handlers
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            var (userId, first_name, last_name, userType, Status) = await LoginUser(username, password);

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.");
                return;
            }

            if (Status == "Inactive")
            {
                MessageBox.Show("Account Inactive");
                return;
            }

            // Check if the user is locked out
            if (IsLockedOut())
            {
                return; // Exit if locked out
            }

            // Validate the login
            

            if (!string.IsNullOrEmpty(userId))
            {
                ResetFailedAttempts();
                UnlockUI();
                Username = username;
                Name = first_name + " " + last_name;
                Role = userType;
                UserID = userId;
                this.Hide();
                new frmDashboard(this).Show();
                MessageBox.Show("Login successful!");
            }
            else
            {
                IncrementFailedAttempts();
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
            // Initialize the timer
            lockoutTimer = new Timer();
            lockoutTimer.Interval = 1000; // Check every second
            lockoutTimer.Tick += LockoutTimer_Tick; // Event handler for tick
            lockoutTimer.Start(); // Start the timer

            // Check if the user is locked out on form load
            LockoutInfo info = LoadLockoutInfo();
            if (info.LockoutUntil.HasValue && info.LockoutUntil.Value > DateTime.Now)
            {
                // Lock UI initially if the user is locked out
                LockUI(info.LockoutUntil.Value);
            }
            else
            {
                // If lockout expired, reset failed attempts and unlock UI
                ResetFailedAttempts();
                UnlockUI();
            }

            lblForgotPassword.Cursor = Cursors.Hand;
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            frmForgot form1 = new frmForgot();
            form1.Show();
        }

        private async Task<(string username, string firstName, string lastName, string role, string status)> LoginUser(string username, string password)
{
    try
    {
        // Fetch the user data directly using the username as the key
        FirebaseResponse response = await client.GetAsync($"Users/{username}");

        if (response == null || response.Body == "null")
        {
            MessageBox.Show("Username does not exist.");
            return (null, null, null, null, null);
        }

        // Parse the response into a UserRegistration object
        UserRegistration userData = response.ResultAs<UserRegistration>();

        // Decrypt the stored password
        string decryptedPassword = PasswordHelper.DecryptPassword(userData.Password);

        // Verify the password
        if (password == decryptedPassword)
        {
            // Return user details
            return (username, userData.First_Name, userData.Last_Name, userData.Role, userData.Status);
        }
        else
        {
            MessageBox.Show("Invalid username or password.");
            return (null, null, null, null, null);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Login Failed: {ex.Message}");
        return (null, null, null, null, null);
    }
}


        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lockoutTimer != null)
            {
                lockoutTimer.Stop();
            }
            Application.Exit();
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
