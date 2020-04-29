using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class TriangularLatticeForm : Form
    {
        private static Random rnd = null;

        public TriangularLatticeForm(int width, int height)
        {
            InitializeComponent();
            Digraph = null;
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            this.width = width;
            this.height = height;
        }

        public Digraph Digraph { get; private set; }
        private int width;
        private int height;

        private void OK_Click(object sender, EventArgs e)
        {
            Digraph = new Digraph();
            int step = (Math.Min(width, height) - 100) / Math.Max((int) Xvalue.Value - 1, (int) Yvalue.Value - 1);
            
            Point p = new Point((width - 100 - step * (int)((int)Xvalue.Value - 1)) / 2 + 100,
                (height - 100 - step * ((int)Yvalue.Value - 1)) / 2 + 75);
            for (int i = 0; i < Yvalue.Value; i++, p.Y += (int)(step * 0.866),
                p.X = i % 2 == 0
                ? (width - 100 - step * ((int)Xvalue.Value - 1)) / 2 + 100
                : (width - 100 - step * ((int)Xvalue.Value - 1)) / 2 + 100 - step / 2)
                for (int j = 0; j < Xvalue.Value; j++, p.X += step)
                {
                    if (rnd != null)
                    {
                        int th = rnd.Next(1, 5);
                        int rp = rnd.Next(1, 5);
                        int s = rnd.Next(0, 2 * th);
                        Digraph.AddVertex(new Vertex(p.X, p.Y), th, rp, s);
                        continue;
                    }
                    Digraph.AddVertex(new Vertex(p.X, p.Y));
                }


            for (int i = 0; i < Yvalue.Value; i++)
                for (int j = 0; j < Xvalue.Value; j++)
                {
                    if (j != Xvalue.Value - 1)
                    {
                        Digraph.AddArc(new Arc(i * (int)Xvalue.Value + j, i * (int)Xvalue.Value + 1 + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        Digraph.AddArc(new Arc(i * (int)Xvalue.Value + 1 + j, i * (int)Xvalue.Value + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                    }

                    if (i != Yvalue.Value - 1)
                    {
                        Digraph.AddArc(new Arc(i * (int)Xvalue.Value + j, i * (int)Xvalue.Value + j % (int)Xvalue.Value + (int)Xvalue.Value,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        Digraph.AddArc(new Arc(i * (int)Xvalue.Value + j % (int)Xvalue.Value + (int)Xvalue.Value, i * (int)Xvalue.Value + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                    }

                    if (i != Yvalue.Value - 1 && ((j != 0 && i % 2 != 0) || (j != Xvalue.Value - 1 && i % 2 == 0)))
                    {
                        Digraph.AddArc(new Arc(i * (int)Xvalue.Value + j, i * (int)Xvalue.Value + j % (int)Xvalue.Value + (int)Xvalue.Value + (int)Math.Pow(-1, i),
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        Digraph.AddArc(new Arc(i * (int)Xvalue.Value + j % (int)Xvalue.Value + (int)Xvalue.Value + (int)Math.Pow(-1, i), i * (int)Xvalue.Value + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                    }
                }

            Close();
        }

        private void ParamsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ParamsCheckBox.Checked) rnd = new Random();
            else rnd = null;
        }
    }
}
