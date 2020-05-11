using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationClasses;

namespace GraphClasses.Commands
{
    public class ChangeColorCommand : ICommand
    {
        private GraphDrawing target;
        private Type type;
        private Color initialColor;
        private Color newColor;

        public event EventHandler Executed;

        public ChangeColorCommand(GraphDrawing target, Type type, Color initialColor, Color newColor)
        {
            if(type != typeof(Arc) && type != typeof(Vertex))
                throw new ArgumentException(nameof(type));
            this.target = target;
            this.type = type;
            this.initialColor = initialColor;
            this.newColor = newColor;
        }

        public void Execute()
        {
            if (type == typeof(Vertex)) target.VerticesColor = newColor;
            else target.ArcsColor = newColor;
            Executed?.Invoke(newColor, null);
        }

        public void UnExecute()
        {
            if (type == typeof(Vertex)) target.VerticesColor = initialColor;
            else target.ArcsColor = initialColor;
            Executed?.Invoke(initialColor, null);
        }
    }
}
