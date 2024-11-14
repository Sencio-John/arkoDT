using System;
using System.Device.Location;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace arkoDT
{
    public partial class frmMap : Form
    {
        private GeoCoordinateWatcher watcher;
        private GMapOverlay markersOverlay; // Overlay to hold markers
        private bool isSidebarVisible = false; // Track sidebar visibility
        private frmInfo infoForm; // Declare an instance variable for frmInfo

        public frmMap()
        {
            InitializeComponent();
            InitializeMap(); // Initialize map and overlay
            SetupWatcher(); // Setup GeoCoordinateWatcher

            this.FormClosing += frmMap_FormClosing;
            this.Load += frmMap_Load; // Link Load event
            btnPinLoc.Click += btnPinLoc_Click; // Button click event

            map.OnMarkerClick += Map_OnMarkerClick; // Subscribe to marker click event
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
                double latitude = watcher.Position.Location.Latitude;
                double longitude = watcher.Position.Location.Longitude;
                AddMarker(latitude, longitude); // Add marker at current location

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

        private void AddMarker(double latitude, double longitude)
        {
            var markerPosition = new PointLatLng(latitude, longitude);
            var marker = new GMarkerGoogle(markerPosition, GMarkerGoogleType.red_dot); // Use GMarkerGoogle

            markersOverlay.Markers.Add(marker); // Add marker to the overlay
            map.Refresh(); // Refresh the map to display the marker

            // Store additional info in marker's Tag
            marker.Tag = new { Latitude = latitude, Longitude = longitude };
        }

        private void Map_OnMarkerClick(GMapMarker marker, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Check if left mouse button was clicked
            {
                ShowSidebar(marker.Tag); // Show sidebar with marker info
            }
        }

        private void ShowSidebar(object markerTag)
        {
            Label lblType = new Label();
            Label lblFamilyName = new Label();
            Label lblWaterLevel = new Label();
            Button btnClose = new Button();
            Panel pnlType = new Panel();
            Panel pnlFamilyName = new Panel();
            Panel pnlWaterLevel = new Panel();


            btnClose.Location = new System.Drawing.Point(122, 3);
            btnClose.Size = new System.Drawing.Size(75, 23);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;

            // Ensure the event is hooked up properly to btnClose
            btnClose.Click += new EventHandler(btnClose_Click);

            lblType.Dock = System.Windows.Forms.DockStyle.Fill;
            lblType.Location = new System.Drawing.Point(0, 0);
            lblType.Size = new System.Drawing.Size(200, 43);
            lblType.TabIndex = 0;
            lblType.Text = "Type: Danger";
            lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlType.Location = new System.Drawing.Point(0, 84);
            pnlType.Size = new System.Drawing.Size(200, 43);
            pnlType.TabIndex = 0;

            lblFamilyName.Dock = System.Windows.Forms.DockStyle.Fill;
            lblFamilyName.Location = new System.Drawing.Point(0, 0);
            lblFamilyName.Size = new System.Drawing.Size(200, 43);
            lblFamilyName.TabIndex = 0;
            lblFamilyName.Text = "Family Name: Sencio";
            lblFamilyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlFamilyName.Location = new System.Drawing.Point(0, 171);
            pnlFamilyName.Size = new System.Drawing.Size(200, 43);
            pnlFamilyName.TabIndex = 1;

            lblWaterLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            lblWaterLevel.Location = new System.Drawing.Point(0, 0);
            lblWaterLevel.Size = new System.Drawing.Size(200, 43);
            lblWaterLevel.TabIndex = 0;
            lblWaterLevel.Text = "Water Level: 2m";
            lblWaterLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlWaterLevel.Location = new System.Drawing.Point(0, 263);
            pnlWaterLevel.Size = new System.Drawing.Size(200, 43);
            pnlWaterLevel.TabIndex = 1;

            pnlSide.Controls.Add(btnClose);
            pnlType.Controls.Add(lblType);
            pnlSide.Controls.Add(pnlType);
            pnlFamilyName.Controls.Add(lblFamilyName);
            pnlSide.Controls.Add(pnlFamilyName);
            pnlWaterLevel.Controls.Add(lblWaterLevel);
            pnlSide.Controls.Add(pnlWaterLevel);

            // Toggle pnlInfo visibility
            pnlSide.Visible = !pnlSide.Visible; // Show or hide the panel
            if (pnlSide.Visible)
            {
                pnlSide.BringToFront(); // Bring panel to front when visible
            }
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
            Panel pnlCards = new Panel();
            Panel pnlHeader = new Panel();
            Label title = new Label();
            Label lblDirection = new Label();
            Label lblFamilyName = new Label();

            title.Dock = System.Windows.Forms.DockStyle.Fill;
            title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title.Location = new System.Drawing.Point(0, 0);
            title.Size = new System.Drawing.Size(179, 27);
            title.TabIndex = 1;
            title.Text = "For Relief";
            title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Size = new System.Drawing.Size(179, 27);
            pnlHeader.TabIndex = 0;

            lblFamilyName.Location = new System.Drawing.Point(104, 30);
            lblFamilyName.Size = new System.Drawing.Size(72, 60);
            lblFamilyName.TabIndex = 2;
            lblFamilyName.Text = "Sencio";
            lblFamilyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            lblDirection.Location = new System.Drawing.Point(3, 30);
            lblDirection.Size = new System.Drawing.Size(72, 60);
            lblDirection.TabIndex = 1;
            lblDirection.Text = "Family Name";
            lblDirection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlCards.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlCards.Location = new System.Drawing.Point(3, 32);
            pnlCards.Size = new System.Drawing.Size(179, 100);
            pnlCards.TabIndex = 1;

            // Ensure the event is hooked up properly to btnRemove
            pnlCards.Click += new EventHandler(pnlCards_Click);

            flpList.Controls.Add(pnlCards);
            pnlCards.Controls.Add(lblFamilyName);
            pnlCards.Controls.Add(lblDirection);
            pnlCards.Controls.Add(pnlHeader);
            pnlHeader.Controls.Add(title);
        }

        private void pnlCards_Click(object sender, EventArgs e)
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
    }
}
