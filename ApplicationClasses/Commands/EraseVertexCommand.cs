using System;
using System.Linq;
using ApplicationClasses;

namespace GraphClasses.Commands
{
    public class EraseVertexCommand : ICommand
    {
        /// <summary>
        /// Digraph from which a vertex is removed
        /// </summary>
        private readonly Digraph digraph;
        /// <summary>
        /// Removing vertex
        /// </summary>
        private Vertex vertex;
        /// <summary>
        /// Vertex threshold
        /// </summary>
        private int threshold = 1;
        /// <summary>
        /// Vertex refractory period
        /// </summary>
        private int refractoryPeriod = 0;
        /// <summary>
        /// Vertex initial state
        /// </summary>
        private int state = 0;
        /// <summary>
        /// Vertex index
        /// </summary>
        private readonly int index;

        /// <summary>
        /// Array of arcs incident to this vertex
        /// </summary>
        private Arc[] incidentArcs;

        /// <summary>
        /// Initializes a new EraseVertexCommand instance
        /// </summary>
        /// <param name="digraph">Digraph from which a vertex is removed</param>
        /// <param name="vertex">Removing vertex</param>
        public EraseVertexCommand(Digraph digraph, Vertex vertex)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            index = digraph.Vertices.IndexOf(vertex);
            if (index == -1)
                throw new ArgumentException("The digraph doesn't contain this vertex", nameof(vertex));
            this.vertex = vertex;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            vertex = digraph.Vertices[index];
            threshold = digraph.Thresholds[index];
            refractoryPeriod = digraph.RefractoryPeriods[index];
            state = digraph.State[index];
            incidentArcs = digraph.Arcs.Where(arc => arc.StartVertex == index || arc.EndVertex == index).ToArray();

            digraph.RemoveVertex(index);
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void UnExecute()
        {
            digraph.AddVertex(vertex, threshold, refractoryPeriod, state, index);
            Array.ForEach(incidentArcs, arc => digraph.AddArc(arc));
        }
    }
}
