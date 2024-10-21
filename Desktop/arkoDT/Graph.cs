using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace arkoDT
{
    public partial class frmGraph : Form
    {
        public frmGraph()
        {
            InitializeComponent();
        }

        private void frmGraph_Load(object sender, EventArgs e)
        {
            // Create a new chart series
            Series series = new Series("Average Water Level");

            // Set chart type (e.g., Line, Bar, Pie)
            series.ChartType = SeriesChartType.Line;

            // Add data points to the series
            series.Points.AddXY("10:00 am", 10);
            series.Points.AddXY("11:00 am", 10);
            series.Points.AddXY("12:00 pm", 20);
            series.Points.AddXY("1:00 pm", 40);

            // Add the series to the chart control
            chrtLine.Series.Clear();  // Clear existing series
            chrtLine.Series.Add(series);

            // Configure chart axes and labels (optional)
            chrtLine.ChartAreas[0].AxisX.Title = "Time";
            chrtLine.ChartAreas[0].AxisY.Title = "Meter";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
