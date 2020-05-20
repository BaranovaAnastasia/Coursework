using System;
using System.Drawing;
using DotsMovementModelingAppLib;

namespace DotsMovementModelingAppLib.Commands
{
    public class ChangeColorCommand : ICommand
    {
        /// <summary>
        /// GraphDrawing instance in which color is changed
        /// </summary>
        private readonly GraphDrawing target;
        /// <summary>
        /// Shows if the color of arcs or vertices is changed
        /// </summary>
        private readonly Type type;
        /// <summary>
        /// Color before changes
        /// </summary>
        private readonly Color oldColor;
        /// <summary>
        /// New color
        /// </summary>
        private readonly Color newColor;

        /// <summary>
        /// Occurs when the command executes or unexecutes 
        /// </summary>
        public event EventHandler Executed;

        /// <summary>
        /// Initializes a new ChangeColorCommand instance
        /// </summary>
        /// <param name="target">GraphDrawing instance in which color is changed</param>
        /// <param name="type">Shows if the color of arcs or vertices is changed</param>
        /// <param name="oldColor">Color before changes</param>
        /// <param name="newColor">New color</param>
        /// <exception cref="ArgumentException"/>
        public ChangeColorCommand(GraphDrawing target, Type type, Color oldColor, Color newColor)
        {
            if(type != typeof(Arc) && type != typeof(Vertex))
                throw new ArgumentException(nameof(type));
            this.target = target;
            this.type = type;
            this.oldColor = oldColor;
            this.newColor = newColor;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            if (type == typeof(Vertex)) target.VerticesColor = newColor;
            else target.ArcsColor = newColor;
            Executed?.Invoke(newColor, null);
        }

        /// <summary>
        /// UnExecutes the command
        /// </summary>
        public void UnExecute()
        {
            if (type == typeof(Vertex)) target.VerticesColor = oldColor;
            else target.ArcsColor = oldColor;
            Executed?.Invoke(oldColor, null);
        }
    }
}
