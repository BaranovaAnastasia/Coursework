using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DotsMovementModelingAppLib.Modeling;

namespace DotsMovementModelingAppLib
{
    public class GraphDrawing : IDisposable
    {
        #region Variables

        /// <summary>
        /// Image of the Graph
        /// </summary>
        public Bitmap Image { get; private set; }

        /// <summary>
        /// Graphics instance for drawing
        /// </summary>
        private Graphics graphics;

        /// <summary>
        /// Pen for drawing vertices
        /// </summary>
        private readonly Pen verticesPen = new Pen(Color.MidnightBlue, 2.5f);

        public Color VerticesColor
        {
            get => verticesPen.Color;
            set => verticesPen.Color = value;
        }

        /// <summary>
        /// Pen for drawing arcs
        /// </summary>
        private Pen arcsPen = new Pen(Color.FromArgb(80, Color.MidnightBlue), 3);

        public Color ArcsColor
        {
            get => arcsPen.Color;
            set => arcsPen = new Pen(Color.FromArgb(80, value), 3);
        }

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

        /// <summary>
        /// Brush for writing titles
        /// </summary>
        private readonly Brush brush = Brushes.Black;

        /// <summary>
        /// Drawing back color
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Vertices radius
        /// </summary>
        private static int _radius = 8;

