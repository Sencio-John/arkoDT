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

            Title.Dock = DockStyle.Fill;
            Title.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            Title.Size = new Size(310, 41);
            Title.Text = "User";
            Title.TextAlign = ContentAlignment.MiddleCenter;

            pnlHeader.AutoScroll = true;
            pnlHeader.BackColor = Color.FromArgb(192, 255, 192);
            pnlHeader.Size = new Size(310, 41);

            lblRole.AutoSize = true;
            lblRole.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular);
            lblRole.Location = new Point(215, 90);
            lblRole.Text = role; // Set role from parameter

            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular);
            lblUserName.Location = new Point(6, 90);
            lblUserName.Text = username; // Set username from parameter

            pnlCards.AutoScroll = true;
            pnlCards.BackColor = SystemColors.ControlLightLight;
            pnlCards.Size = new Size(310, 169);

            pnlHeader.Controls.Add(Title);
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
                        // Populate each card with the user's data
                        UpdateUsersCards(user.Name, user.Role);
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
