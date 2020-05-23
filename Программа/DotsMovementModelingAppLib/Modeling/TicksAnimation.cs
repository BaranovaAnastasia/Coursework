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
        private bool isNew = true;
        private bool isOnWaiting;
        private int indexOfWaited = -1;
        private readonly double[] lastFires;
        private readonly List<double> releaseTime = new List<double>();

        private List<double> verticesTime;
        private List<double> dotsTime = new List<double>();

        /// <summary>
        /// Models and animates the process of dots movement
        /// </summary>
        private void TickModeling(object source, EventArgs e)
        {
            Tick?.Invoke(this, new MovementTickEventArgs((long)verticesTime.Max()));


            int initialCount = involvedArcs.Count;
            long t = indexOfFixedDot == -1
                ? (long) time
                : (long) (time - GetTime(involvedArcs[indexOfFixedDot].Length, speed) +
                          stopwatches[indexOfFixedDot].ElapsedMilliseconds);
            ProcessDots();
            ProcessVertices();
            UpdateChart(initialCount, t);

            

            if (IsMovementEnded)
            {
                Stop();
                AddNumberOfDotsChartPoint((long)(time), initialCount);
                AddNumberOfDotsChartPoint((long)(time), involvedArcs.Count);

                isNew = true;

                Tick?.Invoke(this, new MovementTickEventArgs((long)verticesTime.Max()));
                MovementEnded?.Invoke(this, null);
                return;
            }

            if (indexOfFixedDot == -1 && indexOfWaited == -1 && !isOnWaiting)
            {
                indexOfWaited = -1;
                isOnWaiting = true;
                isNew = true;
                for (int i = 0; i < digraph.Vertices.Count; i++)
                {
                    if ((type == MovementModelingType.Basic && digraph.State[i] >= digraph.Thresholds[i]
                         || type == MovementModelingType.Sandpile && digraph.State[i] >= incidenceList[i].Count)
                            && digraph.RefractoryPeriods[i]
                            - digraph.TimeTillTheEndOfRefractoryPeriod[i].ElapsedMilliseconds != 0)
                    {
                        indexOfWaited = i;

                        double newtime = digraph.RefractoryPeriods[indexOfWaited] - (time - lastFires[indexOfWaited]);
                        time += newtime;
                        return;
                    }
                }
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
                if(releaseCondition(i))
                    lastFires[i] = indexOfFixedDot == -1 ? time : time - GetTime(involvedArcs[indexOfFixedDot].Length, speed) +
                                                                  stopwatches[indexOfFixedDot].ElapsedMilliseconds;
                

                if (indexOfFixedDot == -1 && i == index && releaseCondition(i) && !isOnWaiting)
                {
                    indexOfFixedDot = 0;

                    for (int j = 0; j < incidenceList[i].Count; j++)
                    {
                        if (incidenceList[i][j].Length > incidenceList[i][indexOfFixedDot].Length)
                            indexOfFixedDot = j;
                    }

                    time += GetTime(incidenceList[i][indexOfFixedDot].Length, speed);
                    indexOfFixedDot += involvedArcs.Count;
                    index = -1;
                }
                else if (i == index) index = -1;

                if (isOnWaiting && indexOfWaited == i && releaseCondition(i))
                {
                    //time += stopwatchTime.ElapsedMilliseconds;
                    isOnWaiting = false;
                    indexOfWaited = -1;
                }
                if (!releaseCondition(i)) continue;
                ReleaseDots(i);
                if(digraph.State[i] == 0)
                    verticesTime[i] = 0;

                if (!releaseCondition(i) && stateReleaseCondition(i))
                    verticesTime[i] += digraph.RefractoryPeriods[i];
            }

            CheckDotsNumber(20000);

            if (indexOfFixedDot == -1)
            {
                if (involvedArcs.Count == 0) return;
                indexOfFixedDot = involvedArcs.Count - 1;
                if (Math.Abs(time) <= 0)
                    for (int i = 0; i < involvedArcs.Count; i++)
                    {
                        if (involvedArcs[i].Length > involvedArcs[indexOfFixedDot].Length)
                            indexOfFixedDot = i;
                    }
                isNew = false;
                if (stopwatches[indexOfFixedDot].IsRunning )
                {
                    time -= (time - releaseTime[indexOfFixedDot]);
                }
                time += GetTime(involvedArcs[indexOfFixedDot].Length, speed);
                
            }

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
                if (indexOfFixedDot != -1 && time - GetTime(involvedArcs[indexOfFixedDot].Length, speed) +
                    stopwatches[indexOfFixedDot].ElapsedMilliseconds - releaseTime[i] >= GetTime(involvedArcs[i].Length, speed)
                    || indexOfFixedDot == -1 && time - releaseTime[i] >= GetTime(involvedArcs[i].Length, speed))
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

                    if (i == indexOfFixedDot)
                    {
                        indexOfFixedDot = -1;
                        index = involvedArcs[i].EndVertex;
                        if (!releaseCondition(involvedArcs[i].EndVertex) && i != 0)
                        {
                            indexOfFixedDot = i - 1;
                            time += GetTime(involvedArcs[indexOfFixedDot].Length, speed) -
                                    stopwatches[indexOfFixedDot].ElapsedMilliseconds;
                        }
                    }

                    if (indexOfFixedDot > i) indexOfFixedDot--;


                    stopwatches.RemoveAt(i);
                    involvedArcs.RemoveAt(i);
                    releaseTime.RemoveAt(i);
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
                releaseTime.AddRange(incidenceList[vertexIndex].ConvertAll(arc => 
                    indexOfFixedDot == -1 ? time : time - GetTime(involvedArcs[indexOfFixedDot].Length, speed) +
                                                   stopwatches[indexOfFixedDot].ElapsedMilliseconds));
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
                if(digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex] == null)
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
