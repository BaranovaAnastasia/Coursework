using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationClasses
{
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
        public List<double> TimeTillTheEndOfRefractoryPeriod { get; private set; }

        /// <summary>
        /// List of indices of sink vertices (sandpile modeling)
        /// </summary>
        public List<int> Stock { get; private set; }

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
            TimeTillTheEndOfRefractoryPeriod = new List<double>();
            Stock = new List<int>();
        }

        /// <summary>
        /// Adds vertex to the list of digraph vertices
        /// </summary>
        /// <param name="vertex">Vertex to add</param>
        /// <param name="threshold">Vertex threshold</param>
        /// <param name="refractoryPeriod">Vertex refractory period</param>
        /// <param name="initialState">Vertex initial state</param>
        public void AddVertex(Vertex vertex, int threshold = 1, int refractoryPeriod = 0, int initialState = 0)
        {
            if (threshold <= 0)
                throw new ArgumentOutOfRangeException(nameof(threshold), "The value of the vertex threshold must be a positive number");
            if (refractoryPeriod < 0)
                throw new ArgumentOutOfRangeException(nameof(refractoryPeriod),
                    "The value of the vertex refractory period must be a non-negative number");
            if (initialState < 0)
                throw new ArgumentOutOfRangeException(nameof(initialState),
                    "The value of the vertex initial state must be a non-negative number, not grater than the value of the vertex");

            Vertices.Add(vertex);
            Thresholds.Add(threshold);
            RefractoryPeriods.Add(refractoryPeriod);
            State.Add(initialState);
            TimeTillTheEndOfRefractoryPeriod.Add(0);
        }

        /// <summary>
        /// Removes vertex from the list of digraph vertices
        /// </summary>
        /// <param name="index">Index of the vertex in the list</param>
        public void RemoveVertex(int index)
        {
            if (Vertices.Count <= index || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Index of the vertex must be a non-negative number less than the number of elements in the vertices list");
            Arcs = Arcs.Where(arc => arc.StartVertex != index && arc.EndVertex != index).ToList();
            Arcs = Arcs.ConvertAll(arc =>
                new Arc(arc.StartVertex > index ? arc.StartVertex - 1 : arc.StartVertex,
                    arc.EndVertex > index ? arc.EndVertex - 1 : arc.EndVertex));

            Vertices.RemoveAt(index);
            Thresholds.RemoveAt(index);
            RefractoryPeriods.RemoveAt(index);
            State.RemoveAt(index);
            TimeTillTheEndOfRefractoryPeriod.RemoveAt(index);
        }

        /// <summary>
        /// Adds arc
        /// </summary>
        public void AddArc(Arc arc)
        {
            if (arc.StartVertex >= Vertices.Count)
                throw new ArgumentOutOfRangeException(nameof(arc.StartVertex),
                    "Index of the vertex must be a non-negative number less than the number of elements in the vertices list");
            if (arc.EndVertex >= Vertices.Count)
                throw new ArgumentOutOfRangeException(nameof(arc.EndVertex),
                    "Index of the vertex must be a non-negative number less than the number of elements in the vertices list");

            Arcs.Add(arc);
        }

        /// <summary>
        /// Removes arc from the list of digraph vertices
        /// </summary>
        /// <param name="index">Index of the arc in the list</param>
        public void RemoveArc(int index)
        {
            if (Arcs.Count <= index || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Index of the arc must be a non-negative number less than the number of elements in the arcs list");

            Arcs.RemoveAt(index);
        }

        /// <summary>
        /// Resets digraph sink (sanpile modeling)
        /// </summary>
        public void ResetStock() => Stock = new List<int>();

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
}
