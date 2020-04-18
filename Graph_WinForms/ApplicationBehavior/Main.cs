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

            saveDialog.FileName = "Graph"; // Default file name
            saveDialog.DefaultExt = ".txt"; // Default file extension
            saveDialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            openDialog.DefaultExt = ".txt"; // Default file extension
            openDialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            saveGifDialog.FileName = "Movement";
            saveGifDialog.DefaultExt = ".gif";
            saveGifDialog.Filter = "Gif image (.gif)|*.gif";
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
        }

        private void DarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*BackColor = Color.FromArgb(40,40,40);
            TopMenu.BackColor = Color.FromArgb(40, 40, 40);
            graphDrawing.BackColor = Color.FromArgb(140, 140, 140);
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image*/
            ;
        }

        private void AppParameters_MouseLeave(object sender, EventArgs e)
        {
            Tools.Focus();
        }
    }
}
