using System;
using System.Collections.Generic;
using DotsMovementModelingAppLib;

namespace DotsMovementModelingAppLib.Commands
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
        private int refractoryPeriod;
        /// <summary>
        /// Vertex initial state
        /// </summary>
        private int state;
        /// <summary>
        /// Vertex index
        /// </summary>
        private readonly int index;

        /// <summary>
        /// Array of arcs incident to this vertex
        /// </summary>
        private List<Arc> incidentArcs = new List<Arc>();
        /// <summary>
        /// Array of indices of the arcs incident to this vertex
        /// </summary>
        private List<int> arcsIndices = new List<int>();

        /// <summary>
        /// Initializes a new EraseVertexCommand instance
        /// </summary>
        /// <param name="digraph">Digraph from which a vertex is removed</param>
        /// <param name="vertex">Removing vertex</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public EraseVertexCommand(Digraph digraph, Vertex vertex)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            index = digraph.Vertices.IndexOf(vertex);
            if (index == -1)
                throw new ArgumentException(@"The digraph doesn't contain this vertex", nameof(vertex));
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

            incidentArcs = new List<Arc>();
            arcsIndices = new List<int>();
            for (var i = 0; i < digraph.Arcs.Count; i++)
            {
                if (digraph.Arcs[i].StartVertex != index && digraph.Arcs[i].EndVertex != index)
                    continue;
                arcsIndices.Add(i);
                incidentArcs.Add(digraph.Arcs[i]);
            }

            digraph.RemoveVertex(index);
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void UnExecute()
        {
            digraph.AddVertex(vertex, threshold, refractoryPeriod, state, index);
            for (int i = 0; i < incidentArcs.Count; i++)
                digraph.AddArc(incidentArcs[i], arcsIndices[i]);
        }
    }
}
