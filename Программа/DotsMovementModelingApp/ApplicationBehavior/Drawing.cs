﻿using DotsMovementModelingAppLib;
using DotsMovementModelingAppLib.Commands;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace DotsMovementModelingApp
{
    public partial class MainWindow
    {
        /// <summary>
        /// Draws vertices and arcs by mouse click on a drawing surface
        /// or selects a vertex to add sand to during sandpile modeling
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
                if (digraph.Vertices.Count >= 200)
                {
                    MessageBox.Show(@"Too many vertices. Unable to add a new one.",
                        @"Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (DigraphComponentsRemover.TryToDeleteVertexAt(e.X, e.Y, digraph, graphDrawing.R, out int i))
            {
                var command = new EraseVertexCommand(digraph, digraph.Vertices[i]);
                commandsManager.Execute(command);
                graphDrawing.DrawTheWholeGraph(digraph);
                DrawingSurface.Image = graphDrawing.Image;
            }
            else if (DigraphComponentsRemover.TryToDeleteArcAt(e.X, e.Y, digraph, out Arc arc))
            {
                var command = new EraseArcCommand(digraph, arc);
                commandsManager.Execute(command);
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

            bool highlight = (DateTime.Now - ticks).Ticks < 2200000 &&
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
                                MessageBox.Show($@"Unable to add {vStart + 1}-{i + 1} arc. Such an arc already exists", @"Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (vStart == i)
                                MessageBox.Show($@"Unable to add {vStart + 1}-{i + 1} arc. Arc cannot be a loop", @"Error",
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
