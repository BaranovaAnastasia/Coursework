using System;

namespace DotsMovementModelingAppLib.Commands
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
        private readonly double oldValue;
        /// <summary>
        /// New Parameter value
        /// </summary>
        private readonly double newValue;

        /// <summary>
        /// Occurs when the command executes or unexecutes 
        /// </summary>
        public event EventHandler Executed;

        /// <summary>
        /// Initializes a new ChangeGraphParameterCommand instance
        /// </summary>
        /// <param name="digraph">Digraph whose arc is changed</param>
        /// <param name="index">Changed arc index</param>
        /// <param name="oldValue">Arc length before changes</param>
        /// <param name="newValue">New length</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ChangeArcLengthCommand(Digraph digraph, int index, double oldValue, double newValue)
        {
            this.digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            if (oldValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(newValue), @"Arc length must be positive");
            if (newValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(newValue), @"Arc length must be positive");
            this.index = index;
            this.oldValue = oldValue;
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
            digraph.Arcs[index] = new Arc(digraph.Arcs[index].StartVertex, digraph.Arcs[index].EndVertex, oldValue);
            Executed?.Invoke(oldValue, null);
        }
    }
}
