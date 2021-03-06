﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media.Imaging;

namespace DotsMovementModelingAppLib.Modeling
{
    public partial class MovementModeling
    {
        /// <summary>
        /// GIF image of the process of the movement of points on digraph
        /// </summary>
        public readonly GifBitmapEncoder MovementGif = new GifBitmapEncoder();

        private bool save = true;

        /// <summary>
        /// Collects frames for a GIF image of the process of the movement of points on digraph
        /// (with a limit of 300 frames)
        /// </summary>
        private void TickAddFrame(object source, EventArgs e)
        {
            if (save)
            {
                var bmp = ((Bitmap) DrawingSurface.Image).GetHbitmap();
                var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bmp,
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                MovementGif.Frames.Add(BitmapFrame.Create(src));

                DeleteObject(bmp);

                save = false;

                if (MovementGif.Frames.Count >= 400)
                    mainTimer.Tick -= TickAddFrame;
            }
            else save = true;
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);


        /// <summary>
        /// Adds point to number of dots chart
        /// </summary>
        private void AddNumberOfDotsChartPoint(long t, int count)
        {
            if (numberOfDotsChart == null) return;
            numberOfDotsChart?.chart1.Series[0].Points.AddXY(t / 1000.0, count);
            if (count >= numberOfDotsChart.chart1.ChartAreas[0].AxisY.Maximum
            || t / 1000.0 >= numberOfDotsChart?.chart1.ChartAreas[0].AxisX.Maximum)
                ChangeChartInterval(numberOfDotsChart?.chart1);
        }

        /// <summary>
        /// Adds Avalanche Size to Avalanche Size distribution chart
        /// </summary>
        private void AddAvalancheSize()
        {
            if (distributionChart == null || !IsMovementEndedSandpile) return;
            if (!avalanche.Contains(true)) return;

            int size = avalanche.Count(v => v);

            foreach (var point in distributionChart.chart1.Series[0].Points)
            {
                if (Math.Abs(point.XValue - size) > 0) continue;
                distributionChart.chart1.Series[0].Points.AddXY(size, point.YValues[0] + 1);
                distributionChart.chart1.Series[0].Points.Remove(point);
                return;
            }
            distributionChart.chart1.Series[0].Points.AddXY(size, 1);
        }

        /// <summary>
        /// Changes chart area axis intervals to fit values
        /// </summary>
        private static void ChangeChartInterval(Chart chart)
        {
            if (chart == null) return;
            chart.ChartAreas[0].AxisY.Interval = (int)(chart.ChartAreas[0].AxisY.Maximum / 5);
            chart.ChartAreas[0].AxisX.Interval = (int)(chart.ChartAreas[0].AxisX.Maximum / 10);
        }
    }
}
