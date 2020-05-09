using System;
using System.Drawing;
using System.Linq;
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
        private void TickGifCollecting(object source, EventArgs e)
        {
            var bmp = (DrawingSurface.Image as Bitmap).GetHbitmap();
            var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bmp,
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            MovementGif.Frames.Add(BitmapFrame.Create(src));
            if (MovementGif.Frames.Count >= 300)
                mainTimer.Tick -= TickGifCollecting;

            DeleteObject(bmp);
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);



        /// <summary>
        /// Collects chart data and displays it
        /// </summary>
        private void TickChartFilling(long time, int count)
        {
            if(numberOfDotsChart != null)
            {
                numberOfDotsChart.chart1.Series[0].Points.AddXY(time / 1000.0, count);
                if (count > 10000) numberOfDotsChart.chart1.ChartAreas[0].AxisY.Interval = 2000;
                else if(count > 5000) numberOfDotsChart.chart1.ChartAreas[0].AxisY.Interval = 1000;
                else if (count > 1000) numberOfDotsChart.chart1.ChartAreas[0].AxisY.Interval = 200;
                else if (count > 500) numberOfDotsChart.chart1.ChartAreas[0].AxisY.Interval = 100;
                else if (count > 100) numberOfDotsChart.chart1.ChartAreas[0].AxisY.Interval = 20;
                else if (count > 50) numberOfDotsChart.chart1.ChartAreas[0].AxisY.Interval = 10;

                numberOfDotsChart.chart1.ChartAreas[0].AxisX.Interval = time / 1000.0 > 100
                    ? 20
                    : time / 1000.0 > 50
                        ? 10
                        : time / 1000.0 > 10
                            ? 2
                            : 1;
            }

            if (IsMovementEndedSandpile && distributionChart != null)
            {
                if(avalancheSize == 0) return;
                foreach (DataPoint point in distributionChart.chart1.Series[0].Points)
                {
                    if (point.XValue != avalancheSize) continue;
                    distributionChart.chart1.Series[0].Points.AddXY(avalancheSize, point.YValues[0] + 1);
                    distributionChart.chart1.Series[0].Points.Remove(point);
                    return;
                }
                distributionChart.chart1.Series[0].Points.AddXY(avalancheSize, 1);
            }
        }
    }
}
