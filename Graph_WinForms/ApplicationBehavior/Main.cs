﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Channels;
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

            saveImageDialog.FileName = "GraphImage"; // Default file name
            saveImageDialog.DefaultExt = ".jpg"; // Default file extension
            saveImageDialog.Filter = "JPG Image (.jpg)|*.jpg"; // Filter files by extension

            folderBrowserDialog.SelectedPath = "Digraph";

            openDialog.DefaultExt = ".digraph"; // Default file extension
            openDialog.Filter = "Digraph data files (.digraph)|*.digraph"; // Filter files by extension

            saveGifDialog.FileName = "Movement";
            saveGifDialog.DefaultExt = ".gif";
            saveGifDialog.Filter = "Gif Image (.gif)|*.gif";

            graphDrawing.RadiusChanged += (object sender, EventArgs e) =>
            {
                if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(Digraph);
                else graphDrawing.DrawTheWholeGraphSandpile(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
            };
        }

        /// <summary>
        /// Adjusts controls to fit the form's size
        /// </summary>
        private void GraphBuilder_SizeChanged(object sender, EventArgs e)
        {
            Build.Location = new Point(Size.Width / 2 - Build.Size.Width / 2, Size.Height / 2 - Build.Size.Height - 50);
            RandomGraph.Location = new Point(Build.Location.X, Build.Location.Y + Build.Size.Height + 10);
            Open.Location = new Point(RandomGraph.Location.X, RandomGraph.Location.Y + RandomGraph.Size.Height + 10);
            SquareLattice.Location = new Point(Open.Location.X, Open.Location.Y + Open.Size.Height + 10);
            TriangleLattice.Location = new Point(Open.Location.X + Open.Size.Width - TriangleLattice.Size.Width,
                Open.Location.Y + Open.Size.Height + 10);

            AppParameters.Size = new Size(AppParameters.Width, DrawingSurface.Height);
            AppParameters.Location = new Point(Size.Width - AppParameters.Size.Width - 30, AppParameters.Location.Y);


            if (Size.Width - (Size.Width - AppParameters.Location.X - 10) - Tools.Size.Width - 40 > 0 && Size.Height - 120 > 0)
                DrawingSurface.Size = new Size(Size.Width - (Size.Width - AppParameters.Location.X - 10) - Tools.Size.Width - 40,
                     Size.Height - 120);
            AppParameters.Size = new Size(AppParameters.Width, DrawingSurface.Height);
            GridAdjacencyMatrix.Size = new Size(AdjacencyPage.Width - 20, AdjacencyPage.Height - GridAdjacencyMatrix.Location.Y - 10);
            if (graphDrawing != null)
            {
                graphDrawing.Size = DrawingSurface.Size;
                if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(Digraph);
                else graphDrawing.DrawTheWholeGraphSandpile(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
            }
            TimeTextBox.Location =
                new Point(DrawingSurface.Location.X + DrawingSurface.Size.Width - TimeTextBox.Size.Width,
                    TimeTextBox.Location.Y);
        }

        private void GraphBuilder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Control) return;
            RadiusTrackBar.Enabled = false;
            AppParameters.Enabled = false;
            if (e.KeyCode == Keys.Right)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X + 10, Digraph.Vertices[i].Y);
            if (e.KeyCode == Keys.Left)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X - 10, Digraph.Vertices[i].Y);
            if (e.KeyCode == Keys.Up)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y - 10);
            if (e.KeyCode == Keys.Down)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y + 10);

            if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(Digraph);
            else graphDrawing.DrawTheWholeGraphSandpile(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.ControlKey)
                return;
            RadiusTrackBar.Enabled = true;
            AppParameters.Enabled = true;
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

        private void RadiusTrackBar_ValueChanged(object sender, EventArgs e)
        {
            graphDrawing.R = RadiusTrackBar.Value;
        }

        private void SquareLattice_Click(object sender, EventArgs e)
        {
            SquareLatticeForm square = new SquareLatticeForm(DrawingSurface.Width, DrawingSurface.Height);
            square.ShowDialog();
            if (square.Digraph == null) return;
            Digraph = square.Digraph;
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
            UpdateDigraphInfo();
            ChangeMainMenuState(false);
            ChangeDrawingElementsState(true);
        }

        private void TriangleLattice_Click(object sender, EventArgs e)
        {
            TriangularLatticeForm triangle = new TriangularLatticeForm(DrawingSurface.Width, DrawingSurface.Height);
            triangle.ShowDialog();
            if (triangle.Digraph == null) return;
            Digraph = triangle.Digraph;
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
            UpdateDigraphInfo();
            ChangeMainMenuState(false);
            ChangeDrawingElementsState(true);
        }

        private void EraserToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            using (var boldFont = new Font(e.Font, FontStyle.Bold))
            {
                var headerText = "Eraser";
                var valueText = "Double-click on the edge or vertex to remove it";

                var headerTextSize = TextRenderer.MeasureText(headerText, e.Font);

                TextRenderer.DrawText(e.Graphics, headerText, e.Font, e.Bounds.Location, Color.Black);

                var valueTextPosition = new Point(e.Bounds.X + headerTextSize.Width, e.Bounds.Y);
                TextRenderer.DrawText(e.Graphics, valueText, boldFont, valueTextPosition, Color.Black);
            }
        }

        private void CursorButton_EnabledChanged(object sender, EventArgs e)
        {
            if (CursorButton.Enabled)
            {
                CursorButton.Size = new Size(75, 75);
                CursorButton.Location = new Point(CursorButton.Location.X - 5, CursorButton.Location.Y - 5);
                return;
            }
            CursorButton.Size = new Size(65, 65);
            CursorButton.Location = new Point(CursorButton.Location.X + 5, CursorButton.Location.Y + 5);
        }

        private void VertexButton_EnabledChanged(object sender, EventArgs e)
        {
            if (VertexButton.Enabled)
            {
                VertexButton.Size = new Size(75, 75);
                VertexButton.Location = new Point(VertexButton.Location.X - 5, VertexButton.Location.Y - 5);
                return;
            }
            VertexButton.Size = new Size(65, 65);
            VertexButton.Location = new Point(VertexButton.Location.X + 5, VertexButton.Location.Y + 5);
        }

        private void EdgeButton_EnabledChanged(object sender, EventArgs e)
        {
            if (EdgeButton.Enabled)
            {
                EdgeButton.Size = new Size(75, 75);
                EdgeButton.Location = new Point(EdgeButton.Location.X - 5, EdgeButton.Location.Y - 5);
                return;
            }
            EdgeButton.Size = new Size(65, 65);
            EdgeButton.Location = new Point(EdgeButton.Location.X + 5, EdgeButton.Location.Y + 5);
        }

        private void DeleteButton_EnabledChanged(object sender, EventArgs e)
        {
            if (DeleteButton.Enabled)
            {
                DeleteButton.Size = new Size(75, 75);
                DeleteButton.Location = new Point(DeleteButton.Location.X - 5, DeleteButton.Location.Y - 5);
                return;
            }
            DeleteButton.Size = new Size(65, 65);
            DeleteButton.Location = new Point(DeleteButton.Location.X + 5, DeleteButton.Location.Y + 5);
        }
    }
}
