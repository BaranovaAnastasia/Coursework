using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationClasses;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;
using System.Runtime.Remoting.Channels;
using System.Windows.Media.Imaging;

namespace ApplicationClasses
{
    /// <summary>
    /// Contains instance methods for Modeling the Movement of Points on Directed Metric Graph,
    /// with the Condition of Synchronization at the Vertices
    /// </summary>
    public class MovementModeling
    {
        /// <summary>
        /// Initialize new instance of MovementModeling class
        /// </summary>
        /// <param name="digraph">Digraph</param>
        /// <param name="time">Movement process duration in milliseconds</param>
        /// <param name="speed">Speed in unit per millisecond</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public MovementModeling(Digraph digraph, double speed)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            if (speed <= 0)
                throw new ArgumentOutOfRangeException(nameof(speed), "Speed of movement should be positive");
            this.speed = speed;
            IsActive = false;
        }

        private readonly Digraph digraph;
        private GraphDrawing graphDrawing;
        private PictureBox drawingSurface;
        private readonly double speed;
        private List<Arc>[] incidenceList;
        private List<Arc> involvedArcs;
        private List<Stopwatch> timers;
        private Stopwatch mainStopwatch = new Stopwatch();
        private Timer mainTimer = new Timer() { Interval = 50 };
        public List<Bitmap> Gif = new List<Bitmap>();


        /// <summary>
        /// Creates an array of lists of the arcs coming from the digraph vertices
        /// </summary>
        public static List<Arc>[] GetIncidenceList(Digraph digraph)
        {
            List<Arc>[] incidenceList = new List<Arc>[digraph.Vertices.Count];
            for (int i = 0; i < incidenceList.Length; i++)
                incidenceList[i] = digraph.Arcs.Where(arc => arc.StartVertex == i).ToList();
            return incidenceList;
        }

        /// <summary>
        /// Returns time required to travel a given path at a given speed
        /// </summary>
        /// <param name="length">Path length</param>
        /// <param name="speed">Speed in units per millisecond</param>
        /// <returns>Time in milliseconds</returns>
        public static double GetTime(double length, double speed) => length / speed;


