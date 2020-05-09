using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;

namespace ApplicationClasses.Modeling
{
    public partial class MovementModeling
    {
        /// <summary>
        /// Animates the process of basic movement
        /// </summary>
        private void TickBasicAnimation(object source, EventArgs e)
        {
            ProcessVertices(i => digraph.State[i] >= digraph.Thresholds[i]
                     && (digraph.RefractoryPeriods[i] == 0 || !digraph.TimeTillTheEndOfRefractoryPeriod[i].Enabled),
                i => digraph.State[i] -= digraph.Thresholds[i]);


            GraphDrawing.DrawTheWholeGraph(digraph);

            ProcessDots(i => digraph.State[i] >= digraph.Thresholds[i]
                     && (digraph.RefractoryPeriods[i] == 0 || !digraph.TimeTillTheEndOfRefractoryPeriod[i].Enabled),
                i => digraph.State[i] -= digraph.Thresholds[i]);

            GraphDrawing.DrawVertices(digraph);
            DrawingSurface.Image = GraphDrawing.Image;


            if (!mainStopwatch.IsRunning) mainStopwatch.Start();
            Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));

            if (IsMovementEndedBasic)
                MovementEnded?.Invoke(this, null);
        }

        /// <summary>
        /// Animates the process of sandpile movement
        /// </summary>
        private void TickSandpileAnimation(object source, EventArgs e)
        {
            ProcessVertices(i => !digraph.Stock.Contains(i) && digraph.State[i] >= incidenceList[i].Count
                                 && (digraph.RefractoryPeriods[i] == 0 || !digraph.TimeTillTheEndOfRefractoryPeriod[i].Enabled),
                i => digraph.State[i] -= incidenceList[i].Count);


            GraphDrawing.DrawTheWholeGraphSandpile(digraph, false);

            ProcessDots(i => !digraph.Stock.Contains(i) && digraph.State[i] >= incidenceList[i].Count
                                                        && (digraph.RefractoryPeriods[i] == 0 || !digraph.TimeTillTheEndOfRefractoryPeriod[i].Enabled),
                i => digraph.State[i] -= incidenceList[i].Count);

            GraphDrawing.DrawVerticesSandpile(digraph);
            DrawingSurface.Image = GraphDrawing.Image;

            if (IsMovementEndedSandpile) MovementEnded?.Invoke(this, null);

            if (!mainStopwatch.IsRunning) mainStopwatch.Start();
            Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));
        }

        /// <summary>
        /// Process vertices states to release new dots
        /// </summary>
        /// <param name="releaseCondition">Condition under which dots are released</param>
        /// <param name="stateChanges">How vertex state changes after the dots are releasing</param>
        private void ProcessVertices(Predicate<int> releaseCondition, Action<int> stateChanges)
        {
            int count = involvedArcs.Count; 
            int initialCount = count;
            bool added = false;
            for (var i = 0; i < digraph.Vertices.Count; i++)
            {
                if (!releaseCondition(i)) continue;
                if (distributionChart != null) avalancheSize++;
                ReleaseDots(i, releaseCondition, stateChanges, out added);
            }

            if (timers.Count == 0) TickChartFilling(mainStopwatch.ElapsedMilliseconds, initialCount);
            if (initialCount != timers.Count || added) TickChartFilling(mainStopwatch.ElapsedMilliseconds, involvedArcs.Count);

            StartNewTimers(count);

            CheckDotsNumber(15000);
        }

        /// <summary>
        /// Draws all the moving dots and removes all the dots got to their destination
        /// </summary>
        private void ProcessDots(Predicate<int> releaseCondition, Action<int> stateChanges)
        {
            int currentCount = timers.Count;
            int initialCount = currentCount;
            bool added = false;
            for (var i = 0; i < currentCount; i++)
            {
                if (timers[i].ElapsedMilliseconds >= GetTime(involvedArcs[i].Length, speed))
                {
                    digraph.State[involvedArcs[i].EndVertex]++;
                    ReleaseDots(involvedArcs[i].EndVertex, releaseCondition, stateChanges, out added);
                    timers.RemoveAt(i);
                    involvedArcs.RemoveAt(i);

                    currentCount--;
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

            if (timers.Count == 0) TickChartFilling(mainStopwatch.ElapsedMilliseconds, initialCount);
            if (initialCount != timers.Count || added) TickChartFilling(mainStopwatch.ElapsedMilliseconds, involvedArcs.Count);

            StartNewTimers(currentCount);

            CheckDotsNumber(15000);
        }

        private void ReleaseDots(int vertexIndex, Predicate<int> releaseCondition, Action<int> stateChanges, out bool added)
        {
            added = false;
            while (releaseCondition(vertexIndex))
            {
                involvedArcs.AddRange(incidenceList[vertexIndex]);
                timers.AddRange(incidenceList[vertexIndex].ConvertAll(arc => new Stopwatch()));
                stateChanges(vertexIndex);
                digraph.TimeTillTheEndOfRefractoryPeriod[vertexIndex]?.Start();
                added = true;
            }
        }

        private void CheckDotsNumber(int limit)
        {
            if (timers.Count > limit)
            {
                Stop();
                MessageBox.Show("Operation has been aborted prematurely. The number of dots exceeded the allowable mark of 15.000",
                    "Operation Aborted");
                MovementEnded?.Invoke(this, null);
            }
        }

        private void StartNewTimers(int startIndex)
        {
            for (int i = startIndex; i < timers.Count; i++)
            {
                timers[i].Start();
                digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex]?.Stop();
                digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex]?.Start();
            }
        }
    }
}
