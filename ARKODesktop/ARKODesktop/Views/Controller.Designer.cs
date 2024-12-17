
namespace ARKODesktop
{
    partial class Operations
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
            this.pnlControlData = new System.Windows.Forms.Panel();
            this.lblIRSensor = new System.Windows.Forms.Label();
            this.lblWaterLvl = new System.Windows.Forms.Label();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.pcbCam = new System.Windows.Forms.PictureBox();
            this.pnlControlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCam)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControlData
            // 
            this.pnlControlData.Controls.Add(this.lblIRSensor);
            this.pnlControlData.Controls.Add(this.lblWaterLvl);
            this.pnlControlData.Controls.Add(this.lblLongitude);
            this.pnlControlData.Controls.Add(this.lblLatitude);
            this.pnlControlData.Location = new System.Drawing.Point(12, 12);
            this.pnlControlData.Name = "pnlControlData";
            this.pnlControlData.Size = new System.Drawing.Size(386, 188);
            this.pnlControlData.TabIndex = 0;
            // 
            // lblIRSensor
            // 
            this.lblIRSensor.AutoSize = true;
            this.lblIRSensor.BackColor = System.Drawing.Color.Transparent;
            this.lblIRSensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIRSensor.Location = new System.Drawing.Point(3, 141);
            this.lblIRSensor.Name = "lblIRSensor";
            this.lblIRSensor.Size = new System.Drawing.Size(112, 25);
            this.lblIRSensor.TabIndex = 4;
            this.lblIRSensor.Text = "IR Sensor:";
            // 
            // lblWaterLvl
            // 
            this.lblWaterLvl.AutoSize = true;
            this.lblWaterLvl.BackColor = System.Drawing.Color.Transparent;
            this.lblWaterLvl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaterLvl.Location = new System.Drawing.Point(3, 104);
            this.lblWaterLvl.Name = "lblWaterLvl";
            this.lblWaterLvl.Size = new System.Drawing.Size(133, 25);
            this.lblWaterLvl.TabIndex = 3;
            this.lblWaterLvl.Text = "Water Level:";
            // 
            // lblLongitude
            // 
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.BackColor = System.Drawing.Color.Transparent;
            this.lblLongitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLongitude.Location = new System.Drawing.Point(3, 64);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(107, 25);
            this.lblLongitude.TabIndex = 2;
            this.lblLongitude.Text = "Longitude";
            // 
            // lblLatitude
            // 
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.BackColor = System.Drawing.Color.Transparent;
            this.lblLatitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatitude.Location = new System.Drawing.Point(3, 23);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(95, 25);
            this.lblLatitude.TabIndex = 1;
            this.lblLatitude.Text = "Latitude:";
            // 
            // pcbCam
            // 
            this.pcbCam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcbCam.Location = new System.Drawing.Point(0, 0);
            this.pcbCam.Name = "pcbCam";
            this.pcbCam.Size = new System.Drawing.Size(1600, 900);
            this.pcbCam.TabIndex = 1;
            this.pcbCam.TabStop = false;
            // 
            // Operations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.pnlControlData);
            this.Controls.Add(this.pcbCam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Operations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controller";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlControlData.ResumeLayout(false);
            this.pnlControlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControlData;
        private System.Windows.Forms.Label lblIRSensor;
        private System.Windows.Forms.Label lblWaterLvl;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.PictureBox pcbCam;
    }
}