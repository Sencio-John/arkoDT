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
using System.Device.Location;

namespace arkoDT
{
    public partial class frmInfo : Form
    {
        private frmMap frmMap;
        private MySqlConnection connection;
        public frmInfo(frmMap frmMapInstance)
        {
            InitializeComponent();
            frmMap = frmMapInstance;
            string connectionString = "Server=localhost;Port=4000;Database=arkovessel;Uid=root;Pwd=!Arkovessel!;";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to connect to Database.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Input fields
            string description = txtDescription.Text;
            string residentName = txtResidentName.Text;
            string type = cbType.Text;

            // Validate input
            if (string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(residentName) ||
                string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("All fields must be filled in before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the current coordinates from GeoCoordinateWatcher
            GeoCoordinate coord = null;
            const int maxRetries = 5;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                var watcher = new GeoCoordinateWatcher();
                watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));
                coord = watcher.Position.Location;

                if (!coord.IsUnknown)
                    break;

                retryCount++;
            }

            if (coord == null || coord.IsUnknown)
            {
                MessageBox.Show("Failed to detect device coordinates after multiple attempts. Please ensure location services are enabled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string latitude = coord.Latitude.ToString();
            string longitude = coord.Longitude.ToString();

            // Generate a unique ID
            string pinnedID;
            do
            {
                pinnedID = "ARKO-PIN-" + GenerateRandomID(6);
            } while (CheckIfIDExists(pinnedID));

            MySqlTransaction transaction = null;

            try
            {
                // Start a transaction
                transaction = connection.BeginTransaction();

                // Insert data into the database
                string query = "INSERT INTO pinned (pinned_ID, description, latitude, longitude , resident_Name, status, type) " +
                               "VALUES (@pinnedID, @description, @latitude, @longitude, @residentName, @status, @type)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@pinnedID", pinnedID);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@latitude", latitude);
                    cmd.Parameters.AddWithValue("@longitude", longitude);
                    cmd.Parameters.AddWithValue("@residentName", residentName);
                    cmd.Parameters.AddWithValue("@status", "Active");
                    cmd.Parameters.AddWithValue("@type", type);

                    cmd.ExecuteNonQuery();
                }

                // Commit the transaction
                transaction.Commit();

                // Add marker to the map
                frmMap.AddMarker(double.Parse(latitude), double.Parse(longitude), pinnedID, type, residentName, description);

                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Update map and close form
                frmMap.UpdateLocationsCards();
                this.Close();
            }
            catch (Exception ex)
            {
                // If an error occurs, rollback the transaction
                transaction?.Rollback();

                MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection if it is open
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void lblPopulation_Click(object sender, EventArgs e)
        {

        }

        private void lblAve_Click(object sender, EventArgs e)
        {

        }

        private void lblCurrent_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GenerateRandomID(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool CheckIfIDExists(string pinnedID)
        {
            bool exists = false;

            try
            {
                string query = "SELECT COUNT(*) FROM pinned WHERE pinned_ID = @pinnedID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@pinnedID", pinnedID);
                    exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking ID existence: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return exists;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
