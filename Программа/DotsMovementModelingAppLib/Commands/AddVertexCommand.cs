using System;
using DotsMovementModelingAppLib;

namespace DotsMovementModelingAppLib.Commands
{
    public class AddVertexCommand : ICommand
    {
        /// <summary>
        /// Digraph to which a vertex is added
        /// </summary>
        private readonly Digraph digraph;
        /// <summary>
        /// Adding vertex
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
        private int index = -1;

        /// <summary>
        /// Initializes a new AddVertexCommand instance
        /// </summary>
        /// <param name="digraph">Digraph to which a vertex is added</param>
        /// <param name="vertex">Adding vertex</param>
        /// <exception cref="ArgumentNullException"/>
        public AddVertexCommand(Digraph digraph, Vertex vertex)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            this.vertex = vertex;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            digraph.AddVertex(vertex, threshold, refractoryPeriod, state, index);
            index = digraph.Vertices.IndexOf(vertex);
        }

        /// <summary>
        /// UnExecutes the command
        /// </summary>
        public void UnExecute()
        {
            vertex = digraph.Vertices[index];
            threshold = digraph.Thresholds[index];
            refractoryPeriod = digraph.RefractoryPeriods[index];
            state = digraph.State[index];

            digraph.RemoveVertex(index);
        }
    }
}
