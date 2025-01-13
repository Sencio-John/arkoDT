
namespace arkoDT
{
    partial class frmBluetooth
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
            this.lstDevices = new System.Windows.Forms.ListBox();
            this.btnDiscover = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnSendBT = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timerReceiver = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lstDevices
            // 
            this.lstDevices.FormattingEnabled = true;
            this.lstDevices.Location = new System.Drawing.Point(40, 48);
            this.lstDevices.Name = "lstDevices";
            this.lstDevices.Size = new System.Drawing.Size(235, 290);
            this.lstDevices.TabIndex = 0;
            // 
            // btnDiscover
            // 
            this.btnDiscover.Location = new System.Drawing.Point(503, 102);
            this.btnDiscover.Name = "btnDiscover";
            this.btnDiscover.Size = new System.Drawing.Size(75, 23);
            this.btnDiscover.TabIndex = 1;
            this.btnDiscover.Text = "btnDiscover";
            this.btnDiscover.UseVisualStyleBackColor = true;
            this.btnDiscover.Click += new System.EventHandler(this.btnDiscover_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(503, 146);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "btnConnect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.Location = new System.Drawing.Point(399, 223);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(35, 13);
            this.txtStatus.TabIndex = 3;
            this.txtStatus.Text = "label1";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(503, 190);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(477, 248);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(137, 20);
            this.txtKey.TabIndex = 5;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(477, 284);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(137, 20);
            this.txtPass.TabIndex = 6;
            // 
            // btnSendBT
            // 
            this.btnSendBT.Location = new System.Drawing.Point(503, 338);
            this.btnSendBT.Name = "btnSendBT";
            this.btnSendBT.Size = new System.Drawing.Size(75, 23);
            this.btnSendBT.TabIndex = 7;
            this.btnSendBT.Text = "Send Data";
            this.btnSendBT.UseVisualStyleBackColor = true;
            this.btnSendBT.Click += new System.EventHandler(this.btnSendBT_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(522, 386);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 13);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Response";
            // 
            // timerReceiver
            // 
            this.timerReceiver.Tick += new System.EventHandler(this.timerReceiver_Tick);
            // 
            // frmBluetooth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSendBT);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnDiscover);
            this.Controls.Add(this.lstDevices);
            this.Name = "frmBluetooth";
            this.Text = "Bluetooth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstDevices;
        private System.Windows.Forms.Button btnDiscover;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnSendBT;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timerReceiver;
    }
}