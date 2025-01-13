using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Device.Location;
using System.Data.SQLite;

namespace ARKODesktop.Views
{
    public partial class Operation : Form
    {
        private string connectionString = $@"Data Source={Application.StartupPath}\ArkoDB.db;Version=3;";
        public Operation()
        {
            InitializeComponent();
            InitializeMap();
            addCardOperations();
        }
        public void addCardOperations()
        {
            flpOperations.Controls.Clear();
            // Fetch data from the database
            //List<(int stranded_id, string date_created)> operations = GetOperationsFromDatabase();

            foreach (var operation in operations)
            {
                Panel pnlOperations = new Panel();
                Label lblOperationID = new Label();
                Label lblDate = new Label();
                Label lblNumberOfStranded = new Label();
                Label lblTimeOnline = new Label();
                Label lblTimeOffline = new Label();
                Label lblTimeOff = new Label();
                Label lblTimeOn = new Label();
                Button btnShow = new Button();

                // Set values from the database
                lblOperationID.Text = $"ID: {operation.stranded_id}";
                lblDate.Text = $"Date: {operation.date_created}";
                lblTimeOn.Text = "XX:XX";
                lblTimeOff.Text = "XX:XX";
                lblTimeOnline.Text = "Time Online";
                lblTimeOffline.Text = "Time Offline";
                lblNumberOfStranded.Text = "Number of Stranded";

                // Set styles and locations
                lblTimeOn.AutoSize = true;
                lblTimeOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblTimeOn.Location = new System.Drawing.Point(20, 103);

                lblTimeOff.AutoSize = true;
                lblTimeOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblTimeOff.Location = new System.Drawing.Point(171, 103);

                lblTimeOffline.AutoSize = true;
                lblTimeOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblTimeOffline.Location = new System.Drawing.Point(156, 76);

                lblTimeOnline.AutoSize = true;
                lblTimeOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblTimeOnline.Location = new System.Drawing.Point(5, 76);

                lblNumberOfStranded.AutoSize = true;
                lblNumberOfStranded.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblNumberOfStranded.Location = new System.Drawing.Point(3, 139);

                lblDate.AutoSize = true;
                lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblDate.Location = new System.Drawing.Point(3, 42);

                lblOperationID.AutoSize = true;
                lblOperationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblOperationID.Location = new System.Drawing.Point(2, 4);

                btnShow.Location = new System.Drawing.Point(170, 4);
                btnShow.Size = new System.Drawing.Size(75, 23);
                btnShow.Text = "Show";
                btnShow.UseVisualStyleBackColor = true;
                btnShow.Tag = operation.stranded_id;
                btnShow.Click += btnShow_Click;

                // Add controls to the panel
                pnlOperations.BackColor = System.Drawing.SystemColors.ControlLightLight;
                pnlOperations.Location = new System.Drawing.Point(3, 3);
                pnlOperations.Size = new System.Drawing.Size(248, 166);
                pnlOperations.Controls.Add(lblTimeOff);
                pnlOperations.Controls.Add(lblTimeOn);
                pnlOperations.Controls.Add(lblTimeOnline);
                pnlOperations.Controls.Add(lblTimeOffline);
                pnlOperations.Controls.Add(btnShow);
                pnlOperations.Controls.Add(lblNumberOfStranded);
                pnlOperations.Controls.Add(lblDate);
                pnlOperations.Controls.Add(lblOperationID);

                // Add panel to the flow layout panel
                flpOperations.Controls.Add(pnlOperations);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int strandedId)
            {
                // Locate the marker with the matching stranded_id
                foreach (var overlay in gMap.Overlays)
                {
                    foreach (var marker in overlay.Markers)
                    {
                        if (marker.Tag is int markerStrandedId && markerStrandedId == strandedId)
                        {
                            // Focus the map on the marker
                            gMap.Position = marker.Position;

                            // Optional: Adjust zoom to highlight the marker
                            gMap.Zoom = 17;

                            // Optional: Add a visual effect to the marker (e.g., bounce or highlight)
                            MessageBox.Show($"Focused on marker with stranded_id: {strandedId}");

                            return;
                        }
                    }
                }

                MessageBox.Show("Marker not found.");
            }
        }

        private void InitializeMap()
        {
            //gMap.MapProvider = GMapProviders.GoogleMap;
            //gMap.Position = new PointLatLng(0, 0); // Default position
            //gMap.MinZoom = 15;
            //gMap.MaxZoom = 20;
            //gMap.Zoom = 17;

            //gMap.CanDragMap = true;
            //gMap.DragButton = MouseButtons.Left;
            //gMap.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            //gMap.IgnoreMarkerOnMouseWheel = true;
            //gMap.ShowCenter = true;

            //GMaps.Instance.Mode = AccessMode.ServerAndCache;

            //GMapOverlay markersOverlay = new GMapOverlay("markers");

            //List<(double latitude, double longitude, int stranded_id)> locations = GetLocationsFromDatabase();

            //foreach (var location in locations)
            //{
            //    GMapMarker marker = new GMarkerGoogle(
            //        new PointLatLng(location.latitude, location.longitude),
            //        GMarkerGoogleType.red_dot
            //    );

            //    // Store stranded_id in Tag
            //    marker.Tag = location.stranded_id;

            //    markersOverlay.Markers.Add(marker);
            //    marker.ToolTipText = $"Stranded ID: {location.stranded_id}";
            //    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            //}

            //gMap.Overlays.Add(markersOverlay);

            //// Attach the OnMarkerClick event handler
            //gMap.OnMarkerClick += GMap_OnMarkerClick;
        }

