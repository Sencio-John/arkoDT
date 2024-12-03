using System;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace arkoDT
{
    public partial class frmEdit : Form
    {
        private string username; // To store the username passed from the previous form
        private IFirebaseClient client;
        private frmUsers Users;

        public frmEdit(string username, frmUsers frmUsersInstance)
        {
            InitializeComponent();
            this.username = username;
            Users = frmUsersInstance;

            // Initialize Firebase client
            Firebase_Config firebaseConfig = new Firebase_Config();
            client = firebaseConfig.GetClient();

            if (client == null)
            {
                MessageBox.Show("Failed to connect to Database.");
            }
        }

        // When the form loads, fetch the user's current role and other data
        private async void frmEdit_Load(object sender, EventArgs e)
        {
            try
            {
                // Fetch the user data from Firebase using the username
                FirebaseResponse response = await client.GetAsync($"Users/{username}");
                var user = response.ResultAs<UserRegistration>();

                if (user != null)
                {
                    // Display the current role in the ComboBox (or TextBox)
                    cmbRole.Text = user.Role; // Assuming there's a ComboBox named cmbRole
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading user data.");
            }
        }

        // Save the updated role to Firebase when the user clicks 'Save'
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the new role from the ComboBox
                string newRole = cmbRole.Text;

                // Fetch the current user data (to retain other fields)
                FirebaseResponse response = await client.GetAsync($"Users/{username}");
                var user = response.ResultAs<UserRegistration>();

                if (user != null)
                {
                    // Update only the role, leaving other data unchanged
                    user.Role = newRole;

                    // Save the updated user object back to Firebase
                    SetResponse updateResponse = await client.SetAsync($"Users/{username}", user);

                    if (updateResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MessageBox.Show("Role updated successfully!");
                        Users.LoadUsers();
                        this.Close(); // Close the form after updating the role
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the role.");
                    }
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating the role: " + ex.Message);
            }
        }

        // Close the form without saving when the user clicks 'Back'
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}