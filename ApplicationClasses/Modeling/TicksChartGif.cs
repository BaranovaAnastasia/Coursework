﻿using System;
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
        /// (with a limit of 250 frames)
        /// </summary>
        private void TickGifCollecting(object source, EventArgs e)
        {
            var bmp = (drawingSurface.Image as Bitmap).GetHbitmap();
            var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bmp,
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            MovementGif.Frames.Add(BitmapFrame.Create(src));
            if (MovementGif.Frames.Count >= 250)
            {
                gifTimer.Stop();
            }
        }

        private void TickChartFilling(object source, EventArgs e)
        {
            if (type == MovementModelingType.Sandpile)
            {
                foreach (DataPoint point in resultsForm.chart1.Series[0].Points)
                {
                    if (point.XValue != avalancheSize) continue;
                    else
                    {
                        resultsForm.chart1.Series[0].Points.AddXY(avalancheSize, point.YValues[0] + 1);
                        resultsForm.chart1.Series[0].Points.Remove(point);
                        return;
                    }
                }
                resultsForm.chart1.Series[0].Points.AddXY(avalancheSize, 1);
                return;
            }
            resultsForm.chart1.Series[0].Points.AddXY(mainStopwatch.ElapsedMilliseconds / 1000.0, involvedArcs.Count);
        }
    }
}
