using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ApplicationClasses
{
    /// <summary>
    /// Represents a digraph
    /// </summary>
    [Serializable]
    public class Digraph
    {
        /// <summary>
        /// Digraph vertices list
        /// </summary>
        public List<Vertex> Vertices { get; private set; }

        /// <summary>
        /// Digraph arcs list
        /// </summary>
        public List<Arc> Arcs { get; private set; }

        #region Digraph parameters

        /// <summary>
        /// Digraph vertices thresholds list
        /// </summary>
        public List<int> Thresholds { get; private set; }

        /// <summary>
        /// Vertices refractory periods in milliseconds 
        /// </summary>
        public List<int> RefractoryPeriods { get; private set; }

        /// <summary>
        /// Number of dots at each vertex
        /// </summary>
        public List<int> State { get; private set; }

        /// <summary>
        /// Remaining time until the end of refractory period in milliseconds
        /// </summary>
        [XmlIgnore] public List<Timer> TimeTillTheEndOfRefractoryPeriod { get; private set; }

        /// <summary>
        /// List of indices of sink vertices (sandpile modeling)
        /// </summary>
        public List<int> Stock { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new Digraph instance
        /// </summary>
        public Digraph()
        {
            Vertices = new List<Vertex>();
            Arcs = new List<Arc>();
            Thresholds = new List<int>();
            RefractoryPeriods = new List<int>();
            State = new List<int>();
            Stock = new List<int>();
        }

        #region Events

        /// <summary>
        /// Occurs when vertex is added
        /// </summary>
        public event EventHandler<DigraphChangedEventArgs> VertexAdded;
        /// <summary>
        /// Occurs when vertex is removed
        /// </summary>
        public event EventHandler<DigraphChangedEventArgs> VertexRemoved;
        /// <summary>
        /// Occurs when arc is added
        /// </summary>
        public event EventHandler<DigraphChangedEventArgs> ArcAdded;
        /// <summary>
        /// Occurs when arc is removed
        /// </summary>
        public event EventHandler<DigraphChangedEventArgs> ArcRemoved;

        #endregion


        /// <summary>
        /// Adds vertex to the list of digraph vertices
        /// </summary>
        /// <param name="vertex">Vertex to add</param>
        /// <param name="threshold">Vertex threshold</param>
        /// <param name="refractoryPeriod">Vertex refractory period</param>
        /// <param name="initialState">Vertex initial state</param>
        /// <param name="index">Vertex index</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void AddVertex(Vertex vertex, int threshold = 1, int refractoryPeriod = 0, int initialState = 0, int index = -1)
        {
            if (threshold <= 0)
                throw new ArgumentOutOfRangeException(nameof(threshold), @"The value of the vertex threshold must be a positive number");
            if (refractoryPeriod < 0)
                throw new ArgumentOutOfRangeException(nameof(refractoryPeriod),
                    @"The value of the vertex refractory period must be a non-negative number");
            if (initialState < 0)
                throw new ArgumentOutOfRangeException(nameof(initialState),
                    @"The value of the vertex initial state must be a non-negative number");

            if (index == -1) index = Vertices.Count;
            Vertices.Insert(index, vertex);
            Thresholds.Insert(index, threshold);
            RefractoryPeriods.Insert(index, refractoryPeriod);
            State.Insert(index, initialState);

            Arcs = Arcs.ConvertAll(arc =>
                new Arc(arc.StartVertex >= index ? arc.StartVertex + 1 : arc.StartVertex,
                    arc.EndVertex >= index ? arc.EndVertex + 1 : arc.EndVertex));

            VertexAdded?.Invoke(vertex, new DigraphChangedEventArgs(index));
        }

        /// <summary>
        /// Removes vertex from the list of digraph vertices
        /// </summary>
        /// <param name="index">Index of the vertex in the list</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void RemoveVertex(int index)
        {
            if (Vertices.Count <= index || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    @"Index of the vertex must be a non-negative number less than the number of elements in the vertices list");

            Arcs = Arcs.Where(arc => arc.StartVertex != index && arc.EndVertex != index).ToList();
            Arcs = Arcs.ConvertAll(arc =>
                new Arc(arc.StartVertex > index ? arc.StartVertex - 1 : arc.StartVertex,
                    arc.EndVertex > index ? arc.EndVertex - 1 : arc.EndVertex));

            var removed = Vertices[index];

            Vertices.RemoveAt(index);
            Thresholds.RemoveAt(index);
            RefractoryPeriods.RemoveAt(index);
            State.RemoveAt(index);

            VertexRemoved?.Invoke(removed, new DigraphChangedEventArgs(index));
        }

        /// <summary>
        /// Adds arc
        /// </summary>
        /// <param name="arc">Arc for adding</param>
        /// <param name="index">Arc index</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void AddArc(Arc arc, int index = -1)
        {
            if (arc.StartVertex >= Vertices.Count)
                throw new ArgumentOutOfRangeException(nameof(arc.StartVertex),
                    @"Index of the vertex must be a non-negative number less than the number of elements in the vertices list");
            if (arc.EndVertex >= Vertices.Count)
                throw new ArgumentOutOfRangeException(nameof(arc.EndVertex),
                    @"Index of the vertex must be a non-negative number less than the number of elements in the vertices list");

            if (index == -1) index = Arcs.Count;
            Arcs.Insert(index, arc);

            ArcAdded?.Invoke(arc, new DigraphChangedEventArgs(index));
        }

        /// <summary>
        /// Removes arc from the list of digraph vertices
        /// </summary>
        /// <param name="index">Index of the arc in the list</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void RemoveArc(int index)
        {
            if (Arcs.Count <= index || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    @"Index of the arc must be a non-negative number less than the number of elements in the arcs list");

            var removed = Arcs[index];

            Arcs.RemoveAt(index);

            ArcRemoved?.Invoke(removed, new DigraphChangedEventArgs(index));
        }

        /// <summary>
        /// Resets digraph sink (sandpile modeling)
        /// </summary>
        public void ResetStock() => Stock = new List<int>();

        /// <summary>
        /// Sets timers responsible for compliance with refractory periods
        /// </summary>
        public void SetTimeTillTheEndOfRefractoryPeriod()
        {
            TimeTillTheEndOfRefractoryPeriod = new List<Timer>(RefractoryPeriods.Count);
            for (int i = 0; i < RefractoryPeriods.Count; ++i)
            {
                if (RefractoryPeriods[i] > 0)
                {
                    TimeTillTheEndOfRefractoryPeriod.Add(new Timer() { Interval = RefractoryPeriods[i] });
                    TimeTillTheEndOfRefractoryPeriod[i].Tick +=
                        (sender, e) => (sender as Timer)?.Stop();
                }
                else TimeTillTheEndOfRefractoryPeriod.Add(null);
            }
        }

        /// <summary>
        /// Graph Adjacency Matrix
        /// </summary>
        public double[,] AdjacencyMatrix
        {
            get
            {
                double[,] adjacencyMatrix = new double[Vertices.Count, Vertices.Count];
                foreach (Arc arc in Arcs)
                    adjacencyMatrix[arc.StartVertex, arc.EndVertex] = arc.Length;
                return adjacencyMatrix;
            }
        }
    }

    public class DigraphChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Index of changed item
        /// </summary>
        public readonly int Index;
        /// <summary>
        /// Initializes a new DigraphChangedEventArgs instance
        /// </summary>
        /// <param name="index">Index of changed item</param>
        public DigraphChangedEventArgs(int index) => Index = index;
    }
}