        private void CenterMapOnDevice(double latitude, double longitude)
        {
            //// Center the map on the device's location
            //gMap.Position = new PointLatLng(latitude, longitude);
        }

        private void GetDeviceLocation()
        {
            //// Ensure GeoCoordinateWatcher is supported on your platform
            //GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            //watcher.PositionChanged += (sender, e) =>
            //{
            //    var location = e.Position.Location;
            //    if (!location.IsUnknown)
            //    {
            //        double latitude = location.Latitude;
            //        double longitude = location.Longitude;

            //        // Center map on device location
            //        CenterMapOnDevice(latitude, longitude);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Location is unknown.");
            //    }
            //};

            //watcher.Start();
        }

        private void Operation_Load(object sender, EventArgs e)
        {
            //GetDeviceLocation();
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            //// Initialize GeoCoordinateWatcher to get the current device location
            //GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            //watcher.PositionChanged += (s, ev) =>
            //{
            //    var location = ev.Position.Location;
            //    if (!location.IsUnknown)
            //    {
            //        double latitude = location.Latitude;
            //        double longitude = location.Longitude;

            //        // Add a marker to the map
            //        GMapMarker marker = new GMarkerGoogle(
            //            new PointLatLng(latitude, longitude),
            //            GMarkerGoogleType.red_dot
            //        );
            //        GMapOverlay markersOverlay = new GMapOverlay("markers");
            //        markersOverlay.Markers.Add(marker);
            //        gMap.Overlays.Add(markersOverlay);

            //        // Save location to the SQLite database
            //        SaveLocationToDatabase(latitude, longitude);

            //        // Stop the watcher after getting the location
            //        watcher.Stop();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Location is unknown.");
            //    }
            //};

            //watcher.Start();
        }

        private void SaveLocationToDatabase(double latitude, double longitude)
        {
            //using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            //{
            //    conn.Open();
            //    string status = "Active";

            //    string query = "INSERT INTO Strandeds (latitude, longitude, date_created, time_created, status) VALUES (@latitude, @longitude, @date_created, @time_created, @status)";
            //    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            //    {
            //        cmd.Parameters.AddWithValue("@latitude", latitude);
            //        cmd.Parameters.AddWithValue("@longitude", longitude);
            //        cmd.Parameters.AddWithValue("@date_created", DateTime.Now.ToString("yyyy-MM-dd"));
            //        cmd.Parameters.AddWithValue("@time_created", DateTime.Now.ToString("HH:mm:ss"));
            //        cmd.Parameters.AddWithValue("@status", status);

            //        cmd.ExecuteNonQuery();
            //    }
            //}

            //MessageBox.Show("Location saved successfully!");
            //addCardOperations();
        }

        private void HighlightCardByStrandedId(int strandedId)
        {
            foreach (Control control in flpOperations.Controls)
            {
                if (control is Panel panel)
                {
                    // Find the label with Operation ID
                    Label lblOperationID = panel.Controls.OfType<Label>()
                        .FirstOrDefault(lbl => lbl.Name == "lblOperationID");

                    if (lblOperationID != null && int.TryParse(lblOperationID.Text.Replace("ID: ", ""), out int operationId))
                    {
                        // Check if the card matches the stranded_id
                        if (operationId == strandedId)
                        {
                            panel.BackColor = Color.LightBlue; // Highlight color
                        }
                        else
                        {
                            panel.BackColor = SystemColors.ControlLightLight; // Default color
                        }
                    }
                }
            }
        }

        private void GMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item.Tag is int strandedId)
            {
                HighlightCardByStrandedId(strandedId);
            }
        }

        //private List<(int stranded_id, string date_created)> GetOperationsFromDatabase()
        //{
        //    List<(int stranded_id, string date_created)> operations = new List<(int, string)>();

        //    using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = "SELECT stranded_id, date_created FROM Strandeds";

        //        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
        //        {
        //            using (SQLiteDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int stranded_id = reader.GetInt32(0);
        //                    string date_created = reader.GetString(1);
        //                    operations.Add((stranded_id, date_created));
        //                }
        //            }
        //        }
        //    }

        //    return operations;
        //}

        //private List<(double latitude, double longitude, int stranded_id)> GetLocationsFromDatabase()
        //{
        //    List<(double latitude, double longitude, int stranded_id)> locations = new List<(double, double, int)>();

        //    using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = "SELECT latitude, longitude, stranded_id FROM Strandeds";

        //        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
        //        {
        //            using (SQLiteDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    string latitudeStr = reader.GetString(0);
        //                    string longitudeStr = reader.GetString(1);
        //                    int stranded_id = reader.GetInt32(2);

        //                    if (double.TryParse(latitudeStr, out double latitude) &&
        //                        double.TryParse(longitudeStr, out double longitude))
        //                    {
        //                        locations.Add((latitude, longitude, stranded_id));
        //                    }
        //                    else
        //                    {
        //                        // Handle invalid or missing data if needed
        //                        MessageBox.Show($"Invalid location data: {latitudeStr}, {longitudeStr}");
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return locations;
        //}

    }
}
