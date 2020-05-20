using System;
using DotsMovementModelingAppLib;

namespace DotsMovementModelingAppLib.Commands
{
    public class ChangeRadiusCommand : ICommand
    {
        /// <summary>
        /// GraphDrawing instance in which radius is changed
        /// </summary>
        private readonly GraphDrawing target;
        /// <summary>
        /// Radius before changes
        /// </summary>
        private readonly int oldRadius;
        /// <summary>
        /// New radius
        /// </summary>
        private readonly int newRadius;
        /// <summary>
        /// Occurs when the command executes or unexecutes 
        /// </summary>
        public event EventHandler Executed;

        /// <summary>
        /// Initializes a new ChangeColorCommand instance
        /// </summary>
        /// <param name="target">GraphDrawing instance in which color is changed</param>
        /// <param name="oldRadius">Previous vertices radius</param>
        /// <param name="newRadius">New vertices radius</param>
        /// <exception cref="ArgumentException"/>
        public ChangeRadiusCommand(GraphDrawing target, int oldRadius, int newRadius)
        {
            if (oldRadius < 8)
                throw new ArgumentOutOfRangeException(nameof(oldRadius));
            if (newRadius < 8)
                throw new ArgumentOutOfRangeException(nameof(newRadius));
            this.target = target;
            this.oldRadius = oldRadius;
            this.newRadius = newRadius;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            target.R = newRadius;
            Executed?.Invoke(newRadius, null);
        }

        /// <summary>
        /// UnExecutes the command
        /// </summary>
        public void UnExecute()
        {
            target.R = oldRadius;
            Executed?.Invoke(oldRadius, null);
        }
    }
}
