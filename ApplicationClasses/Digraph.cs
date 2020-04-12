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
        private readonly List<Vertex> vertices = new List<Vertex>();

        /// <summary>
        /// Digraph arcs list
        /// </summary>
        private List<Arc> arcs = new List<Arc>();

        /// <summary>
        /// Digraph vertices thresholds list
        /// </summary>
        private readonly List<int> thresholds = new List<int>();

        /// <summary>
        /// Digraph vertices refractory periods list
        /// </summary>
        private readonly List<int> refractoryPeriods = new List<int>();

        private readonly List<int> state = new List<int>();
        private readonly List<double> timeTillTheEndOfRefractoryPeriod = new List<double>();

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
            if (initialState < 0 || initialState > refractoryPeriod)
                throw new ArgumentOutOfRangeException(nameof(initialState),
                    "The value of the vertex initial state must be a non-negative number, not grater than the value of the vertex refractory period");
            vertices.Add(vertex);
            thresholds.Add(threshold);
            refractoryPeriods.Add(refractoryPeriod);
            state.Add(initialState);
            timeTillTheEndOfRefractoryPeriod.Add(0);
        }

        /// <summary>
        /// Removes vertex from the list of digraph vertices
        /// </summary>
        /// <param name="index">Index of the vertex in the list</param>
        public void RemoveVertex(int index)
        {
            if (vertices.Count <= index || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Index of the vertex must be a non-negative number less than the number of elements in the vertices list");
            arcs = arcs.Where(arc => arc.StartVertex != index && arc.EndVertex != index).ToList();
            arcs = arcs.ConvertAll(arc =>
                new Arc(arc.StartVertex > index ? arc.StartVertex - 1 : arc.StartVertex,
                    arc.EndVertex > index ? arc.EndVertex - 1 : arc.EndVertex));
            vertices.RemoveAt(index);
            thresholds.RemoveAt(index);
            refractoryPeriods.RemoveAt(index);
            timeTillTheEndOfRefractoryPeriod.RemoveAt(index);
        }

        public void AddArc(Arc arc)
        {
            if (arc.StartVertex >= vertices.Count)
                throw new ArgumentOutOfRangeException(nameof(arc.StartVertex),
                    "Index of the vertex must be a non-negative number less than the number of elements in the vertices list");
            if (arc.EndVertex >= vertices.Count)
                throw new ArgumentOutOfRangeException(nameof(arc.EndVertex),
                    "Index of the vertex must be a non-negative number less than the number of elements in the vertices list");
            arcs.Add(arc);
        }

        /// <summary>
        /// Removes arc from the list of digraph vertices
        /// </summary>
        /// <param name="index">Index of the arc in the list</param>
        public void RemoveArc(int index)
        {
            if (arcs.Count <= index || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Index of the arc must be a non-negative number less than the number of elements in the arcs list");
            arcs.RemoveAt(index);
        }

        public List<Vertex> Vertices => vertices;
        public List<Arc> Arcs => arcs;
        public List<int> Thresholds => thresholds;
        public List<int> RefractoryPeriods => refractoryPeriods;
        public List<int> State => state;
        public List<double> TimeTillTheEndOfRefractoryPeriod => timeTillTheEndOfRefractoryPeriod;


        /// <summary>
        /// Graph Adjacency Matrix
        /// </summary>
        public double[,] AdjacencyMatrix
        {
            get
            {
                double[,] adjacencyMatrix = new double[vertices.Count, vertices.Count];
                foreach (Arc arc in arcs)
                    adjacencyMatrix[arc.StartVertex, arc.EndVertex] = arc.Length;
                return adjacencyMatrix;
            }
        }
    }
}
