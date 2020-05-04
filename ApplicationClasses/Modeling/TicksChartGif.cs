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
        private void TickChartFilling(object source, EventArgs e)
        {
            if(numberOfDotsChart != null)
                numberOfDotsChart.chart1.Series[0].Points.AddXY(mainStopwatch.ElapsedMilliseconds / 1000.0, involvedArcs.Count);

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
