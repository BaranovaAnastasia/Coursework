using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace ApplicationClasses.Modeling
{
    public partial class MovementModeling
    {
        /// <summary>
        /// Models and animates the process of dots movement
        /// </summary>
        private void TickModeling(object source, EventArgs e)
        {
            Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));

            int initialCount = involvedArcs.Count;
            int numberOfNewDots = ProcessVertices() + ProcessDots();

            UpdateChart(initialCount);
            StartNewTimers(numberOfNewDots);

            if (!mainStopwatch.IsRunning) mainStopwatch.Start();
            if (IsMovementEnded) MovementEnded?.Invoke(this, null);
        }

        /// <summary>
        /// Process vertices states to release new dots
        /// </summary>
        private int ProcessVertices()
        {
            int count = involvedArcs.Count; // number of 'old' dots 

            for (var i = 0; i < digraph.Vertices.Count; i++)
            {
                if (!releaseCondition(i)) continue;
                ReleaseDots(i);
            }

            CheckDotsNumber(20000);
            return timers.Count - count;
        }

        /// <summary>
        /// Draws all the moving dots, removes all the dots got to their destination
        /// and releases new dots if destination vertices are ready
        /// </summary>
        private int ProcessDots()
        {
            if (type == MovementModelingType.Basic)
                GraphDrawing.DrawTheWholeGraph(digraph);
            else GraphDrawing.DrawTheWholeGraphSandpile(digraph, false);

            int count = timers.Count;    // number of 'old' dots
            for (var i = 0; i < count; i++)
            {
                if (timers[i].ElapsedMilliseconds >= GetTime(involvedArcs[i].Length, speed))
                {
                    digraph.State[involvedArcs[i].EndVertex]++;
                    ReleaseDots(involvedArcs[i].EndVertex);
                    timers.RemoveAt(i);
                    involvedArcs.RemoveAt(i);

                    count--;
                    i--;
                    continue;
                }

                PointF point =
                    GetPoint(digraph.Vertices[involvedArcs[i].StartVertex],
                        digraph.Vertices[involvedArcs[i].EndVertex],
                        involvedArcs[i].Length,
                        timers[i]);

                GraphDrawing.DrawDot(point);
            }

            CheckDotsNumber(20000);

            if (type == MovementModelingType.Basic)
                GraphDrawing.DrawVertices(digraph);
            else GraphDrawing.DrawVerticesSandpile(digraph);
            DrawingSurface.Image = GraphDrawing.Image;

            return timers.Count - count;
        }

        /// <summary>
        /// Checks if the vertex is ready to fire and releases new dots
        /// </summary>
        private void ReleaseDots(int vertexIndex)
        {
            if (!releaseCondition(vertexIndex)) return;
            while (releaseCondition(vertexIndex))
            {
                involvedArcs.AddRange(incidenceList[vertexIndex]);
                timers.AddRange(incidenceList[vertexIndex].ConvertAll(arc => new Stopwatch()));
                stateChange(vertexIndex);
                digraph.TimeTillTheEndOfRefractoryPeriod[vertexIndex]?.Start();
            }
            if (distributionChart != null) avalancheSize++;
        }

        /// <summary>
        /// Checks current number of dots
        /// and aborts the process if it exceeds the limit
        /// </summary>
        private void CheckDotsNumber(int limit)
        {
            if (timers.Count > limit)
            {
                Stop();
                MessageBox.Show("Operation has been aborted prematurely." + Environment.NewLine +
                                $"The number of dots exceeded the allowable mark of {limit}",
                    "Operation Aborted");
                MovementEnded?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Starts count last timers
        /// </summary>
        /// <param name="count">Number of timers to start</param>
        private void StartNewTimers(int count)
        {
            int fired = 0;
            for (int i = timers.Count - 1; fired < count; i--, fired++)
            {
                timers[i].Start();
                digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex]?.Stop();
                digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex]?.Start();
            }
        }

        /// <summary>
        /// Updates number of dots chart
        /// </summary>
        /// <param name="val">Number of dots before changes</param>
        private void UpdateChart(int val)
        {
            if (!modes.Contains(MovementModelingMode.Chart)
                || numberOfDotsChart == null
                || val == timers.Count) return;

            AddNumberOfDotsChartPoint(mainStopwatch.ElapsedMilliseconds, val);
            AddNumberOfDotsChartPoint(mainStopwatch.ElapsedMilliseconds, involvedArcs.Count);
        }
    }
}
