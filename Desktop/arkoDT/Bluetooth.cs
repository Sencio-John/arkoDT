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
    public partial class frmBluetooth : Form
    {

        private BluetoothClient bluetoothClient;
        private BluetoothDeviceInfo[] devices;

        public frmBluetooth()
        {
            InitializeComponent();
            bluetoothClient = new BluetoothClient();
        }

        private void btnDiscover_Click(object sender, EventArgs e)
        {
            try
            {
                txtStatus.Text = "Discovering devices...";
                devices = bluetoothClient.DiscoverDevices();

                lstDevices.Items.Clear();
                foreach (var device in devices)
                {
                    lstDevices.Items.Add($"{device.DeviceName} ({device.DeviceAddress})");
                }

                txtStatus.Text = "Discovery complete.";
            }
            catch (Exception ex)
            {
                txtStatus.Text = $"Error: {ex.Message}";
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex < 0)
            {
                txtStatus.Text = "Please select a device.";
                return;
            }

            try
            {
                var selectedDevice = devices[lstDevices.SelectedIndex];
                txtStatus.Text = $"Connecting to {selectedDevice.DeviceName}...";

                // Specify the service to connect to
                var service = BluetoothService.SerialPort;
                bluetoothClient.Connect(selectedDevice.DeviceAddress, service);

                txtStatus.Text = $"Connected to {selectedDevice.DeviceName}.";

                timerReceiver.Start();
            }
            catch (Exception ex)
            {
                txtStatus.Text = $"Error: {ex.Message}";
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (bluetoothClient != null && bluetoothClient.Connected)
                {
                    bluetoothClient.Close();
                    txtStatus.Text = "Disconnected from the device.";
                    timerReceiver.Stop();
                }
                else
                {
                    txtStatus.Text = "No active connection to disconnect.";
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = $"Error while disconnecting: {ex.Message}";
            }
        }

        private void btnSendBT_Click(object sender, EventArgs e)
        {
            String data = String.Concat("Verify,", txtKey.Text, "," ,txtPass.Text);


            SendData(data);
        }

        private void SendData(string data)
        {
            try
            {
                if (bluetoothClient != null && bluetoothClient.Connected)
                {
                    // Get the stream and send data
                    var stream = bluetoothClient.GetStream();
                    byte[] buffer = Encoding.UTF8.GetBytes(data);
                    stream.Write(buffer, 0, buffer.Length);
                    txtStatus.Text = "Data sent successfully.";
                }
                else
                {
                    lblStatus.Text = "No active Bluetooth connection.";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error while sending data: {ex.Message}";
            }
        }
        private void ReceiveData()
        {
            try
            {
                if (bluetoothClient != null && bluetoothClient.Connected)
                {
                    // Get the stream and read data
                    var stream = bluetoothClient.GetStream();
                    if (stream.DataAvailable)
                    {
                        byte[] buffer = new byte[1024]; // Adjust size as needed
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        lblStatus.Text = $"Received data: {receivedData}";
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error while receiving data: {ex.Message}";
            }
        }

        private void timerReceiver_Tick(object sender, EventArgs e)
        {
            ReceiveData();
        }
    }
}
