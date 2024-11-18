
namespace arkoDT
{
    partial class frmServerHandler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServerHandler));
            this.txtCCID = new System.Windows.Forms.TextBox();
            this.txtCCPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmitData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCCID
            // 
            this.txtCCID.Location = new System.Drawing.Point(286, 195);
            this.txtCCID.Name = "txtCCID";
            this.txtCCID.Size = new System.Drawing.Size(267, 20);
            this.txtCCID.TabIndex = 0;
            // 
            // txtCCPassword
            // 
            this.txtCCPassword.Location = new System.Drawing.Point(286, 221);
            this.txtCCPassword.Name = "txtCCPassword";
            this.txtCCPassword.Size = new System.Drawing.Size(267, 20);
            this.txtCCPassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Control Center ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Control Center Password:";
            // 
            // btnSubmitData
            // 
            this.btnSubmitData.Location = new System.Drawing.Point(478, 247);
            this.btnSubmitData.Name = "btnSubmitData";
            this.btnSubmitData.Size = new System.Drawing.Size(75, 23);
            this.btnSubmitData.TabIndex = 4;
            this.btnSubmitData.Text = "Submit";
            this.btnSubmitData.UseVisualStyleBackColor = true;
            this.btnSubmitData.Click += new System.EventHandler(this.btnSubmitData_Click);
            // 
            // frmServerHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(704, 511);
            this.Controls.Add(this.btnSubmitData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCCPassword);
            this.Controls.Add(this.txtCCID);
            this.DoubleBuffered = true;
            this.Name = "frmServerHandler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "w";
            this.Load += new System.EventHandler(this.frmServerHandler_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCCID;
        private System.Windows.Forms.TextBox txtCCPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubmitData;
    }
}