        public int GetNumber(Digraph digraph, double time, int speed, Form form, out Series data)
        {
            data = new Series("Number of dots")
            {
                ChartType = SeriesChartType.Line,
                MarkerColor = Color.Red,
                ChartArea = "Chart",
                BorderWidth = 3
            };
            double fullTime = time;

            int number = 0; int k = 1;
            List<Arc>[] incidenceList = GetIncidenceList(digraph);
            List<Arc> involvedArcs = new List<Arc>(0);
            List<double> involvedArcsLength = new List<double>(0);
            double minLength;
            do
            {
                data.Points.AddXY(fullTime - time, number);
                for (int i = 0; i < digraph.State.Count; i++)
                    if (digraph.State[i] == 0)
                    {
                        involvedArcs.AddRange(incidenceList[i]);
                        involvedArcsLength.AddRange(incidenceList[i].ConvertAll(arc => arc.Length));
                        number += incidenceList[i].Count;
                    }
                data.Points.AddXY(fullTime - time, number);
                form.Text = involvedArcs.Count.ToString();
                if (involvedArcs.Count != 0)
                {
                    minLength = involvedArcsLength[0];
                    List<int> minLengthIndices = new List<int>(1);
                    minLengthIndices.Add(0);
                    for (var i = 1; i < involvedArcs.Count; i++)
                    {
                        if (involvedArcsLength[i] == minLength)
                            minLengthIndices.Add(i);
                        if (involvedArcsLength[i] >= minLength) continue;
                        minLength = involvedArcsLength[i];
                        minLengthIndices = new List<int>(1);
                        minLengthIndices.Add(i);
                    }
                    if (time > GetTime(minLength, speed))
                    {
                        time -= GetTime(minLength, speed);
                        data.Points.AddXY(fullTime - time, number);
                        number = number - minLengthIndices.Count;
                        for (int i = 0; i < digraph.State.Count; i++)
                            digraph.State[i] = digraph.State[i] < digraph.Thresholds[i] - 1 ? digraph.State[i] + 1 : 0;
                        involvedArcsLength = involvedArcsLength.ConvertAll(len => len - minLength);
                        for (int i = 0; i < involvedArcsLength.Count; i++)
                        {
                            if (involvedArcsLength[i] == 0)
                            {
                                involvedArcsLength.RemoveAt(i);
                                involvedArcs.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                    else time = 0;
                    form.Text = number + " " + k++;
                }
                else return number;
            } while (time > 0);
            return number;
        }

        /// <summary>
        /// Returns current coordinates of the dot traveling along the arc
        /// </summary>
        /// <param name="start">Vertex from which the dot exited</param>
        /// <param name="end">Vertex towards which the dot is now going</param>
        /// <param name="length">Arc length</param>
        /// <param name="timer">Elapsed time since the dot was exited</param>
        /// <returns></returns>
        public PointF GetPoint(Vertex start, Vertex end, double length, Stopwatch timer)
        {
            double len = timer.ElapsedMilliseconds * speed;
            double x = 1.0 * start.X + (end.X - start.X) * len / length;
            double y = 1.0 * start.Y + (end.Y - start.Y) * len / length;
            return new PointF((float)x, (float)y);
        }

        private Results resultsForm = null;
        private Series data;

        /// <summary>
        /// Starts dots movement
        /// </summary>
        public void Movement(GraphDrawing graphics, PictureBox picture, MovementModelingType type, MovementModelingMode[] modes)
        {
            if (type == MovementModelingType.Basic) mainTimer.Tick += TickBasicAnimation;
            else mainTimer.Tick += TickSandpileAnimation;
            if (modes.Contains(MovementModelingMode.Chart))
            {
                mainTimer.Tick += TickChartFilling;
                resultsForm = new Results(); 
                data = new Series("Number of dots")
                {
                    ChartType = SeriesChartType.Line,
                    Color = Color.DarkCyan,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerColor = Color.DarkCyan,
                    ChartArea = "Chart",
                    BorderWidth = 1
                };
                resultsForm.chart1.Series.Add(data);
                resultsForm.chart1.ChartAreas.Add("Chart");
                    //resultsForm.chart1.ChartAreas[0].AxisX.Interval = 0.1;
                resultsForm.Closing += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                {
                    mainTimer.Tick -= TickChartFilling;
                };
                resultsForm.Show();
            }

            incidenceList = GetIncidenceList(digraph);
            involvedArcs = new List<Arc>();
            timers = new List<Stopwatch>();
            graphDrawing = graphics;
            drawingSurface = picture;
            IsActive = true;
            mainTimer.Start();
            mainStopwatch.Start();
        }

        public bool IsActive { get; private set; }

        public void Stop()
        {
            mainTimer.Stop();
            mainStopwatch.Stop();
            timers.ForEach(timer => timer.Stop());
            IsActive = false;
        }

        public void Go()
        {
            mainTimer.Start();
            mainStopwatch.Start();
            timers.ForEach(timer => timer.Start());
            IsActive = true;
        }

        /// <summary>
        /// Draws all the currently moving dots
        /// </summary>
        private void TickBasicAnimation(object source, EventArgs e)
        {
            Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));
            int count = involvedArcs.Count;
            for (var i = 0; i < digraph.State.Count; i++)
            {
                if (digraph.State[i] >= digraph.Thresholds[i] && digraph.TimeTillTheEndOfRefractoryPeriod[i] <= 0)
                {
                    involvedArcs.AddRange(incidenceList[i]);
                    timers.AddRange(incidenceList[i].ConvertAll(arc => new Stopwatch()));
                    digraph.State[i] -= digraph.Thresholds[i];
                    digraph.TimeTillTheEndOfRefractoryPeriod[i] += digraph.RefractoryPeriods[i];

                    if (digraph.RefractoryPeriods[i] == 0)
                        while (digraph.State[i] >= digraph.Thresholds[i])
                        {
                            involvedArcs.AddRange(incidenceList[i]);
                            timers.AddRange(incidenceList[i].ConvertAll(arc => new Stopwatch()));
                            digraph.State[i] -= digraph.Thresholds[i];
                        }

                    continue;
                }

                digraph.TimeTillTheEndOfRefractoryPeriod[i] =
                    digraph.TimeTillTheEndOfRefractoryPeriod[i] - mainTimer.Interval >= 0
                        ? digraph.TimeTillTheEndOfRefractoryPeriod[i] - mainTimer.Interval
                        : 0;
            }

            for (int i = count; i < timers.Count; i++)
                timers[i].Start();

            if (involvedArcs.Count == 0)
            {

                graphDrawing.DrawTheWholeGraph(digraph);
                drawingSurface.Image = graphDrawing.Image;
                if (IsMovementEndedBasic()) MovementEnded?.Invoke(this, null);
                return;
            }

            graphDrawing.DrawTheWholeGraph(digraph);
            for (var i = 0; i < involvedArcs.Count; i++)
            {
                if (timers[i].ElapsedMilliseconds >= GetTime(involvedArcs[i].Length, speed))
                {
                    digraph.State[involvedArcs[i].EndVertex]++;
                    timers.RemoveAt(i);
                    involvedArcs.RemoveAt(i);

                    i--;
                    continue;
                }
                PointF point =
                    GetPoint(digraph.Vertices[involvedArcs[i].StartVertex],
                        digraph.Vertices[involvedArcs[i].EndVertex],
                        involvedArcs[i].Length,
                        timers[i]);
                graphDrawing.DrawDot(point);
                drawingSurface.Image = graphDrawing.Image;
            }
            for (int i = 0; i < digraph.Vertices.Count; ++i)
                graphDrawing.DrawVertex(digraph.Vertices[i].X, digraph.Vertices[i].Y, i + 1, new Pen(Color.MidnightBlue, 2.5f));

            if (IsMovementEndedBasic()) MovementEnded?.Invoke(this, null);
        }


        private void TickSandpileAnimation(object source, EventArgs e)
        {
                Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));
            int count = involvedArcs.Count;
            for (var i = 0; i < digraph.State.Count; i++)
            {
                if (digraph.State[i] >= incidenceList[i].Count && digraph.TimeTillTheEndOfRefractoryPeriod[i] <= 0)
                {
                    involvedArcs.AddRange(incidenceList[i]);
                    timers.AddRange(incidenceList[i].ConvertAll(arc => new Stopwatch()));
                    digraph.State[i] -= incidenceList[i].Count;
                    digraph.TimeTillTheEndOfRefractoryPeriod[i] += digraph.RefractoryPeriods[i];

                    if (digraph.RefractoryPeriods[i] == 0)
                        while (digraph.State[i] >= incidenceList[i].Count)
                        {
                            involvedArcs.AddRange(incidenceList[i]);
                            timers.AddRange(incidenceList[i].ConvertAll(arc => new Stopwatch()));
                            digraph.State[i] -= incidenceList[i].Count;
                        }

                    continue;
                }

                digraph.TimeTillTheEndOfRefractoryPeriod[i] =
                    digraph.TimeTillTheEndOfRefractoryPeriod[i] - mainTimer.Interval >= 0
                        ? digraph.TimeTillTheEndOfRefractoryPeriod[i] - mainTimer.Interval
                        : 0;
            }

