
namespace ARKODesktop.Views
{
    partial class Devices
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.fplBluetoothDevices = new System.Windows.Forms.FlowLayoutPanel();
            this.btnScanBT = new System.Windows.Forms.Button();
            this.gbVerify = new System.Windows.Forms.GroupBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.lblBTConnection = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeyPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.gbNetConfigure = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnConnectNet = new System.Windows.Forms.Button();
            this.txtSSID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNPass = new System.Windows.Forms.TextBox();
            this.readTimerBT = new System.Windows.Forms.Timer(this.components);
            this.pnlAddDevice = new System.Windows.Forms.Panel();
            this.pnlHeaderAddDevice = new System.Windows.Forms.Panel();
            this.lblAddDeviceHeaderTitle = new System.Windows.Forms.Label();
            this.gbVesselInfo = new System.Windows.Forms.GroupBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblNetwork = new System.Windows.Forms.Label();
            this.lblVesselName = new System.Windows.Forms.Label();
            this.btnAddDevice = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.flpDevices = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDevicesBackground = new System.Windows.Forms.Panel();
            this.pnlDeviceHeader = new System.Windows.Forms.Panel();
            this.lblDeviceHeaderTitle = new System.Windows.Forms.Label();
            this.gbVerify.SuspendLayout();
            this.gbNetConfigure.SuspendLayout();
            this.pnlAddDevice.SuspendLayout();
            this.pnlHeaderAddDevice.SuspendLayout();
            this.gbVesselInfo.SuspendLayout();
            this.pnlDevicesBackground.SuspendLayout();
            this.pnlDeviceHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // fplBluetoothDevices
            // 
            this.fplBluetoothDevices.AutoScroll = true;
            this.fplBluetoothDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fplBluetoothDevices.Location = new System.Drawing.Point(28, 82);
            this.fplBluetoothDevices.Name = "fplBluetoothDevices";
            this.fplBluetoothDevices.Size = new System.Drawing.Size(319, 285);
            this.fplBluetoothDevices.TabIndex = 0;
            // 
            // btnScanBT
            // 
            this.btnScanBT.Location = new System.Drawing.Point(219, 53);
            this.btnScanBT.Name = "btnScanBT";
            this.btnScanBT.Size = new System.Drawing.Size(128, 23);
            this.btnScanBT.TabIndex = 1;
            this.btnScanBT.Text = "Scan Bluetooth";
            this.btnScanBT.UseVisualStyleBackColor = true;
            this.btnScanBT.Click += new System.EventHandler(this.btnScanBT_Click);
            // 
            // gbVerify
            // 
            this.gbVerify.Controls.Add(this.btnVerify);
            this.gbVerify.Controls.Add(this.lblBTConnection);
            this.gbVerify.Controls.Add(this.label2);
            this.gbVerify.Controls.Add(this.txtKeyPass);
            this.gbVerify.Controls.Add(this.label1);
            this.gbVerify.Controls.Add(this.txtKey);
            this.gbVerify.Enabled = false;
            this.gbVerify.Location = new System.Drawing.Point(380, 53);
            this.gbVerify.Name = "gbVerify";
            this.gbVerify.Size = new System.Drawing.Size(426, 129);
            this.gbVerify.TabIndex = 2;
            this.gbVerify.TabStop = false;
            this.gbVerify.Text = "Verify Connection";
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(319, 94);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 5;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // lblBTConnection
            // 
            this.lblBTConnection.AutoSize = true;
            this.lblBTConnection.Location = new System.Drawing.Point(29, 25);
            this.lblBTConnection.Name = "lblBTConnection";
            this.lblBTConnection.Size = new System.Drawing.Size(81, 13);
            this.lblBTConnection.TabIndex = 4;
            this.lblBTConnection.Text = "Connected To: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // txtKeyPass
            // 
            this.txtKeyPass.Location = new System.Drawing.Point(106, 98);
            this.txtKeyPass.Name = "txtKeyPass";
            this.txtKeyPass.PasswordChar = '*';
            this.txtKeyPass.Size = new System.Drawing.Size(163, 20);
            this.txtKeyPass.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Key";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(106, 56);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(163, 20);
            this.txtKey.TabIndex = 0;
            // 
            // gbNetConfigure
            // 
            this.gbNetConfigure.Controls.Add(this.label6);
            this.gbNetConfigure.Controls.Add(this.btnConnectNet);
            this.gbNetConfigure.Controls.Add(this.txtSSID);
            this.gbNetConfigure.Controls.Add(this.label4);
            this.gbNetConfigure.Controls.Add(this.label5);
            this.gbNetConfigure.Controls.Add(this.txtNPass);
            this.gbNetConfigure.Enabled = false;
            this.gbNetConfigure.Location = new System.Drawing.Point(380, 225);
            this.gbNetConfigure.Name = "gbNetConfigure";
            this.gbNetConfigure.Size = new System.Drawing.Size(426, 142);
            this.gbNetConfigure.TabIndex = 3;
            this.gbNetConfigure.TabStop = false;
            this.gbNetConfigure.Text = "Network Configuration";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(29, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Change or set vessel network";
            // 
            // btnConnectNet
            // 
            this.btnConnectNet.Location = new System.Drawing.Point(319, 98);
            this.btnConnectNet.Name = "btnConnectNet";
            this.btnConnectNet.Size = new System.Drawing.Size(75, 23);
            this.btnConnectNet.TabIndex = 10;
            this.btnConnectNet.Text = "Connect";
            this.btnConnectNet.UseVisualStyleBackColor = true;
            // 
            // txtSSID
            // 
            this.txtSSID.Location = new System.Drawing.Point(106, 60);
            this.txtSSID.Name = "txtSSID";
            this.txtSSID.Size = new System.Drawing.Size(163, 20);
            this.txtSSID.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "SSID";
            // 
            // txtNPass
            // 
            this.txtNPass.Location = new System.Drawing.Point(106, 102);
            this.txtNPass.Name = "txtNPass";
            this.txtNPass.PasswordChar = '*';
            this.txtNPass.Size = new System.Drawing.Size(163, 20);
            this.txtNPass.TabIndex = 8;
            // 
            // readTimerBT
            // 
            this.readTimerBT.Enabled = true;
            this.readTimerBT.Interval = 1000;
            this.readTimerBT.Tick += new System.EventHandler(this.readTimerBT_Tick);
            // 
            // pnlAddDevice
            // 
            this.pnlAddDevice.Controls.Add(this.pnlHeaderAddDevice);
            this.pnlAddDevice.Controls.Add(this.gbVesselInfo);
            this.pnlAddDevice.Controls.Add(this.label3);
            this.pnlAddDevice.Controls.Add(this.fplBluetoothDevices);
            this.pnlAddDevice.Controls.Add(this.btnScanBT);
            this.pnlAddDevice.Controls.Add(this.gbNetConfigure);
            this.pnlAddDevice.Controls.Add(this.gbVerify);
            this.pnlAddDevice.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddDevice.Location = new System.Drawing.Point(0, 0);
            this.pnlAddDevice.Name = "pnlAddDevice";
            this.pnlAddDevice.Size = new System.Drawing.Size(1150, 380);
            this.pnlAddDevice.TabIndex = 4;
            // 
            // pnlHeaderAddDevice
            // 
            this.pnlHeaderAddDevice.Controls.Add(this.lblAddDeviceHeaderTitle);
            this.pnlHeaderAddDevice.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaderAddDevice.Location = new System.Drawing.Point(0, 0);
            this.pnlHeaderAddDevice.Name = "pnlHeaderAddDevice";
            this.pnlHeaderAddDevice.Size = new System.Drawing.Size(1150, 43);
            this.pnlHeaderAddDevice.TabIndex = 6;
            // 
            // lblAddDeviceHeaderTitle
            // 
            this.lblAddDeviceHeaderTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblAddDeviceHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddDeviceHeaderTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddDeviceHeaderTitle.Location = new System.Drawing.Point(0, 0);
            this.lblAddDeviceHeaderTitle.Name = "lblAddDeviceHeaderTitle";
            this.lblAddDeviceHeaderTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblAddDeviceHeaderTitle.Size = new System.Drawing.Size(1150, 43);
            this.lblAddDeviceHeaderTitle.TabIndex = 0;
            this.lblAddDeviceHeaderTitle.Text = "Add Device";
            this.lblAddDeviceHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbVesselInfo
            // 
            this.gbVesselInfo.Controls.Add(this.lblIP);
            this.gbVesselInfo.Controls.Add(this.lblNetwork);
            this.gbVesselInfo.Controls.Add(this.lblVesselName);
            this.gbVesselInfo.Controls.Add(this.btnAddDevice);
            this.gbVesselInfo.Enabled = false;
            this.gbVesselInfo.Location = new System.Drawing.Point(812, 130);
            this.gbVesselInfo.Name = "gbVesselInfo";
            this.gbVesselInfo.Size = new System.Drawing.Size(326, 170);
            this.gbVesselInfo.TabIndex = 5;
            this.gbVesselInfo.TabStop = false;
            this.gbVesselInfo.Text = "Vessel Information";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(23, 95);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(71, 15);
            this.lblIP.TabIndex = 8;
            this.lblIP.Text = "IP Address: ";
            // 
            // lblNetwork
            // 
            this.lblNetwork.AutoSize = true;
            this.lblNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetwork.Location = new System.Drawing.Point(23, 67);
            this.lblNetwork.Name = "lblNetwork";
            this.lblNetwork.Size = new System.Drawing.Size(58, 15);
            this.lblNetwork.TabIndex = 7;
            this.lblNetwork.Text = "Network: ";
            // 
            // lblVesselName
            // 
            this.lblVesselName.AutoSize = true;
            this.lblVesselName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVesselName.Location = new System.Drawing.Point(23, 36);
            this.lblVesselName.Name = "lblVesselName";
            this.lblVesselName.Size = new System.Drawing.Size(86, 15);
            this.lblVesselName.TabIndex = 6;
            this.lblVesselName.Text = "Vessel Name: ";
            // 
            // btnAddDevice
            // 
            this.btnAddDevice.Location = new System.Drawing.Point(194, 134);
            this.btnAddDevice.Name = "btnAddDevice";
            this.btnAddDevice.Size = new System.Drawing.Size(126, 23);
            this.btnAddDevice.TabIndex = 6;
            this.btnAddDevice.Text = "Add to Devices";
            this.btnAddDevice.UseVisualStyleBackColor = true;
            this.btnAddDevice.Click += new System.EventHandler(this.btnAddDevice_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "List Bluetooth Devices";
            // 
            // flpDevices
            // 
            this.flpDevices.AutoScroll = true;
            this.flpDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpDevices.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpDevices.Location = new System.Drawing.Point(0, 43);
            this.flpDevices.Name = "flpDevices";
            this.flpDevices.Size = new System.Drawing.Size(1150, 276);
            this.flpDevices.TabIndex = 5;
            // 
            // pnlDevicesBackground
            // 
            this.pnlDevicesBackground.Controls.Add(this.pnlDeviceHeader);
            this.pnlDevicesBackground.Controls.Add(this.flpDevices);
            this.pnlDevicesBackground.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDevicesBackground.Location = new System.Drawing.Point(0, 380);
            this.pnlDevicesBackground.Name = "pnlDevicesBackground";
            this.pnlDevicesBackground.Size = new System.Drawing.Size(1150, 319);
            this.pnlDevicesBackground.TabIndex = 6;
            // 
            // pnlDeviceHeader
            // 
            this.pnlDeviceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnlDeviceHeader.Controls.Add(this.lblDeviceHeaderTitle);
            this.pnlDeviceHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDeviceHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlDeviceHeader.Name = "pnlDeviceHeader";
            this.pnlDeviceHeader.Size = new System.Drawing.Size(1150, 43);
            this.pnlDeviceHeader.TabIndex = 0;
            // 
            // lblDeviceHeaderTitle
            // 
            this.lblDeviceHeaderTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(194)))), ((int)(((byte)(236)))));
            this.lblDeviceHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDeviceHeaderTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeviceHeaderTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDeviceHeaderTitle.Name = "lblDeviceHeaderTitle";
            this.lblDeviceHeaderTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblDeviceHeaderTitle.Size = new System.Drawing.Size(1150, 43);
            this.lblDeviceHeaderTitle.TabIndex = 0;
            this.lblDeviceHeaderTitle.Text = "Device List";
            this.lblDeviceHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Devices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 699);
            this.Controls.Add(this.pnlAddDevice);
            this.Controls.Add(this.pnlDevicesBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Devices";
            this.Text = "Devices";
            this.gbVerify.ResumeLayout(false);
            this.gbVerify.PerformLayout();
            this.gbNetConfigure.ResumeLayout(false);
            this.gbNetConfigure.PerformLayout();
            this.pnlAddDevice.ResumeLayout(false);
            this.pnlAddDevice.PerformLayout();
            this.pnlHeaderAddDevice.ResumeLayout(false);
            this.gbVesselInfo.ResumeLayout(false);
            this.gbVesselInfo.PerformLayout();
            this.pnlDevicesBackground.ResumeLayout(false);
            this.pnlDeviceHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fplBluetoothDevices;
        private System.Windows.Forms.Button btnScanBT;
        private System.Windows.Forms.GroupBox gbVerify;
        private System.Windows.Forms.GroupBox gbNetConfigure;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Label lblBTConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKeyPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnConnectNet;
        private System.Windows.Forms.TextBox txtSSID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNPass;
        private System.Windows.Forms.Timer readTimerBT;
        private System.Windows.Forms.Panel pnlAddDevice;
        private System.Windows.Forms.GroupBox gbVesselInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flpDevices;
        private System.Windows.Forms.Button btnAddDevice;
        private System.Windows.Forms.Label lblNetwork;
        private System.Windows.Forms.Label lblVesselName;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlDevicesBackground;
        private System.Windows.Forms.Panel pnlDeviceHeader;
        private System.Windows.Forms.Label lblDeviceHeaderTitle;
        private System.Windows.Forms.Panel pnlHeaderAddDevice;
        private System.Windows.Forms.Label lblAddDeviceHeaderTitle;
    }
}