using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace DotsMovementModelingAppLib.Modeling
{
    public partial class MovementModeling
    {
        private List<double> verticesTime;
        private List<double> dotsTime = new List<double>();
        private long time = 0;
        private long lastTime = 0;
        /// <summary>
        /// Models and animates the process of dots movement
        /// </summary>
        private void TickModeling(object source, EventArgs e)
        {
            if (dotsTime.Count > 0)
            {
                int index = dotsTime.IndexOf(dotsTime.Max());
                time = (long)(dotsTime.Max() - GetTime(involvedArcs[index].Length, speed) +
                            stopwatches[index].ElapsedMilliseconds) + lastTime;
            }
            else time = (long)verticesTime.Max() + lastTime;
            Tick?.Invoke(this,
            new MovementTickEventArgs(time));



            int initialCount = involvedArcs.Count;
            ProcessDots();
            ProcessVertices();
            UpdateChart(initialCount, time);

            if (IsMovementEnded)
            {
                Stop();
                lastTime += (long)(verticesTime.Max());

                verticesTime = new List<double>(digraph.Vertices.Count);
                verticesTime.AddRange(digraph.Vertices.ConvertAll(vertex => 0d));
                dotsTime = new List<double>();

                AddNumberOfDotsChartPoint(lastTime, initialCount);
                AddNumberOfDotsChartPoint(lastTime, involvedArcs.Count);
                

                Tick?.Invoke(this, new MovementTickEventArgs(lastTime));
                MovementEnded?.Invoke(this, null);
                return;
            }
        }

        /// <summary>
        /// Process vertices states to release new dots
        /// </summary>
        private void ProcessVertices()
        {
            int count = involvedArcs.Count; // number of 'old' dots 

            for (var i = 0; i < digraph.Vertices.Count; i++)
            {
                if (!releaseCondition(i)) continue;
                ReleaseDots(i);
                if (digraph.State[i] == 0)
                    verticesTime[i] = 0;

                if (!releaseCondition(i) && stateReleaseCondition(i))
                    verticesTime[i] += digraph.RefractoryPeriods[i];
            }

            CheckDotsNumber(20000);

            StartNewTimers(stopwatches.Count - count);
        }

        private int index = -1;
        /// <summary>
        /// Draws all the moving dots, removes all the dots got to their destination
        /// and releases new dots if destination vertices are ready
        /// </summary>
        private void ProcessDots()
        {
            if (type == MovementModelingType.Basic)
                GraphDrawing.DrawTheWholeGraph(digraph);
            else GraphDrawing.DrawTheWholeGraphSandpile(digraph, false);

            for (var i = 0; i < involvedArcs.Count; i++)
            {
                if (stopwatches[i].ElapsedMilliseconds >= GetTime(involvedArcs[i].Length, speed))
                {
                    bool addTime = !stateReleaseCondition(involvedArcs[i].EndVertex);

                    digraph.State[involvedArcs[i].EndVertex]++;

                    if (verticesTime[involvedArcs[i].EndVertex] < dotsTime[i])
                        verticesTime[involvedArcs[i].EndVertex] = dotsTime[i];
                    if (!releaseCondition(involvedArcs[i].EndVertex) &&
                        stateReleaseCondition(involvedArcs[i].EndVertex) && addTime)
                        verticesTime[involvedArcs[i].EndVertex] += digraph.RefractoryPeriods[involvedArcs[i].EndVertex]
                                                                   - digraph.TimeTillTheEndOfRefractoryPeriod[
                                                                       involvedArcs[i].EndVertex].ElapsedMilliseconds;


                    stopwatches.RemoveAt(i);
                    involvedArcs.RemoveAt(i);
                    dotsTime.RemoveAt(i);

                    i--;
                    continue;
                }

                PointF point =
                    GetPoint(digraph.Vertices[involvedArcs[i].StartVertex],
                        digraph.Vertices[involvedArcs[i].EndVertex],
                        involvedArcs[i].Length,
                        stopwatches[i]);

                GraphDrawing.DrawDot(point);
            }

            CheckDotsNumber(20000);

            if (type == MovementModelingType.Basic)
                GraphDrawing.DrawVertices(digraph);
            else GraphDrawing.DrawVerticesSandpile(digraph);
            DrawingSurface.Image = GraphDrawing.Image;
        }

        /// <summary>
        /// Checks if the vertex is ready to fire and releases new dots
        /// </summary>
        private void ReleaseDots(int vertexIndex)
        {
            stopwatchTime.Restart();
            if (!releaseCondition(vertexIndex)) return;
            while (releaseCondition(vertexIndex))
            {
                dotsTime.AddRange(incidenceList[vertexIndex].ConvertAll(arc =>
                    GetTime(arc.Length, speed) + verticesTime[vertexIndex]));
                involvedArcs.AddRange(incidenceList[vertexIndex]);
                stopwatches.AddRange(incidenceList[vertexIndex].ConvertAll(arc => new Stopwatch()));

                stateChange(vertexIndex);
                digraph.TimeTillTheEndOfRefractoryPeriod[vertexIndex]?.Restart();
            }
            if (distributionChart != null) avalanche[vertexIndex] = true;
        }

        /// <summary>
        /// Checks current number of dots
        /// and aborts the process if it exceeds the limit
        /// </summary>
        private void CheckDotsNumber(int limit)
        {
            if (stopwatches.Count > limit)
            {
                Stop();
                MessageBox.Show("Operation has been aborted prematurely." + Environment.NewLine +
                                $"The number of dots exceeded the allowable mark of {limit}",
                    "Operation Aborted", MessageBoxButton.OK, MessageBoxImage.Error);
                Tick?.Invoke(this, new MovementTickEventArgs((long)time));
                MovementEnded?.Invoke(limit, null);
            }
        }

        /// <summary>
        /// Starts count last timers
        /// </summary>
        /// <param name="count">Number of timers to start</param>
        private void StartNewTimers(int count)
        {
            int fired = 0;
            for (int i = stopwatches.Count - 1; fired < count; i--, fired++)
            {
                stopwatches[i].Start();
                if (digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex] == null)
                    digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex] = new Stopwatch();
                {
                    digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex].Stop();
                    digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex].Start();


                }
            }
        }

        /// <summary>
        /// Updates number of dots chart
        /// </summary>
        /// <param name="val">Number of dots before changes</param>
        private void UpdateChart(int val, long time)
        {
            if (!actions.Contains(MovementModelingActions.Chart)
                || numberOfDotsChart == null
                || val == stopwatches.Count
                || indexOfFixedDot == -1) return;

            AddNumberOfDotsChartPoint(time, val);
            AddNumberOfDotsChartPoint(time, involvedArcs.Count);
        }
    }
}