            for (int i = count; i < timers.Count; i++)
                timers[i].Start();

            /*if (involvedArcs.Count == 0)
            {

                graphDrawing.DrawTheWholeGraphSandpile(digraph, incidenceList);
                drawingSurface.Image = graphDrawing.Image;
                if (IsMovementEndedSandpile()) MovementEnded?.Invoke(this, null);
                return;
            }*/

            graphDrawing.DrawTheWholeGraphSandpile(digraph, incidenceList);
            for (var i = 0; i < involvedArcs.Count; i++)
            {
                if (timers[i].ElapsedMilliseconds >= GetTime(involvedArcs[i].Length, speed))
                {
                    digraph.State[involvedArcs[i].EndVertex]++;
                    timers.RemoveAt(i);
                    involvedArcs.RemoveAt(i);
                    i--;
                    continue;
                }
                PointF point =
                    GetPoint(digraph.Vertices[involvedArcs[i].StartVertex],
                        digraph.Vertices[involvedArcs[i].EndVertex],
                        involvedArcs[i].Length,
                        timers[i]);
                graphDrawing.DrawDot(point);
                drawingSurface.Image = graphDrawing.Image;
            }
            //for (int i = 0; i < digraph.Vertices.Count; ++i)
               // graphDrawing.DrawVertex(digraph.Vertices[i].X, digraph.Vertices[i].Y, i + 1, new Pen(Color.MidnightBlue, 2.5f));


            if(IsMovementEndedSandpile()) MovementEnded?.Invoke(this, null);
        }

        private void TickGifCollecting(object source, EventArgs e)
        {
            Gif.Add(new Bitmap((Bitmap)drawingSurface.Image));
            if (mainStopwatch.ElapsedMilliseconds >= 20000)
            {
                mainTimer.Stop();
                mainStopwatch.Stop();
                System.Windows.Media.Imaging.GifBitmapEncoder gEnc = new System.Windows.Media.Imaging.GifBitmapEncoder();
                foreach (System.Drawing.Bitmap bmpImage in Gif)
                {
                    var bmp = bmpImage.GetHbitmap();
                    var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        bmp,
                        IntPtr.Zero,
                        System.Windows.Int32Rect.Empty,
                        System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                    gEnc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(src));
                    DeleteObject(bmp); // recommended, handle memory leak
                }
                using (FileStream fs = new FileStream(@"C:\Users\Anastasia\Desktop\some stuff\testGif.gif", FileMode.Create))
                {
                    gEnc.Save(fs);
                }
            }
        }

        private void TickChartFilling(object source, EventArgs e)
        {
            data.Points.AddXY(mainStopwatch.ElapsedMilliseconds/1000.0, involvedArcs.Count);
        }

        public event EventHandler MovementEnded;
        public event EventHandler<MovementTickEventArgs> Tick;

        public bool IsMovementEndedBasic()
        {
            if (involvedArcs.Count != 0) return false;
            for (int i = 0; i < digraph.State.Count; i++)
                if (digraph.State[i] >= digraph.Thresholds[i])
                    return false;
            return true;
        }

        public bool IsMovementEndedSandpile()
        {
            if (involvedArcs.Count != 0) return false;
            for (int i = 0; i < digraph.State.Count; i++)
                if (digraph.State[i] >= incidenceList[i].Count)
                    return false;
            return true;
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
    }

    public enum MovementModelingMode
    {
        Animation,
        Chart,
        Gif
    }
    public enum MovementModelingType
    {
        Basic,
        Sandpile
    }

    public class MovementTickEventArgs : EventArgs
    {
        private readonly Stopwatch Time;
        public long ElapsedTime => Time.ElapsedMilliseconds;

        public MovementTickEventArgs(Stopwatch time)
        {
            Time = time;
        }
    }
}
