using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
<<<<<<< Updated upstream
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Device.Location;
=======
>>>>>>> Stashed changes

namespace ARKODesktop.Views
{
    public partial class Operation : Form
    {
        public Operation()
        {
            InitializeComponent();
<<<<<<< Updated upstream
            InitializeMap();
        }

        public void addCardOperations()
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

            lblTimeOn.AutoSize = true;
            lblTimeOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTimeOn.Location = new System.Drawing.Point(20, 103);
            lblTimeOn.Name = "lblTimeOn";
            lblTimeOn.Size = new System.Drawing.Size(43, 16);
            lblTimeOn.TabIndex = 9;
            lblTimeOn.Text = "XX:XX";

            lblTimeOff.AutoSize = true;
            lblTimeOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTimeOff.Location = new System.Drawing.Point(171, 103);
            lblTimeOff.Size = new System.Drawing.Size(43, 16);
            lblTimeOff.TabIndex = 10;
            lblTimeOff.Text = "XX:XX";

            lblTimeOffline.AutoSize = true;
            lblTimeOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTimeOffline.Location = new System.Drawing.Point(156, 76);
            lblTimeOffline.Size = new System.Drawing.Size(79, 16);
            lblTimeOffline.TabIndex = 8;
            lblTimeOffline.Text = "Time Offline";

            lblTimeOnline.AutoSize = true;
            lblTimeOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTimeOnline.Location = new System.Drawing.Point(5, 76);
            lblTimeOnline.Name = "labelTimeOnline";
            lblTimeOnline.Size = new System.Drawing.Size(80, 16);
            lblTimeOnline.TabIndex = 7;
            lblTimeOnline.Text = "Time Online";

            btnShow.Location = new System.Drawing.Point(170, 4);
            btnShow.Size = new System.Drawing.Size(75, 23);
            btnShow.TabIndex = 6;
            btnShow.Text = "Show";
            btnShow.UseVisualStyleBackColor = true;
            btnShow.Click += btnShow_Click;

            lblNumberOfStranded.AutoSize = true;
            lblNumberOfStranded.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblNumberOfStranded.Location = new System.Drawing.Point(3, 139);
            lblNumberOfStranded.Size = new System.Drawing.Size(128, 16);
            lblNumberOfStranded.TabIndex = 5;
            lblNumberOfStranded.Text = "Number of Stranded";

            lblDate.AutoSize = true;
            lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblDate.Location = new System.Drawing.Point(3, 42);
            lblDate.Size = new System.Drawing.Size(37, 16);
            lblDate.TabIndex = 1;
            lblDate.Text = "Date";

            lblOperationID.AutoSize = true;
            lblOperationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblOperationID.Location = new System.Drawing.Point(2, 4);
            lblOperationID.Size = new System.Drawing.Size(115, 24);
            lblOperationID.TabIndex = 0;
            lblOperationID.Text = "Operation ID";

            pnlOperations.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlOperations.Location = new System.Drawing.Point(3, 3);
            pnlOperations.Size = new System.Drawing.Size(248, 166);
            pnlOperations.TabIndex = 0;

            pnlOperations.Controls.Add(lblTimeOff);
            pnlOperations.Controls.Add(lblTimeOn);
            pnlOperations.Controls.Add(lblTimeOnline);
            pnlOperations.Controls.Add(lblTimeOffline);
            pnlOperations.Controls.Add(btnShow);
            pnlOperations.Controls.Add(lblNumberOfStranded);
            pnlOperations.Controls.Add(lblDate);
            pnlOperations.Controls.Add(lblOperationID);
            flpOperations.Controls.Add(pnlOperations);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            
        }
        private void InitializeMap()
        {
            // Set the map provider (Google Maps, Bing Maps, etc.)
            gMap.MapProvider = GMapProviders.GoogleMap;

            // Set the map's initial position and zoom level
            gMap.Position = new PointLatLng(0, 0); // Default position (e.g., equator)
            gMap.MinZoom = 15;
            gMap.MaxZoom = 20;
            gMap.Zoom = 17; // Default zoom level

            // Enable drag, zoom, and mouse wheel controls
            gMap.CanDragMap = true;
            gMap.DragButton = MouseButtons.Left;
            gMap.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            gMap.IgnoreMarkerOnMouseWheel = true;
            gMap.ShowCenter = true;

            // Enable caching
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
        }

        private void CenterMapOnDevice(double latitude, double longitude)
        {
            // Center the map on the device's location
            gMap.Position = new PointLatLng(latitude, longitude);
        }

        private void GetDeviceLocation()
        {
            // Ensure GeoCoordinateWatcher is supported on your platform
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            watcher.PositionChanged += (sender, e) =>
            {
                var location = e.Position.Location;
                if (!location.IsUnknown)
                {
                    double latitude = location.Latitude;
                    double longitude = location.Longitude;

                    // Center map on device location
                    CenterMapOnDevice(latitude, longitude);
                }
                else
                {
                    MessageBox.Show("Location is unknown.");
                }
            };

            watcher.Start();
        }

        private void Operation_Load(object sender, EventArgs e)
        {
            // Dynamically get the device's location
            GetDeviceLocation();
=======
>>>>>>> Stashed changes
        }
    }
}
