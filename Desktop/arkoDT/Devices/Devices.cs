using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace arkoDT
{
    public partial class frmDevices : Form
    {
        private BluetoothClient bluetoothClient = new BluetoothClient();
        public frmDevices()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UpdateDeviceCards();
        }

        public void UpdateDeviceCards()
        {
            // Get available Bluetooth devices
            List<BluetoothDeviceInfo> devices = GetAvailableBluetoothDevices();

            if (devices.Count == 0)
            {
                MessageBox.Show("No Bluetooth devices found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Clear existing cards from the flow layout panel
            flpDevices.Controls.Clear();

            // Iterate through each device and create cards
            foreach (var device in devices)
            {
                Panel pnlCards = new Panel();
                Panel pnlHeader = new Panel();
                Label Title = new Label();
                Label lblDeviceName = new Label();
                Label lblStatus = new Label();
                Button btnDisconnect = new Button();
                Button btnConnect = new Button();

                Title.Dock = DockStyle.Fill;
                Title.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                Title.Location = new Point(0, 0);
                Title.Size = new Size(310, 41);
                Title.TabIndex = 0;
                Title.Text = "Devices";
                Title.TextAlign = ContentAlignment.MiddleCenter;

                pnlHeader.AutoScroll = true;
                pnlHeader.Location = new Point(0, 0);
                pnlHeader.Size = new Size(310, 41);
                pnlHeader.TabIndex = 1;

                // Change pnlHeader background color based on connection status
                pnlHeader.BackColor = device.Connected ? Color.LightGreen : Color.LightCoral;

                btnDisconnect.Location = new Point(3, 143);
                btnDisconnect.Size = new Size(75, 23);
                btnDisconnect.TabIndex = 2;
                btnDisconnect.Text = "Disconnect";
                btnDisconnect.UseVisualStyleBackColor = true;

                // Event handler for Disconnect button
                btnDisconnect.Click += (s, e) =>
                {
                    try
                    {
                        if (device.Connected)
                        {
                            // Disconnect the device
                            bluetoothClient.Close();

                            MessageBox.Show($"{device.DeviceName} disconnected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Update UI elements for disconnection
                            pnlHeader.BackColor = Color.LightCoral;
                            lblStatus.Text = "Status: Not Connected";
                        }
                        else
                        {
                            MessageBox.Show($"{device.DeviceName} is not connected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                btnConnect.Location = new Point(221, 143);
                btnConnect.Size = new Size(86, 23);
                btnConnect.TabIndex = 3;
                btnConnect.Text = "Connect";
                btnConnect.UseVisualStyleBackColor = true;

                // Event handler for Connect button
                btnConnect.Click += (s, e) =>
                {
                    try
                    {
                        MessageBox.Show($"Connecting to {device.DeviceName}...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Specify the service to connect to
                        var service = BluetoothService.SerialPort;
                        bluetoothClient.Connect(device.DeviceAddress, service);

                        MessageBox.Show($"{device.DeviceName} connected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Update UI elements for connection
                        pnlHeader.BackColor = Color.LightGreen;
                        lblStatus.Text = "Status: Connected";

                        //timerReceiver.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                lblStatus.AutoSize = true;
                lblStatus.Font = new Font("Microsoft Sans Serif", 15.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                lblStatus.Location = new Point(215, 90);
                lblStatus.Size = new Size(92, 31);
                lblStatus.TabIndex = 3;
                lblStatus.Text = device.Connected ? "Status: Connected" : "Status: Not Connected";

                lblDeviceName.AutoSize = true;
                lblDeviceName.Font = new Font("Microsoft Sans Serif", 15.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                lblDeviceName.Location = new Point(6, 90);
                lblDeviceName.Size = new Size(177, 31);
                lblDeviceName.TabIndex = 2;
                lblDeviceName.Text = $"Device Name: {device.DeviceName}";

                pnlCards.AutoScroll = true;
                pnlCards.BackColor = SystemColors.ControlLightLight;
                pnlCards.Location = new Point(3, 3);
                pnlCards.Size = new Size(310, 169);
                pnlCards.TabIndex = 0;

                pnlHeader.Controls.Add(Title);
                pnlCards.Controls.Add(btnDisconnect);
                pnlCards.Controls.Add(btnConnect);
                pnlCards.Controls.Add(lblStatus);
                pnlCards.Controls.Add(lblDeviceName);
                pnlCards.Controls.Add(pnlHeader);
                flpDevices.Controls.Add(pnlCards);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //// Get the parent panel (card) of the clicked button
            //Button btnConnect = sender as Button;
            //if (btnConnect == null) return;

            //Panel pnlCard = btnConnect.Parent as Panel;
            //if (pnlCard == null) return;

            //// Find the device name from the card's label
            //Label lblDeviceName = pnlCard.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Text.StartsWith("Device Name:"));
            //if (lblDeviceName == null) return;

            //string deviceName = lblDeviceName.Text.Replace("Device Name:", "").Trim();

            //try
            //{
            //    // Get the Bluetooth device by name
            //    List<BluetoothDeviceInfo> devices = GetAvailableBluetoothDevices();
            //    var deviceToConnect = devices.FirstOrDefault(d => d.DeviceName == deviceName);

            //    if (deviceToConnect == null)
            //    {
            //        MessageBox.Show("Device not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    // Attempt to connect to the device
            //    using (BluetoothClient client = new BluetoothClient())
            //    {
            //        client.Connect(deviceToConnect.DeviceAddress, BluetoothService.SerialPort);
            //    }

            //    MessageBox.Show($"Successfully connected to {deviceName}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    // Refresh the cards to update the connection status
            //    UpdateDeviceCards();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Failed to connect to {deviceName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private List<BluetoothDeviceInfo> GetAvailableBluetoothDevices()
        {
            List<BluetoothDeviceInfo> devices = new List<BluetoothDeviceInfo>();

            try
            {
                // Use the class-level bluetoothClient instance
                devices.AddRange(bluetoothClient.DiscoverDevices());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error discovering devices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return devices;
        }

        private void frmDevices_Load(object sender, EventArgs e)
        {
        }
    }
}
