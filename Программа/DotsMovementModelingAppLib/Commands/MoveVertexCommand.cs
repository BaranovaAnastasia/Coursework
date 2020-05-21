using System;
using System.Drawing;

namespace DotsMovementModelingAppLib.Commands
{
    public class MoveVertexCommand : ICommand
    {
        /// <summary>
        /// Digraph whose vertex is moving
        /// </summary>
        private readonly Digraph digraph;
        /// <summary>
        /// Vertex coordinates before moving
        /// </summary>
        private Point oldPoint;
        /// <summary>
        /// New vertex coordinates
        /// </summary>
        private Point newPoint;
        /// <summary>
        /// Vertex index
        /// </summary>
        private readonly int index;

        /// <summary>
        /// Initializes a new MoveVertexCommand instance
        /// </summary>
        /// <param name="digraph">Digraph whose vertex is moving</param>
        /// <param name="index">Vertex index</param>
        /// <param name="oldPoint">Vertex coordinates before moving</param>
        /// <param name="newPoint">New vertex coordinates</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public MoveVertexCommand(Digraph digraph, int index, Point oldPoint, Point newPoint)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            if (digraph.Vertices.Count <= index)
                throw new ArgumentOutOfRangeException(nameof(index));
            this.oldPoint = oldPoint;
            this.newPoint = newPoint;
            this.index = index;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute() =>
            digraph.Vertices[index] = new Vertex(newPoint.X, newPoint.Y);

        /// <summary>
        /// UnExecutes the command
        /// </summary>
        public void UnExecute() =>
            digraph.Vertices[index] = new Vertex(oldPoint.X, oldPoint.Y);
    }
}
