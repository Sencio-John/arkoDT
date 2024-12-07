using System;
using System.Device.Location;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MySql.Data.MySqlClient;

namespace arkoDT
{
    public partial class frmMap : Form
    {
        private GeoCoordinateWatcher watcher;
        private GMapOverlay markersOverlay; // Overlay to hold markers
        private bool isSidebarVisible = false; // Track sidebar visibility
        private frmInfo infoForm; // Declare an instance variable for frmInfo
        private MySqlConnection connection;
        private Panel _currentSelectedPanel = null;

        public frmMap()
        {
            InitializeComponent();
            InitializeMap(); // Initialize map and overlay
            SetupWatcher(); // Setup GeoCoordinateWatcher

            this.FormClosing += frmMap_FormClosing;
            this.Load += frmMap_Load; // Link Load event
            btnPinLoc.Click += btnPinLoc_Click; // Button click event

            map.OnMarkerClick += Map_OnMarkerClick; // Subscribe to marker click event

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

        private void InitializeMap()
        {
            map = new GMapControl(); // Initialize GMapControl
            map.Dock = DockStyle.Fill; // Set docking style
            Controls.Add(map); // Add map to form controls

            markersOverlay = new GMapOverlay("markers"); // Create a new overlay for markers
            map.Overlays.Add(markersOverlay); // Add overlay to the map
        }

        private void SetupWatcher()
        {
            watcher = new GeoCoordinateWatcher();
            watcher.PositionChanged += Watcher_PositionChanged;
            watcher.Start(); // Start the watcher
        }

        private void frmMap_Load(object sender, EventArgs e)
        {
            try
            {
                map.MapProvider = GMapProviders.GoogleMap;
                map.MinZoom = 2;
                map.MaxZoom = 20;
                map.Zoom = 18; // Adjust initial zoom level

                // Set a default position for the map
                map.Position = new PointLatLng(37.7749, -122.4194); // Set to San Francisco as a default location
                map.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading map: {ex.Message}");
            }
            UpdateLocationsCards();
        }

        private void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            var latitude = e.Position.Location.Latitude;
            var longitude = e.Position.Location.Longitude;

            // Center map on current location
            map.Position = new PointLatLng(latitude, longitude);
            map.Refresh(); // Refresh the map position when the location changes
        }

        private void frmMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            watcher.Stop(); // Stop the watcher when closing
        }

        private void btnPinLoc_Click(object sender, EventArgs e)
        {
            // Get the current location from the watcher
            if (!watcher.Position.Location.IsUnknown)
            {
                //double latitude = watcher.Position.Location.Latitude;
                //double longitude = watcher.Position.Location.Longitude;
                //AddMarker(latitude, longitude); // Add marker at current location

                // Check if frmInfo is already opened
                if (infoForm == null || infoForm.IsDisposed || !infoForm.Visible)
                {
                    infoForm = new frmInfo(this); // Create a new instance of frmInfo
                    infoForm.Show(this); // Show the instance of frmInfo in front of frmMap
                }
                else
                {
                    infoForm.BringToFront(); // Bring existing instance to front
                }
            }
            else
            {
                MessageBox.Show("Current location is unknown.");
            }
        }

        public void AddMarker(double latitude, double longitude, string pinnedID, string type, string residentName, string description)
        {
            var markerPosition = new PointLatLng(latitude, longitude);
            var marker = new GMarkerGoogle(markerPosition, GMarkerGoogleType.red_dot); // Use GMarkerGoogle

            markersOverlay.Markers.Add(marker); // Add marker to the overlay
            map.Refresh(); // Refresh the map to display the marker

            // Store additional info in marker's Tag
            marker.Tag = new { PinnedID = pinnedID, Type = type, ResidentName = residentName, Description = description };
        }

