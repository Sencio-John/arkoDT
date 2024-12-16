
namespace arkoDT
{
    partial class frmMap
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
            this.btnPinLoc = new System.Windows.Forms.Button();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlSide = new System.Windows.Forms.Panel();
            this.btnPinned = new System.Windows.Forms.Button();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.flpList = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPinLoc
            // 
            this.btnPinLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPinLoc.Location = new System.Drawing.Point(1803, 8);
            this.btnPinLoc.Margin = new System.Windows.Forms.Padding(2);
            this.btnPinLoc.Name = "btnPinLoc";
            this.btnPinLoc.Size = new System.Drawing.Size(93, 21);
            this.btnPinLoc.TabIndex = 9;
            this.btnPinLoc.Text = "Pin Location";
            this.btnPinLoc.UseVisualStyleBackColor = true;
            this.btnPinLoc.Click += new System.EventHandler(this.btnPinLoc_Click);
            // 
            // map
            // 
            this.map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.map.Bearing = 0F;
            this.map.CanDragMap = true;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemory = 5;
            this.map.Location = new System.Drawing.Point(8, 31);
            this.map.Margin = new System.Windows.Forms.Padding(2);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomEnabled = true;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(1888, 1002);
            this.map.TabIndex = 10;
            this.map.Visible = false;
            this.map.Zoom = 0D;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(8, 11);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(50, 21);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pnlSide
            // 
            this.pnlSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlSide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSide.Location = new System.Drawing.Point(1708, 459);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(184, 294);
            this.pnlSide.TabIndex = 14;
            this.pnlSide.Visible = false;
            // 
            // btnPinned
            // 
            this.btnPinned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPinned.Location = new System.Drawing.Point(1803, 33);
            this.btnPinned.Margin = new System.Windows.Forms.Padding(2);
            this.btnPinned.Name = "btnPinned";
            this.btnPinned.Size = new System.Drawing.Size(93, 21);
            this.btnPinned.TabIndex = 15;
            this.btnPinned.Text = "Pinned Location";
            this.btnPinned.UseVisualStyleBackColor = true;
            this.btnPinned.Click += new System.EventHandler(this.btnPinned_Click);
            // 
            // cbFilter
            // 
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "All",
            "Active",
            "Completed"});
            this.cbFilter.Location = new System.Drawing.Point(41, 7);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(139, 21);
            this.cbFilter.TabIndex = 1;
            // 
            // flpList
            // 
            this.flpList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flpList.AutoScroll = true;
            this.flpList.Location = new System.Drawing.Point(1692, 102);
            this.flpList.Name = "flpList";
            this.flpList.Size = new System.Drawing.Size(200, 351);
            this.flpList.TabIndex = 13;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.label1);
            this.pnlFilter.Controls.Add(this.cbFilter);
            this.pnlFilter.Location = new System.Drawing.Point(1708, 59);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(184, 40);
            this.pnlFilter.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter:";
            // 
            // frmMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.ControlBox = false;
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.btnPinned);
            this.Controls.Add(this.pnlSide);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.flpList);
            this.Controls.Add(this.map);
            this.Controls.Add(this.btnPinLoc);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMap_FormClosing);
            this.Load += new System.EventHandler(this.frmMap_Load);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPinLoc;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlSide;
        private System.Windows.Forms.Button btnPinned;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.FlowLayoutPanel flpList;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label label1;
    }
}