﻿using System;
using System.Drawing;
using ApplicationClasses;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
        /// <summary>
        /// Opens the editor for creating a new graph
        /// </summary>
        private void Build_Click(object sender, EventArgs e)
        {
            UpdateDigraphInfo();
            ChangeMainMenuState(false);
            ChangeDrawingElementsState(true);
        }

        /// <summary>
        /// Opens a form allowing to build a random graph
        /// </summary>
        private void RandomGraph_Click(object sender, EventArgs e)
        {
            using (var randomDigraphForm = new RandomDigraphGeneratorForm(DrawingSurface.Width, DrawingSurface.Height))
            {
                randomDigraphForm.ShowDialog();
                if (randomDigraphForm.Digraph == null) return;
                Digraph = randomDigraphForm.Digraph;
            }

            UpdateDigraphInfo();
            ChangeMainMenuState(false);
            ChangeDrawingElementsState(true);
        }

        /// <summary>
        /// Opens a file dialog to select a file with a digraph info,
        /// reads it and opens a digraph in the editor
        /// </summary>
        private void Open_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openDialog = DigraphOpenFileDialog())
                {
                    if (openDialog.ShowDialog() != DialogResult.OK) return;
                    using (FileStream fs = new FileStream(openDialog.FileName, FileMode.Open))
                    {
                        XmlSerializer formatter = new XmlSerializer(typeof(Digraph));
                        Digraph = (Digraph)formatter.Deserialize(fs);
                    }
                }
                
                UpdateDigraphInfo();
                ChangeMainMenuState(false);
                ChangeDrawingElementsState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid file:" + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens a form allowing to build a square lattice graph
        /// </summary>
        private void SquareLattice_Click(object sender, EventArgs e)
        {
            using (var square = new SquareLatticeForm(DrawingSurface.Width, DrawingSurface.Height))
            {
                square.ShowDialog();
                if (square.SquareLatticeDigraph == null) return;
                Digraph = square.SquareLatticeDigraph;
            }

            UpdateDigraphInfo();
            ChangeMainMenuState(false);
            ChangeDrawingElementsState(true);
        }

        /// <summary>
        /// Opens a form allowing to build a triangular lattice graph
        /// </summary>
        private void TriangleLattice_Click(object sender, EventArgs e)
        {
            using (var triangle = new TriangularLatticeForm(DrawingSurface.Width, DrawingSurface.Height))
            {
                triangle.ShowDialog();
                if (triangle.TriangularLatticeDigraph == null) return;
                Digraph = triangle.TriangularLatticeDigraph;
            }

            UpdateDigraphInfo();
            ChangeMainMenuState(false);
            ChangeDrawingElementsState(true);
        }


        #region Additional methods

        /// <summary>
        /// Activates or deactivates graph editor controls
        /// </summary>
        private void ChangeDrawingElementsState(bool state)
        {
            Tools.Visible = state;
            DrawingSurface.Visible = state;
            AppParameters.Visible = state;
            if (!state)
            {
                TimeTextBox.Visible = false;
                SandpilePanel.Visible = false;
            }

            saveToolStripMenuItem.Visible = state;
            MainMenuToolStripMenuItem.Visible = state;
            MovementToolStripMenuItem.Visible = state;
            StopToolStripMenuItem.Visible = state;
            ResetToolStripMenuItem.Visible = state;

            RadiusLabel.Visible = state;
            RadiusTrackBar.Visible = state;

            SandpilePanel.Size = new Size(358, 32);
        }

        /// <summary>
        /// Activates or deactivates main menu controls
        /// </summary>
        private void ChangeMainMenuState(bool state)
        {
            Build.Visible = state;
            RandomGraph.Visible = state;
            Open.Visible = state;
            SquareLattice.Visible = state;
            TriangleLattice.Visible = state;
        }

        /// <summary>
        /// Redraws the digraph, refill DataGridViews with the digraph info
        /// </summary>
        private void UpdateDigraphInfo()
        {
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
            ArcName.Items.Clear();
            ArcName.Text = String.Empty;
            ArcLength.Text = String.Empty;
            Digraph.Arcs.ForEach(arc => ArcName.Items.Add((arc.StartVertex + 1) + "-" + (arc.EndVertex + 1)));
            DigraphBuilding.PrintGraphAdjacencyInfo(Digraph.AdjacencyMatrix, GridAdjacencyMatrix);
            DigraphBuilding.PrintGraphThresholds(Digraph, GridThresholds);
            DigraphBuilding.PrintGraphRefractoryPeriods(Digraph, GridRefractoryPeriods);
            DigraphBuilding.PrintGraphInitialState(Digraph, GridInitialState);
        }

        /// <summary>
        /// Returns all the variables to its initial state
        /// </summary>
        private void RefreshVariables()
        {
            CursorButton_Click(null, null);
            if (isOnMovement) ResetToolStripMenuItem_Click(null, null);

            Digraph = new Digraph();

            graphDrawing.ClearTheSurface();
            DrawingSurface.Image = graphDrawing.Image;
            ArcName.Items.Clear();
            ArcName.Text = String.Empty;
            GridAdjacencyMatrix.Columns.Clear();
            GridThresholds.Columns.Clear();
            GridRefractoryPeriods.Columns.Clear();
            GridInitialState.Columns.Clear();

            vStart = vEnd = -1;
            IsPressed = false;
            MovingVertexIndex = -1;
            ArcLength.Text = String.Empty;
        }

        #endregion
    }
}
