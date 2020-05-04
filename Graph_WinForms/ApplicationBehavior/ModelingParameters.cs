using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
        /// <summary>
        /// Changes movement modeling type to basic
        /// </summary>
        private void BasicTypeCheckBox_CheckedChanged(object sender, EventArgs e) =>
            SandpileTypeCheckBox.Checked = !BasicTypeCheckBox.Checked;

        /// <summary>
        /// Changes movement modeling type to Sandpile
        /// </summary>
        private void SandpileTypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            BasicTypeCheckBox.Checked = !SandpileTypeCheckBox.Checked;
            if (SandpileTypeCheckBox.Checked) ChartCheckBox_CheckedChanged(sender, e);
            else if (ChartCheckBox.Checked)
            {
                SandpileChartType1.Visible = SandpileChartType2.Visible = false;
                SaveGifCheckBox.Location = new Point(11, 246);
                SpeedLabel.Location = new Point(6, 308);
                SpeedNumeric.Location = new Point(11, 346);
            }
        }

        /// <summary>
        /// Shows additional chart types for sandpile modeling
        /// </summary>
        private void ChartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SandpileTypeCheckBox.Checked && ChartCheckBox.Checked)
            {
                SaveGifCheckBox.Location =
                    new Point(SaveGifCheckBox.Location.X,
                        SandpileChartType2.Location.Y + SandpileChartType2.Size.Height + 10);
                SpeedLabel.Location =
                    new Point(SpeedLabel.Location.X,
                        SaveGifCheckBox.Location.Y + SaveGifCheckBox.Size.Height + 28);
                SpeedNumeric.Location =
                    new Point(SpeedNumeric.Location.X,
                        SpeedLabel.Location.Y + SpeedLabel.Size.Height + 10);
                SandpileChartType1.Visible = SandpileChartType2.Visible = true;
                return;
            }
            if (SandpileTypeCheckBox.Checked && !ChartCheckBox.Checked)
            {
                SandpileChartType1.Visible = SandpileChartType2.Visible = false;
                SaveGifCheckBox.Location = new Point(11, 246);
                SpeedLabel.Location = new Point(6, 308);
                SpeedNumeric.Location = new Point(11, 346);
            }
        }


        /// <summary>
        /// Starts Sandpile movement after stock vertices are selected
        /// </summary>
        private void StockLabel_Click(object sender, EventArgs e)
        {
            if (SandpilePanel.Size.Height > 60) return;
            SandpilePanel.Visible = false;
            TimeTextBox.Visible = true;
            TimeTextBox.BringToFront();
            SandpileLabel.Text = "Select vertex to add a grain of sand to       ";
            SandpileLabel.Font = new Font("Segoe UI", 9);
            SandpilePanel.Size = new Size(SandpilePanel.Size.Width, 91);

            movement.MovementEnded += MovementEndedSandpileEventHandler;

            movement.StartMovementModeling();
        }

        /// <summary>
        /// Shows a tip for selecting a vertex to add sand to
        /// </summary>
        private void MovementEndedSandpileEventHandler(object sender, EventArgs e)
        {
            SandpilePanel.Visible = true;
            SandpilePanel.BringToFront();
        }


        /// <summary>
        /// Adds a grain of sand to a random vertex
        /// </summary>
        private async void RandomAddingLabel_Click(object sender, EventArgs e)
        {
            int rndVertex;
            do { rndVertex = rnd.Next(Digraph.Vertices.Count); }
            while (Digraph.Stock.Contains(rndVertex));

            Digraph.State[rndVertex]++;
            SandpilePanel.Visible = false;
            graphDrawing.HighlightVertexToAddSand(Digraph.Vertices[rndVertex]);
            DrawingSurface.Image = graphDrawing.Image;

            if (SaveGifCheckBox.Checked && movement.MovementGif.Frames.Count < 250)
            {
                var bmp = (DrawingSurface.Image as Bitmap).GetHbitmap();
                var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bmp,
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                movement.MovementGif.Frames.Add(BitmapFrame.Create(src));
                DeleteObject(bmp);
            }

            await Task.Delay(1000);
            movement.Go();
        }


        private void RandomAddingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RandomAddingCheckBox.Checked)
            {
                movement.MovementEnded -= MovementEndedSandpileEventHandler;
                movement.MovementEnded += RandomAddingLabel_Click;
                return;
            }
            movement.MovementEnded += MovementEndedSandpileEventHandler;
            movement.MovementEnded -= RandomAddingLabel_Click;
        }


        private void SandpileChartType1_CheckedChanged(object sender, EventArgs e) =>
            SandpileChartType2.Checked = !SandpileChartType1.Checked;

        private void SandpileChartType2_CheckedChanged(object sender, EventArgs e) =>
            SandpileChartType1.Checked = !SandpileChartType2.Checked;
    }
}
