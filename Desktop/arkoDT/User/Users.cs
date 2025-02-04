﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace arkoDT
{
    public partial class frmUsers : Form
    {
        private MySqlConnection connection;
        private frmDashboard dashboard;
        string userID;
        string username;
        string role;
        string firstName;
        string lastName;
        string fullName;
        string status;
        string currentUserID;

        public frmUsers(frmDashboard frmDashboardInstance)
        {
            InitializeComponent();
            string connectionString = "Server=127.0.0.1;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);
            dashboard = frmDashboardInstance;

            currentUserID = dashboard.userID;


            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to connect to Database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            LoadUsers();
        }

        public void UpdateUsersCards(string username, string role, string Name, string status, string userID)
        {
            Panel pnlCards = new Panel();
            Panel pnlHeader = new Panel();
            Label Title = new Label();
            Label lblUserName = new Label();
            Label lblRole = new Label();
            Label lblStatus = new Label();
            Button btnEdit = new Button();
            Button btnChangeStatus = new Button();
            PictureBox pbProfile = new PictureBox();

            Title.Dock = DockStyle.Fill;
            Title.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            Title.Size = new Size(310, 41);
            Title.Text = username;
            Title.TextAlign = ContentAlignment.MiddleCenter;

            if (status == "Inactive")
            {
                pnlHeader.AutoScroll = true;
                pnlHeader.BackColor = Color.FromArgb(255, 128, 128);
                pnlHeader.Size = new Size(310, 41);
            }
            else
            {
                pnlHeader.AutoScroll = true;
                pnlHeader.BackColor = Color.FromArgb(192, 255, 192);
                pnlHeader.Size = new Size(310, 41);
            }
            

            pbProfile.Image = Image.FromFile(@"C:\Users\SENCIO\Documents\GitHub\arkoDT\Desktop\arkoDT\Resources\profile.jpg");
            pbProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            pbProfile.Location = new System.Drawing.Point(36, 47);
            pbProfile.Size = new System.Drawing.Size(80, 80);
            pbProfile.TabIndex = 4;
            pbProfile.TabStop = false;

            pbProfile.Paint += (sender, e) =>
            {
                var pictureBox = sender as PictureBox;
                if (pictureBox != null && pictureBox.Image != null)
                {
                    // Create a circular clipping region
                    using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
                        pictureBox.Region = new Region(path); // Apply circular region to the PictureBox

                        // Draw the image inside the circular region
                        e.Graphics.SetClip(path); // Apply clipping
                        e.Graphics.DrawImage(pictureBox.Image, 0, 0, pictureBox.Width, pictureBox.Height); // Draw image within the clipped region
                    }
                }
            };

            btnEdit.Location = new System.Drawing.Point(3, 143);
            btnEdit.Size = new System.Drawing.Size(75, 23);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;

            btnEdit.Tag = userID;
            btnEdit.Click += btnEdit_Click;

            btnChangeStatus.Location = new System.Drawing.Point(221, 143);
            btnChangeStatus.Size = new System.Drawing.Size(86, 23);
            btnChangeStatus.TabIndex = 3;
            btnChangeStatus.Text = "Change Status";
            btnChangeStatus.UseVisualStyleBackColor = true;

            btnChangeStatus.Click += new EventHandler(btnChangeStatus_Click);
            btnChangeStatus.Tag = userID;

            if (currentUserID == userID)
            {
                btnEdit.Visible = false;
                btnChangeStatus.Visible = false;
            }

            lblStatus.AutoSize = true;
            lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblStatus.Location = new System.Drawing.Point(155, 120);
            lblStatus.Size = new System.Drawing.Size(60, 24);
            lblStatus.TabIndex = 4;
            lblStatus.Text = status;

            lblRole.AutoSize = true;
            lblRole.Font = new Font("Microsoft Sans Serif", 15.25F, FontStyle.Regular);
            lblRole.Location = new Point(155, 87);
            lblRole.Text = role;

            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Microsoft Sans Serif", 15.25F, FontStyle.Regular);
            lblUserName.Location = new Point(155, 54);
            lblUserName.Text = Name;

            pnlCards.AutoScroll = true;
            pnlCards.BackColor = SystemColors.ControlLightLight;
            pnlCards.Size = new Size(310, 169);

            pnlHeader.Controls.Add(Title);
            pnlCards.Controls.Add(pbProfile);
            pnlCards.Controls.Add(btnChangeStatus);
            pnlCards.Controls.Add(btnEdit);
            pnlCards.Controls.Add(lblStatus);
            pnlCards.Controls.Add(lblRole);
            pnlCards.Controls.Add(lblUserName);
            pnlCards.Controls.Add(pnlHeader);

            flpUsers.Controls.Add(pnlCards);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmUC form1 = new frmUC(this, dashboard);
            form1.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            // Ensuring connection is closed after form load
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag != null)
            {
                string selectedUserID = btn.Tag.ToString();

                // Pass the user_ID to the frmEdit form
                frmEdit editForm = new frmEdit(selectedUserID, this);
                editForm.Show();
            }
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || btn.Tag == null)
                return;

            string selectedUserID = btn.Tag.ToString();

            DialogResult result = MessageBox.Show(
                "Are you sure you want to proceed?",  // Message text
                "Confirmation",                      // Title
                MessageBoxButtons.YesNo,             // Buttons
                MessageBoxIcon.Question              // Icon
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open(); // Ensure the connection is open

                    // Retrieve the current status of the selected user
                    string currentStatusQuery = "SELECT status FROM users WHERE user_ID = @userID";
                    using (MySqlCommand getStatusCmd = new MySqlCommand(currentStatusQuery, connection))
                    {
                        getStatusCmd.Parameters.AddWithValue("@userID", selectedUserID);
                        string currentStatus = getStatusCmd.ExecuteScalar()?.ToString();

                        // Toggle the status
                        string newStatus = currentStatus == "Active" ? "Inactive" : "Active";

                        // Update the status in the database
                        string updateStatusQuery = "UPDATE users SET status = @newStatus WHERE user_ID = @userID";
                        using (MySqlCommand updateStatusCmd = new MySqlCommand(updateStatusQuery, connection))
                        {
                            updateStatusCmd.Parameters.AddWithValue("@newStatus", newStatus);
                            updateStatusCmd.Parameters.AddWithValue("@userID", selectedUserID);
                            updateStatusCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"User status changed to {newStatus}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating the status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close(); // Ensure the connection is closed
                }

                // Refresh the user list
                LoadUsers();
            }
        }

        public void LoadUsers()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open(); // Ensure the connection is open
                }

                string query = @"
                                SELECT u.user_ID, u.username, u.role, u.status, ui.first_Name, ui.last_Name
                                FROM users u
                                INNER JOIN users_info ui ON u.user_ID = ui.user_ID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    flpUsers.Controls.Clear(); // Clear existing cards

                    while (reader.Read())
                    {
                        userID = reader["user_ID"].ToString();
                        username = reader["username"].ToString();
                        role = reader["role"].ToString();
                        firstName = reader["first_Name"].ToString();
                        lastName = reader["last_Name"].ToString();
                        status = reader["status"].ToString();

                        fullName = $"{firstName} {lastName}";

                        // Populate each card with the user's data
                        UpdateUsersCards(username, role, fullName, status, userID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                RetryLoadUsers(); // Trigger retry logic
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close(); // Ensure the connection is closed
                }
            }
        }

        // Retry loading users after a failure
        private void RetryLoadUsers()
        {
            Timer retryTimer = new Timer();
            retryTimer.Interval = 1000; // Retry after 1 second
            retryTimer.Tick += (sender, e) =>
            {
                try
                {
                    // Attempt to fetch users again
                    string query = "SELECT user_ID, username, role, status FROM users";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    flpUsers.Controls.Clear(); // Clear existing cards

                    while (reader.Read())
                    {
                        userID = reader["user_ID"].ToString();
                        username = reader["username"].ToString();
                        role = reader["role"].ToString();
                        status = reader["status"].ToString();

                        // Fetch additional information from users_info table
                        string userInfoQuery = "SELECT first_Name, last_Name FROM users_info WHERE user_ID = @userID";
                        MySqlCommand infoCmd = new MySqlCommand(userInfoQuery, connection);
                        infoCmd.Parameters.AddWithValue("@userID", userID);
                        MySqlDataReader infoReader = infoCmd.ExecuteReader();

                        fullName = "";
                        if (infoReader.Read())
                        {
                            firstName = infoReader["first_Name"].ToString();
                            lastName = infoReader["last_Name"].ToString();
                            fullName = firstName + " " + lastName;
                        }
                        infoReader.Close();

                        // Populate each card with the user's data
                        UpdateUsersCards(username, role, fullName, status, userID);
                    }

                    reader.Close();
                    retryTimer.Stop(); // Stop retrying if successful
                }
                catch (Exception)
                {
                    // If retry fails, it will try again
                }
            };
            retryTimer.Start(); // Start the retry mechanism
        }

        // Ensure connection is closed when form is disposed
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void frmUsers_Load_1(object sender, EventArgs e)
        {
            try
            {
                // Ensure you close any previous DataReader or connections if any
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
