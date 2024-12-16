
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.lblIRSensor = new System.Windows.Forms.Label();
            this.lblWaterLvl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
<<<<<<< Updated upstream
            // Controller
=======
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblWaterLvl);
            this.panel1.Controls.Add(this.lblIRSensor);
            this.panel1.Controls.Add(this.lblLongitude);
            this.panel1.Controls.Add(this.lblLatitude);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1896, 71);
            this.panel1.TabIndex = 0;
            // 
            // lblLatitude
            // 
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatitude.Location = new System.Drawing.Point(151, 22);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(92, 25);
            this.lblLatitude.TabIndex = 1;
            this.lblLatitude.Text = "Latitude: ";
            // 
            // lblLongitude
            // 
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLongitude.Location = new System.Drawing.Point(418, 23);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(104, 25);
            this.lblLongitude.TabIndex = 2;
            this.lblLongitude.Text = "Longitude:";
            // 
            // lblIRSensor
            // 
            this.lblIRSensor.AutoSize = true;
            this.lblIRSensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIRSensor.Location = new System.Drawing.Point(990, 22);
            this.lblIRSensor.Name = "lblIRSensor";
            this.lblIRSensor.Size = new System.Drawing.Size(104, 25);
            this.lblIRSensor.TabIndex = 3;
            this.lblIRSensor.Text = "IR Sensor:";
            // 
            // lblWaterLvl
            // 
            this.lblWaterLvl.AutoSize = true;
            this.lblWaterLvl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaterLvl.Location = new System.Drawing.Point(695, 23);
            this.lblWaterLvl.Name = "lblWaterLvl";
            this.lblWaterLvl.Size = new System.Drawing.Size(128, 25);
            this.lblWaterLvl.TabIndex = 4;
            this.lblWaterLvl.Text = "Water Level: ";
            // 
            // Operations
>>>>>>> Stashed changes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
<<<<<<< Updated upstream
            this.ClientSize = new System.Drawing.Size(1600, 900);
=======
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.panel1);
>>>>>>> Stashed changes
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Controller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controller";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblIRSensor;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.Label lblWaterLvl;
    }
}