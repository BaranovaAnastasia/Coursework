using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationClasses;
using GraphClasses.Commands;

namespace CourseworkApp
{
    public partial class MainWindow : Form, IDisposable
    {
        public MainWindow()
        {
            InitializeComponent();
            GraphBuilder_SizeChanged(null, null);

            graphDrawing = new GraphDrawing(DrawingSurface.Width, DrawingSurface.Height);

            graphDrawing.RadiusChanged += (sender, args1) =>
            {
                if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(digraph);
                else graphDrawing.DrawTheWholeGraphSandpile(digraph, false);
                DrawingSurface.Image = graphDrawing.Image;
            };

            graphDrawing.SandpilePaletteChanged +=
                (sender, e) =>
                    DigraphInformationDemonstration.DisplaySandpileColors(graphDrawing, SandpilePalette);

            CommandsManager.CanRedoChanged += (sender, e) => RedoButton.Enabled = (bool)sender;
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

            if (graphDrawing == null) return;

            graphDrawing.Size = DrawingSurface.Size;
            if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(digraph);
            else graphDrawing.DrawTheWholeGraphSandpile(digraph, false);
            DrawingSurface.Image = graphDrawing.Image;
        }

        /// <summary>
        /// Moves digraph image on the drawing surface
        /// </summary>
        private void GraphBuilder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Control) return;

            if (e.KeyCode == Keys.Right)
                xCoefficient += 10;
            else if (e.KeyCode == Keys.Left)
                xCoefficient -= 10;
            else if (e.KeyCode == Keys.Up)
                yCoefficient -= 10;
            else if (e.KeyCode == Keys.Down)
                yCoefficient += 10;
            else if (e.KeyCode == Keys.Oemplus)
                enlargeCoefficient *= 1.1;
            else if (e.KeyCode == Keys.OemMinus)
                enlargeCoefficient *= 0.9;
            else if(!isOnMovement && e.KeyCode == Keys.Z)
                UndoButton_Click(sender, e);
            else if (!isOnMovement && e.KeyCode == Keys.Y)
                RedoButton_Click(sender, e);
            else return;

            if (isOnMovement && SandpileTypeCheckBox.Checked)
                graphDrawing.DrawTheWholeGraphSandpile(digraph, false, xCoefficient, yCoefficient, enlargeCoefficient);
            else graphDrawing.DrawTheWholeGraph(digraph, xCoefficient, yCoefficient, enlargeCoefficient);
            DrawingSurface.Image = graphDrawing.Image;
        }
        /// <summary>
        /// Executes commands to move digraph itself
        /// </summary>
        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Control) return;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                if (xCoefficient == 0 && yCoefficient == 0) return;
                var command = new MoveDigraphCommand(digraph, xCoefficient, yCoefficient);
                commandsManager.Execute(command);
                xCoefficient = yCoefficient = 0;
            }

            if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Oemplus)
            {
                var command = new EnlargeDigraphCommand(digraph, enlargeCoefficient);
                commandsManager.Execute(command);
                enlargeCoefficient = 1;
            }
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
            digraph.VertexAdded += (sender, e) =>
            {
                graphDrawing.DrawVertex(((Vertex)sender).X, ((Vertex)sender).Y,
                    e.Index + 1);
                DrawingSurface.Image = graphDrawing.Image;
                AddVertexToGridAdjacencyMatrix(e.Index);
                AddVertexToGridParameters(e.Index);
            };

            digraph.ArcAdded += (sender, e) =>
            {
                ArcName.Items.Insert(e.Index, ((Arc)sender).ToString());
                graphDrawing.DrawArc(digraph.Vertices[((Arc)sender).StartVertex],
                    digraph.Vertices[((Arc)sender).EndVertex],
                    (Arc)sender);
                DrawingSurface.Image = graphDrawing.Image;
                GridAdjacencyMatrix[((Arc)sender).EndVertex, ((Arc)sender).StartVertex].Value = ((Arc)sender).Length;
                vStart = vEnd = -1;
            };

            digraph.VertexRemoved += (sender, e) =>
            {
                RemoveVertexFromGridAdjacencyMatrix(e.Index);
                RemoveVertexFromGridParameters(e.Index);
                ArcName.Items.Clear();
                foreach (var arc in digraph.Arcs)
                    ArcName.Items.Add(arc.ToString());
            };

            digraph.ArcRemoved += (sender, e) =>
            {
                GridAdjacencyMatrix[((Arc)sender).EndVertex, ((Arc)sender).StartVertex].Value = 0;
                ArcName.Items.RemoveAt(e.Index);
            };
        }
    }
}
