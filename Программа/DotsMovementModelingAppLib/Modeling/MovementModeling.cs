using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
#pragma warning disable 67

namespace DotsMovementModelingAppLib.Modeling
{
    /// <summary>
    /// Contains instance methods for Modeling the Movement of Points on Directed Metric Graph,
    /// with the Condition of Synchronization at the Vertices
    /// </summary>
    public partial class MovementModeling
    {
        /// <summary>
        /// Initializes new instance of MovementModeling class
        /// </summary>
        /// <param name="digraph">Digraph</param>
        /// <param name="speed">Speed in unit per millisecond</param>
        /// <param name="type">Modeling type</param>
        /// <param name="actions">Array of additional actions</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public MovementModeling(Digraph digraph, double speed, MovementModelingType type, MovementModelingActions[] actions)
        {
            if (speed <= 0)
                throw new ArgumentOutOfRangeException(nameof(speed), @"Speed of movement should be positive");
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            this.speed = speed;
            this.type = type;
            this.actions = actions;
            IsActive = false;

            MovementEnded += (sender, e) =>
            {
                if (type == MovementModelingType.Sandpile) GraphDrawing.DrawTheWholeGraphSandpile(digraph, false);
                if (type == MovementModelingType.Basic) GraphDrawing.DrawTheWholeGraph(digraph);
                DrawingSurface.Image = GraphDrawing.Image;
            };

            lastFires = new Double[digraph.Vertices.Count];
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
        /// Array of additional actions
        /// </summary>
        private readonly MovementModelingActions[] actions;
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
        private readonly List<Arc> involvedArcs = new List<Arc>();
        /// <summary>
        /// Stopwatches for each moving dot
        /// </summary>
        private readonly List<Stopwatch> stopwatches = new List<Stopwatch>();
        /// <summary>
        /// Timer updating the process data every few milliseconds
        /// </summary>
        private Timer mainTimer;
        /// <summary>
        /// Current avalanche size
        /// </summary>
        private bool[] avalanche;

        private Predicate<int> releaseCondition;
        private Action<int> stateChange;

        private ChartWindow numberOfDotsChart;
        private ChartWindow distributionChart;

        private double time;
        private int indexOfFixedDot = -1;
        readonly Stopwatch stopwatchTime = new Stopwatch();

        #endregion

        /// <summary>
        /// Starts dots movement
        /// </summary>
        public void StartMovementModeling()
        {
            digraph.SetTimeTillTheEndOfRefractoryPeriod();

            // Fill incidence list
            incidenceList = GetIncidenceList(digraph);

            mainTimer = new Timer { Interval = 1 };
            mainTimer.Tick += TickModeling;

            // Select dots release condition
            // and changes occurring to vertex state after the release
            if (type == MovementModelingType.Basic)
            {
                releaseCondition = i => digraph.State[i] >= digraph.Thresholds[i]
                                        && (digraph.RefractoryPeriods[i] == 0 ||
                                            digraph.TimeTillTheEndOfRefractoryPeriod[i].ElapsedMilliseconds >= digraph.RefractoryPeriods[i]
                                            || !digraph.TimeTillTheEndOfRefractoryPeriod[i].IsRunning);
                stateChange = i => digraph.State[i] -= digraph.Thresholds[i];
            }
            else
            {
                releaseCondition = i => !digraph.Stock.Contains(i) && digraph.State[i] >= incidenceList[i].Count
                                                                   && (digraph.RefractoryPeriods[i] == 0 ||
                                                                       digraph.TimeTillTheEndOfRefractoryPeriod[i].ElapsedMilliseconds >= digraph.RefractoryPeriods[i]
                                                                       || !digraph.TimeTillTheEndOfRefractoryPeriod[i].IsRunning);
                stateChange = i => digraph.State[i] -= incidenceList[i].Count;
            }

            //Prepare chart windows if it's needed
            if (actions.Contains(MovementModelingActions.Chart))
            {
                PrepareSandpileCharts();
                PrepareBasicCharts();
                numberOfDotsChart?.Show();
                distributionChart?.Show();
            }

            //Add gif frames collecting
            if (actions.Contains(MovementModelingActions.Gif))
                mainTimer.Tick += TickAddFrame;

            Go();
        }

        /// <summary>
        /// Stops the movement
        /// </summary>
        public void Stop()
        {
            mainTimer.Stop();
            for (int i = 0; i < digraph.Vertices.Count; i++)
            {
                if (digraph.TimeTillTheEndOfRefractoryPeriod[i].IsRunning)
                    digraph.TimeTillTheEndOfRefractoryPeriod[i].Stop();
                else digraph.TimeTillTheEndOfRefractoryPeriod[i] = null;
            }
            stopwatches.ForEach(timer => timer.Stop());

            stopwatchTime.Stop();
            IsActive = false;
        }

        /// <summary>
        /// Starts or restarts the movement
        /// </summary>
        public void Go()
        {
            if (IsMovementEnded)
            {
                MovementEnded?.Invoke(this, null);
                return;
            }
            mainTimer.Start();
            if (time > 0)
                stopwatchTime.Start();

            for (int i = 0; i < digraph.Vertices.Count && time > 0; i++)
            {
                if (digraph.TimeTillTheEndOfRefractoryPeriod[i] == null)
                    digraph.TimeTillTheEndOfRefractoryPeriod[i] = new Stopwatch();
                else digraph.TimeTillTheEndOfRefractoryPeriod[i].Start();
            }

            stopwatches.ForEach(timer => timer.Start());
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
        private bool IsMovementEndedBasic
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
        private bool IsMovementEndedSandpile
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
        /// Shows if the movement is ended
        /// </summary>
        public bool IsMovementEnded
        {
            get
            {
                if (type == MovementModelingType.Basic) return IsMovementEndedBasic;
                return IsMovementEndedSandpile;
            }
        }

        /// <summary>
        /// Processes selected types of sandpile chart and prepares windows for displaying these charts
        /// </summary>
        private void PrepareSandpileCharts()
        {
            if (type != MovementModelingType.Sandpile) return;
            if (SandpileChartTypes.Contains(SandpileChartType.NumberOfDotsChart))
                numberOfDotsChart = new ChartWindow();

            if (SandpileChartTypes.Contains(SandpileChartType.AvalancheSizesDistributionChart))
            {
                distributionChart = new ChartWindow();
                avalanche = new bool[digraph.Vertices.Count];
                MovementEnded += delegate
                {
                    AddAvalancheSize();
                    avalanche = new bool[digraph.Vertices.Count];
                };
                distributionChart.AvalancheSizesDistributionChartPrepare();
                distributionChart.Closing += (sender, e) => distributionChart = null;
            }
        }

        /// <summary>
        /// Processes basic modeling chart selection and prepares windows for displaying the chart
        /// </summary>
        private void PrepareBasicCharts()
        {
            if (type != MovementModelingType.Basic) return;
            numberOfDotsChart = new ChartWindow();
            numberOfDotsChart.Closing += (sender, e) => numberOfDotsChart = null;
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

    public enum MovementModelingActions
    {
        Animation = 0,
        Chart = 1,
        Gif = 2
    }
    public enum MovementModelingType
    {
        Basic = 0,
        Sandpile = 1
    }
    public enum SandpileChartType
    {
        NumberOfDotsChart = 0,
        AvalancheSizesDistributionChart = 1
    }

    public class MovementTickEventArgs : EventArgs
    {
        public readonly long ElapsedTime;

        /// <summary>
        /// Initializes a new MovementTickEventArgs instance
        /// </summary>
        /// <param name="time">Elapsed time in milliseconds</param>
        public MovementTickEventArgs(long time) =>
            ElapsedTime = time;
    }
}
