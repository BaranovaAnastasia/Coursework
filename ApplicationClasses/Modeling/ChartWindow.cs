using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CsvHelper;

namespace ApplicationClasses.Modeling
{
    public partial class ChartWindow : Form
    {
        SaveFileDialog saveImageDialog = new SaveFileDialog();
        SaveFileDialog saveDataDialog = new SaveFileDialog();
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

        public ChartWindow()
        {
            InitializeComponent();
            Series data = new Series("Number of Dots")
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

            saveImageDialog.FileName = "ChartImage"; // Default file name
            saveImageDialog.DefaultExt = ".jpg"; // Default file extension
            saveImageDialog.Filter = "JPEG Image (.jpeg)|*.jpeg"; // Filter files by extension

            saveDataDialog.FileName = "ChartData"; // Default file name
            saveDataDialog.DefaultExt = ".csv"; // Default file extension
            saveDataDialog.Filter = "CSV file (.csv)|*.csv"; // Filter files by extension

            folderBrowserDialog.SelectedPath = "Chart";
        }

        private void ChartWindow_SizeChanged(object sender, EventArgs e) =>
            chart1.Size = new Size(Width, Height - menuStrip1.Height - 5);

        public void AvalancheSizesDistributionChartPrepare()
        {
            Text = "Distribution of Avalanche Sizes Chart";
            chart1.Series[0] = new Series("Distribution of\n\rAvalanche Sizes")
            {
                ChartType = SeriesChartType.Point,
                Color = Color.DarkCyan,
                MarkerStyle = MarkerStyle.Circle,
                MarkerColor = Color.DarkCyan,
                ChartArea = "Chart",
                BorderWidth = 1
            };
            chart1.Series[0].ToolTip = "Size = #VALX,\n\rFrequency = #VALY";
            chart1.ChartAreas[0].AxisX.Title = "Avalanche Size";
            chart1.ChartAreas[0].AxisY.Title = "Frequency";
            chart1.ChartAreas[0].AxisX.Interval = 2;
            chart1.ChartAreas[0].AxisY.Interval = 5;
        }

        private void saveChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                using (FileStream stream = new FileStream(saveImageDialog.FileName, FileMode.Create))
                    chart1.SaveImage(stream, ChartImageFormat.Jpeg);
        }

        private void saveDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == DialogResult.OK)
                using (var sw = new StreamWriter(saveDataDialog.FileName, false))
                {
                    sw.WriteLine(chart1.ChartAreas[0].AxisX.Title + ";" + chart1.ChartAreas[0].AxisY.Title);
                    foreach (var point in chart1.Series[0].Points)
                        sw.WriteLine(point.XValue + ";" + point.YValues[0]);
                }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(folderBrowserDialog.SelectedPath + @"\ChartImage.jpg", FileMode.Create))
                    chart1.SaveImage(stream, ChartImageFormat.Jpeg);

                using (var sw = new StreamWriter(folderBrowserDialog.SelectedPath + @"\Data.csv", false))
                {
                    sw.WriteLine(chart1.ChartAreas[0].AxisX.Title + ";" + chart1.ChartAreas[0].AxisY.Title);
                    foreach (var point in chart1.Series[0].Points)
                        sw.WriteLine(point.XValue + ";" + point.YValues[0]);
                }
            }
        }
    }
}
