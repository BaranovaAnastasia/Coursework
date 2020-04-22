using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ApplicationClasses.Modeling
{
    public partial class ChartWindow : Form
    {
        SaveFileDialog saveDialog = new SaveFileDialog();
        public ChartWindow()
        {
            InitializeComponent();
            Series data = new Series("Number of dots")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.DarkCyan,
                MarkerStyle = MarkerStyle.Circle,
                MarkerColor = Color.DarkCyan,
                ChartArea = "Chart",
                BorderWidth = 1
            };
            chart1.Series.Add(data);
            chart1.Series[0].ToolTip = "t = #VALX,\n\rN = #VALY";
            chart1.ChartAreas.Add("Chart");
            chart1.ChartAreas[0].AxisX.Title = "t, s";
            chart1.ChartAreas[0].AxisY.Title = "Amount";
            chart1.ChartAreas[0].AxisX.Interval = 0.5;
            chart1.ChartAreas[0].AxisY.Interval = 5;

            saveDialog.FileName = "Chart"; // Default file name
            saveDialog.DefaultExt = ".jpg"; // Default file extension
            saveDialog.Filter = "JPEG Image (.jpeg)|*.jpeg"; // Filter files by extension
        }

        private void ChartWindow_SizeChanged(object sender, EventArgs e) =>
            chart1.Size = new Size(Width, Height - menuStrip1.Height - 5);

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveDialog.FileName, FileMode.Create))
                {
                    chart1.SaveImage(stream, ChartImageFormat.Jpeg);
                }
            }
        }
    }
}
