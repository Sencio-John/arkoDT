
namespace arkoDT
{
    partial class frmProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProfile));
            this.pnlProfile = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlContacts = new System.Windows.Forms.Panel();
            this.pbProfile = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEditPicture = new System.Windows.Forms.Button();
            this.btnEditDetails = new System.Windows.Forms.Button();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlDetails = new System.Windows.Forms.Label();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.btnSendCode = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlProfile.SuspendLayout();
            this.pnlContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlProfile
            // 
            this.pnlProfile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlProfile.Controls.Add(this.btnSave);
            this.pnlProfile.Controls.Add(this.txtLastName);
            this.pnlProfile.Controls.Add(this.txtFirstName);
            this.pnlProfile.Controls.Add(this.btnEditDetails);
            this.pnlProfile.Controls.Add(this.btnEditPicture);
            this.pnlProfile.Controls.Add(this.label2);
            this.pnlProfile.Controls.Add(this.label1);
            this.pnlProfile.Controls.Add(this.pbProfile);
            this.pnlProfile.Location = new System.Drawing.Point(59, 85);
            this.pnlProfile.Name = "pnlProfile";
            this.pnlProfile.Size = new System.Drawing.Size(438, 532);
            this.pnlProfile.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pnlContacts
            // 
            this.pnlContacts.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlContacts.Controls.Add(this.btnSendCode);
            this.pnlContacts.Controls.Add(this.label5);
            this.pnlContacts.Controls.Add(this.txtOTP);
            this.pnlContacts.Controls.Add(this.txtEmail);
            this.pnlContacts.Controls.Add(this.label4);
            this.pnlContacts.Controls.Add(this.label3);
            this.pnlContacts.Location = new System.Drawing.Point(540, 85);
            this.pnlContacts.Name = "pnlContacts";
            this.pnlContacts.Size = new System.Drawing.Size(438, 245);
            this.pnlContacts.TabIndex = 1;
            // 
            // pbProfile
            // 
            this.pbProfile.Image = ((System.Drawing.Image)(resources.GetObject("pbProfile.Image")));
            this.pbProfile.Location = new System.Drawing.Point(87, 27);
            this.pbProfile.Name = "pbProfile";
            this.pbProfile.Size = new System.Drawing.Size(271, 218);
            this.pbProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbProfile.TabIndex = 0;
            this.pbProfile.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(9, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(9, 424);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Last Name";
            // 
            // btnEditPicture
            // 
            this.btnEditPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditPicture.BackgroundImage")));
            this.btnEditPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEditPicture.Location = new System.Drawing.Point(289, 42);
            this.btnEditPicture.Name = "btnEditPicture";
            this.btnEditPicture.Size = new System.Drawing.Size(31, 23);
            this.btnEditPicture.TabIndex = 3;
            this.btnEditPicture.UseVisualStyleBackColor = true;
            // 
            // btnEditDetails
            // 
            this.btnEditDetails.Location = new System.Drawing.Point(330, 287);
            this.btnEditDetails.Name = "btnEditDetails";
            this.btnEditDetails.Size = new System.Drawing.Size(75, 23);
            this.btnEditDetails.TabIndex = 4;
            this.btnEditDetails.Text = "Edit Details";
            this.btnEditDetails.UseVisualStyleBackColor = true;
            this.btnEditDetails.Click += new System.EventHandler(this.btnEditDetails_Click);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Enabled = false;
            this.txtFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(116, 355);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(271, 29);
            this.txtFirstName.TabIndex = 5;
            // 
            // txtLastName
            // 
            this.txtLastName.Enabled = false;
            this.txtLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(116, 419);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(271, 29);
            this.txtLastName.TabIndex = 6;
            // 
            // txtOTP
            // 
            this.txtOTP.Enabled = false;
            this.txtOTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOTP.Location = new System.Drawing.Point(139, 156);
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(271, 29);
            this.txtOTP.TabIndex = 10;
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(139, 92);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(193, 29);
            this.txtEmail.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(32, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Verify";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(32, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Email";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(3, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Contact Details";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.cmbRole);
            this.panel1.Controls.Add(this.pnlDetails);
            this.panel1.Controls.Add(this.txtGender);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(540, 372);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 245);
            this.panel1.TabIndex = 12;
            // 
            // pnlDetails
            // 
            this.pnlDetails.AutoSize = true;
            this.pnlDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDetails.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlDetails.Location = new System.Drawing.Point(3, 15);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(113, 24);
            this.pnlDetails.TabIndex = 11;
            this.pnlDetails.Text = "Information";
            // 
            // txtGender
            // 
            this.txtGender.Enabled = false;
            this.txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.Location = new System.Drawing.Point(139, 92);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(271, 29);
            this.txtGender.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(32, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 24);
            this.label7.TabIndex = 7;
            this.label7.Text = "Gender";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.Location = new System.Drawing.Point(32, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 24);
            this.label8.TabIndex = 8;
            this.label8.Text = "Role";
            // 
            // cmbRole
            // 
            this.cmbRole.Enabled = false;
            this.cmbRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(139, 153);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(271, 32);
            this.cmbRole.TabIndex = 12;
            // 
            // btnSendCode
            // 
            this.btnSendCode.Location = new System.Drawing.Point(338, 97);
            this.btnSendCode.Name = "btnSendCode";
            this.btnSendCode.Size = new System.Drawing.Size(72, 23);
            this.btnSendCode.TabIndex = 12;
            this.btnSendCode.Text = "Send Code";
            this.btnSendCode.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(180, 478);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // frmProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1004, 681);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlContacts);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pnlProfile);
            this.DoubleBuffered = true;
            this.Name = "frmProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profile";
            this.Load += new System.EventHandler(this.frmProfile_Load);
            this.pnlProfile.ResumeLayout(false);
            this.pnlProfile.PerformLayout();
            this.pnlContacts.ResumeLayout(false);
            this.pnlContacts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlProfile;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlContacts;
        private System.Windows.Forms.PictureBox pbProfile;
        private System.Windows.Forms.Button btnEditPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Button btnEditDetails;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOTP;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label pnlDetails;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSendCode;
        private System.Windows.Forms.Button btnSave;
    }
}