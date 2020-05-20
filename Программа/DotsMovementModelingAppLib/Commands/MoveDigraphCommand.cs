using System;
using DotsMovementModelingAppLib;

namespace DotsMovementModelingAppLib.Commands
{
    public class MoveDigraphCommand : ICommand
    {
        /// <summary>
        /// Digraph whose vertex is moving
        /// </summary>
        private readonly Digraph digraph;
        /// <summary>
        /// X axis offset
        /// </summary>
        private readonly int xCoefficient;
        /// <summary>
        /// Y axis offset
        /// </summary>
        private readonly int yCoefficient;

        /// <summary>
        /// Initializes a new MoveVertexCommand instance
        /// </summary>
        /// <param name="digraph">Digraph whose vertex is moving</param>
        /// <param name="xCoefficient">X axis offset</param>
        /// <param name="yCoefficient">Y axis offset</param>
        /// <exception cref="ArgumentNullException"/>
        public MoveDigraphCommand(Digraph digraph, int xCoefficient, int yCoefficient)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            this.xCoefficient = xCoefficient;
            this.yCoefficient = yCoefficient;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            for (int i = 0; i < digraph.Vertices.Count; i++)
                digraph.Vertices[i] = 
                    new Vertex(digraph.Vertices[i].X + xCoefficient, digraph.Vertices[i].Y + yCoefficient);
        }

        /// <summary>
        /// UnExecutes the command
        /// </summary>
        public void UnExecute()
        {
            for (int i = 0; i < digraph.Vertices.Count; i++)
                digraph.Vertices[i] =
                    new Vertex(digraph.Vertices[i].X - xCoefficient, digraph.Vertices[i].Y - yCoefficient);
        }
    }
}
