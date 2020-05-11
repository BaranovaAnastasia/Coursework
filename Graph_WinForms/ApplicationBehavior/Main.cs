using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationClasses;
using GraphClasses.Commands;

namespace Graph_WinForms
{
    public partial class MainWindow : Form, IDisposable
    {
        public MainWindow()
        {
            InitializeComponent();
            GraphBuilder_SizeChanged(null, null);

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            graphDrawing = new GraphDrawing(DrawingSurface.Width, DrawingSurface.Height);

            graphDrawing.RadiusChanged += (sender, args1) =>
            {
                if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(Digraph);
                else graphDrawing.DrawTheWholeGraphSandpile(Digraph, false);
                DrawingSurface.Image = graphDrawing.Image;
            };

            graphDrawing.SandpilePaletteChanged +=
                (sender, e) =>
                    DigraphInformationDemonstration.DisplaySandpileColors(graphDrawing, SandpilePalette);

            SubscribeToDigraphEvents();

            CommandsManager.CanRedoChanged += (sender, e) => RedoButton.Enabled = (bool) sender;
            CommandsManager.CanUndoChanged += (sender, e) => UndoButton.Enabled = (bool)sender;
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
            Right.Location = new Point(Right.Location.X, ClearButton.Location.Y - RadiusValueLabel.Height - 10);
            Down.Location = new Point(Down.Location.X, ClearButton.Location.Y - RadiusValueLabel.Height - 10);
            Left.Location = new Point(Left.Location.X, ClearButton.Location.Y - RadiusValueLabel.Height - 10);
            Up.Location = new Point(Up.Location.X, Down.Location.Y - Up.Height);

            RadiusValueLabel.Location = new Point(10, Up.Location.Y - RadiusValueLabel.Height - 10);
            RadiusTrackBar.Location = new Point(10, RadiusValueLabel.Location.Y - RadiusTrackBar.Height);
            RadiusLabel.Location = new Point(10, RadiusTrackBar.Location.Y - RadiusLabel.Height);
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

            #region Graph moving

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

            #endregion

            #region Scaling

            if (e.KeyCode == Keys.Oemplus)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 1.1), (int)(Digraph.Vertices[i].Y * 1.1));
            if (e.KeyCode == Keys.OemMinus)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 0.9), (int)(Digraph.Vertices[i].Y * 0.9));

            #endregion

            if (!isOnMovement)
            {
                if(e.KeyCode == Keys.Z)
                    UndoButton_Click(sender, e);
                if(e.KeyCode == Keys.Y)
                    RedoButton_Click(sender, e);
            }

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

        /// <summary>
        /// Subscribes handlers to digraph events
        /// </summary>
        private void SubscribeToDigraphEvents()
        {
            Digraph.VertexAdded += (sender, e) =>
            {
                graphDrawing.DrawVertex(((Vertex)sender).X, ((Vertex)sender).Y,
                    e.Index + 1);
                DrawingSurface.Image = graphDrawing.Image;
                AddVertexToGridAdjacencyMatrix(e.Index);
                AddVertexToGridParameters(e.Index);
            };

            Digraph.ArcAdded += (sender, e) =>
            {
                ArcName.Items.Insert(e.Index, ((Arc)sender).ToString());
                graphDrawing.DrawArc(Digraph.Vertices[((Arc)sender).StartVertex],
                    Digraph.Vertices[((Arc)sender).EndVertex],
                    (Arc)sender);
                DrawingSurface.Image = graphDrawing.Image;
                GridAdjacencyMatrix[((Arc)sender).EndVertex, ((Arc)sender).StartVertex].Value = ((Arc)sender).Length;
                vStart = vEnd = -1;
            };

            Digraph.VertexRemoved += (sender, e) =>
            {
                RemoveVertexFromGridAdjacencyMatrix(e.Index);
                RemoveVertexFromGridParameters(e.Index);
                ArcName.Items.Clear();
                foreach (var arc in Digraph.Arcs)
                    ArcName.Items.Add(arc.ToString());
            };

            Digraph.ArcRemoved += (sender, e) =>
            {
                GridAdjacencyMatrix[((Arc)sender).EndVertex, ((Arc)sender).StartVertex].Value = 0;
                ArcName.Items.RemoveAt(e.Index);
            };
        }
    }
}
