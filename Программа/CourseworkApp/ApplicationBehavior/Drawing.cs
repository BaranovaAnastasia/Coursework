using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ApplicationClasses;
using GraphClasses.Commands;

namespace CourseworkApp
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
                if (digraph.Vertices.Count >= 100)
                {
                    MessageBox.Show(@"Too many vertices. Unable to add a new one.", @"Failed");
                    return;
                }
                var command = new AddVertexCommand(digraph, new Vertex(e.X, e.Y));
                commandsManager.Execute(command);
                return;
            }

            if (!EdgeButton.Enabled)
            {
                if (FindArcVertices(e.X, e.Y))
                {
                    var command = new AddArcCommand(digraph, new Arc(vStart, vEnd));
                    commandsManager.Execute(command);
                }
            }
        }


        /// <summary>
        /// Deletes vertices and arcs by mouse double click on a drawing surface 
        /// </summary>
        private void DrawingSurface_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (DeleteButton.Enabled || isOnMovement) return;

            var wasSmthDeleted = false;

            if (DigraphBuilding.TryToDeleteVertexAt(e.X, e.Y, digraph, graphDrawing.R, out int i))
            {
                var command = new EraseVertexCommand(digraph, digraph.Vertices[i]);
                commandsManager.Execute(command);
                wasSmthDeleted = true;
            }
            else if (DigraphBuilding.TryToDeleteArcAt(e.X, e.Y, digraph, out Arc arc))
            {
                var command = new EraseArcCommand(digraph, arc);
                commandsManager.Execute(command);
                wasSmthDeleted = true;
            }

            if (wasSmthDeleted)
            {
                graphDrawing.DrawTheWholeGraph(digraph);
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

            for (int i = 0; i < digraph.Vertices.Count; i++)
                if (Math.Pow((digraph.Vertices[i].X - e.X), 2) + Math.Pow((digraph.Vertices[i].Y - e.Y), 2) <= Math.Pow(graphDrawing.R, 2))
                {
                    movedVertexIndex = i;
                    movedVertex = digraph.Vertices[i];
                    ticks = DateTime.Now;
                    return;
                }
        }

        /// <summary>
        /// Moves chosen vertex or the whole graph
        /// </summary>
        private void DrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isOnMovement || !isPressed || CursorButton.Enabled || movedVertexIndex == -1) return;

            digraph.Vertices[movedVertexIndex] = new Vertex(e.X, e.Y);
            graphDrawing.DrawTheWholeGraph(digraph);
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
                Math.Pow(e.X - movedVertex.X, 2) + Math.Pow(e.Y - movedVertex.Y, 2) <= graphDrawing.R * graphDrawing.R;

            if (movedVertexIndex != -1 && !highlight)
            {
                // Keeping the image inside the borders of the sheet
                float x = e.X, y = e.Y;
                if (x < graphDrawing.R + 5)
                    x = graphDrawing.R + 5;
                if (y < graphDrawing.R + 5)
                {
                    y = graphDrawing.R + 5;
                    if (GridAdjacencyMatrix[movedVertexIndex, movedVertexIndex].Value.ToString() != "0")
                        y += graphDrawing.R;
                }
                if (x > DrawingSurface.Width - graphDrawing.R - 5)
                {
                    x = DrawingSurface.Width - graphDrawing.R - 5;
                    if (GridAdjacencyMatrix[movedVertexIndex, movedVertexIndex].Value.ToString() != "0")
                        x -= graphDrawing.R;
                }
                if (y > DrawingSurface.Height - graphDrawing.R - 5)
                    y = DrawingSurface.Height - graphDrawing.R - 5;

                var command = new MoveVertexCommand(digraph, movedVertexIndex,
                    new Point(movedVertex.X, movedVertex.Y),
                    new Point((int)x, (int)y));
                commandsManager.Execute(command);
            }

            if (highlight) digraph.Vertices[movedVertexIndex] = new Vertex(movedVertex.X, movedVertex.Y);
            graphDrawing.DrawTheWholeGraph(digraph);

            if (highlight) graphDrawing.HighlightVertex(digraph.Vertices[movedVertexIndex]);

            DrawingSurface.Image = graphDrawing.Image;
            movedVertexIndex = -1;
            isPressed = false;
        }


        #region Adding and removing vertices 

        /// <summary>
        /// Adds a new line and a row to Adjacency Matrix in DataGridView
        /// </summary>
        private void AddVertexToGridAdjacencyMatrix(int index)
        {
            var column = new DataGridViewColumn
            {
                Name = string.Empty,
                HeaderText = (index + 1).ToString(),
                FillWeight = 1,
                Width = 35,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = new DataGridViewTextBoxCell()
            };
            GridAdjacencyMatrix.Columns.Insert(index, column);
            GridAdjacencyMatrix.Rows.Insert(index);

            for (int i = 0; i < digraph.Vertices.Count; i++)
            {
                GridAdjacencyMatrix[index, i].Value = 0;
                GridAdjacencyMatrix[i, index].Value = 0;
                GridAdjacencyMatrix.Columns[i].HeaderCell.Value = (i + 1).ToString();
                GridAdjacencyMatrix.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            GridAdjacencyMatrix.Rows[index].HeaderCell.Value = (index + 1).ToString();
        }

        private void RemoveVertexFromGridAdjacencyMatrix(int index)
        {
            GridAdjacencyMatrix.Columns.RemoveAt(index);
            if (GridAdjacencyMatrix.Columns.Count != 0) GridAdjacencyMatrix.Rows.RemoveAt(index);
            for (int j = index; j < digraph.Vertices.Count; j++)
            {
                GridAdjacencyMatrix.Columns[j].HeaderCell.Value = (j + 1).ToString();
                GridAdjacencyMatrix.Rows[j].HeaderCell.Value = (j + 1).ToString();
            }
        }

        private void AddVertexToGridParameters(int index)
        {
            if (GridParameters.ColumnCount == 0)
            {
                GridParameters.Columns.Add(String.Empty, "th");
                GridParameters.Columns.Add(String.Empty, "p");
                GridParameters.Columns.Add(String.Empty, "s");
                for (int i = 0; i < GridParameters.ColumnCount; ++i)
                {
                    GridParameters.Columns[i].FillWeight = 1;
                    GridParameters.Columns[i].Width = 70;
                    GridParameters.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            GridParameters.Rows.Insert(index);
            GridParameters.Rows[index].HeaderCell.Value = digraph.Vertices.Count.ToString();
            GridParameters[0, index].Value = digraph.Thresholds[index];
            GridParameters[1, index].Value = digraph.RefractoryPeriods[index];
            GridParameters[2, index].Value = digraph.State[index];
        }
        private void RemoveVertexFromGridParameters(int index)
        {
            GridParameters.Rows.RemoveAt(index);
            for (int j = index; j < digraph.Vertices.Count; j++)
                GridParameters.Rows[j].HeaderCell.Value = (j + 1).ToString();
        }

        #endregion


        /// <summary>
        /// Finds vertices chosen for creating a new arc 
        /// </summary>
        /// <param name="x">X coordinate of a place where mouse click occurred</param>
        /// <param name="y">Y coordinate of a place where mouse click occurred</param>
        private bool FindArcVertices(int x, int y)
        {
            for (int i = 0; i < digraph.Vertices.Count; i++)
                if (Math.Pow((digraph.Vertices[i].X - x), 2) + Math.Pow((digraph.Vertices[i].Y - y), 2) <= Math.Pow(graphDrawing.R, 2))
                {
                    if (vStart == -1)
                    {
                        vStart = i;
                        graphDrawing.HighlightVertex(digraph.Vertices[i]);
                        DrawingSurface.Image = graphDrawing.Image;
                        return false;
                    }
                    if (vEnd == -1)
                    {
                        if (GridAdjacencyMatrix[i, vStart].Value.ToString() != "0" || vStart == i)
                        {
                            graphDrawing.UnhighlightVertex(digraph.Vertices[vStart]);
                            DrawingSurface.Image = graphDrawing.Image;
                            if (GridAdjacencyMatrix[i, vStart].Value.ToString() != "0")
                                MessageBox.Show(@"The edge already exists", @"Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (vStart == i)
                                MessageBox.Show(@"Arc cannot be a loop", @"Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            vStart = vEnd = -1;
                            return false;
                        }
                        vEnd = i;
                        graphDrawing.HighlightVertex(digraph.Vertices[i]);
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
            for (int i = 0; i < digraph.Vertices.Count; i++)
                if (Math.Pow((digraph.Vertices[i].X - x), 2) + Math.Pow((digraph.Vertices[i].Y - y), 2) <=
                    Math.Pow(graphDrawing.R, 2))
                {
                    if (digraph.Stock.Contains(i)) digraph.Stock.Remove(i);
                    else digraph.Stock.Add(i);
                    graphDrawing.DrawTheWholeGraphSandpile(digraph, false);
                    DrawingSurface.Image = graphDrawing.Image;
                    return;
                }
        }

        /// <summary>
        /// Searches for a vertex at (x, y) and adds sand to it
        /// </summary>
        private async void SelectVertexToAddSand(int x, int y)
        {
            for (int i = 0; i < digraph.Vertices.Count; i++)
                if (Math.Pow((digraph.Vertices[i].X - x), 2) + Math.Pow((digraph.Vertices[i].Y - y), 2) <=
                    Math.Pow(graphDrawing.R, 2))
                {
                    if (digraph.Stock.Contains(i)) return;
                    digraph.State[i]++;
                    SandpilePanel.Visible = false;
                    graphDrawing.HighlightVertexToAddSand(digraph.Vertices[i]);
                    DrawingSurface.Image = graphDrawing.Image;
                    await Task.Delay(200);
                    if (SaveGifCheckBox.Checked && movement.MovementGif.Frames.Count < 250)
                    {
                        var bmp = ((Bitmap)DrawingSurface.Image).GetHbitmap();
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
