
namespace arkoDT.Controller
{
    partial class frmControls
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
            this.btnMicToggle = new System.Windows.Forms.Button();
            this.btnSpeakerToggle = new System.Windows.Forms.Button();
            this.lblTest = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMicToggle
            // 
            this.btnMicToggle.Location = new System.Drawing.Point(665, 92);
            this.btnMicToggle.Name = "btnMicToggle";
            this.btnMicToggle.Size = new System.Drawing.Size(75, 23);
            this.btnMicToggle.TabIndex = 0;
            this.btnMicToggle.Text = "Mic: On";
            this.btnMicToggle.UseVisualStyleBackColor = true;
            this.btnMicToggle.Click += new System.EventHandler(this.ToggleMic);
            // 
            // btnSpeakerToggle
            // 
            this.btnSpeakerToggle.Location = new System.Drawing.Point(665, 121);
            this.btnSpeakerToggle.Name = "btnSpeakerToggle";
            this.btnSpeakerToggle.Size = new System.Drawing.Size(75, 23);
            this.btnSpeakerToggle.TabIndex = 1;
            this.btnSpeakerToggle.Text = "Speaker: On";
            this.btnSpeakerToggle.UseVisualStyleBackColor = true;
            this.btnSpeakerToggle.Click += new System.EventHandler(this.ToggleSpeaker);
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(89, 68);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(35, 13);
            this.lblTest.TabIndex = 2;
            this.lblTest.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(665, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Reconnect";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTest);
            this.Controls.Add(this.btnSpeakerToggle);
            this.Controls.Add(this.btnMicToggle);
            this.Name = "frmControls";
            this.Text = "Arko Controller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.frmControls_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmControls_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmControls_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMicToggle;
        private System.Windows.Forms.Button btnSpeakerToggle;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.Button button1;
    }
}