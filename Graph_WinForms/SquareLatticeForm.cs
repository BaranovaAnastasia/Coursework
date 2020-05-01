using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class SquareLatticeForm : Form
    {
        private static Random rnd = null;   //Random values generator

        /// <summary>
        /// Generated square lattice digraph
        /// </summary>
        public Digraph SquareLatticeDigraph { get; private set; }

        private int width;      //Drawing surface width (maximum width)
        private int height;     //Drawing surface height (maximum height)

        /// <summary>
        /// Initializes a new instance of SquareLatticeForm
        /// </summary>
        /// <param name="width">Drawing surface width (maximum width)</param>
        /// <param name="height">Drawing surface height (maximum height)</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public SquareLatticeForm(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            InitializeComponent();

            SquareLatticeDigraph = null;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Generates square lattice digraph with chosen parameters
        /// and closes the form
        /// </summary>
        private void OK_Click(object sender, EventArgs e)
        {
            SquareLatticeDigraph = new Digraph();
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
            int step = (Math.Min(width, height) - 100) / Math.Max((int)Xvalue.Value - 1, (int)Yvalue.Value - 1);

            //Current vertex coordinates
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
                        SquareLatticeDigraph.AddVertex(new Vertex(p.X, p.Y), th, rp, s);
                        continue;
                    }
                    SquareLatticeDigraph.AddVertex(new Vertex(p.X, p.Y));
                }
        }

        /// <summary>
        /// Adds arcs to the digraph
        /// </summary>
        private void AddArcs()
        {
            for (int i = 0; i < Xvalue.Value; i++)
                for (int j = 0; j < Yvalue.Value; j++)
                {
                    if (j != Yvalue.Value - 1)
                    {
                        SquareLatticeDigraph.AddArc(new Arc(i * (int)Yvalue.Value + j, i * (int)Yvalue.Value + 1 + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        SquareLatticeDigraph.AddArc(new Arc(i * (int)Yvalue.Value + 1 + j, i * (int)Yvalue.Value + j,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                    }

                    if (i != Xvalue.Value - 1)
                    {
                        SquareLatticeDigraph.AddArc(new Arc(i * (int)Yvalue.Value + j, i * (int)Yvalue.Value + j % (int)Yvalue.Value + (int)Yvalue.Value,
                            rnd != null ? rnd.Next(1, 5) + rnd.NextDouble() : 1));
                        SquareLatticeDigraph.AddArc(new Arc(i * (int)Yvalue.Value + j % (int)Yvalue.Value + (int)Yvalue.Value, i * (int)Yvalue.Value + j,
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
