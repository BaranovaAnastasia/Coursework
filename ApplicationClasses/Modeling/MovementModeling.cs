using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ApplicationClasses.Modeling
{
    /// <summary>
    /// Contains instance methods for Modeling the Movement of Points on Directed Metric Graph,
    /// with the Condition of Synchronization at the Vertices
    /// </summary>
    public partial class MovementModeling
    {
        /// <summary>
        /// Initialize new instance of MovementModeling class
        /// </summary>
        /// <param name="digraph">Digraph</param>
        /// <param name="speed">Speed in unit per millisecond</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public MovementModeling(Digraph digraph, double speed, MovementModelingType type, MovementModelingMode[] modes)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            if (speed <= 0)
                throw new ArgumentOutOfRangeException(nameof(speed), "Speed of movement should be positive");
            this.speed = speed;
            Type = type;
            Modes = modes;
            IsActive = false;

            MovementEnded += (object sender, EventArgs e) =>
            {
                if (type == MovementModelingType.Sandpile) GraphDrawing.DrawTheWholeGraphSandpile(digraph, false);
                if (type == MovementModelingType.Basic) GraphDrawing.DrawTheWholeGraph(digraph);
                DrawingSurface.Image = GraphDrawing.Image;
            };
        }

        private readonly Digraph digraph;
        private readonly double speed;
        private readonly MovementModelingType Type;
        private readonly MovementModelingMode[] Modes;
        public GraphDrawing GraphDrawing;
        public PictureBox DrawingSurface;
        public SandpileChartType[] SandpileChartTypes;

        private List<Arc>[] incidenceList;
        private List<Arc> involvedArcs;
        private List<Stopwatch> timers;
        private readonly Stopwatch mainStopwatch = new Stopwatch();
        private Timer mainTimer = null;
        private readonly Timer gifTimer = new Timer() { Interval = 30 };
        private int avalancheSize;


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

        private ChartWindow NumberOfDotsChart = null;
        private ChartWindow DistibutionChart = null;

        /// <summary>
        /// Starts dots movement
        /// </summary>
        public void Movement()
        {
            incidenceList = GetIncidenceList(digraph);

            mainTimer = new Timer() { Interval = (int)(50 / (1000 * speed)) };
            if (Type == MovementModelingType.Basic) mainTimer.Tick += TickBasicAnimation;
            else
            {
                mainTimer.Tick += TickSandpileAnimation;
            }
            if (Modes.Contains(MovementModelingMode.Chart))
            {
                if (Type == MovementModelingType.Sandpile)
                {
                    if (SandpileChartTypes.Contains(SandpileChartType.NumberOfDotsChart))
                    {
                        NumberOfDotsChart = new ChartWindow();
                        NumberOfDotsChart.Closing +=
                            delegate (object sender, System.ComponentModel.CancelEventArgs e)
                            { mainTimer.Tick -= TickChartFilling; };
                        mainTimer.Tick += TickChartFilling;
                    }

                    if (SandpileChartTypes.Contains(SandpileChartType.AvalancheSizesDistributionChart))
                    {
                        DistibutionChart = new ChartWindow();
                        DistibutionChart.Closing +=
                            delegate (object sender, System.ComponentModel.CancelEventArgs e)
                            { MovementEnded -= TickChartFilling; };
                        MovementEnded += TickChartFilling;
                        MovementEnded += delegate (object sender, EventArgs args) { avalancheSize = 0; };
                        DistibutionChart.AvalancheSizesDistributionChartPrepare();
                    }
                }
                else
                {
                    NumberOfDotsChart = new ChartWindow();
                    mainTimer.Tick += TickChartFilling;
                    NumberOfDotsChart.Closing +=
                        delegate (object sender, System.ComponentModel.CancelEventArgs e)
                        { mainTimer.Tick -= TickChartFilling; };
                }
                NumberOfDotsChart?.Show();
                DistibutionChart?.Show();
            }

            if (Modes.Contains(MovementModelingMode.Gif)) gifTimer.Tick += TickGifCollecting;

            involvedArcs = new List<Arc>();
            timers = new List<Stopwatch>();
            IsActive = true;
            mainTimer.Start();
            //mainStopwatch.Start();
            if (Modes.Contains(MovementModelingMode.Gif)) gifTimer.Start();
        }

        public bool IsActive { get; private set; }

        public void Stop()
        {
            mainTimer.Stop();
            mainStopwatch.Stop();
            if (Modes.Contains(MovementModelingMode.Gif)) gifTimer.Stop();
            timers.ForEach(timer => timer.Stop());
            IsActive = false;
        }

        public void Go()
        {
            mainTimer.Start();
            mainStopwatch.Start();
            if (Modes.Contains(MovementModelingMode.Gif)) gifTimer.Start();
            timers.ForEach(timer => timer.Start());
            IsActive = true;
        }

        public event EventHandler MovementEnded;
        public event EventHandler<MovementTickEventArgs> Tick;

        public bool IsMovementEndedBasic
        {
            get
            {
                if (involvedArcs.Count != 0) return false;
                for (int i = 0; i < digraph.State.Count; i++)
                    if (digraph.State[i] >= digraph.Thresholds[i])
                        return false;
                return true;
            }
        }

        public bool IsMovementEndedSandpile
        {
            get
            {
                if (involvedArcs.Count != 0) return false;
                for (int i = 0; i < digraph.State.Count; i++)
                    if (!digraph.Stock.Contains(i) && digraph.State[i] >= incidenceList[i].Count)
                        return false;
                return true;
            }
        }
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

    public enum SandpileChartType
    {
        NumberOfDotsChart,
        AvalancheSizesDistributionChart
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
