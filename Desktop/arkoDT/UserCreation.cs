using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Collections.Generic;


namespace arkoDT
{
    public partial class frmUC : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "9LIEv3oM8IkGzdbhvL6949CXXFAD86pu2v2ISD1r",
            BasePath = "https://arko-uno-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        IFirebaseClient client;
        private string generatedID;

        public frmUC()
        {
            InitializeComponent();

            // Initialize Firebase Client
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                MessageBox.Show("Connected to Firebase!");
            }
            else
            {
                MessageBox.Show("Failed to connect to Firebase.");
            }
        }

        // Close form on Back button click
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Generate a random unique ID when the form loads
        public async void frmUC_Load(object sender, EventArgs e)
        {
            // Generate random ID
            await GenerateRandomIDOnLoad();
        }

        // Generate the random ID and check for uniqueness
        public async Task GenerateRandomIDOnLoad()
        {
            generatedID = await GenerateUniqueID();
            MessageBox.Show("Generated Unique ID: " + generatedID);
        }

        // Generate a unique random ID
        private async Task<string> GenerateUniqueID()
        {
            string newID;
            bool exists;

            do
            {
                newID = GenerateRandomID(8); // Generate an 8-character random ID
                exists = await IsIDExists(newID); // Check if the ID already exists in Firebase
            } while (exists);

            return newID;
        }

        // Method to generate a random alphanumeric string
        private string GenerateRandomID(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Check if the generated ID exists in Firebase
        private async Task<bool> IsIDExists(string id)
        {
            try
            {
                FirebaseResponse response = await client.GetAsync("Users/" + id);
                return response.Body != "null"; // If response body is not "null", the ID exists
            }
            catch (Exception)
            {
                return false; // Assume ID does not exist in case of error
            }
        }

        // Check if the username already exists in Firebase
        private async Task<bool> IsUsernameExists(string username)
        {
            FirebaseResponse response = await client.GetAsync("Users/");
            var users = response.ResultAs<Dictionary<string, UserRegistration>>();

            if (users != null && users.Values.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                return true; // Username exists
            }

            return false;
        }

        // Check if the email already exists in Firebase
        private async Task<bool> IsEmailExists(string email)
        {
            FirebaseResponse response = await client.GetAsync("Users/");
            var users = response.ResultAs<Dictionary<string, UserRegistration>>();

            if (users != null && users.Values.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                return true; // Email exists
            }

            return false;
        }

        // Validate the email format
        private bool IsValidEmail(string email)
        {
            // Regular expression to validate email format
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Create user and store in Firebase
        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Basic validation
                if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Username and Email cannot be empty.");
                    return;
                }

                // Check if username exists
                bool usernameExists = await IsUsernameExists(txtUsername.Text);
                if (usernameExists)
                {
                    MessageBox.Show("Username already exists. Please choose another.");
                    return;
                }

                // Check if email exists
                bool emailExists = await IsEmailExists(txtEmail.Text);
                if (emailExists)
                {
                    MessageBox.Show("Email already exists. Please choose another.");
                    return;
                }

                // Validate email format
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Invalid email format.");
                    return;
                }

                // Create a new user registration object
                UserRegistration register = new UserRegistration
                {
                    Name = txtName.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text, // You may want to encrypt the password here
                    Email = txtEmail.Text,
                    Role = cbRole.Text,
                    Status = "Active"
                };

                // Save the user to Firebase with the generated ID
                SetResponse responce = await client.SetAsync("Users/" + generatedID, register);

                MessageBox.Show(string.Format("User {0} ({1}) has been successfully inserted into the database with ID {2}.", register.Name, register.Username, generatedID));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message); // Provide meaningful error message
            }
        }

        private async void frmUC_Load_1(object sender, EventArgs e)
        {
            generatedID = await GenerateUniqueID();
            MessageBox.Show("Generated Unique ID: " + generatedID);
        }
    }
}
