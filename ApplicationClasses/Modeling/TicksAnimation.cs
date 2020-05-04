using System;
using System.Diagnostics;
using System.Drawing;

namespace ApplicationClasses.Modeling
{
    public partial class MovementModeling
    {
        /// <summary>
        /// Animates the process of basic movement
        /// </summary>
        private void TickBasicAnimation(object source, EventArgs e)
        {
            if (IsMovementEndedBasic) { MovementEnded?.Invoke(this, null); return;}
            if(!mainStopwatch.IsRunning) mainStopwatch.Start();
            Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));

            // Releasing new dots
            ProcessVertices(
                i => digraph.State[i] >= digraph.Thresholds[i] 
                     && (digraph.RefractoryPeriods[i] == 0 || !digraph.TimeTillTheEndOfRefractoryPeriod[i].Enabled),
                i => digraph.State[i] -= digraph.Thresholds[i]);


            GraphDrawing.DrawTheWholeGraph(digraph);
            DrawDots();
            GraphDrawing.DrawVertices(digraph); 
            DrawingSurface.Image = GraphDrawing.Image;

            if (IsMovementEndedBasic) MovementEnded?.Invoke(this, null);
        }

        /// <summary>
        /// Animates the process of sandpile movement
        /// </summary>
        private void TickSandpileAnimation(object source, EventArgs e)
        {
            if (IsMovementEndedSandpile) { MovementEnded?.Invoke(this, null); return; }
            if (!mainStopwatch.IsRunning) mainStopwatch.Start();
            Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));

            ProcessVertices(i => !digraph.Stock.Contains(i) && digraph.State[i] >= incidenceList[i].Count 
                                 && (digraph.RefractoryPeriods[i] == 0 || !digraph.TimeTillTheEndOfRefractoryPeriod[i].Enabled),
                i => digraph.State[i] -= incidenceList[i].Count);


            GraphDrawing.DrawTheWholeGraphSandpile(digraph, false);
            DrawDots();
            GraphDrawing.DrawVerticesSandpile(digraph);
            DrawingSurface.Image = GraphDrawing.Image;

            if (IsMovementEndedSandpile) MovementEnded?.Invoke(this, null);
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
                if(!releaseCondition(i)) continue;
                if(distributionChart != null) avalancheSize++;
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
        }

        /// <summary>
        /// Draws all the moving dots and removes all the dots got to their destination
        /// </summary>
        private void DrawDots()
        {
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
                GraphDrawing.DrawDot(point);
            }
        }
    }
}
