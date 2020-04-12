using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class GraphBuilder : Form
    {
        /// <summary>
        /// Draws vertices and arcs by mouse click on a drawing surfsce 
        /// </summary>
        private void DrawingSurface_MouseClick(object sender, MouseEventArgs e)
        {
            if (!VertexButton.Enabled)
            {
                Digraph.AddVertex(new Vertex(e.X, e.Y));
                graphDrawing.DrawVertex(e.X, e.Y, Digraph.Vertices.Count, new Pen(Color.MidnightBlue, 2.5f));
                DrawingSurface.Image = graphDrawing.Image;
                AddVertexToGridAdjacencyMatrix();
                AddVertexToGridThresholds();
                AddVertexToGridRefractoryPeriods();
                AddVertexToGridInitialState();
            }

            if (!EdgeButton.Enabled)
            {
                FindArcVertices(e.X, e.Y);
                if (vStart != -1 && vEnd != -1)
                {
                    if(vStart == vEnd)
                    {
                        MessageBox.Show("Arc cannot be a loop", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        graphDrawing.UnhighlightVertex(Digraph.Vertices[vStart]);
                        DrawingSurface.Image = graphDrawing.Image;
                        vStart = vEnd = -1;
                        return;
                    }
                    Digraph.AddArc(new Arc(vStart, vEnd));
                    ArcName.Items.Add(((vStart + 1) + "-" + (vEnd + 1)));
                    graphDrawing.DrawArc(Digraph.Vertices[vStart], Digraph.Vertices[vEnd], Digraph.Arcs[Digraph.Arcs.Count - 1]);
                    DrawingSurface.Image = graphDrawing.Image;
                    GridAdjacencyMatrix[vEnd, vStart].Value = 1;
                    vStart = vEnd = -1;
                }
            }
        }

        /// <summary>
        /// Deletes vertices and arcs by mouse double click on a drawing surface 
        /// </summary>
        private void DrawingSurface_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (DeleteButton.Enabled) return;

            bool wasSmthDeleted = false;

            ApplicationMethods.TryToDeleteVertexAt(e.X, e.Y, Digraph, out int i);
            if (i != -1)
            {
                RemoveVertexFromGridAdjacencyMatrix(i);
                RemoveVertexFromGridThresholds(i);
                RemoveVertexFromGridRefractoryPeriods(i);
                RemoveVertexFromGridInitialState(i);
                wasSmthDeleted = true;
            }
            else
            {
                ApplicationMethods.TryToDeleteArcAt(e.X, e.Y, Digraph, out int sv, out int ev);
                if (sv != -1 && ev != -1)
                {
                    GridAdjacencyMatrix[ev, sv].Value = 0;
                    wasSmthDeleted = true;
                }

            }
            if (wasSmthDeleted)
            {
                graphDrawing.DrawTheWholeGraph(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
                ArcName.Items.Clear();
                Digraph.Arcs.ForEach(arc => ArcName.Items.Add((arc.StartVertex + 1) + "-" + (arc.EndVertex + 1)));
            }
        }

        /// <summary>
        /// Remembers vertex chosen for moving or point where the movement began
        /// </summary>
        private void DrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (CoursorButton.Enabled) return;
            IsPressed = true;

            for (int i = 0; i < Digraph.Vertices.Count; i++)
                if (Math.Pow((Digraph.Vertices[i].X - e.X), 2) + Math.Pow((Digraph.Vertices[i].Y - e.Y), 2) <= Math.Pow(GraphDrawing.R, 2))
                {
                    MovingVertexIndex = i;
                    Ticks = DateTime.Now;
                    MovingVetrex = Digraph.Vertices[i];
                    return;
                }
            p = new Point(e.X, e.Y);
        }

        /// <summary>
        /// Moves chosen vertex or the whole graph
        /// </summary>
        private void DrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPressed || CoursorButton.Enabled || MovingVertexIndex == -1) return;
            Digraph.Vertices[MovingVertexIndex] = new Vertex(e.X, e.Y);
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        /// <summary>
        /// Redraws graph and refreshes all the variables connected with moving
        /// </summary>
        private void DrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsPressed || CoursorButton.Enabled) return;

            bool highlight = (DateTime.Now - Ticks).Ticks < 2250000 &&
                Math.Pow(e.X - MovingVetrex.X, 2) + Math.Pow(e.Y - MovingVetrex.Y, 2) <= GraphDrawing.R * GraphDrawing.R;

            if (MovingVertexIndex != -1 && !highlight)
            {
                // Keeping the image inside the borders of the sheet
                int x = e.X, y = e.Y;
                if (x < GraphDrawing.R + 5)
                    x = GraphDrawing.R + 5;
                if (y < GraphDrawing.R + 5)
                {
                    y = GraphDrawing.R + 5;
                    if (GridAdjacencyMatrix[MovingVertexIndex, MovingVertexIndex].Value.ToString() != "0")
                        y += GraphDrawing.R;
                }
                if (x > DrawingSurface.Width - GraphDrawing.R - 5)
                {
                    x = DrawingSurface.Width - GraphDrawing.R - 5;
                    if (GridAdjacencyMatrix[MovingVertexIndex, MovingVertexIndex].Value.ToString() != "0")
                        x -= GraphDrawing.R;
                }
                if (y > DrawingSurface.Height - GraphDrawing.R - 5)
                    y = DrawingSurface.Height - GraphDrawing.R - 5;

                Digraph.Vertices[MovingVertexIndex] = new Vertex(x, y);
            }

            if (highlight) Digraph.Vertices[MovingVertexIndex] = new Vertex(MovingVetrex.X, MovingVetrex.Y);
            graphDrawing.DrawTheWholeGraph(Digraph);
            if (highlight) graphDrawing.HighlightVertex(Digraph.Vertices[MovingVertexIndex]);
            DrawingSurface.Image = graphDrawing.Image;
            MovingVertexIndex = -1;
            IsPressed = false;
        }

        /// <summary>
        /// Adds a new line and a row to Adjacency Matrix in DataGridView
        /// </summary>
        private void AddVertexToGridAdjacencyMatrix()
        {
            GridAdjacencyMatrix.Columns.Add(String.Empty, Digraph.Vertices.Count.ToString());
            GridAdjacencyMatrix.Columns[Digraph.Vertices.Count - 1].FillWeight = 1;
            GridAdjacencyMatrix.Columns[Digraph.Vertices.Count - 1].Width = 35;
            GridAdjacencyMatrix.Columns[Digraph.Vertices.Count - 1].SortMode = DataGridViewColumnSortMode.NotSortable;
            GridAdjacencyMatrix.Rows.Add();

            for (int i = 0; i < Digraph.Vertices.Count; i++)
            {
                GridAdjacencyMatrix[Digraph.Vertices.Count - 1, i].Value = 0;
                GridAdjacencyMatrix[i, Digraph.Vertices.Count - 1].Value = 0;
            }
            GridAdjacencyMatrix.Rows[Digraph.Vertices.Count - 1].HeaderCell.Value = Digraph.Vertices.Count.ToString();
        }

        /// <summary>
        /// Removes a line and a row from Adjacency Matrix in DataGridView
        /// </summary>
        /// <param name="index">index of a line/row</param>
        private void RemoveVertexFromGridAdjacencyMatrix(int index)
        {
            GridAdjacencyMatrix.Columns.RemoveAt(index);
            if (GridAdjacencyMatrix.Columns.Count != 0) GridAdjacencyMatrix.Rows.RemoveAt(index);
            for (int j = index; j < Digraph.Vertices.Count; j++)
            {
                GridAdjacencyMatrix.Columns[j].HeaderCell.Value = (j + 1).ToString();
                GridAdjacencyMatrix.Rows[j].HeaderCell.Value = (j + 1).ToString();
            }
        }

        private void AddVertexToGridThresholds()
        {
            GridThresholds.Rows.Add();
            GridThresholds[0, Digraph.Vertices.Count - 1].Value = Digraph.Thresholds[Digraph.Vertices.Count - 1];
            GridThresholds.Rows[Digraph.Vertices.Count - 1].HeaderCell.Value = Digraph.Vertices.Count.ToString();
        }
        private void RemoveVertexFromGridThresholds(int index)
        {
            GridThresholds.Rows.RemoveAt(index);
            for (int j = index; j < Digraph.Vertices.Count; j++)
                GridThresholds.Rows[j].HeaderCell.Value = (j + 1).ToString();
        }

        private void AddVertexToGridRefractoryPeriods()
        {
            GridRefractoryPeriods.Rows.Add();
            GridRefractoryPeriods[0, Digraph.Vertices.Count - 1].Value = Digraph.Thresholds[Digraph.Vertices.Count - 1];
            GridRefractoryPeriods.Rows[Digraph.Vertices.Count - 1].HeaderCell.Value = Digraph.Vertices.Count.ToString();
        }
        private void RemoveVertexFromGridRefractoryPeriods(int index)
        {
            GridRefractoryPeriods.Rows.RemoveAt(index);
            for (int j = index; j < Digraph.Vertices.Count; j++)
                GridThresholds.Rows[j].HeaderCell.Value = (j + 1).ToString();
        }

        private void AddVertexToGridInitialState()
        {
            GridInitialState.Rows.Add();
            GridInitialState[0, Digraph.Vertices.Count -1].Value = Digraph.State[Digraph.Vertices.Count - 1];
            GridInitialState.Rows[Digraph.Vertices.Count - 1].HeaderCell.Value = Digraph.Vertices.Count.ToString();
        }
        private void RemoveVertexFromGridInitialState(int index)
        {
            GridInitialState.Rows.RemoveAt(index);
            for (int j = index; j < Digraph.Vertices.Count; j++)
                GridInitialState.Rows[j].HeaderCell.Value = (j + 1).ToString();
        }

        /// <summary>
        /// Finds vertices chosen for creating a new arc 
        /// </summary>
        /// <param name="x">X coordinate of a place where mouse click occurred</param>
        /// <param name="y">Y coordinate of a place where mouse click occurred</param>
        private void FindArcVertices(int x, int y)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                if (Math.Pow((Digraph.Vertices[i].X - x), 2) + Math.Pow((Digraph.Vertices[i].Y - y), 2) <= Math.Pow(GraphDrawing.R, 2))
                {
                    if (vStart == -1)
                    {
                        vStart = i;
                        graphDrawing.HighlightVertex(Digraph.Vertices[i]);
                        DrawingSurface.Image = graphDrawing.Image;
                        return;
                    }
                    if (vEnd == -1)
                    {
                        if (GridAdjacencyMatrix[i, vStart].Value.ToString() != "0")
                        {
                            MessageBox.Show("The edge already exists", "Error");
                            vStart = vEnd = -1;
                            return;
                        }
                        vEnd = i;
                        graphDrawing.HighlightVertex(Digraph.Vertices[i]);
                        DrawingSurface.Image = graphDrawing.Image;
                        return;
                    }
                }
        }
    }
}
