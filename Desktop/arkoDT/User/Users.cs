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

namespace arkoDT
{
    public partial class frmUsers : Form
    {
        private IFirebaseClient client;
        private frmDashboard dashboard;
        string username;
        string firstName;
        string lastName;
        string role;
        string status;
        public frmUsers(frmDashboard frmDashboardInstance)
        {

            
            InitializeComponent();
            Firebase_Config firebaseConfig = new Firebase_Config();
            client = firebaseConfig.GetClient();
            dashboard = frmDashboardInstance;

            if (client == null)
            {
                MessageBox.Show("Failed to connect to Database.");
            }
            LoadUsers();
        }

        // This method updates user cards
        public void UpdateUsersCards(string username, string firstName, string lastName, string role, string status)
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

            pnlHeader.AutoScroll = true;
            pnlHeader.Size = new Size(310, 41);
            if(status == "Active")
            {
                pnlHeader.BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                pnlHeader.BackColor = Color.FromArgb(255, 192, 192);
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

            btnEdit.Tag = username;  // Store the username here, do not display it
            btnEdit.Click += new EventHandler(btnEdit_Click);

            btnChangeStatus.Tag = username; // Store only the username



            btnChangeStatus.Location = new System.Drawing.Point(221, 143);
            btnChangeStatus.Size = new System.Drawing.Size(86, 23);
            btnChangeStatus.TabIndex = 3;
            btnChangeStatus.Text = "Change Status";
            btnChangeStatus.UseVisualStyleBackColor = true;

            // Ensure the event is hooked up properly to btnChangeStatus
            btnChangeStatus.Click += new EventHandler(btnChangeStatus_Click);

            lblStatus.AutoSize = true;
            lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblStatus.Location = new System.Drawing.Point(155, 120);
            lblStatus.Size = new System.Drawing.Size(60, 24);
            lblStatus.TabIndex = 4;
            lblStatus.Text = status;

            lblRole.AutoSize = true;
            lblRole.Font = new Font("Microsoft Sans Serif", 15.25F, FontStyle.Regular);
            lblRole.Location = new Point(155, 87);
            lblRole.Text = role; // Set role from parameter

            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Microsoft Sans Serif", 15.25F, FontStyle.Regular);
            lblUserName.Location = new Point(155, 54);
            lblUserName.Text = firstName + " " + lastName; // Display first and last name, NOT username

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

        // Method for adding a new user
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmUC form1 = new frmUC(this, dashboard);
            form1.Show();
        }

        // Button for adding new user
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Editing user Role
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Get the username from the Tag property of the button
            Button clickedButton = sender as Button;
            string username = clickedButton.Tag.ToString(); // Get username from Tag property

            // You can now pass the username to the next form for editing
            frmEdit form1 = new frmEdit(username, this);  // Passing username to edit the user's details
            form1.Show();
        }

        private async void btnChangeStatus_Click(object sender, EventArgs e)
        {
            // Ensure this is triggered only once per button click
            Button btnChangeStatus = sender as Button;
            if (btnChangeStatus?.Tag is string username)
            {
                try
                {
                    DialogResult result = MessageBox.Show(
                        "Are you sure you want to change the status?",  // Message text
                        "Confirmation",                                 // Title
                        MessageBoxButtons.YesNo,                       // Buttons
                        MessageBoxIcon.Question                        // Icon
                    );

                    if (result == DialogResult.Yes)
                    {
                        // FETCH MUNA NG DATA
                        FirebaseResponse response = await client.GetAsync($"Users/{username}");
                        var user = response.ResultAs<UserRegistration>();

                        if (user != null)
                        {
                            // Status Toggler
                            string newStatus = user.Status == "Active" ? "Inactive" : "Active";

                            // UPDATE
                            await client.SetAsync($"Users/{username}/Status", newStatus);


                            MessageBox.Show($"Status changed successfully to {newStatus}!", "Success");
                            LoadUsers();
                        }
                        else
                        {
                            MessageBox.Show("User not found.", "Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error");
                }
            }
        }

        public async void LoadUsers()
        {
            try
            {
                // Fetch all users from the database
                FirebaseResponse response = await client.GetAsync("Users/");
                var users = response.ResultAs<Dictionary<string, UserRegistration>>();

                flpUsers.Controls.Clear(); // Clear existing cards

                if (users != null && users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        username = user.Key;                  // Username is the key
                        firstName = user.Value.First_Name;
                        lastName = user.Value.Last_Name;
                        role = user.Value.Role;
                        status = user.Value.Status;

                        // Populate each card with the user's data
                        UpdateUsersCards(username, firstName, lastName, role, status);
                    }
                }
                else
                {
                    // No users found
                    flpUsers.Controls.Clear(); // Clear cards if no data
                    MessageBox.Show("No users found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                RetryLoadUsers(); // Retry in case of failure
            }
        }

        private void RetryLoadUsers()
        {
            Timer retryTimer = new Timer
            {
                Interval = 5000 // Retry every 5 seconds
            };

            retryTimer.Tick += async (sender, e) =>
            {
                try
                {
                    // Attempt to fetch users again
                    FirebaseResponse response = await client.GetAsync("Users/");
                    var users = response.ResultAs<Dictionary<string, UserRegistration>>();

                    flpUsers.Controls.Clear(); // Clear existing cards

                    if (users != null && users.Count > 0)
                    {
                        foreach (var user in users)
                        {
                            username = user.Key;                  // Username is the key
                            firstName = user.Value.First_Name;
                            lastName = user.Value.Last_Name;
                            role = user.Value.Role;
                            status = user.Value.Status;

                            // Populate each card with the user's data
                            UpdateUsersCards(username, firstName, lastName, role, status);
                        }

                        ((Timer)sender).Stop();     // Stop the timer on success
                    }
                    else
                    {
                        // Handle case where no users are found
                        MessageBox.Show("No users found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    // Do nothing; the timer will keep retrying
                }
            };

            retryTimer.Start();
        }

    }
}
