using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Imaging;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            GraphBuilder_SizeChanged(null, null);

            graphDrawing = new GraphDrawing(DrawingSurface.Width, DrawingSurface.Height);

            saveDataDialog.FileName = "GraphData"; // Default file name
            saveDataDialog.DefaultExt = ".digraph"; // Default file extension
            saveDataDialog.Filter = "Digraph data files (.digraph)|*.digraph"; // Filter files by extension

            saveDataDialog.FileName = "GraphImage"; // Default file name
            saveDataDialog.DefaultExt = ".jpg"; // Default file extension
            saveDataDialog.Filter = "JPG Image (.jpg)|*.jpg"; // Filter files by extension

            folderBrowserDialog.SelectedPath = "Digraph";

            openDialog.DefaultExt = ".digraph"; // Default file extension
            openDialog.Filter = "Digraph data files (.digraph)|*.digraph"; // Filter files by extension

            saveGifDialog.FileName = "Movement";
            saveGifDialog.DefaultExt = ".gif";
            saveGifDialog.Filter = "Gif Image (.gif)|*.gif";
        }

        /// <summary>
        /// Adjusts controls to fit the form's size
        /// </summary>
        private void GraphBuilder_SizeChanged(object sender, EventArgs e)
        {
            Build.Location = new Point(Size.Width / 2 - Build.Size.Width / 2, Size.Height / 2 - Build.Size.Height);
            RandomGraph.Location = new Point(Build.Location.X, Build.Location.Y + Build.Size.Height + 10);
            Open.Location = new Point(RandomGraph.Location.X, RandomGraph.Location.Y + RandomGraph.Size.Height + 10);
            AppParameters.Size = new Size(AppParameters.Width, DrawingSurface.Height);
            AppParameters.Location = new Point(Size.Width - AppParameters.Size.Width - 30, AppParameters.Location.Y);
            TimeTextBox.Location = new Point(DrawingSurface.Location.X + DrawingSurface.Size.Width - TimeTextBox.Size.Width + 5, TimeTextBox.Location.Y);
            if (Size.Width - (Size.Width - AppParameters.Location.X - 10) - Tools.Size.Width - 40 > 0 && Size.Height - 120 > 0)
                DrawingSurface.Size = new Size(Size.Width - (Size.Width - AppParameters.Location.X - 10) - Tools.Size.Width - 40,
                     Size.Height - 120);
            AppParameters.Size = new Size(AppParameters.Width, DrawingSurface.Height);
            GridAdjacencyMatrix.Size = new Size(AdjacencyPage.Width - 20, AdjacencyPage.Height - GridAdjacencyMatrix.Location.Y - 10);
            if (graphDrawing != null)
            {
                graphDrawing.Size = DrawingSurface.Size;
                graphDrawing.DrawTheWholeGraph(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
            }
        }

        private void GraphBuilder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Control) return;
            if (e.KeyCode == Keys.Right)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X - 10, Digraph.Vertices[i].Y);
            if (e.KeyCode == Keys.Left)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X + 10, Digraph.Vertices[i].Y);
            if (e.KeyCode == Keys.Up)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y + 10);
            if (e.KeyCode == Keys.Down)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y - 10);

            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        private void BasicTypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SandpileTypeCheckBox.Checked = !BasicTypeCheckBox.Checked;
            if (SandpileTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraphSandpile(Digraph);
            else graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        private void SandpileTypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            BasicTypeCheckBox.Checked = !SandpileTypeCheckBox.Checked; DrawingSurface.Image = graphDrawing.Image;
            if (SandpileTypeCheckBox.Checked) ChartCheckBox_CheckedChanged(sender, e);
            else if (ChartCheckBox.Checked)
            {
                SandpileChartType1.Visible = SandpileChartType2.Visible = false;
                SaveGifCheckBox.Location = new Point(11, 246);
                SpeedLabel.Location = new Point(6, 308);
                SpeedNumeric.Location = new Point(11, 346);
            }
        }

        private void DarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*BackColor = Color.FromArgb(40,40,40);
            TopMenu.BackColor = Color.FromArgb(40, 40, 40);
            graphDrawing.BackColor = Color.FromArgb(140, 140, 140);
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
            foreach (Control control in Controls)
            {
                control.ForeColor = Color.AliceBlue;
            }*/
        }

        private void AppParameters_MouseLeave(object sender, EventArgs e)
        {
            Tools.Focus();
        }
        private static readonly Random rnd = new Random();
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

            movement.Movement();
        }

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
                return;
            }
        }

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
            }
            else
            {
                movement.MovementEnded += MovementEndedSandpileEventHandler;
                movement.MovementEnded -= RandomAddingLabel_Click;
            }
        }

        private void MovementEndedSandpileEventHandler(object o, EventArgs args)
        {
            SandpilePanel.Visible = true;
            SandpilePanel.BringToFront();
        }
    }
}
