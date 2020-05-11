using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ApplicationClasses.Modeling
{
    public partial class ChartWindow : Form, IDisposable
    {
        private readonly SaveFileDialog saveImageDialog = new SaveFileDialog();
        private readonly SaveFileDialog saveDataDialog = new SaveFileDialog();
        private readonly FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

        public ChartWindow()
        {
            InitializeComponent();
            ChartWindow_SizeChanged(null, null);

            saveImageDialog.FileName = "ChartImage"; // Default file name
            saveImageDialog.DefaultExt = ".jpg"; // Default file extension
            saveImageDialog.Filter = "JPEG Image (.jpeg)|*.jpeg"; // Filter files by extension

            saveDataDialog.FileName = "ChartData"; // Default file name
            saveDataDialog.DefaultExt = ".csv"; // Default file extension
            saveDataDialog.Filter = "CSV file (.csv)|*.csv"; // Filter files by extension

            folderBrowserDialog.SelectedPath = "Chart";
        }

        /// <summary>
        /// Adjusts chart size to fit window size
        /// </summary>
        private void ChartWindow_SizeChanged(object sender, EventArgs e)
        {
            label1.Location = new Point(0, Height - 80);
            chart1.Size = new Size(Width, Height - label1.Height - 80 > 0 ? Height - label1.Height - 80 : 0);
        }

        /// <summary>
        /// Prepares chart area for displaying Avalanche Sizes Distribution
        /// </summary>
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

        /// <summary>
        /// Saves chart image
        /// </summary>
        private void SaveImage_Click(object sender, EventArgs e)
        {
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                using (FileStream stream = new FileStream(saveImageDialog.FileName, FileMode.Create))
                    chart1.SaveImage(stream, ChartImageFormat.Jpeg);
        }

        /// <summary>
        /// Saves chart data (.csv)
        /// </summary>
        private void SaveData_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == DialogResult.OK)
                using (var sw = new StreamWriter(saveDataDialog.FileName, false))
                {
                    sw.WriteLine(chart1.ChartAreas[0].AxisX.Title + ";" + chart1.ChartAreas[0].AxisY.Title);
                    foreach (var point in chart1.Series[0].Points)
                        sw.WriteLine(point.XValue + ";" + point.YValues[0]);
                }
        }

        /// <summary>
        /// Saves folder with chart image and data
        /// </summary>
        private void SaveAll_Click(object sender, EventArgs e)
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

        public new void Dispose()
        {
            saveImageDialog.Dispose();
            saveDataDialog.Dispose();
            folderBrowserDialog.Dispose();
        }
    }
}
