using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
        /// <summary>
        /// Draws vertices and arcs by mouse click on a drawing surface 
        /// </summary>
        private void DrawingSurface_MouseClick(object sender, MouseEventArgs e)
        {
            if (SandpilePanel.Visible)
            {
                if (SandpilePanel.Size.Height < 50)
                    SelectStock(e.X, e.Y);
                else SelectVertexToAddSand(e.X, e.Y);
                return;
            }
            if (isOnMovement) return;

            if (!VertexButton.Enabled)
            {
                Digraph.AddVertex(new Vertex(e.X, e.Y));
                graphDrawing.DrawVertex(e.X, e.Y, Digraph.Vertices.Count, new Pen(Color.MidnightBlue, 2.5f));
                DrawingSurface.Image = graphDrawing.Image;
                AddVertexToGridAdjacencyMatrix();
                AddVertexToGridParameters();
                return;
            }

            if (!EdgeButton.Enabled)
            {
                if (FindArcVertices(e.X, e.Y))
                {
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
            if (DeleteButton.Enabled || isOnMovement) return;

            bool wasSmthDeleted = false;

            if (DigraphBuilding.TryToDeleteVertexAt(e.X, e.Y, Digraph, graphDrawing.R, out int i))
            {
                RemoveVertexFromGridAdjacencyMatrix(i);
                RemoveVertexFromGridParameters(i);
                wasSmthDeleted = true;
            }
            else if (DigraphBuilding.TryToDeleteArcAt(e.X, e.Y, Digraph, out Arc arc))
            {
                GridAdjacencyMatrix[arc.EndVertex, arc.StartVertex].Value = 0;
                ArcName.Items.Remove((arc.StartVertex + 1) + "-" + (arc.EndVertex + 1));
                wasSmthDeleted = true;
            }

            if (wasSmthDeleted)
            {
                graphDrawing.DrawTheWholeGraph(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
            }
        }


        /// <summary>
        /// Remembers vertex chosen for moving and point where the movement began
        /// </summary>
        private void DrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (VertexColorDialogOpen.Visible) VerticesColorPanel_Leave(sender, e);
            if (ArcsColorDialogOpen.Visible) ArcsColorPanel_Leave(sender, e);

            if (CursorButton.Enabled || isOnMovement) return;
            isPressed = true;

            for (int i = 0; i < Digraph.Vertices.Count; i++)
                if (Math.Pow((Digraph.Vertices[i].X - e.X), 2) + Math.Pow((Digraph.Vertices[i].Y - e.Y), 2) <= Math.Pow(graphDrawing.R, 2))
                {
                    movingVertexIndex = i;
                    ticks = DateTime.Now;
                    movingVertex = Digraph.Vertices[i];
                    return;
                }
        }

        /// <summary>
        /// Moves chosen vertex or the whole graph
        /// </summary>
        private void DrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isOnMovement || !isPressed || CursorButton.Enabled || movingVertexIndex == -1) return;
            Digraph.Vertices[movingVertexIndex] = new Vertex(e.X, e.Y);
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        /// <summary>
        /// Redraws graph if it's needed and 
        /// refreshes all the variables connected with moving
        /// </summary>
        private void DrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            if (isOnMovement || !isPressed || CursorButton.Enabled) return;

            bool highlight = (DateTime.Now - ticks).Ticks < 2250000 &&
                Math.Pow(e.X - movingVertex.X, 2) + Math.Pow(e.Y - movingVertex.Y, 2) <= graphDrawing.R * graphDrawing.R;

            // Keeping the image inside the borders of the sheet
            if (movingVertexIndex != -1 && !highlight)
            {
                float x = e.X, y = e.Y;
                if (x < graphDrawing.R + 5)
                    x = graphDrawing.R + 5;
                if (y < graphDrawing.R + 5)
                {
                    y = graphDrawing.R + 5;
                    if (GridAdjacencyMatrix[movingVertexIndex, movingVertexIndex].Value.ToString() != "0")
                        y += graphDrawing.R;
                }
                if (x > DrawingSurface.Width - graphDrawing.R - 5)
                {
                    x = DrawingSurface.Width - graphDrawing.R - 5;
                    if (GridAdjacencyMatrix[movingVertexIndex, movingVertexIndex].Value.ToString() != "0")
                        x -= graphDrawing.R;
                }
                if (y > DrawingSurface.Height - graphDrawing.R - 5)
                    y = DrawingSurface.Height - graphDrawing.R - 5;

                Digraph.Vertices[movingVertexIndex] = new Vertex((int)x, (int)y);
            }

            if (highlight) Digraph.Vertices[movingVertexIndex] = new Vertex(movingVertex.X, movingVertex.Y);
            graphDrawing.DrawTheWholeGraph(Digraph);

            if (highlight) graphDrawing.HighlightVertex(Digraph.Vertices[movingVertexIndex]);

            DrawingSurface.Image = graphDrawing.Image;
            movingVertexIndex = -1;
            isPressed = false;
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

        private void AddVertexToGridParameters()
        {
            GridParameters.Rows.Add();
            GridParameters.Rows[Digraph.Vertices.Count - 1].HeaderCell.Value = Digraph.Vertices.Count.ToString();
            GridParameters[0, Digraph.Vertices.Count - 1].Value = Digraph.Thresholds[Digraph.Vertices.Count - 1];
            GridParameters[1, Digraph.Vertices.Count - 1].Value = Digraph.RefractoryPeriods[Digraph.Vertices.Count - 1];
            GridParameters[2, Digraph.Vertices.Count - 1].Value = Digraph.State[Digraph.Vertices.Count - 1];
        }
        private void RemoveVertexFromGridParameters(int index)
        {
            GridParameters.Rows.RemoveAt(index);
            for (int j = index; j < Digraph.Vertices.Count; j++)
                GridParameters.Rows[j].HeaderCell.Value = (j + 1).ToString();
        }


        /// <summary>
        /// Finds vertices chosen for creating a new arc 
        /// </summary>
        /// <param name="x">X coordinate of a place where mouse click occurred</param>
        /// <param name="y">Y coordinate of a place where mouse click occurred</param>
        private bool FindArcVertices(int x, int y)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                if (Math.Pow((Digraph.Vertices[i].X - x), 2) + Math.Pow((Digraph.Vertices[i].Y - y), 2) <= Math.Pow(graphDrawing.R, 2))
                {
                    if (vStart == -1)
                    {
                        vStart = i;
                        graphDrawing.HighlightVertex(Digraph.Vertices[i]);
                        DrawingSurface.Image = graphDrawing.Image;
                        return false;
                    }
                    if (vEnd == -1)
                    {
                        if (GridAdjacencyMatrix[i, vStart].Value.ToString() != "0" || vStart == i)
                        {
                            graphDrawing.UnhighlightVertex(Digraph.Vertices[vStart]);
                            DrawingSurface.Image = graphDrawing.Image;
                            if (GridAdjacencyMatrix[i, vStart].Value.ToString() != "0")
                                MessageBox.Show("The edge already exists", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (vStart == i)
                                MessageBox.Show("Arc cannot be a loop", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            vStart = vEnd = -1;
                            return false;
                        }
                        vEnd = i;
                        graphDrawing.HighlightVertex(Digraph.Vertices[i]);
                        DrawingSurface.Image = graphDrawing.Image;
                        return true;
                    }
                }
            return false;
        }


        /// <summary>
        /// Searches for a vertex at (x, y) and turns it into a stock vertex
        /// </summary>
        private void SelectStock(int x, int y)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                if (Math.Pow((Digraph.Vertices[i].X - x), 2) + Math.Pow((Digraph.Vertices[i].Y - y), 2) <=
                    Math.Pow(graphDrawing.R, 2))
                {
                    if (Digraph.Stock.Contains(i)) Digraph.Stock.Remove(i);
                    else Digraph.Stock.Add(i);
                    graphDrawing.DrawTheWholeGraphSandpile(Digraph, false);
                    DrawingSurface.Image = graphDrawing.Image;
                    return;
                }
        }

        /// <summary>
        /// Searches for a vertex at (x, y) and adds sand to it
        /// </summary>
        private async void SelectVertexToAddSand(int x, int y)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                if (Math.Pow((Digraph.Vertices[i].X - x), 2) + Math.Pow((Digraph.Vertices[i].Y - y), 2) <=
                    Math.Pow(graphDrawing.R, 2))
                {
                    if (Digraph.Stock.Contains(i)) return;
                    Digraph.State[i]++;
                    SandpilePanel.Visible = false;
                    graphDrawing.HighlightVertexToAddSand(Digraph.Vertices[i]);
                    DrawingSurface.Image = graphDrawing.Image;
                    await Task.Delay(200);
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
                    movement.Go();
                }
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}
