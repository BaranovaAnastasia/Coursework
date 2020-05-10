using System;
using ApplicationClasses;

namespace GraphClasses.Commands
{
    public class EraseArcCommand : ICommand
    {
        /// <summary>
        /// Digraph from which an arc is removed
        /// </summary>
        private readonly Digraph digraph;
        /// <summary>
        /// Removing arc
        /// </summary>
        private Arc arc;
        /// <summary>
        /// Arc index
        /// </summary>
        private readonly int index;

        /// <summary>
        /// Initializes a new EraseArcCommand instance
        /// </summary>
        /// <param name="digraph">Digraph from which an arc is removed</param>
        /// <param name="arc">Removing Arc</param>
        public EraseArcCommand(Digraph digraph, Arc arc)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            index = digraph.Arcs.IndexOf(arc);
            if (index == -1)
                throw new ArgumentException("The digraph doesn't contain this arc", nameof(arc));
            this.arc = arc;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            arc = digraph.Arcs[index];
            digraph.RemoveArc(index);
        }

        /// <summary>
        /// UnExecutes the command
        /// </summary>
        public void UnExecute() =>
            digraph.AddArc(arc, index);
    }
}
