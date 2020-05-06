using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class MainWindow : Form, IDisposable
    {
        public MainWindow()
        {
            InitializeComponent();
            GraphBuilder_SizeChanged(null, null);

            graphDrawing = new GraphDrawing(DrawingSurface.Width, DrawingSurface.Height);

            graphDrawing.RadiusChanged += (object sender, EventArgs args1) =>
            {
                if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(Digraph);
                else graphDrawing.DrawTheWholeGraphSandpile(Digraph, false);
                DrawingSurface.Image = graphDrawing.Image;
            };

            graphDrawing.SandpilePaletteChanged +=
                (object sender, EventArgs e) =>
                    DigraphInformationDemonstration.DisplaySandpileColors(graphDrawing, SandpilePalette);
        }

        /// <summary>
        /// Adjusts controls to fit the form's size
        /// </summary>
        private void GraphBuilder_SizeChanged(object sender, EventArgs e)
        {
            #region Main menu items

            Build.Location = new Point(Size.Width / 2 - Build.Size.Width / 2, Size.Height / 2 - Build.Size.Height - 50);
            RandomGraph.Location = new Point(Build.Location.X, Build.Location.Y + Build.Size.Height + 10);
            Open.Location = new Point(RandomGraph.Location.X, RandomGraph.Location.Y + RandomGraph.Size.Height + 10);
            SquareLattice.Location = new Point(Open.Location.X, Open.Location.Y + Open.Size.Height + 10);
            TriangleLattice.Location = new Point(Open.Location.X + Open.Size.Width - TriangleLattice.Size.Width,
                Open.Location.Y + Open.Size.Height + 10);

            #endregion

            #region App parameters items

            AppParameters.Location = new Point(Size.Width - AppParameters.Size.Width - 30, AppParameters.Location.Y);
            AppParameters.Height = Height - 120 > 0 ? Height - 120 : 0;
            GridParameters.Height = AppParameters.Height - ParametersLegendLabel.Height - 60;
            ParametersLegendLabel.Location = 
                new Point(ParametersLegendLabel.Location.X, AppParameters.Height - ParametersLegendLabel.Height - 15);
            GridAdjacencyMatrix.Height = AdjacencyPage.Height - GridAdjacencyMatrix.Location.Y - 10;

            #endregion

            if (Size.Width - (Size.Width - AppParameters.Location.X - 10) - Tools.Size.Width - 40 > 0 && Size.Height - 120 > 0)
                DrawingSurface.Size = new Size(Size.Width - (Size.Width - AppParameters.Location.X - 10) - Tools.Size.Width - 40,
                     AppParameters.Height);

            #region Tools Items

            Tools.Height = DrawingSurface.Height;
            ClearButton.Location = new Point(10, Tools.Height - ClearButton.Height - 10);
            //RadiusValueLabel.Location = new Point(10, ClearButton.Location.Y - RadiusValueLabel.Height - 10);
            //RadiusTrackBar.Location = new Point(10, RadiusValueLabel.Location.Y - RadiusTrackBar.Height);
            //RadiusLabel.Location = new Point(10, RadiusTrackBar.Location.Y - RadiusLabel.Height);
            SandpilePalette.Height = RadiusLabel.Location.Y - 10;

            #endregion

            if (graphDrawing != null)
            {
                graphDrawing.Size = DrawingSurface.Size;
                if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(Digraph);
                else graphDrawing.DrawTheWholeGraphSandpile(Digraph, false);
                DrawingSurface.Image = graphDrawing.Image;
            }

            TimeTextBox.Location =
                new Point(DrawingSurface.Location.X + DrawingSurface.Size.Width - TimeTextBox.Size.Width,
                    TimeTextBox.Location.Y);

        }

        /// <summary>
        /// Moves digraph on the drawing surface
        /// </summary>
        private void GraphBuilder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Control) return;
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

            if (e.KeyCode == Keys.Oemplus)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 1.1), (int)(Digraph.Vertices[i].Y * 1.1));
            if (e.KeyCode == Keys.OemMinus)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 0.9), (int)(Digraph.Vertices[i].Y * 0.9));


            if (isOnMovement && SandpileTypeCheckBox.Checked)
                graphDrawing.DrawTheWholeGraphSandpile(Digraph, false);
            else graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        /// <summary>
        /// Shifts focus to allow user to move the graph
        /// </summary>
        private void Movement_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
                Tools.Focus();
        }

        public new void Dispose()
        {
            graphDrawing.Dispose();
        }

        private void EnlargeButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 1.1), (int)(Digraph.Vertices[i].Y * 1.1));
            UpdateImage();
        }

        private void ReduceButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 0.9), (int)(Digraph.Vertices[i].Y * 0.9));
            UpdateImage();
        }

        private void Up_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y - 10);
            UpdateImage();
        }

        private void Left_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X - 10, Digraph.Vertices[i].Y);
            UpdateImage();
        }

        private void Right_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X + 10, Digraph.Vertices[i].Y);
            UpdateImage();
        }

        private void Down_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y + 10);
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (isOnMovement && SandpileTypeCheckBox.Checked)
                graphDrawing.DrawTheWholeGraphSandpile(Digraph, false);
            else graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }


        private void VertexColorDialogOpen_Click(object sender, EventArgs e)
        {
            if (GraphStyleColorDialog.ShowDialog() == DialogResult.Cancel) return;

            VerticesColorPanel.BackColor = GraphStyleColorDialog.Color;
            graphDrawing.VerticesPen = new Pen(GraphStyleColorDialog.Color, graphDrawing.VerticesPen.Width);
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;

        }

        private void ArcsColorDialogOpen_Click(object sender, EventArgs e)
        {
            if (GraphStyleColorDialog.ShowDialog() == DialogResult.Cancel) return;

            ArcsColorPanel.BackColor = GraphStyleColorDialog.Color;
            graphDrawing.ArcsPen = new Pen(Color.FromArgb(80, GraphStyleColorDialog.Color), graphDrawing.ArcsPen.Width);
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        private void VerticesColorPanel_Leave(object sender, EventArgs e) =>
            VertexColorDialogOpen.Visible = false;

        private void VerticesColorPanel_Enter(object sender, EventArgs e) =>
            VertexColorDialogOpen.Visible = true;

        private void VerticesColorPanel_Click(object sender, EventArgs e) =>
            VerticesColorPanel.Focus();

        private void ArcsColorPanel_Click(object sender, EventArgs e) =>
            ArcsColorPanel.Focus();

        private void ArcsColorPanel_Enter(object sender, EventArgs e) =>
            ArcsColorDialogOpen.Visible = true;

        private void ArcsColorPanel_Leave(object sender, EventArgs e) =>
            ArcsColorDialogOpen.Visible = false;
    }
}
