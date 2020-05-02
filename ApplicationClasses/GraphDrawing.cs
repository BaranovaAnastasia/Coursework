using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using ApplicationClasses.Modeling;

namespace ApplicationClasses
{
    public class GraphDrawing : IDisposable
    {
        /// <summary>
        /// Image of the Graph
        /// </summary>
        public Bitmap Image { get; private set; }

        /// <summary>
        /// Graphics instance for drawing
        /// </summary>
        private Graphics drawing;

        /// <summary>
        /// Pen for drawing vertices
        /// </summary>
        private readonly Pen verticesPen = new Pen(Color.MidnightBlue, 2.5f);

        /// <summary>
        /// Pen for drawing arcs
        /// </summary>
        private readonly Pen arcsPen = new Pen(Color.FromArgb(80, Color.MidnightBlue), 3);

        /// <summary>
        /// Pen for highlighting vertices
        /// </summary>
        private readonly Pen highlightPen = new Pen(Color.MediumAquamarine, 2.5f);
        /// <summary>
        /// Pen for highlighting vertices for adding sand (sandpile modeling)
        /// </summary>
        private readonly Pen highlightSandpilePen = new Pen(Color.Gold, 4);

        /// <summary>
        /// Font for vertices titles
        /// </summary>
        private Font font = new Font("Segoe UI", 5);
        private readonly Font sandpileFont = new Font("Segoe UI", 5);

        /// <summary>
        /// Brush for writing titles
        /// </summary>
        private readonly Brush brush = Brushes.Black;

        /// <summary>
        /// Drawing back color
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Colors palette for sandpile drawing
        /// </summary>
        public Color[] SandpilePalette { get; set; }

        /// <summary>
        /// Vertices radius
        /// </summary>
        private static int radius = 8;