        /// <summary>
        /// Vertices radius
        /// </summary>
        public int R
        {
            get => _radius;
            set
            {
                if (value < 8)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _radius = value;
                font = new Font(font.FontFamily.Name, _radius * 0.625f);
                RadiusChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Occurs when the Vertices radius changes
        /// </summary>
        public event EventHandler RadiusChanged;

        #endregion

        /// <summary>
        /// Initializes a new instance of the GraphDrawing class
        /// </summary>
        /// <param name="width">Width of the drawing surface</param>
        /// <param name="height">Height of the drawing surface</param>
        /// <exception cref="ArgumentException"/>
        public GraphDrawing(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("The image cannot have negative dimensions");
            Image = new Bitmap(width, height);
            graphics = Graphics.FromImage(Image);
            BackColor = Color.White;
            ClearTheSurface();
        }

        /// <summary>
        /// Cleans the drawing surface
        /// </summary>
        public void ClearTheSurface() => graphics.Clear(BackColor);

        /// <summary>
        /// Draws graph vertices
        /// </summary>
        /// <param name="x">X coordinate of the point where the vertex is</param>
        /// <param name="y">Y coordinate of the point where the vertex is</param>
        /// <param name="number">Number of the point</param>
        /// <param name="pen">Pen to draw</param>
        public void DrawVertex(int x, int y, int number, Pen pen = null)
        {
            if (pen == null) pen = verticesPen;
            graphics.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            graphics.DrawEllipse(pen, (x - R), (y - R), 2 * R, 2 * R);
            PointF point = number >= 100
                ? new PointF(x - font.Size * 2f, y - font.Size * 1.4f)
                : number >= 10
                    ? new PointF(x - font.Size * 1.4f, y - font.Size * 1.4f)
                    : new PointF(x - font.Size * 0.8f, y - font.Size * 1.4f);

            graphics.DrawString(number.ToString(), font, brush, point);
        }

        /// <summary>
        /// Highlights graph vertex
        /// </summary>
        public void HighlightVertex(Vertex vertex) =>
            graphics.DrawEllipse(highlightPen, (vertex.X - R), (vertex.Y - R), 2 * R, 2 * R);

        /// <summary>
        /// Highlights graph vertex before adding sand to it (sandpile modeling)
        /// </summary>
        public void HighlightVertexToAddSand(Vertex vertex) =>
            graphics.DrawEllipse(highlightSandpilePen, (vertex.X - R * 1.1f), (vertex.Y - R * 1.1f), 2 * R * 1.1f, 2 * R * 1.1f);


        /// <summary>
        /// Removes highlighting from graph vertex
        /// </summary>
        public void UnhighlightVertex(Vertex vertex) =>
            graphics.DrawEllipse(verticesPen, (vertex.X - R), (vertex.Y - R), 2 * R, 2 * R);

        /// <summary>
        /// Draws graph arc
        /// </summary>
        /// <param name="startVertex">Starting vertex</param>
        /// <param name="endVertex">Ending vertex</param>
        /// <param name="arc">Arc itself</param>
        /// <param name="xOffset">X-axis image offset</param>
        /// <param name="yOffset">Y-axis image offset</param>
        /// <param name="sizeCoef">Resizing coefficient</param>
        public void DrawArc(Vertex startVertex, Vertex endVertex, Arc arc, int xOffset = 0, int yOffset = 0, double sizeCoef = 1)
        {
            startVertex.X = (int) ((startVertex.X + xOffset) * sizeCoef);
            startVertex.Y = (int)((startVertex.Y + yOffset) * sizeCoef);
            endVertex.X = (int)((endVertex.X + xOffset) * sizeCoef);
            endVertex.Y = (int)((endVertex.Y + yOffset) * sizeCoef);
            if (arc.StartVertex == arc.EndVertex)
                throw new ArgumentException("Arc cannot be a loop");
            graphics.DrawLine(arcsPen, startVertex.X, startVertex.Y, endVertex.X, endVertex.Y);
            DrawVertex(startVertex.X, startVertex.Y, arc.StartVertex + 1);
            DrawVertex(endVertex.X, endVertex.Y, arc.EndVertex + 1);

            // Drawing the edge's direction
            double[] l = { startVertex.X - endVertex.X, startVertex.Y - endVertex.Y };
            double length = Math.Sqrt(l[0] * l[0] + l[1] * l[1]);
            l[0] /= length;
            l[1] /= length;
            double[] w = { -l[1] * R / 3, l[0] * R / 3 };
            double x = endVertex.X + l[0] * 2 * R + w[0];
            double y = endVertex.Y + l[1] * 2 * R + w[1];
            graphics.DrawLine(arcsPen, (float)x, (float)y, (float)(endVertex.X + l[0] * R), (float)(endVertex.Y + l[1] * R));
            x -= 2 * w[0];
            y -= 2 * w[1];
            graphics.DrawLine(arcsPen, (float)x, (float)y, (float)(endVertex.X + l[0] * R), (float)(endVertex.Y + l[1] * R));
        }

        /// <summary>
        /// Draws all the digraph vertices
        /// </summary>
        public void DrawVertices(Digraph digraph, int xOffset = 0, int yOffset = 0, double sizeCoef = 1)
        {
            for (int i = 0; i < digraph.Vertices.Count; ++i)
                DrawVertex((int)((digraph.Vertices[i].X + xOffset)*sizeCoef), (int)((digraph.Vertices[i].Y + yOffset) * sizeCoef), i + 1);
        }

        /// <summary>
        /// Draws the whole digraph
        /// </summary>
        public void DrawTheWholeGraph(Digraph digraph, int xOffset = 0, int yOffset = 0, double sizeCoef = 1)
        {
            ClearTheSurface();
            digraph.Arcs.ForEach(arc =>
                DrawArc(digraph.Vertices[arc.StartVertex], digraph.Vertices[arc.EndVertex], arc,
                    xOffset, yOffset, sizeCoef));
            DrawVertices(digraph, xOffset, yOffset, sizeCoef);
        }

        /// <summary>
        /// Draws a moving dot
        /// </summary>
        /// <param name="p"></param>
        public void DrawDot(PointF p) =>
            graphics.FillEllipse(Brushes.Black, p.X - 4, p.Y - 4, 8, 8);


        #region Sandpile

        /// <summary>
        /// Colors palette for sandpile drawing
        /// </summary>
        private Color[] sandpilePalette;

        /// <summary>
        /// Digraph incidence list
        /// </summary>
        private List<Arc>[] incidenceList;

        /// <summary>
        /// Number of dots in a vertex font
        /// </summary>
        private readonly Font sandpileFont = new Font("Segoe UI", 5);

        /// <summary>
        /// Occurs when the palette colors changes
        /// </summary>
        public event EventHandler SandpilePaletteChanged;

        /// <summary>
        /// Colors palette for sandpile drawing
        /// </summary>
        public Color[] SandpilePalette
        {
            get => sandpilePalette;
            set
            {
                sandpilePalette = value;
                SandpilePaletteChanged?.Invoke(value, new EventArgs());
            }
        }

        /// <summary>
        /// Draws the whole digraph using sandpile palette
        /// </summary>
        /// <param name="digraph">Digraph</param>
        /// <param name="update">Is it needed to update an incidence list and colors palette
        /// (true if digraph has changed since the last call, or if the method is called for the first time)</param>
        /// <param name="xOffset">X-axis image offset</param>
        /// <param name="yOffset">Y-axis image offset</param>
        /// <param name="sizeCoef">Resizing coefficient</param>
        public void DrawTheWholeGraphSandpile(Digraph digraph, bool update, int xOffset = 0, int yOffset = 0, double sizeCoef = 1)
        {
            if (update)
            {
                incidenceList = MovementModeling.GetIncidenceList(digraph);
                SandpilePalette = GetSandpilePalette(incidenceList.Max(arcs => arcs.Count));
            }

            ClearTheSurface();
            digraph.Arcs.ForEach(arc =>
                DrawArc(digraph.Vertices[arc.StartVertex], digraph.Vertices[arc.EndVertex], arc,
                    xOffset, yOffset, sizeCoef));
            DrawVerticesSandpile(digraph, xOffset, yOffset, sizeCoef);
        }

        /// <summary>
        /// Draws all the digraph vertices in sandpile format
        /// </summary>
        /// <param name="digraph">Digraph</param>
        /// <param name="xOffset">X-axis image offset</param>
        /// <param name="yOffset">Y-axis image offset</param>
        /// <param name="sizeCoef">Resizing coefficient</param>
        public void DrawVerticesSandpile(Digraph digraph, int xOffset = 0, int yOffset = 0, double sizeCoef = 1)
        {
            for (int i = 0; i < digraph.State.Count; i++)
            {
                DrawVertex((int)((digraph.Vertices[i].X + xOffset) * sizeCoef),
                    (int)((digraph.Vertices[i].Y + yOffset) * sizeCoef), 
                    i + 1,
                    new Pen(digraph.State[i] >= incidenceList[i].Count || digraph.Stock.Contains(i)
                        ? Color.Black
                        : SandpilePalette[digraph.State[i]], 4f));

                if (digraph.Stock.Contains(i))
                    graphics.FillEllipse(Brushes.Black, 
                        (int)((digraph.Vertices[i].X + xOffset) * sizeCoef) - R, 
                        (int)((digraph.Vertices[i].Y + yOffset) * sizeCoef) - R, 
                        2 * R, 2 * R);
                else
                    graphics.DrawString($"({digraph.State[i]})", sandpileFont, brush,
                        (int)((digraph.Vertices[i].X + xOffset) * sizeCoef) + R,
                        (int)((digraph.Vertices[i].Y + yOffset) * sizeCoef) - R - 10f);
            }
        }

        /// <summary>
        /// Returns a gradient colors palette
        /// </summary>
        /// <param name="start">Start color</param>
        /// <param name="end">End color</param>
        /// <param name="steps">Number of colors</param>
        private static Color[] GetGradientColors(Color start, Color end, int steps)
        {
            Color[] colors = new Color[steps];
            colors[steps - 1] = end;
            colors[0] = start;

            var aStep = (end.A - start.A) * 1.0 / steps;
            var rStep = (end.R - start.R) * 1.0 / steps;
            var gStep = (end.G - start.G) * 1.0 / steps;
            var bStep = (end.B - start.B) * 1.0 / steps;

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

        /// <summary>
        /// Returns colors palette for sandpile drawing
        /// </summary>
        /// <param name="paletteSize">Number of colors in palette</param>
        private Color[] GetSandpilePalette(int paletteSize)
        {
            List<Color> colors = new List<Color>(paletteSize);
            int steps = paletteSize > 5 ? 5 : paletteSize;
            int stepLength = paletteSize / steps;
            int currStep = stepLength;
            for (int i = 0; i < steps; i++)
            {
                switch (i)
                {
                    case 0:
                        colors.AddRange(GetGradientColors(Color.Red, Color.Yellow,
                            steps * stepLength < paletteSize
                            ? stepLength + 1 : stepLength));
                        break;
                    case 1:
                        if (stepLength > 1 || steps * stepLength < paletteSize && stepLength > 0)
                        {
                            colors.Remove(Color.Yellow);
                            currStep++;
                        }
                        colors.AddRange(GetGradientColors(Color.Yellow, Color.Green,
                            steps * stepLength < paletteSize && paletteSize % steps * stepLength >= 2
                                ? currStep + 1 : currStep));
                        currStep = stepLength;
                        break;
                    case 2:
                        if (stepLength > 1 || steps * stepLength < paletteSize && steps * stepLength % paletteSize >= 2 && stepLength > 0)
                        {
                            colors.Remove(Color.Green);
                            currStep++;
                        }
                        colors.AddRange(GetGradientColors(Color.Green, Color.LightSkyBlue,
                            steps * stepLength < paletteSize && paletteSize % steps * stepLength >= 3
                                ? currStep + 1 : currStep));
                        currStep = stepLength;
                        break;
                    case 3:
                        if (stepLength > 1 || steps * stepLength < paletteSize && steps * stepLength % paletteSize >= 3 && stepLength > 0)
                        {
                            colors.Remove(Color.LightSkyBlue);
                            currStep++;
                        }
                        colors.AddRange(GetGradientColors(Color.LightSkyBlue, Color.Blue,
                            steps * stepLength < paletteSize && paletteSize % steps * stepLength >= 4
                                ? currStep + 1 : currStep));
                        currStep = stepLength;
                        break;
                    case 4:
                        if (stepLength > 1 || steps * stepLength < paletteSize && steps * stepLength % paletteSize >= 4 && stepLength > 0)
                        {
                            colors.Remove(Color.Blue);
                            currStep++;
                        }
                        colors.AddRange(GetGradientColors(Color.Blue, Color.DeepPink, currStep));
                        currStep = stepLength;
                        break;
                }
            }

            return colors.ToArray();
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
                if(value.Height == 0 || value.Width == 0) return;
                Image = new Bitmap(value.Width, value.Height);
                graphics = Graphics.FromImage(Image);
            }
        }

        public void Dispose()
        {
            Image.Dispose();
            graphics.Dispose();
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
