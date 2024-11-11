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
        public frmUsers()
        {
            
            InitializeComponent();
            Firebase_Config firebaseConfig = new Firebase_Config();
            client = firebaseConfig.GetClient();

            if (client == null)
            {
                MessageBox.Show("Failed to connect to Database.");
            }
            LoadUsers();
        }

        public void UpdateUsersCards(string username, string role)
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
            Title.Text = "User";
            Title.TextAlign = ContentAlignment.MiddleCenter;

            pnlHeader.AutoScroll = true;
            pnlHeader.BackColor = Color.FromArgb(192, 255, 192);
            pnlHeader.Size = new Size(310, 41);

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

            // Ensure the event is hooked up properly to btnEdit
            btnEdit.Click += new EventHandler(btnEdit_Click);

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
            lblStatus.Text = "Status";

            lblRole.AutoSize = true;
            lblRole.Font = new Font("Microsoft Sans Serif", 15.25F, FontStyle.Regular);
            lblRole.Location = new Point(155, 87);
            lblRole.Text = role; // Set role from parameter

            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Microsoft Sans Serif", 15.25F, FontStyle.Regular);
            lblUserName.Location = new Point(155, 54);
            lblUserName.Text = username; // Set username from parameter

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
            frmUC form1 = new frmUC(this);
            form1.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmEdit form1 = new frmEdit();
            form1.Show();
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
             "Are you sure you want to proceed?",  // Message text
             "Confirmation",                      // Title
             MessageBoxButtons.YesNo,             // Buttons
             MessageBoxIcon.Question              // Icon
             );

            if (result == DialogResult.Yes)
            {
                // User clicked Yes
                MessageBox.Show("You selected Yes.", "Result");
            }
            else
            {
                // User clicked No
                MessageBox.Show("You selected No.", "Result");
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

                if (users != null)
                {
                    foreach (var user in users.Values)
                    {
                        string Name = user.First_Name + " " + user.Last_Name;
                        // Populate each card with the user's data
                        UpdateUsersCards(Name, user.Role);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load users: " + ex.Message);
            }
        }
    }
}