        private void Map_OnMarkerClick(GMapMarker marker, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Check if left mouse button was clicked
            {
                // Retrieve the data stored in the marker's Tag
                var markerData = (dynamic)marker.Tag;
                string pinnedID = markerData.PinnedID;
                string type = markerData.Type;
                string residentName = markerData.ResidentName;
                string description = markerData.Description;

                // Show the sidebar with the marker's details
                ShowSidebar(type, residentName, description);

                // Show the FlipList
                flpList.Visible = true;

                // Highlight the card in the FlipList that matches the clicked marker
                HighlightCard(pinnedID);
            }
        }

        private void HighlightCard(string pinnedID)
        {
            foreach (Control control in flpList.Controls)
            {
                if (control is Panel pnlCard && pnlCard.Tag != null)
                {
                    var cardData = (dynamic)pnlCard.Tag;
                    if (cardData.PinnedID == pinnedID)
                    {
                        // Highlight the card by changing its background color or other visual cues
                        pnlCard.BackColor = Color.Yellow; // Change background color as an example
                    }
                    else
                    {
                        pnlCard.BackColor = SystemColors.ControlLightLight; // Reset background color for non-matching cards
                    }
                }
            }
        }

        private void ShowSidebar(string type, string residentName, string description)
        {
            pnlSide.Visible = true;
            Label lblType = new Label();
            Label lblFamilyName = new Label();
            Label lblDescription = new Label();
            Button btnClose = new Button();
            Panel pnlType = new Panel();
            Panel pnlFamilyName = new Panel();
            Panel pnlDescription = new Panel(); // New panel for description
            Panel pnlWaterLevel = new Panel();

            btnClose.Location = new Point(122, 3);
            btnClose.Size = new Size(75, 23);
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;

            btnClose.Click += new EventHandler(btnClose_Click);

            // Set the values of the labels based on the passed parameters
            lblType.Dock = DockStyle.Fill;
            lblType.Text = type;
            lblType.TextAlign = ContentAlignment.MiddleCenter;

            pnlType.Location = new Point(0, 84);
            pnlType.Size = new Size(200, 43);
            pnlType.Controls.Add(lblType);

            lblFamilyName.Dock = DockStyle.Fill;
            lblFamilyName.Text = residentName;
            lblFamilyName.TextAlign = ContentAlignment.MiddleCenter;

            pnlFamilyName.Location = new Point(0, 171);
            pnlFamilyName.Size = new Size(200, 43);
            pnlFamilyName.Controls.Add(lblFamilyName);

            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Text = "Description: " + description; // Set description text
            lblDescription.TextAlign = ContentAlignment.MiddleCenter;

            pnlDescription.Location = new Point(0, 257); // Adjust the position of the new panel
            pnlDescription.Size = new Size(200, 43);
            pnlDescription.Controls.Add(lblDescription);

            // Adding everything to the sidebar panel
            pnlSide.Controls.Clear();
            pnlSide.Controls.Add(btnClose);
            pnlSide.Controls.Add(pnlType);
            pnlSide.Controls.Add(pnlFamilyName);
            pnlSide.Controls.Add(pnlDescription); // Add description panel to the sidebar
            pnlSide.Controls.Add(pnlWaterLevel);

            //pnlSide.Visible = !pnlSide.Visible; // Toggle visibility
            //if (pnlSide.Visible)
            //{
            //    pnlSide.BringToFront(); // Bring panel to front when visible
            //}
            pnlSide.BringToFront(); // Bring panel to front when visible
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close or hide the panel or perform any other action
            pnlSide.Visible = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPinned_Click(object sender, EventArgs e)
        {
            flpList.Visible = true;
        }

        private void btnCloseFLP_Click(object sender, EventArgs e)
        {
            flpList.Visible = false;
        }

        public void UpdateLocationsCards()
        {
            try
            {
                // Ensure the connection is open before executing the query
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                flpList.Controls.Clear();

                // Query to retrieve data from the database for both active and inactive records
                string query = "SELECT pinned_ID, type, resident_Name, latitude, longitude, status, description FROM pinned";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Retrieve data from database
                        string pinnedID = reader["pinned_ID"].ToString();
                        string type = reader["type"].ToString();
                        string residentName = reader["resident_Name"].ToString();
                        double latitude = Convert.ToDouble(reader["latitude"]);
                        double longitude = Convert.ToDouble(reader["longitude"]);
                        string status = reader["status"].ToString();
                        string description = reader["description"].ToString();

                        // Create and configure the new card panel
                        Panel pnlCards = new Panel();
                        Panel pnlHeader = new Panel();
                        Label title = new Label();
                        Label lblDirection = new Label();
                        Label lblFamilyName = new Label();

                        title.Dock = System.Windows.Forms.DockStyle.Fill;
                        title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                        title.Location = new System.Drawing.Point(0, 0);
                        title.Size = new System.Drawing.Size(179, 27);
                        title.TabIndex = 1;
                        title.Text = type;
                        title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        pnlHeader.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
                        pnlHeader.Location = new System.Drawing.Point(0, 0);
                        pnlHeader.Size = new System.Drawing.Size(179, 27);
                        pnlHeader.TabIndex = 0;

                        lblFamilyName.Location = new System.Drawing.Point(104, 30);
                        lblFamilyName.Size = new System.Drawing.Size(72, 60);
                        lblFamilyName.TabIndex = 2;
                        lblFamilyName.Text = residentName;
                        lblFamilyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        lblDirection.Location = new System.Drawing.Point(3, 30);
                        lblDirection.Size = new System.Drawing.Size(72, 60);
                        lblDirection.TabIndex = 1;
                        lblDirection.Text = "Resident Name";
                        lblDirection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        pnlCards.BackColor = System.Drawing.SystemColors.ControlLightLight;
                        pnlCards.Location = new System.Drawing.Point(3, 32);
                        pnlCards.Size = new System.Drawing.Size(179, 100);
                        pnlCards.TabIndex = 1;

                        pnlCards.Tag = new { PinnedID = pinnedID, Type = type, ResidentName = residentName, Description = description };

                        pnlCards.Click += (sender, e) => pnlCards_Click(sender, e, type, residentName, description);

                        flpList.Controls.Add(pnlCards);
                        pnlCards.Controls.Add(lblFamilyName);
                        pnlCards.Controls.Add(lblDirection);
                        pnlCards.Controls.Add(pnlHeader);
                        pnlHeader.Controls.Add(title);

                        // Add marker to map and pass necessary data for later identification
                        AddMarker(latitude, longitude, pinnedID, type, residentName, description);
                    }

                    if (!reader.HasRows)
                    {
                        MessageBox.Show("No data returned from database.");
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading location cards: {ex.Message}");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close(); // Close the connection after use
                }
            }
        }

        private void pnlCards_Click(object sender, EventArgs e, string type, string residentName, string description)
        {
            Panel clickedPanel = sender as Panel;

            // Check if the clicked panel is the same as the previously selected panel
            if (_currentSelectedPanel == clickedPanel)
            {
                // Close the sidebar if the same panel is clicked again
                CloseSidebar();
                _currentSelectedPanel = null; // Reset the current selected panel
                return;
            }

            // Reset background color for all panels in the flpList
            foreach (Control control in flpList.Controls)
            {
                if (control is Panel pnlCard)
                {
                    pnlCard.BackColor = System.Drawing.SystemColors.ControlLightLight; // Reset to default color
                }
            }

            // Highlight the clicked panel
            clickedPanel.BackColor = Color.Yellow; // Change to highlight color

            // Show or update the sidebar with the new details
            ShowSidebar(type, residentName, description);

            // Set the clicked panel as the current selected panel
            _currentSelectedPanel = clickedPanel;
        }

        private void CloseSidebar()
        {
            pnlSide.Visible = false;
        }
    }
}
