using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

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
        /// <param name="type">Modeling type</param>
        /// <param name="modes">Array of additional modes</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public MovementModeling(Digraph digraph, double speed, MovementModelingType type, MovementModelingMode[] modes)
        {
            if (speed <= 0)
                throw new ArgumentOutOfRangeException(nameof(speed), "Speed of movement should be positive");
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            this.speed = speed;
            this.type = type;
            this.modes = modes;
            IsActive = false;

            MovementEnded += (object sender, EventArgs e) =>
            {
                if (type == MovementModelingType.Sandpile) GraphDrawing.DrawTheWholeGraphSandpile(digraph, false);
                if (type == MovementModelingType.Basic) GraphDrawing.DrawTheWholeGraph(digraph);
                DrawingSurface.Image = GraphDrawing.Image;
            };
        }

        /// <summary>
        /// Shows if the movement is currently active
        /// </summary>
        public bool IsActive { get; private set; }

        #region Modeling parameters viriables

        /// <summary>
        /// Digraph for which movement is modeling
        /// </summary>
        private readonly Digraph digraph;
        /// <summary>
        /// Movement speed
        /// </summary>
        private readonly double speed;
        /// <summary>
        /// Modeling type
        /// </summary>
        private readonly MovementModelingType type;
        /// <summary>
        /// Array of additional modes
        /// </summary>
        private readonly MovementModelingMode[] modes;
        /// <summary>
        /// GraphDrawing for drawing animation frames
        /// </summary>
        public GraphDrawing GraphDrawing;
        /// <summary>
        /// PictureBox for displaying animation
        /// </summary>
        public PictureBox DrawingSurface;
        /// <summary>
        /// Array of sandpile chart types
        /// </summary>
        public SandpileChartType[] SandpileChartTypes;

        #endregion

        #region Variables directly related to modeling 

        /// <summary>
        /// Digraph incidence list
        /// </summary>
        private List<Arc>[] incidenceList;
        /// <summary>
        /// List of arcs along which dots are currently moving
        /// </summary>
        private List<Arc> involvedArcs;
        /// <summary>
        /// Stopwatches for each moving dot
        /// </summary>
        private List<Stopwatch> timers;
        /// <summary>
        /// Stopwatch counting the time of the whole process
        /// </summary>
        private readonly Stopwatch mainStopwatch = new Stopwatch();
        /// <summary>
        /// Timer updating the process data every few milliseconds
        /// </summary>
        private Timer mainTimer = null;
        /// <summary>
        /// Current avalanche size
        /// </summary>
        private int avalancheSize;

        private ChartWindow numberOfDotsChart = null;
        private ChartWindow distributionChart = null;

        #endregion


        /// <summary>
        /// Starts dots movement
        /// </summary>
        public void StartMovementModeling()
        {
            // Fill incidence list
            incidenceList = GetIncidenceList(digraph);

            mainTimer = new Timer() { Interval = (int)(50 / (1000 * speed)) };

            //Select animation type by modeling type
            if (type == MovementModelingType.Basic) mainTimer.Tick += TickBasicAnimation;
            else mainTimer.Tick += TickSandpileAnimation;

            //Prepare chart windows if it's needed
            if (modes.Contains(MovementModelingMode.Chart))
            {
                PrepareSandpileCharts();
                PrepareBasicCharts();
                numberOfDotsChart?.Show();
                distributionChart?.Show();
            }

            //Add gif frames collecting
            if (modes.Contains(MovementModelingMode.Gif))
                mainTimer.Tick += TickGifCollecting;

            //Start movement
            involvedArcs = new List<Arc>();
            timers = new List<Stopwatch>();
            Go();
        }

        /// <summary>
        /// Stops the movement
        /// </summary>
        public void Stop()
        {
            mainTimer.Stop();
            mainStopwatch.Stop();
            timers.ForEach(timer => timer.Stop());
            IsActive = false;
        }

        /// <summary>
        /// Restarts the movement
        /// </summary>
        public void Go()
        {
            mainTimer.Start();
            timers.ForEach(timer => timer.Start());
            IsActive = true;
        }

        /// <summary>
        /// Occurs when the movement is ended
        /// </summary>
        public event EventHandler MovementEnded;
        /// <summary>
        /// Occurs when main timer tick occurs
        /// </summary>
        public event EventHandler<MovementTickEventArgs> Tick;

        /// <summary>
        /// Shows if modeling of basic movement is ended
        /// </summary>
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

        /// <summary>
        /// Shows if modeling of sandpile movement is ended
        /// </summary>
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

        /// <summary>
        /// Processes selected types of sandpile chart and prepares windows for displaying these charts
        /// </summary>
        private void PrepareSandpileCharts()
        {
            if (type != MovementModelingType.Sandpile) return;
            if (SandpileChartTypes.Contains(SandpileChartType.NumberOfDotsChart))
            {
                numberOfDotsChart = new ChartWindow();
                numberOfDotsChart.Closing +=
                    delegate (object sender, System.ComponentModel.CancelEventArgs e)
                    { mainTimer.Tick -= TickChartFilling; };
                mainTimer.Tick += TickChartFilling;
            }

            if (SandpileChartTypes.Contains(SandpileChartType.AvalancheSizesDistributionChart))
            {
                distributionChart = new ChartWindow();
                distributionChart.Closing +=
                    delegate (object sender, System.ComponentModel.CancelEventArgs e)
                    { MovementEnded -= TickChartFilling; };
                MovementEnded += TickChartFilling;
                MovementEnded += delegate (object sender, EventArgs args) { avalancheSize = 0; };
                distributionChart.AvalancheSizesDistributionChartPrepare();
            }
        }

        /// <summary>
        /// Processes basic modeling chart selection and prepares windows for displaying the chart
        /// </summary>
        private void PrepareBasicCharts()
        {
            if (type != MovementModelingType.Basic) return;
            numberOfDotsChart = new ChartWindow();
            mainTimer.Tick += TickChartFilling;
            numberOfDotsChart.Closing +=
                delegate (object sender, System.ComponentModel.CancelEventArgs e)
                    { mainTimer.Tick -= TickChartFilling; };
        }

        #region Helper static methods

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

        #endregion
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
