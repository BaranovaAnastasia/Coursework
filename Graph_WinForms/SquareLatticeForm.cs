using System;
using System.Collections;
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
    public partial class SquareLatticeForm : Form
    {
        private static Random rnd = null;

        public SquareLatticeForm(int width, int height)
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
            int step = (Math.Min(width, height) - 100) / Math.Max((int)Xvalue.Value - 1, (int)Yvalue.Value - 1);

            Point p = new Point((width - 100 - step * ((int)Yvalue.Value - 1)) / 2 + 50,
                (height - 100 - step * ((int)Xvalue.Value - 1)) / 2 + 50);
            for (int i = 0; i < Xvalue.Value; i++, p.Y += step, p.X = (width - 100 - step * ((int)Yvalue.Value - 1)) / 2 + 50)
                for (int j = 0; j < Yvalue.Value; j++, p.X += step)
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


            for (int i = 0; i < Xvalue.Value; i++)
                for (int j = 0; j < Yvalue.Value; j++)
                {
                    if (j != Yvalue.Value - 1)
                    {
                        Digraph.AddArc(new Arc(i * (int)Yvalue.Value + j, i * (int)Yvalue.Value + 1 + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        Digraph.AddArc(new Arc(i * (int)Yvalue.Value + 1 + j, i * (int)Yvalue.Value + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                    }

                    if (i != Xvalue.Value - 1)
                    {
                        Digraph.AddArc(new Arc(i * (int)Yvalue.Value + j, i * (int)Yvalue.Value + j % (int)Yvalue.Value + (int)Yvalue.Value,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        Digraph.AddArc(new Arc(i * (int)Yvalue.Value + j % (int)Yvalue.Value + (int)Yvalue.Value, i * (int)Yvalue.Value + j,
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
