using System;
using ApplicationClasses;

namespace GraphClasses.Commands
{
    public class AddArcCommand : ICommand
    {
        /// <summary>
        /// Digraph to which an arc is added
        /// </summary>
        private readonly Digraph digraph;
        /// <summary>
        /// Adding arc
        /// </summary>
        private Arc arc;
        /// <summary>
        /// Arc index
        /// </summary>
        private int index = -1;

        /// <summary>
        /// Initializes a new AddArcCommand instance
        /// </summary>
        /// <param name="digraph">Digraph to which an arc is added</param>
        /// <param name="arc">Adding Arc</param>
        public AddArcCommand(Digraph digraph, Arc arc)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            this.arc = arc;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            digraph.AddArc(arc, index);
            index = digraph.Arcs.IndexOf(arc);
        }

        /// <summary>
        /// UnExecutes the command
        /// </summary>
        public void UnExecute()
        {
            arc = digraph.Arcs[index];
            digraph.RemoveArc(index);
        }
    }
}
