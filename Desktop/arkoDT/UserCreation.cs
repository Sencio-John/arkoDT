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

            try
            {
                // Initialize Firebase Client with error handling
                client = new FireSharp.FirebaseClient(config);

                if (client != null)
                {
                    MessageBox.Show("Welcome to User Creation");
                }
                else
                {
                    MessageBox.Show("Failed to open the User Creation Window");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to Database: {ex.Message}");
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
            generatedID = await GenerateUniqueID();
        }

        // Generate the random ID and check for uniqueness
        public async Task GenerateRandomIDOnLoad()
        {
            generatedID = await GenerateUniqueID();
            if (generatedID != null)
            {
                MessageBox.Show("Generated Unique ID: " + generatedID);
            }
            else
            {
                MessageBox.Show("Could not generate a unique ID. Please try again.");
            }
        }

        // Generate a unique random ID
        private async Task<string> GenerateUniqueID()
        {
            string newID;
            bool exists;
            int retryCount = 0;
            const int maxRetries = 30;

            do
            {
                newID = GenerateRandomID(8); // Generate an 8-character random ID
                exists = await IsIDExists(newID); // Check if the ID already exists in Firebase
                retryCount++;
            } while (exists && retryCount < maxRetries);

            if (retryCount >= maxRetries)
            {
                MessageBox.Show("Failed to generate a unique ID after multiple attempts.");
                return null;
            }

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
            try
            {
                FirebaseResponse response = await client.GetAsync("Users/");
                if (response == null || response.Body == "null")
                {
                    MessageBox.Show("No data found under 'Users' in Firebase.");
                    return false;
                }

                var users = response.ResultAs<Dictionary<string, UserRegistration>>();
                if (users != null && users.Values.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking username existence: {ex.Message}");
            }

            return false;
        }

        // Check if the email already exists in Firebase
        // Check if the email already exists in Firebase
        private async Task<bool> IsEmailExists(string email)
        {
            try
            {
                if (client == null)
                {
                    MessageBox.Show("Database client is not initialized.");
                    return false;
                }

                FirebaseResponse response = await client.GetAsync("Users/");

                // Log the raw response for debugging
                if (response == null || response.Body == "null")
                {
                    // No data exists under "Users" in Firebase
                    return false;
                }

                var users = response.ResultAs<Dictionary<string, UserRegistration>>();

                // Check if users dictionary is not null and contains valid UserRegistration instances
                if (users != null && users.Values.Any(u => u?.Email != null && u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                {
                    return true; // Email exists
                }
            }
            catch (Exception ex)
            {
                // Log more details about the error
                MessageBox.Show($"An error occurred while checking email existence: {ex.Message}\n{ex.StackTrace}");
            }

            return false;
        }





        // Validate the email format
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Create user and store in Firebase
        // Create user and store in Firebase
        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Basic validation for empty fields
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
                    Password = txtPassword.Text, // Consider encrypting this in a real application
                    Email = txtEmail.Text,
                    Role = cbRole.Text,
                    Status = "Active"
                };

                // Save the user to Firebase with the generated ID
                SetResponse response = await client.SetAsync("Users/" + generatedID, register);

                MessageBox.Show($"New User has been successfully inserted into the database.");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message); // Provide a meaningful error message
            }

        }

    }
}
