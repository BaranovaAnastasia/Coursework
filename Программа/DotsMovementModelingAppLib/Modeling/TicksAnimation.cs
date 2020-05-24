using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace DotsMovementModelingAppLib.Modeling
{
    public partial class MovementModeling
    {
        private List<double> verticesTime;
        private List<double> dotsTime = new List<double>();
        private long time;
        private long lastTime;
        private int lastUpdated;
        private int initialCount;
        private readonly bool[] refractoryPeriodAdded;
        private double oldMax = 0;
        private readonly bool[] hasFired;

        /// <summary>
        /// Models and animates the process of dots movement
        /// </summary>
        private void TickModeling(object source, EventArgs e)
        {
            if (dotsTime.Count > 0)
            {
                int index = dotsTime.IndexOf(dotsTime.Max());
                time = (long)(dotsTime[index] - GetTime(involvedArcs[index].Length, speed) +
                            stopwatches[index].ElapsedMilliseconds) + lastTime;
            }
            else
            {
                int index = verticesTime.IndexOf(verticesTime.Max());
                time = (long)verticesTime[index] - digraph.RefractoryPeriods[index]
                       + digraph.TimeTillTheEndOfRefractoryPeriod[index].ElapsedMilliseconds + lastTime;
            }
            Tick?.Invoke(this, new MovementTickEventArgs(time));

            initialCount = involvedArcs.Count;
            ProcessDots();
            ProcessVertices();

            if (IsMovementEnded)
            {
                Stop();
                lastTime += (long)(verticesTime.Max());

                verticesTime = new List<double>(digraph.Vertices.Count);
                verticesTime.AddRange(digraph.Vertices.ConvertAll(vertex => 0d));
                dotsTime = new List<double>();
                oldMax = 0;

                AddNumberOfDotsChartPoint(lastTime, initialCount);
                AddNumberOfDotsChartPoint(lastTime, involvedArcs.Count);


                Tick?.Invoke(this, new MovementTickEventArgs(lastTime));
                MovementEnded?.Invoke(this, null);
            }
            else if (involvedArcs.Count == 0 && initialCount != 0) stopwatchTime.Restart();
        }

        /// <summary>
        /// Process vertices states to release new dots
        /// </summary>
        private void ProcessVertices()
        {
            int sum = involvedArcs.Count;
            for (int i = 0; i < digraph.Vertices.Count; i++)
            {
                if (releaseCondition(i))
                    sum += incidenceList[i].Count;
            }
            if (sum > 20000) CheckDotsNumber(involvedArcs.Count - 1);

            int count = involvedArcs.Count; // number of 'old' dots 
            double newOldMax = -1;

            for (var i = 0; i < digraph.Vertices.Count; i++)
            {
                if (!releaseCondition(i)) continue;
                ReleaseDots(i);

                if (verticesTime[i] > newOldMax) newOldMax = verticesTime[i];

                refractoryPeriodAdded[i] = false;
            }

            if (newOldMax > 0) oldMax = newOldMax;

            UpdateChart(initialCount, (long)oldMax + lastTime);

            for (var i = 0; i < digraph.Vertices.Count; i++)
            {
                if (digraph.State[i] == 0)
                    verticesTime[i] = 0;

                if (!releaseCondition(i) && stateReleaseCondition(i) && !refractoryPeriodAdded[i])
                {
                    verticesTime[i] += digraph.RefractoryPeriods[i];
                    refractoryPeriodAdded[i] = true;
                }
            }
            CheckDotsNumber(20000);

            StartNewTimers(stopwatches.Count - count);
        }

        /// <summary>
        /// Draws all the moving dots, removes all the dots got to their destination
        /// and releases new dots if destination vertices are ready
        /// </summary>
        private void ProcessDots()
        {
            if (type == MovementModelingType.Basic)
                GraphDrawing.DrawTheWholeGraph(digraph);
            else GraphDrawing.DrawTheWholeGraphSandpile(digraph, false);

            int newOldMax = -1;
            for (var i = 0; i < involvedArcs.Count; i++)
            {
                if (stopwatches[i].ElapsedMilliseconds >= GetTime(involvedArcs[i].Length, speed))
                {
                    if (Math.Max(dotsTime[i], verticesTime[involvedArcs[i].EndVertex]) > oldMax)
                        oldMax = Math.Max(dotsTime[i], verticesTime[involvedArcs[i].EndVertex]);
                    bool addTime = !stateReleaseCondition(involvedArcs[i].EndVertex);

                    digraph.State[involvedArcs[i].EndVertex]++;

                    if (verticesTime[involvedArcs[i].EndVertex] < dotsTime[i])
                        verticesTime[involvedArcs[i].EndVertex] = dotsTime[i];
                    if (!releaseCondition(involvedArcs[i].EndVertex) &&
                        stateReleaseCondition(involvedArcs[i].EndVertex) && addTime)
                    {
                        verticesTime[involvedArcs[i].EndVertex] += digraph.RefractoryPeriods[involvedArcs[i].EndVertex]
                                                                   - digraph.TimeTillTheEndOfRefractoryPeriod[
                                                                       involvedArcs[i].EndVertex].ElapsedMilliseconds;

                        refractoryPeriodAdded[involvedArcs[i].EndVertex] = true;
                    }

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

            if (newOldMax > 0) oldMax = newOldMax;

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
            if (!releaseCondition(vertexIndex)) return;
            while (releaseCondition(vertexIndex))
            {
                hasFired[vertexIndex] = true;
                dotsTime.AddRange(incidenceList[vertexIndex].ConvertAll(arc =>
                    GetTime(arc.Length, speed) + verticesTime[vertexIndex]));
                involvedArcs.AddRange(incidenceList[vertexIndex]);
                stopwatches.AddRange(incidenceList[vertexIndex].ConvertAll(arc => new Stopwatch()));

                stateChange(vertexIndex);
                digraph.TimeTillTheEndOfRefractoryPeriod[vertexIndex].Restart();

            }
            if (distributionChart != null) avalanche[vertexIndex] = true;
            hasFired[vertexIndex] = true;
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

                Stop();
                lastTime += (long)(dotsTime.Max() -
                                    GetTime(involvedArcs[dotsTime.IndexOf(dotsTime.Max())].Length, speed));

                verticesTime = new List<double>(digraph.Vertices.Count);
                verticesTime.AddRange(digraph.Vertices.ConvertAll(vertex => 0d));
                dotsTime = new List<double>();


                Tick?.Invoke(this, new MovementTickEventArgs(lastTime));
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
        private void UpdateChart(int val, long timePoint)
        {
            if (!actions.Contains(MovementModelingActions.Chart)
                || numberOfDotsChart == null
                || val == involvedArcs.Count) return;

            AddNumberOfDotsChartPoint(timePoint, val);
            AddNumberOfDotsChartPoint(timePoint, involvedArcs.Count);
        }
    }
}
