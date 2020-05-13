using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media.Imaging;

namespace ApplicationClasses.Modeling
{
    public partial class MovementModeling
    {
        /// <summary>
        /// GIF image of the process of the movement of points on digraph
        /// </summary>
        public readonly GifBitmapEncoder MovementGif = new GifBitmapEncoder();

        /// <summary>
        /// Collects frames for a GIF image of the process of the movement of points on digraph
        /// (with a limit of 300 frames)
        /// </summary>
        private void TickAddFrame(object source, EventArgs e)
        {
            var bmp = ((Bitmap) DrawingSurface.Image).GetHbitmap();
            var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bmp,
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            MovementGif.Frames.Add(BitmapFrame.Create(src));

            DeleteObject(bmp);

            if (MovementGif.Frames.Count >= 300)
                mainTimer.Tick -= TickAddFrame;
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);


        /// <summary>
        /// Adds point to number of dots chart
        /// </summary>
        private void AddNumberOfDotsChartPoint(long time, int count)
        {
            numberOfDotsChart?.chart1.Series[0].Points.AddXY(time / 1000.0, count);
            if (count >= numberOfDotsChart?.chart1.ChartAreas[0].AxisY.Maximum
            || time / 1000.0 >= numberOfDotsChart?.chart1.ChartAreas[0].AxisX.Maximum)
                ChangeChartInterval(numberOfDotsChart?.chart1);
        }

        /// <summary>
        /// Adds Avalanche Size to Avalanche Size distribution chart
        /// </summary>
        private void AddAvalancheSize()
        {
            if (distributionChart == null || !IsMovementEndedSandpile) return;
            if (avalancheSize == 0) return;

            foreach (var point in distributionChart.chart1.Series[0].Points)
            {
                if (Math.Abs(point.XValue - avalancheSize) > 0) continue;
                distributionChart.chart1.Series[0].Points.AddXY(avalancheSize, point.YValues[0] + 1);
                distributionChart.chart1.Series[0].Points.Remove(point);
                return;
            }
            distributionChart.chart1.Series[0].Points.AddXY(avalancheSize, 1);
        }

        /// <summary>
        /// Changes chart area axis intervals to fit values
        /// </summary>
        private static void ChangeChartInterval(Chart chart)
        {
            chart.ChartAreas[0].AxisY.Interval = (int)(chart.ChartAreas[0].AxisY.Maximum / 5);
            chart.ChartAreas[0].AxisX.Interval = (int)(chart.ChartAreas[0].AxisX.Maximum / 10);
        }
    }
}
