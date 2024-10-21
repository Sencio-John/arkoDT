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
            InitializeSidebar(); // Initialize sidebar

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
                map.MaxZoom = 17;
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
                if (infoForm == null || infoForm.IsDisposed)
                {
                    infoForm = new frmInfo(); // Create a new instance of frmInfo
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
            // Example values; replace with actual values based on the pin clicked
            string population = "Population: 5000";
            string currentWaterLevel = "Current Water Level: 12m";
            string averageWaterLevel = "Average Water Level: 10m";

            // Update pnlInfo with actual data
            pnlInfo.Controls.Clear(); // Clear existing controls
            pnlInfo.Controls.Add(new Label { Text = population, Dock = DockStyle.Top, Height = 30 });
            pnlInfo.Controls.Add(new Label { Text = currentWaterLevel, Dock = DockStyle.Top, Height = 30 });
            var lblAve = new Label { Text = averageWaterLevel, Dock = DockStyle.Top, Height = 30 };
            lblAve.Cursor = Cursors.Hand; // Change the cursor to indicate it is clickable
            lblAve.Click += lblAve_Click; // Attach click event handler
            pnlInfo.Controls.Add(lblAve);

            // Ensure the close button is still present
            pnlInfo.Controls.Add(new Button { Text = "Close", Dock = DockStyle.Top, Height = 30 });

            // Toggle pnlInfo visibility
            pnlInfo.Visible = !pnlInfo.Visible; // Show or hide the panel
            if (pnlInfo.Visible)
            {
                pnlInfo.BringToFront(); // Bring panel to front when visible
            }

            // Wire the close button's click event to hide the panel
            var closeButton = (Button)pnlInfo.Controls[pnlInfo.Controls.Count - 1]; // Get the last button added
            closeButton.Click += (s, e) => { pnlInfo.Visible = false; isSidebarVisible = false; }; // Hide the panel on click
        }

        private void InitializeSidebar()
        {
            // Ensure pnlInfo is set to the correct properties
            pnlInfo.Width = 200; // Set the desired width for the sidebar
            pnlInfo.Dock = DockStyle.Left; // Dock to the left side
            pnlInfo.BackColor = Color.LightGray; // Set a background color if needed
            pnlInfo.Visible = false; // Hide initially
        }
        private void lblAve_Click(object sender, EventArgs e)
        {
            frmGraph form1 = new frmGraph();
            form1.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
