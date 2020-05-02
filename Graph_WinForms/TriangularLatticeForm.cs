using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class TriangularLatticeForm : Form
    {
        private static Random rnd = null;   // Random values generator

        /// <summary>
        /// Generated square lattice digraph
        /// </summary>
        public Digraph TriangularLatticeDigraph { get; private set; }

        private readonly int width;      //Drawing surface width (maximum width)
        private readonly int height;     //Drawing surface height (maximum height)

        /// <summary>
        /// Initializes a new instance of TriangularLatticeForm
        /// </summary>
        /// <param name="width">Drawing surface width (maximum width)</param>
        /// <param name="height">Drawing surface height (maximum height)</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public TriangularLatticeForm(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            InitializeComponent();

            TriangularLatticeDigraph = null;
            this.width = width;
            this.height = height;
        }


        private void OK_Click(object sender, EventArgs e)
        {
            TriangularLatticeDigraph = new Digraph();
            AddVertices();
            AddArcs();
            Close();
        }


        /// <summary>
        /// Adds vertices to the digraph
        /// </summary>
        private void AddVertices()
        {
            //Distance between adjacent vertices 
            int step = (int)((Math.Min(width, height)) * 1.0 / Math.Max((int)Xvalue.Value - 0.5, (int)Yvalue.Value - 1));

            //Current vertex coordinates
            int x = (int)((width - step * ((int)Xvalue.Value - 0.5)) / 2.0 + step / 2);
            Point p = new Point(x, (height - 100 - step * ((int)Yvalue.Value - 1)) / 2 + 75);

            for (int i = 0; i < Yvalue.Value; i++, p.Y += (int)(step * 0.866),
                 p.X = i % 2 == 0 ? x : x - step / 2)
                for (int j = 0; j < Xvalue.Value; j++, p.X += step)
                {
                    if (rnd != null)
                    {
                        int th = rnd.Next(1, 5);
                        int rp = rnd.Next(1, 10001);
                        int s = rnd.Next(0, 2 * th);
                        TriangularLatticeDigraph.AddVertex(new Vertex(p.X, p.Y), th, rp, s);
                        continue;
                    }
                    TriangularLatticeDigraph.AddVertex(new Vertex(p.X, p.Y));
                }
        }

        /// <summary>
        /// Adds arcs to the digraph
        /// </summary>
        private void AddArcs()
        {
            for (int i = 0; i < Yvalue.Value; i++)
                for (int j = 0; j < Xvalue.Value; j++)
                {
                    if (j != Xvalue.Value - 1)
                    {
                        TriangularLatticeDigraph.AddArc(new Arc(i * (int)Xvalue.Value + j, i * (int)Xvalue.Value + 1 + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        TriangularLatticeDigraph.AddArc(new Arc(i * (int)Xvalue.Value + 1 + j, i * (int)Xvalue.Value + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                    }

                    if (i != Yvalue.Value - 1)
                    {
                        TriangularLatticeDigraph.AddArc(new Arc(i * (int)Xvalue.Value + j, i * (int)Xvalue.Value + j % (int)Xvalue.Value + (int)Xvalue.Value,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        TriangularLatticeDigraph.AddArc(new Arc(i * (int)Xvalue.Value + j % (int)Xvalue.Value + (int)Xvalue.Value, i * (int)Xvalue.Value + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                    }

                    if (i != Yvalue.Value - 1 && ((j != 0 && i % 2 != 0) || (j != Xvalue.Value - 1 && i % 2 == 0)))
                    {
                        TriangularLatticeDigraph.AddArc(new Arc(i * (int)Xvalue.Value + j, i * (int)Xvalue.Value + j % (int)Xvalue.Value + (int)Xvalue.Value + (int)Math.Pow(-1, i),
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        TriangularLatticeDigraph.AddArc(new Arc(i * (int)Xvalue.Value + j % (int)Xvalue.Value + (int)Xvalue.Value + (int)Math.Pow(-1, i), i * (int)Xvalue.Value + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                    }
                }
        }


        /// <summary>
        /// Changes the random values generator value
        /// to allow or forbid random digraph parameters filling
        /// </summary>
        private void ParamsCheckBox_CheckedChanged(object sender, EventArgs e) =>
            rnd = ParamsCheckBox.Checked ? new Random() : null;

        /// <summary>
        /// Closes the form
        /// </summary>
        private void Cancel_Click(object sender, EventArgs e) => Close();
    }
}
