using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace arkoDT.Profile
{
    public partial class frmChangePassword : Form
    {
        private bool isFirstImage = true;
        private frmProfile profile;
        public string userID;
        private MySqlConnection connection;
        public frmChangePassword(frmProfile frmProfileInstance)
        {
            InitializeComponent();
            btnShowPass.BackgroundImage = Image.FromFile(Application.StartupPath + @"\..\..\Resources\hide.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;
            profile = frmProfileInstance;
            userID = profile.userID;
            string connectionString = "Server=127.0.0.1;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to connect to Database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (isFirstImage)
            {
                txtCurrentPassword.PasswordChar = '\0';
                txtNewPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/view.png");
            }
            else
            {
                txtCurrentPassword.PasswordChar = '●';
                txtNewPassword.PasswordChar = '●';
                txtConfirmPassword.PasswordChar = '●';
                btnShowPass.BackgroundImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/hide.png");
            }

            isFirstImage = !isFirstImage;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get the values from the form
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Step 1: Validate current password
            string query = "SELECT password FROM users WHERE user_id = @userID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userID", userID);

            try
            {
                string encryptedPassword = cmd.ExecuteScalar()?.ToString();

                if (encryptedPassword == null)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Decrypt the password from the database
                string decryptedPassword = PasswordHelper.DecryptPassword(encryptedPassword);

                if (decryptedPassword != currentPassword)
                {
                    MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while verifying the password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Step 2: Validate new password and confirm password
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Encrypt the new password before saving
            string encryptedNewPassword;
            try
            {
                encryptedNewPassword = PasswordHelper.EncryptPassword(newPassword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while encrypting the new password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Step 3: Update password
            string updateQuery = "UPDATE users SET password = @newPassword WHERE user_id = @userID";
            MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
            updateCmd.Parameters.AddWithValue("@newPassword", encryptedNewPassword);
            updateCmd.Parameters.AddWithValue("@userID", userID);

            try
            {
                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update the password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

