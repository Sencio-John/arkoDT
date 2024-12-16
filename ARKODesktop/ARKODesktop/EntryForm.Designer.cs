
namespace ARKODesktop
{
    partial class EntryForm
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
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.flpSideBar = new System.Windows.Forms.FlowLayoutPanel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnDevices = new System.Windows.Forms.Button();
            this.btnOperations = new System.Windows.Forms.Button();
            this.dashTime = new System.Windows.Forms.Timer(this.components);
            this.pnlLoadForm = new System.Windows.Forms.Panel();
            this.pnlTopBar.SuspendLayout();
            this.flpSideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(197)))), ((int)(((byte)(228)))));
            this.pnlTopBar.Controls.Add(this.lblTime);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1350, 30);
            this.pnlTopBar.TabIndex = 0;
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(0, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(1350, 30);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "00:00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpSideBar
            // 
            this.flpSideBar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flpSideBar.Controls.Add(this.picLogo);
            this.flpSideBar.Controls.Add(this.btnDashboard);
            this.flpSideBar.Controls.Add(this.btnDevices);
            this.flpSideBar.Controls.Add(this.btnOperations);
            this.flpSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpSideBar.Location = new System.Drawing.Point(0, 30);
            this.flpSideBar.Name = "flpSideBar";
            this.flpSideBar.Padding = new System.Windows.Forms.Padding(2);
            this.flpSideBar.Size = new System.Drawing.Size(200, 699);
            this.flpSideBar.TabIndex = 1;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::ARKODesktop.Properties.Resources.ArkoLogoSideBar;
            this.picLogo.Location = new System.Drawing.Point(5, 5);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(192, 92);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(5, 103);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(189, 72);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnDevices
            // 
            this.btnDevices.Location = new System.Drawing.Point(5, 181);
            this.btnDevices.Name = "btnDevices";
            this.btnDevices.Size = new System.Drawing.Size(189, 72);
            this.btnDevices.TabIndex = 3;
            this.btnDevices.Text = "Devices";
            this.btnDevices.UseVisualStyleBackColor = true;
            this.btnDevices.Click += new System.EventHandler(this.btnDevices_Click);
            // 
            // btnOperations
            // 
            this.btnOperations.Location = new System.Drawing.Point(5, 259);
            this.btnOperations.Name = "btnOperations";
            this.btnOperations.Size = new System.Drawing.Size(189, 72);
            this.btnOperations.TabIndex = 2;
            this.btnOperations.Text = "Operations";
            this.btnOperations.UseVisualStyleBackColor = true;
            this.btnOperations.Click += new System.EventHandler(this.btnOperations_Click);
            // 
            // dashTime
            // 
            this.dashTime.Enabled = true;
            this.dashTime.Interval = 1000;
            this.dashTime.Tick += new System.EventHandler(this.dashTime_Tick);
            // 
            // pnlLoadForm
            // 
            this.pnlLoadForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoadForm.Location = new System.Drawing.Point(200, 30);
            this.pnlLoadForm.Name = "pnlLoadForm";
            this.pnlLoadForm.Size = new System.Drawing.Size(1150, 699);
            this.pnlLoadForm.TabIndex = 2;
            // 
            // EntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.pnlLoadForm);
            this.Controls.Add(this.flpSideBar);
            this.Controls.Add(this.pnlTopBar);
            this.Name = "EntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ARKO";
            this.pnlTopBar.ResumeLayout(false);
            this.flpSideBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.FlowLayoutPanel flpSideBar;
        private System.Windows.Forms.Timer dashTime;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Panel pnlLoadForm;
        private System.Windows.Forms.Button btnOperations;
        private System.Windows.Forms.Button btnDevices;
    }
}

