using System;
using System.Collections.Generic;
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
    public partial class MovementModeling
    {
        /// <summary>
        /// Draws all the currently moving dots
        /// </summary>
        private void TickBasicAnimation(object source, EventArgs e)
        {
            if (IsMovementEndedBasic){ MovementEnded?.Invoke(this, null); return;}
            if(!mainStopwatch.IsRunning) mainStopwatch.Start();
            Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));
            int count = involvedArcs.Count;
            for (var i = 0; i < digraph.State.Count; i++)
            {
                if (digraph.State[i] >= digraph.Thresholds[i] && digraph.TimeTillTheEndOfRefractoryPeriod[i] <= 0)
                {
                    involvedArcs.AddRange(incidenceList[i]);
                    timers.AddRange(incidenceList[i].ConvertAll(arc => new Stopwatch()));
                    digraph.State[i] -= digraph.Thresholds[i];
                    digraph.TimeTillTheEndOfRefractoryPeriod[i] += digraph.RefractoryPeriods[i];

                    if (digraph.RefractoryPeriods[i] == 0)
                        while (digraph.State[i] >= digraph.Thresholds[i])
                        {
                            involvedArcs.AddRange(incidenceList[i]);
                            timers.AddRange(incidenceList[i].ConvertAll(arc => new Stopwatch()));
                            digraph.State[i] -= digraph.Thresholds[i];
                        }

                    continue;
                }

                digraph.TimeTillTheEndOfRefractoryPeriod[i] =
                    digraph.TimeTillTheEndOfRefractoryPeriod[i] - mainTimer.Interval >= 0
                        ? digraph.TimeTillTheEndOfRefractoryPeriod[i] - mainTimer.Interval
                        : 0;
            }

            for (int i = count; i < timers.Count; i++)
                timers[i].Start();

            if (involvedArcs.Count == 0)
            {

                GraphDrawing.DrawTheWholeGraph(digraph);
                DrawingSurface.Image = GraphDrawing.Image;
                if (IsMovementEndedBasic) MovementEnded?.Invoke(this, null);
                return;
            }

            GraphDrawing.DrawTheWholeGraph(digraph);
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
                DrawingSurface.Image = GraphDrawing.Image;
            }
            for (int i = 0; i < digraph.Vertices.Count; ++i)
                GraphDrawing.DrawVertex(digraph.Vertices[i].X, digraph.Vertices[i].Y, i + 1, new Pen(Color.MidnightBlue, 2.5f));

            if (IsMovementEndedBasic) MovementEnded?.Invoke(this, null);
        }

        private void TickSandpileAnimation(object source, EventArgs e)
        {
            if (IsMovementEndedSandpile) { MovementEnded?.Invoke(this, null); return; }
            if (!mainStopwatch.IsRunning) mainStopwatch.Start();
            Tick?.Invoke(this, new MovementTickEventArgs(mainStopwatch));
            int count = involvedArcs.Count;
            for (var i = 0; i < digraph.State.Count; i++)
            { 
                if(digraph.Stock.Contains(i)) continue;
                if (digraph.State[i] >= incidenceList[i].Count && digraph.TimeTillTheEndOfRefractoryPeriod[i] <= 0)
                {
                    involvedArcs.AddRange(incidenceList[i]);
                    timers.AddRange(incidenceList[i].ConvertAll(arc => new Stopwatch()));
                    digraph.State[i] -= incidenceList[i].Count;
                    digraph.TimeTillTheEndOfRefractoryPeriod[i] += digraph.RefractoryPeriods[i];

                    avalancheSize++;

                    if (digraph.RefractoryPeriods[i] == 0)
                        while (digraph.State[i] >= incidenceList[i].Count)
                        {
                            involvedArcs.AddRange(incidenceList[i]);
                            timers.AddRange(incidenceList[i].ConvertAll(arc => new Stopwatch()));
                            digraph.State[i] -= incidenceList[i].Count;
                        }

                    continue;
                }

                digraph.TimeTillTheEndOfRefractoryPeriod[i] =
                    digraph.TimeTillTheEndOfRefractoryPeriod[i] - mainTimer.Interval >= 0
                        ? digraph.TimeTillTheEndOfRefractoryPeriod[i] - mainTimer.Interval
                        : 0;
            }

            for (int i = count; i < timers.Count; i++)
                timers[i].Start();

            GraphDrawing.DrawTheWholeGraphSandpile(digraph, incidenceList, palette);
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
                DrawingSurface.Image = GraphDrawing.Image;
            }
            GraphDrawing.DrawVerticesSandpile(digraph, incidenceList, palette);
            DrawingSurface.Image = GraphDrawing.Image;

            if (IsMovementEndedSandpile) MovementEnded?.Invoke(this, null);
        }

    }
}
