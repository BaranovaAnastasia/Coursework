using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DotsMovementModelingAppLib.Modeling
{
    public partial class ChartWindow : Form
    {
        /// <summary>
        /// Initializes a new ChartWindow instance
        /// </summary>
        public ChartWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prepares chart area for displaying Avalanche Sizes Distribution
        /// </summary>
        public void AvalancheSizesDistributionChartPrepare()
        {
            Text = @"Distribution of Avalanche Sizes Chart";
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
            label1.Visible = false;
        }

        /// <summary>
        /// Saves chart data (.csv)
        /// </summary>
        private void SaveData_Click(object sender, EventArgs e)
        {

            using (var saveDialog = new SaveFileDialog
            {
                FileName = "ChartImage",
                DefaultExt = ".jpg",
                Filter = @"JPEG Image (.jpeg)|*.jpeg"
            })
            {

                if (saveDialog.ShowDialog() != DialogResult.OK) return;
                using (var stream = new FileStream(saveDialog.FileName, FileMode.Create))
                    chart1.SaveImage(stream, ChartImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// Saves chart Image
        /// </summary>
        private void SaveImage_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog
            {
                FileName = "ChartData",
                DefaultExt = ".csv",
                Filter = @"CSV file (.csv)|*.csv"
            })
            {

                if (saveDialog.ShowDialog() != DialogResult.OK) return;
                using (var stream = new StreamWriter(saveDialog.FileName, false))
                {
                    stream.WriteLine(chart1.ChartAreas[0].AxisX.Title + ";" + chart1.ChartAreas[0].AxisY.Title);
                    foreach (var point in chart1.Series[0].Points)
                        stream.WriteLine(point.XValue + ";" + point.YValues[0]);
                }
            }
        }

        /// <summary>
        /// Saves folder with chart image and data
        /// </summary>
        private void SaveAll_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog {SelectedPath = @"Chart"})
            {
                if(folderBrowser.ShowDialog() != DialogResult.OK) return;

                Directory.CreateDirectory(folderBrowser.SelectedPath + @"\Chart");

                using (var stream = new FileStream(folderBrowser.SelectedPath + @"\Chart\ChartImage.jpg", FileMode.Create))
                    chart1.SaveImage(stream, ChartImageFormat.Jpeg);

                using (var stream = new StreamWriter(folderBrowser.SelectedPath + @"\Chart\ChartData.csv", false))
                {
                    stream.WriteLine(chart1.ChartAreas[0].AxisX.Title + ";" + chart1.ChartAreas[0].AxisY.Title);
                    foreach (var point in chart1.Series[0].Points)
                        stream.WriteLine(point.XValue + ";" + point.YValues[0]);
                }
            }
        }
    }
}
