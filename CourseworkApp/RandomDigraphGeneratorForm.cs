using System;
using System.Windows.Forms;
using ApplicationClasses;

namespace CourseworkApp
{
    public partial class RandomDigraphGeneratorForm : Form
    {
        private static readonly Random rnd = new Random();   //Random values generator

        /// <summary>
        /// Generated random digraph
        /// </summary>
        public Digraph Digraph { get; private set; }

        private readonly int width;      //Drawing surface width (maximum width)
        private readonly int height;     //Drawing surface height (maximum height)

        /// <summary>
        /// Initializes a new instance of RandomDigraphGeneratorForm
        /// </summary>
        /// <param name="width">Drawing surface width (maximum width)</param>
        /// <param name="height">Drawing surface height (maximum height)</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RandomDigraphGeneratorForm(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            InitializeComponent();

            Digraph = null;
            this.width = width;
            this.height = height;
        }

        private void VNRandom_Click(object sender, EventArgs e) =>
            NumOfVertices.Value = rnd.Next(3, 21);

        /// <summary>
        /// Generates a random digraph
        /// </summary>
        private void Button2_Click(object sender, EventArgs e)
        {
            Digraph = new Digraph();
            bool[] visitedV = new bool[(int)NumOfVertices.Value];
            for (int i = 0; i < (int)NumOfVertices.Value; i++)
            {
                int th = rnd.Next(1, 5);
                int p = rnd.Next(1, 10001);
                int s = rnd.Next(0, 2 * th);
                Digraph.AddVertex(new Vertex(rnd.Next(10, width - 10), rnd.Next(10, height - 10)), th, p, s);
                visitedV[i] = i == 0;
            }

            int start = 0;
            int end;
            for (int i = 0; i < (int)NumOfVertices.Value - 1; i++)
            {
                do { } while ((end = rnd.Next(1, visitedV.Length)) == start || visitedV[end]);
                Digraph.AddArc(new Arc(start, end, rnd.Next(3, 11) + rnd.NextDouble()));
                visitedV[end] = true;
                start = end;
            }
            Digraph.AddArc(new Arc(start, 0, rnd.Next(3, 11) + rnd.NextDouble()));
            this.Close();
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        private void Cancel_Click(object sender, EventArgs e) => Close();
    }
}
