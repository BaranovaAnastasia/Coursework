using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace ApplicationClasses
{
    public class GraphDrawing
    {
        /// <summary>
        /// Image of the Graph
        /// </summary>
        public Bitmap Image { get; private set; }

        /// <summary>
        /// Graphics' instance for drawing
        /// </summary>
        private Graphics drawing;

        /// <summary>
        /// MidnightBlue Pen for drawing vertices
        /// </summary>
        private readonly Pen verticesPen = new Pen(Color.MidnightBlue, 2.5f);
        /// <summary>
        /// MidnightBlue Pen for drawing edges
        /// </summary>
        private readonly Pen edgesPen = new Pen(Color.FromArgb(80, Color.MidnightBlue), 3);
        /// <summary>
        /// MediumAquamarine Pen for highlighting vertices
        /// </summary>
        private readonly Pen highlightPen = new Pen(Color.MediumAquamarine, 2.5f);

        /// <summary>
        /// Font for vertices titles
        /// </summary>
        private readonly Font font = new Font("Times New Roman", 7);

        /// <summary>
        /// DarkSlateGray Brush for writing titles
        /// </summary>
        private readonly Brush brush = Brushes.Black;

        public Color BackColor { get; set; }

        /// <summary>
        /// Vertices radius
        /// </summary>
        public static readonly int R = 8;

        private PointF point; //Helper variable

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
        public void DrawVertex(int x, int y, int number, Pen pen)
        {
            drawing.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            drawing.DrawEllipse(pen, (x - R), (y - R), 2 * R, 2 * R);
            point = number >= 100 ? new PointF(x - 13.5f, y - 7) :
                number >= 10 ? new PointF(x - 8, y - 7) : new PointF(x - 4f, y - 7);
            drawing.DrawString(number.ToString(), font, brush, point);
        }

        /// <summary>
        /// Highlights graph vertex
        /// </summary>
        public void HighlightVertex(Vertex vertex) => drawing.DrawEllipse(highlightPen, (vertex.X - R), (vertex.Y - R), 2 * R, 2 * R);

        /// <summary>
        /// Removes highlighting from graph vertex
        /// </summary>
        public void UnhighlightVertex(Vertex vertex) => drawing.DrawEllipse(verticesPen, (vertex.X - R), (vertex.Y - R), 2 * R, 2 * R);

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
            drawing.DrawLine(edgesPen, startVertex.X, startVertex.Y, endVertex.X, endVertex.Y);
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
            drawing.DrawLine(edgesPen, (float)x, (float)y, (float)(endVertex.X + l[0] * R), (float)(endVertex.Y + l[1] * R));
            x -= 2 * w[0];
            y -= 2 * w[1];
            drawing.DrawLine(edgesPen, (float)x, (float)y, (float)(endVertex.X + l[0] * R), (float)(endVertex.Y + l[1] * R));
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

        public void DrawTheWholeGraphSandpile(Digraph digraph, List<Arc>[] incidenceList)
        {
            int max = incidenceList.Max(arcs => arcs.Count);
            colors = GetGradientColors(Color.Red, Color.Blue, max);
            ClearTheSurface();
            for (int i = 0; i < colors.Length; i++)
            {
                drawing.DrawLine(new Pen(colors[i], 2.5f), Image.Width - 50, i*10 + 10, Image.Width - 10, i * 10 + 10);
                drawing.DrawString(i.ToString(), font, brush, Image.Width - 10, i * 10 + 10);
            }
            digraph.Arcs.ForEach(arc =>
                    DrawArc(digraph.Vertices[arc.StartVertex], digraph.Vertices[arc.EndVertex], arc));
            for (int i = 0; i < digraph.Vertices.Count; ++i)
                DrawVertex(digraph.Vertices[i].X, digraph.Vertices[i].Y, i + 1,
                    new Pen(digraph.State[i] >= max ? Color.Black : colors[digraph.State[i]], 2.5f));
        }

        private Color[] colors;
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

        public void DrawDot(PointF point) => drawing.FillEllipse(Brushes.Black, point.X - 4, point.Y - 4, 8, 8);

        public Size Size
        {
            get => Image.Size;
            set
            {
                Image = new Bitmap(value.Width, value.Height);
                drawing = Graphics.FromImage(Image);
            }
        }
    }
}
