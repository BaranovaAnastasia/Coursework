using System;
using System.Windows.Forms;
using ApplicationClasses;

namespace CourseworkApp
{
    public partial class RandomDigraphGeneratorForm : Form
    {
        private static readonly Random Rnd = new Random();   //Random values generator

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
            NumOfVertices.Value = Rnd.Next(3, 21);

        /// <summary>
        /// Generates a random digraph
        /// </summary>
        private void Button2_Click(object sender, EventArgs e)
        {
            Digraph = new Digraph();
            bool[] visitedV = new bool[(int)NumOfVertices.Value];
            for (int i = 0; i < (int)NumOfVertices.Value; i++)
            {
                int th = Rnd.Next(1, 5);
                int p = Rnd.Next(1, 10001);
                int s = Rnd.Next(0, 2 * th);
                Digraph.AddVertex(new Vertex(Rnd.Next(10, width - 10), Rnd.Next(10, height - 10)), th, p, s);
                visitedV[i] = i == 0;
            }

            int start = 0;
            for (int i = 0; i < (int)NumOfVertices.Value - 1; i++)
            {
                int end;
                do { } while ((end = Rnd.Next(1, visitedV.Length)) == start || visitedV[end]);
                Digraph.AddArc(new Arc(start, end, Rnd.Next(3, 11) + Rnd.NextDouble()));
                visitedV[end] = true;
                start = end;
            }
            Digraph.AddArc(new Arc(start, 0, Rnd.Next(3, 11) + Rnd.NextDouble()));
            this.Close();
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        private void Cancel_Click(object sender, EventArgs e) => Close();
    }
}
