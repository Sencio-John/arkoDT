
namespace ARKODesktop.Views
{
    partial class Operation
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
<<<<<<< Updated upstream
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.flpOperations = new System.Windows.Forms.FlowLayoutPanel();
            this.chrtWaterLevel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chrtWaterLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // flpOperations
            // 
            this.flpOperations.AutoScroll = true;
            this.flpOperations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpOperations.Location = new System.Drawing.Point(8, 8);
            this.flpOperations.Margin = new System.Windows.Forms.Padding(2);
            this.flpOperations.Name = "flpOperations";
            this.flpOperations.Size = new System.Drawing.Size(275, 685);
            this.flpOperations.TabIndex = 0;
            // 
            // chrtWaterLevel
            // 
            this.chrtWaterLevel.BorderlineColor = System.Drawing.Color.Black;
            this.chrtWaterLevel.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea3.Name = "ChartArea1";
            this.chrtWaterLevel.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chrtWaterLevel.Legends.Add(legend3);
            this.chrtWaterLevel.Location = new System.Drawing.Point(298, 360);
            this.chrtWaterLevel.Name = "chrtWaterLevel";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chrtWaterLevel.Series.Add(series3);
            this.chrtWaterLevel.Size = new System.Drawing.Size(824, 332);
            this.chrtWaterLevel.TabIndex = 1;
            this.chrtWaterLevel.Text = "chart1";
            // 
            // gMap
            // 
            this.gMap.Bearing = 0F;
            this.gMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gMap.CanDragMap = true;
            this.gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemory = 5;
            this.gMap.Location = new System.Drawing.Point(298, 35);
            this.gMap.MarkersEnabled = true;
            this.gMap.MaxZoom = 2;
            this.gMap.MinZoom = 2;
            this.gMap.MouseWheelZoomEnabled = true;
            this.gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMap.Name = "gMap";
            this.gMap.NegativeMode = false;
            this.gMap.PolygonsEnabled = true;
            this.gMap.RetryLoadTile = 0;
            this.gMap.RoutesEnabled = true;
            this.gMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMap.ShowTileGridLines = false;
            this.gMap.Size = new System.Drawing.Size(824, 309);
            this.gMap.TabIndex = 2;
            this.gMap.Zoom = 0D;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1001, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // Operation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 704);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.flpOperations);
            this.Controls.Add(this.chrtWaterLevel);
            this.Controls.Add(this.gMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Operation";
            this.Text = "Operations";
            this.Load += new System.EventHandler(this.Operation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chrtWaterLevel)).EndInit();
=======
            this.SuspendLayout();
            // 
            // Operations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 660);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Operations";
            this.Text = "Operations";
>>>>>>> Stashed changes
            this.ResumeLayout(false);

        }

        #endregion
<<<<<<< Updated upstream

        private System.Windows.Forms.FlowLayoutPanel flpOperations;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtWaterLevel;
        private GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.ComboBox comboBox1;
=======
>>>>>>> Stashed changes
    }
}