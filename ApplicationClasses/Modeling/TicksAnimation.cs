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

            if (IsMovementEndedBasic)
                MovementEnded?.Invoke(this, null);

            if (!mainStopwatch.IsRunning) mainStopwatch.Start();
            Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));
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
            int count = involvedArcs.Count; //Number of new dots

            for (var i = 0; i < digraph.Vertices.Count; i++)
            {
                if (!releaseCondition(i)) continue;
                if (distributionChart != null) avalancheSize++;
                while (releaseCondition(i))
                {
                    involvedArcs.AddRange(incidenceList[i]);
                    timers.AddRange(incidenceList[i].ConvertAll(arc => new Stopwatch()));
                    stateChanges(i);
                    digraph.TimeTillTheEndOfRefractoryPeriod[i]?.Start();
                }
            }

            for (int i = count; i < timers.Count; i++)
                timers[i].Start();

            if (timers.Count > 15000)
            {
                Stop();
                MessageBox.Show("Operation has been aborted prematurely. The number of dots exceeded the allowable mark of 15.000",
                    "Operation Aborted");
                MovementEnded?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Draws all the moving dots and removes all the dots got to their destination
        /// </summary>
        private void ProcessDots(Predicate<int> releaseCondition, Action<int> stateChanges)
        {
            int currentCount = timers.Count;
            for (var i = 0; i < currentCount; i++)
            {
                if (timers[i].ElapsedMilliseconds >= GetTime(involvedArcs[i].Length, speed))
                {
                    digraph.State[involvedArcs[i].EndVertex]++;
                    ReleaseDots(involvedArcs[i].EndVertex, releaseCondition, stateChanges);
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

            for (int i = currentCount; i < timers.Count; i++)
            {
                timers[i].Start();
                digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex]?.Stop();
                digraph.TimeTillTheEndOfRefractoryPeriod[involvedArcs[i].StartVertex]?.Start();
            }

            if (timers.Count > 15000)
            {
                Stop();
                MessageBox.Show("Operation has been aborted prematurely. The number of dots exceeded the allowable mark of 15.000",
                    "Operation Aborted");
                MovementEnded?.Invoke(this, null);
            }
        }

        private void ReleaseDots(int vertexIndex, Predicate<int> releaseCondition, Action<int> stateChanges)
        {
            while (releaseCondition(vertexIndex))
            {
                involvedArcs.AddRange(incidenceList[vertexIndex]);
                timers.AddRange(incidenceList[vertexIndex].ConvertAll(arc => new Stopwatch()));
                stateChanges(vertexIndex);
                digraph.TimeTillTheEndOfRefractoryPeriod[vertexIndex]?.Start();
            }
        }
    }
}
