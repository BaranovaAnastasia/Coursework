using System;
using System.Collections.Generic;

namespace ApplicationClasses
{
    public class ConnectivityCheck
    {
        /// <summary>
        /// Number of vertices of the graph
        /// </summary>
        private readonly int numberOfVertices;
        /// <summary>
        /// Graph Adjacency List
        /// </summary>
        private readonly List<int>[] adjacencyList;

        /// <summary>
        /// Initializes a new instance of the ConnectivityCheck class
        /// </summary>
        /// <param name="numberOfVertices">Number of vertices of the graph</param>
        public ConnectivityCheck(int numberOfVertices)
        {
            if (numberOfVertices <= 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfVertices),
                    "Number of vertices of the graph should be a positive number");
            this.numberOfVertices = numberOfVertices;
            adjacencyList = new List<int>[numberOfVertices];
            for (int i = 0; i < numberOfVertices; ++i)
                adjacencyList[i] = new List<int>();

        }

        /// <summary>
        /// Adds graph edge to Adjacency List
        /// </summary>
        public void AddEdge(Arc edge) => adjacencyList[edge.StartVertex].Add(edge.EndVertex);

        /// <summary>
        /// Recursive DFS function
        /// </summary>
        /// <param name="startVertex">The vertex with which we start the traversing</param>
        /// <param name="visited">Shows if vertices have been visited</param>
        private void DFS(int startVertex, bool[] visited)
        {
            visited[startVertex] = true;
            foreach (int i in adjacencyList[startVertex])
                if (!visited[i])
                    DFS(i, visited);
        }

        /// <summary>
        /// Returns an inverted graph
        /// </summary>
        /// <returns></returns>
        private ConnectivityCheck GetInvertedGraph()
        {
            ConnectivityCheck g = new ConnectivityCheck(numberOfVertices);

            for (int v = 0; v < numberOfVertices; v++)
                foreach (int i in adjacencyList[v])
                    g.adjacencyList[i].Add(v);
            return g;
        }

        /// <summary>
        /// Connectivity check method (using Kosaraju algorithm)
        /// </summary>
        public bool IsStronglyConnected()
        {
            bool[] visited = new bool[numberOfVertices];
            // Fist DFS traversing
            DFS(0, visited);
            // Returning false there is a vertex that hasn't been visited 
            foreach (bool v in visited)
                if (!v) return false;

            // Inverting the graph
            ConnectivityCheck gr = GetInvertedGraph();

            visited = new bool[numberOfVertices]; // Refreshing
            // DFS traversing for inverted graph
            gr.DFS(0, visited);
            // Returning false if there is a vertex that hasn't been visited 
            return Array.TrueForAll(visited, v => v);
        }


        /// <summary>
        /// Checks if graph is valid
        /// </summary>
        public static bool IsGraphValid(Digraph digraph)
        {
            if (digraph.Vertices.Count < 3) return false;
            ConnectivityCheck check = new ConnectivityCheck(digraph.Vertices.Count);
            foreach (Arc arc in digraph.Arcs)
                check.AddEdge(arc);
            return check.IsStronglyConnected();
        }
    }
}
