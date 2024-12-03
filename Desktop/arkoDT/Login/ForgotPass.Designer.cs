
namespace arkoDT
{
    partial class frmForgot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmForgot));
            this.label1 = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.btnSendOTP = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.lblConfirmPass = new System.Windows.Forms.Label();
            this.btnShowPass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(161, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "FORGOT";
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(103, 67);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(46, 17);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Email:";
            // 
            // lblCode
            // 
            this.lblCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(105, 109);
            this.lblCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(45, 17);
            this.lblCode.TabIndex = 2;
            this.lblCode.Text = "Code:";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(153, 68);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(181, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // txtOTP
            // 
            this.txtOTP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOTP.Location = new System.Drawing.Point(153, 108);
            this.txtOTP.Margin = new System.Windows.Forms.Padding(2);
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(181, 20);
            this.txtOTP.TabIndex = 4;
            // 
            // btnSendOTP
            // 
            this.btnSendOTP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSendOTP.Location = new System.Drawing.Point(338, 67);
            this.btnSendOTP.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendOTP.Name = "btnSendOTP";
            this.btnSendOTP.Size = new System.Drawing.Size(66, 20);
            this.btnSendOTP.TabIndex = 5;
            this.btnSendOTP.Text = "Get Code";
            this.btnSendOTP.UseVisualStyleBackColor = true;
            this.btnSendOTP.Click += new System.EventHandler(this.btnSendOTP_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnVerify.Enabled = false;
            this.btnVerify.Location = new System.Drawing.Point(196, 142);
            this.btnVerify.Margin = new System.Windows.Forms.Padding(2);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(87, 21);
            this.btnVerify.TabIndex = 6;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(153, 182);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(181, 20);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.Visible = false;
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(76, 182);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(73, 17);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password:";
            this.lblPassword.Visible = false;
            // 
            // btnChange
            // 
            this.btnChange.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnChange.Location = new System.Drawing.Point(174, 268);
            this.btnChange.Margin = new System.Windows.Forms.Padding(2);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(138, 24);
            this.btnChange.TabIndex = 9;
            this.btnChange.Text = "Change Password";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Visible = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtConfirm
            // 
            this.txtConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirm.Location = new System.Drawing.Point(153, 215);
            this.txtConfirm.Margin = new System.Windows.Forms.Padding(2);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '●';
            this.txtConfirm.Size = new System.Drawing.Size(181, 20);
            this.txtConfirm.TabIndex = 11;
            this.txtConfirm.Visible = false;
            // 
            // lblConfirmPass
            // 
            this.lblConfirmPass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfirmPass.AutoSize = true;
            this.lblConfirmPass.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirmPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPass.Location = new System.Drawing.Point(24, 218);
            this.lblConfirmPass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConfirmPass.Name = "lblConfirmPass";
            this.lblConfirmPass.Size = new System.Drawing.Size(125, 17);
            this.lblConfirmPass.TabIndex = 10;
            this.lblConfirmPass.Text = "Confirm Password:";
            this.lblConfirmPass.Visible = false;
            // 
            // btnShowPass
            // 
            this.btnShowPass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnShowPass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowPass.BackgroundImage")));
            this.btnShowPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowPass.Location = new System.Drawing.Point(338, 199);
            this.btnShowPass.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowPass.Name = "btnShowPass";
            this.btnShowPass.Size = new System.Drawing.Size(66, 20);
            this.btnShowPass.TabIndex = 12;
            this.btnShowPass.Text = "7";
            this.btnShowPass.UseVisualStyleBackColor = true;
            this.btnShowPass.Visible = false;
            this.btnShowPass.Click += new System.EventHandler(this.btnShowPass_Click);
            // 
            // frmForgot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(475, 333);
            this.Controls.Add(this.btnShowPass);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.lblConfirmPass);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnSendOTP);
            this.Controls.Add(this.txtOTP);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmForgot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forgot Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtOTP;
        private System.Windows.Forms.Button btnSendOTP;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Label lblConfirmPass;
        private System.Windows.Forms.Button btnShowPass;
    }
}