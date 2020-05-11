using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphClasses.Commands
{
    public class ChangeColorCommand : ICommand
    {
        private Color target;
        private Color initialColor;
        private Color newColor;

        public event EventHandler Executed;

        public ChangeColorCommand(Color target, Color initialColor, Color newColor)
        {
            this.target = target;
            this.initialColor = initialColor;
            this.newColor = newColor;
        }

        public void Execute()
        {
            target = newColor;
            Executed?.Invoke(newColor, null);
        }

        public void UnExecute()
        {
            target = initialColor;
            Executed?.Invoke(newColor, null);
        }
    }
}
