using System;
using ApplicationClasses;

namespace GraphClasses.Commands
{
    public class ResizeDigraphCommand : ICommand
    {
        /// <summary>
        /// Digraph whose vertex is moving
        /// </summary>
        private readonly Digraph digraph;
        /// <summary>
        /// Scaling coefficient
        /// </summary>
        private readonly double coefficient;

        /// <summary>
        /// Initializes a new ResizeDigraphCommand instance
        /// </summary>
        /// <param name="digraph">Digraph whose vertex is moving</param>
        /// <param name="coefficient">X axis offset</param>
        /// <exception cref="ArgumentNullException"/>
        public ResizeDigraphCommand(Digraph digraph, double coefficient)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            this.coefficient = coefficient;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            for (int i = 0; i < digraph.Vertices.Count; i++)
                digraph.Vertices[i] =
                    new Vertex((int)(digraph.Vertices[i].X * coefficient), (int)(digraph.Vertices[i].Y * coefficient));
        }

        /// <summary>
        /// UnExecutes the command
        /// </summary>
        public void UnExecute()
        {
            for (int i = 0; i < digraph.Vertices.Count; i++)
                digraph.Vertices[i] =
                    new Vertex((int)(digraph.Vertices[i].X / coefficient), (int)(digraph.Vertices[i].Y / coefficient));
        }
    }
}