        /// <summary>
        /// Vertices radius
        /// </summary>
        public int R
        {
            get => radius;
            set
            {
                if (value < 8)
                    throw new ArgumentOutOfRangeException(nameof(value));
                radius = value;
                font = new Font(font.FontFamily.Name, radius * 0.625f);
                RadiusChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        private PointF point; //Helper variable

        /// <summary>
        /// Occurs when the Vertices radius changes
        /// </summary>
        public event EventHandler RadiusChanged;

        /// <summary>
        /// Initializes a new instance of the GraphDrawing class
        /// </summary>
        /// <param name="width">Width of the drawing surface</param>
        /// <param name="height">Height of the drawing surface</param>
        public GraphDrawing(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("The image cannot have negative dimensions");
            Image = new Bitmap(width, height);
            drawing = Graphics.FromImage(Image);
            BackColor = Color.White;
            ClearTheSurface();
        }

        /// <summary>
        /// Cleans the drawing surface
        /// </summary>
        public void ClearTheSurface() => drawing.Clear(BackColor);

        /// <summary>
        /// Draws graph vertices
        /// </summary>
        /// <param name="x">X coordinate of the point where the vertex is</param>
        /// <param name="y">Y coordinate of the point where the vertex is</param>
        /// <param name="number">Number of the point</param>
        /// <param name="pen">Pen to draw</param>
        public void DrawVertex(int x, int y, int number, Pen pen)
        {
            drawing.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            drawing.DrawEllipse(pen, (x - R), (y - R), 2 * R, 2 * R);
            point = number >= 100
                ? new PointF(x - font.Size * 2f, y - font.Size * 1.4f)
                : number >= 10
                    ? new PointF(x - font.Size * 1.4f, y - font.Size * 1.4f)
                    : new PointF(x - font.Size * 0.8f, y - font.Size * 1.4f);

            drawing.DrawString(number.ToString(), font, brush, point);
        }

        /// <summary>
        /// Highlights graph vertex
        /// </summary>
        public void HighlightVertex(Vertex vertex) =>
            drawing.DrawEllipse(highlightPen, (vertex.X - R), (vertex.Y - R), 2 * R, 2 * R);

        /// <summary>
        /// Highlights graph vertex before adding sand to it (sandpile modeling)
        /// </summary>
        public void HighlightVertexToAddSand(Vertex vertex) =>
            drawing.DrawEllipse(highlightSandpilePen, (vertex.X - R * 1.1f), (vertex.Y - R * 1.1f), 2 * R * 1.1f, 2 * R * 1.1f);


        /// <summary>
        /// Removes highlighting from graph vertex
        /// </summary>
        public void UnhighlightVertex(Vertex vertex) =>
            drawing.DrawEllipse(verticesPen, (vertex.X - R), (vertex.Y - R), 2 * R, 2 * R);

        /// <summary>
        /// Draws graph arc
        /// </summary>
        /// <param name="startVertex">Starting vertex</param>
        /// <param name="endVertex">Ending vertex</param>
        /// <param name="arc">Arc itself</param>
        public void DrawArc(Vertex startVertex, Vertex endVertex, Arc arc)
        {
            if (arc.StartVertex == arc.EndVertex)
                throw new ArgumentException("Arc cannot be a loop");
            drawing.DrawLine(arcsPen, startVertex.X, startVertex.Y, endVertex.X, endVertex.Y);
            DrawVertex(startVertex.X, startVertex.Y, arc.StartVertex + 1, verticesPen);
            DrawVertex(endVertex.X, endVertex.Y, arc.EndVertex + 1, verticesPen);

            // Drawing the edge's direction
            double[] l = { startVertex.X - endVertex.X, startVertex.Y - endVertex.Y };
            double length = Math.Sqrt(l[0] * l[0] + l[1] * l[1]);
            l[0] /= length;
            l[1] /= length;
            double[] w = { -l[1] * R / 3, l[0] * R / 3 };
            double x = endVertex.X + l[0] * 2 * R + w[0];
            double y = endVertex.Y + l[1] * 2 * R + w[1];
            drawing.DrawLine(arcsPen, (float)x, (float)y, (float)(endVertex.X + l[0] * R), (float)(endVertex.Y + l[1] * R));
            x -= 2 * w[0];
            y -= 2 * w[1];
            drawing.DrawLine(arcsPen, (float)x, (float)y, (float)(endVertex.X + l[0] * R), (float)(endVertex.Y + l[1] * R));
        }

        /// <summary>
        /// Draws the whole digraph
        /// </summary>
        public void DrawTheWholeGraph(Digraph digraph)
        {
            ClearTheSurface();
            digraph.Arcs.ForEach(arc =>
                DrawArc(digraph.Vertices[arc.StartVertex], digraph.Vertices[arc.EndVertex], arc));
            for (int i = 0; i < digraph.Vertices.Count; ++i)
                DrawVertex(digraph.Vertices[i].X, digraph.Vertices[i].Y, i + 1, verticesPen);
        }

        #region Sandpile

        /// <summary>
        /// Draws the whole digraph using sandpile palette
        /// </summary>
        public void DrawTheWholeGraphSandpile(Digraph digraph, List<Arc>[] incidenceList = null, Color[] palette = null)
        {
            if (incidenceList == null) incidenceList = MovementModeling.GetIncidenceList(digraph);
            if (palette != null) SandpilePalette = palette;
            else if(SandpilePalette == null) 
                SandpilePalette = GetGradientColors(Color.Crimson, Color.CadetBlue, incidenceList.Max(arcs => arcs.Count));
            
            ClearTheSurface();
            for (int i = 0; i < SandpilePalette.Length; i++)
            {
                drawing.DrawLine(new Pen(SandpilePalette[i], 7f), Image.Width - 75, i * 10 + 40, Image.Width - 30, i * 10 + 40);
                drawing.DrawString(i.ToString(), sandpileFont, brush, Image.Width - 10, i * 10 + 35);
            }
            digraph.Arcs.ForEach(arc =>
                    DrawArc(digraph.Vertices[arc.StartVertex], digraph.Vertices[arc.EndVertex], arc));
            for (int i = 0; i < digraph.Vertices.Count; ++i)
            {
                DrawVertex(digraph.Vertices[i].X, digraph.Vertices[i].Y, i + 1,
                    new Pen(digraph.State[i] >= incidenceList[i].Count || digraph.Stock.Contains(i)
                        ? Color.Black
                        : SandpilePalette[digraph.State[i]], 4f));
                if (digraph.Stock.Contains(i))
                    drawing.FillEllipse(Brushes.Black, (digraph.Vertices[i].X - R), (digraph.Vertices[i].Y - R), 2 * R, 2 * R);
                drawing.DrawString(digraph.State[i].ToString(), sandpileFont, brush,
                    digraph.Vertices[i].X + R, digraph.Vertices[i].Y - R - 5);
            }
        }

        public void DrawDot(PointF point) => drawing.FillEllipse(Brushes.Black, point.X - 4, point.Y - 4, 8, 8);

        public void DrawVerticesSandpile(Digraph digraph, List<Arc>[] incidenceList, Color[] palette)
        {
            for (int i = 0; i < digraph.State.Count; i++)
            {
                DrawVertex(digraph.Vertices[i].X, digraph.Vertices[i].Y, i + 1,
                    new Pen(digraph.State[i] >= incidenceList[i].Count || digraph.Stock.Contains(i)
                        ? Color.Black
                        : palette[digraph.State[i]], 4f));
                if (digraph.Stock.Contains(i))
                    drawing.FillEllipse(Brushes.Black, (digraph.Vertices[i].X - R), (digraph.Vertices[i].Y - R), 2 * R, 2 * R);
            }
        }

        /// <summary>
        /// Creates a gradient colors palette for sandpile graph drawing
        /// </summary>
        /// <param name="start">Start color</param>
        /// <param name="end">End color</param>
        /// <param name="steps">Number of colors</param>
        private static Color[] GetGradientColors(Color start, Color end, int steps)
        {
            Color[] colors = new Color[steps];
            colors[0] = start;
            colors[steps - 1] = end;

            double aStep = (end.A - start.A) / steps;
            double rStep = (end.R - start.R) / steps;
            double gStep = (end.G - start.G) / steps;
            double bStep = (end.B - start.B) / steps;

            for (int i = 1; i < steps - 1; i++)
            {
                var a = start.A + (aStep * i);
                var r = start.R + (rStep * i);
                var g = start.G + (gStep * i);
                var b = start.B + (bStep * i);
                colors[i] = Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
            }
            return colors;
        }

        #endregion

        /// <summary>
        /// Drawing surface size
        /// </summary>
        public Size Size
        {
            get => Image.Size;
            set
            {
                Image = new Bitmap(value.Width, value.Height);
                drawing = Graphics.FromImage(Image);
            }
        }

        public void Dispose()
        {
            Image.Dispose();
            drawing.Dispose();
            verticesPen.Dispose();
            arcsPen.Dispose();
            highlightPen.Dispose();
            highlightSandpilePen.Dispose();
            font.Dispose();
            sandpileFont.Dispose();
            brush.Dispose();
        }
    }
}
