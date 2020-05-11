using System;
using ApplicationClasses;

namespace GraphClasses.Commands
{
    public class ChangeArcLengthCommand : ICommand
    {
        /// <summary>
        /// Digraph to which a vertex is added
        /// </summary>
        private readonly Digraph digraph;
        /// <summary>
        /// Changed value index
        /// </summary>
        private readonly int index;
        /// <summary>
        /// Parameter value before changes
        /// </summary>
        private readonly double initialValue;
        /// <summary>
        /// New Parameter value
        /// </summary>
        private readonly double newValue;

        public event EventHandler Executed;

        /// <summary>
        /// Initializes a new ChangeGraphParameterCommand instance
        /// </summary>
        /// <param name="digraph">Digraph whose arc is changed</param>
        /// <param name="index">Changed arc index</param>
        /// <param name="initialValue">Arc length before changes</param>
        /// <param name="newValue">New length</param>
        public ChangeArcLengthCommand(Digraph digraph, int index, double initialValue, double newValue)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            if (initialValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(newValue), "Arc length must be positive");
            if (newValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(newValue), "Arc length must be positive");
            this.index = index;
            this.initialValue = initialValue;
            this.newValue = newValue;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            digraph.Arcs[index] = new Arc(digraph.Arcs[index].StartVertex, digraph.Arcs[index].EndVertex, newValue);
            Executed?.Invoke(newValue, null);
        }

        /// <summary>
        /// UnExecutes the command
        /// </summary>
        public void UnExecute()
        {
            digraph.Arcs[index] = new Arc(digraph.Arcs[index].StartVertex, digraph.Arcs[index].EndVertex, initialValue);
            Executed?.Invoke(initialValue, null);
        }
    }
}
