using System;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using ARKODesktop.Controller;
using Newtonsoft.Json;
using ARKODesktop.Views.JsonDataAccess;
using ARKODesktop.Controller.DAO;

namespace ARKODesktop.Views
{
    public partial class Devices : Form
    {
        private BluetoothComms btComms;
        private VesselDataAccess vesselDAO;
        public Devices()
        {
            InitializeComponent();
            btComms = new BluetoothComms();
            vesselDAO = new VesselDataAccess();
        }

        void addCardBluetooth(BluetoothDeviceInfo device)
        {
            Label label = new Label();
            Button button = new Button();
            Panel panel = new Panel();

            label.AutoSize = true;
            label.Location = new System.Drawing.Point(9, 9);
            label.Size = new System.Drawing.Size(35, 13);
            label.TabIndex = 0;
            label.Text = device.DeviceName;

            button.Location = new System.Drawing.Point(12, 35);
            button.Size = new System.Drawing.Size(75, 23);
            button.TabIndex = 1;
            button.Text = "Connect";
            button.Tag = device;
            button.Click += btnConnect_Click;
            button.UseVisualStyleBackColor = true;

            panel.BackColor = System.Drawing.SystemColors.HighlightText;
            panel.Controls.Add(button);
            panel.Controls.Add(label);
            panel.Location = new System.Drawing.Point(504, 128);
            panel.Margin = new System.Windows.Forms.Padding(0);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Size = new System.Drawing.Size(247, 72);
            panel.TabIndex = 0;

            fplBluetoothDevices.Controls.Add(panel);
        }

        public void addCardDevices()
        {
            Panel pnlCard = new Panel();
            Label lblVesselName = new Label();
            Label lblNetworkName = new Label();
            Panel pnlPlacer = new Panel();
            Label lblStatus = new Label();
            Button btnControl = new Button();
            Button btnManage = new Button();

            btnControl.Location = new System.Drawing.Point(273, 117);
            btnControl.Name = "btnControl";
            btnControl.Size = new System.Drawing.Size(75, 23);
            btnControl.TabIndex = 4;
            btnControl.Text = "Control";
            btnControl.UseVisualStyleBackColor = true;

            btnManage.Location = new System.Drawing.Point(192, 117);
            btnManage.Name = "btnManage";
            btnManage.Size = new System.Drawing.Size(75, 23);
            btnManage.TabIndex = 3;
            btnManage.Text = "Manage";
            btnManage.UseVisualStyleBackColor = true;

            lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblStatus.Location = new System.Drawing.Point(0, 0);
            lblStatus.Size = new System.Drawing.Size(130, 29);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "●   Connected";
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            pnlPlacer.Location = new System.Drawing.Point(216, 3);
            pnlPlacer.Size = new System.Drawing.Size(130, 29);
            pnlPlacer.TabIndex = 5;

            lblNetworkName.AutoSize = true;
            lblNetworkName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblNetworkName.Location = new System.Drawing.Point(3, 38);
            lblNetworkName.Name = "lblNetworkName";
            lblNetworkName.Size = new System.Drawing.Size(97, 16);
            lblNetworkName.TabIndex = 1;
            lblNetworkName.Text = "Network Name";

            lblVesselName.AutoSize = true;
            lblVesselName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblVesselName.Location = new System.Drawing.Point(3, 5);
            lblVesselName.Size = new System.Drawing.Size(123, 24);
            lblVesselName.TabIndex = 0;
            lblVesselName.Text = "Vessel Name";

            pnlCard.BackColor = System.Drawing.SystemColors.ControlLightLight;
            pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlCard.Location = new System.Drawing.Point(10, 10);
            pnlCard.Margin = new System.Windows.Forms.Padding(10);
            pnlCard.Size = new System.Drawing.Size(351, 147);
            pnlCard.TabIndex = 0;

            pnlPlacer.Controls.Add(lblStatus);
            pnlCard.Controls.Add(pnlPlacer);
            pnlCard.Controls.Add(btnControl);
            pnlCard.Controls.Add(btnManage);
            pnlCard.Controls.Add(lblNetworkName);
            pnlCard.Controls.Add(lblVesselName);
            flpDevices.Controls.Add(pnlCard);
        }

        public void setVesselInfo()
        {
            gbVesselInfo.Enabled = true;
            gbNetConfigure.Enabled = true;
            lblVesselName.Text = "Vessel Name: " + btComms.DeviceName;
            lblNetwork.Text = "Network: " + btComms.DeviceNetwork;
            lblIP.Text = "IP Address: " + btComms.IpAddress;
            
        }

        private async void btnScanBT_Click(object sender, EventArgs e)
        {
            BluetoothDeviceInfo[] deviceList =  await btComms.DiscoverDevicesAsync();

            foreach (var device in deviceList)
            {
                addCardBluetooth(device);
            }
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                await btComms.SendDataAsync(
                                JsonConvert.SerializeObject(new
                                {
                                    command = "Verify",
                                    key = txtKey.Text,
                                    password = txtKeyPass.Text
                                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Error Sending Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            BluetoothDeviceInfo device = button.Tag as BluetoothDeviceInfo;
            bool result = await btComms.ConnectToDeviceAsync(device.DeviceAddress);

            if (result)
            {
                lblBTConnection.Text = $"Connected to: {device.DeviceName}" ;
                gbVerify.Enabled = true;
                MessageBox.Show($"Bluetooth successfully connected to: {device.DeviceName}", "Bluetooth Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblBTConnection.Text = $"Connected to: ";
                gbVerify.Enabled = false;
                MessageBox.Show($"Cannot connect to : {device.DeviceName}" , "Bluetooth Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void readTimerBT_Tick(object sender, EventArgs e)
        {
            String rMessage = await btComms.ReceiveDataAsync();
            
            
            if (!string.IsNullOrEmpty(rMessage))
            {
                try
                {
                    BTVerify respond = JsonConvert.DeserializeObject<BTVerify>(rMessage);

                    if (respond.response)
                    {
                        btComms.Token = respond.token;
                        btComms.DeviceName = respond.vessel_name;
                        btComms.DeviceNetwork = respond.wifi_ssid;
                        btComms.IpAddress = respond.ip_address;
                        MessageBox.Show($"Verified Successfuly", "Verification Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        setVesselInfo();
                    }
                    else
                    {
                        MessageBox.Show($"Invalid Credentials", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
            
        }

        private void btnAddDevice_Click(object sender, EventArgs e)
        {
            if (vesselDAO.AddVessel(btComms))
            {
                MessageBox.Show($"New Device Successfuly Added", "Insertion Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Failed to Insert Device", "Insertion Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
