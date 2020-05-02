﻿using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            GraphBuilder_SizeChanged(null, null);

            graphDrawing = new GraphDrawing(DrawingSurface.Width, DrawingSurface.Height);

            graphDrawing.RadiusChanged += (object sender, EventArgs e) =>
            {
                if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(Digraph);
                else graphDrawing.DrawTheWholeGraphSandpile(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
            };
        }

        /// <summary>
        /// Adjusts controls to fit the form's size
        /// </summary>
        private void GraphBuilder_SizeChanged(object sender, EventArgs e)
        {
            Build.Location = new Point(Size.Width / 2 - Build.Size.Width / 2, Size.Height / 2 - Build.Size.Height - 50);
            RandomGraph.Location = new Point(Build.Location.X, Build.Location.Y + Build.Size.Height + 10);
            Open.Location = new Point(RandomGraph.Location.X, RandomGraph.Location.Y + RandomGraph.Size.Height + 10);
            SquareLattice.Location = new Point(Open.Location.X, Open.Location.Y + Open.Size.Height + 10);
            TriangleLattice.Location = new Point(Open.Location.X + Open.Size.Width - TriangleLattice.Size.Width,
                Open.Location.Y + Open.Size.Height + 10);

            AppParameters.Size = new Size(AppParameters.Width, DrawingSurface.Height);
            AppParameters.Location = new Point(Size.Width - AppParameters.Size.Width - 30, AppParameters.Location.Y);

            if (Size.Width - (Size.Width - AppParameters.Location.X - 10) - Tools.Size.Width - 40 > 0 && Size.Height - 120 > 0)
                DrawingSurface.Size = new Size(Size.Width - (Size.Width - AppParameters.Location.X - 10) - Tools.Size.Width - 40,
                     Size.Height - 120);

            AppParameters.Size = new Size(AppParameters.Width, DrawingSurface.Height);
            foreach (var page in AppParameters.Controls)
                foreach (Control control in (page as TabPage).Controls)
                    if (control is DataGridView) control.Size = new Size(control.Width, AppParameters.Height - 60);

            GridAdjacencyMatrix.Size = new Size(AdjacencyPage.Width - 20, AdjacencyPage.Height - GridAdjacencyMatrix.Location.Y - 10);

            if (graphDrawing != null)
            {
                graphDrawing.Size = DrawingSurface.Size;
                if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(Digraph);
                else graphDrawing.DrawTheWholeGraphSandpile(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
            }

            TimeTextBox.Location =
                new Point(DrawingSurface.Location.X + DrawingSurface.Size.Width - TimeTextBox.Size.Width,
                    TimeTextBox.Location.Y);
        }

        /// <summary>
        /// Moves digraph on the drawing surface
        /// </summary>
        private void GraphBuilder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Control) return;
            if (e.KeyCode == Keys.Right)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X + 10, Digraph.Vertices[i].Y);
            if (e.KeyCode == Keys.Left)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X - 10, Digraph.Vertices[i].Y);
            if (e.KeyCode == Keys.Up)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y - 10);
            if (e.KeyCode == Keys.Down)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y + 10);

            if (e.KeyCode == Keys.Oemplus)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 1.1), (int)(Digraph.Vertices[i].Y * 1.1));
            if (e.KeyCode == Keys.OemMinus)
                for (int i = 0; i < Digraph.Vertices.Count; i++)
                    Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 0.9), (int)(Digraph.Vertices[i].Y * 0.9));


            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }
        private void Movement_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
                Tools.Focus();
        }
    }
}
