using System;
using System.Drawing;
using ApplicationClasses;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using GraphClasses.Commands;

namespace CourseworkApp
{
    public partial class MainWindow
    {
        /// <summary>
        /// Opens the editor for creating a new graph
        /// </summary>
        private void Build_Click(object sender, EventArgs e)
        {
            RefreshVariables();
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
                digraph = randomDigraphForm.Digraph;
            }

            SubscribeToDigraphEvents();
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

                    RefreshVariables();
                    using (FileStream fs = new FileStream(openDialog.FileName, FileMode.Open))
                    {
                        XmlSerializer formatter = new XmlSerializer(typeof(Digraph));
                        digraph = (Digraph)formatter.Deserialize(fs);
                    }
                }

                SubscribeToDigraphEvents();
                UpdateDigraphInfo();
                ChangeMainMenuState(false);
                ChangeDrawingElementsState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Invalid file:" + Environment.NewLine + ex.Message, @"Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                digraph = square.SquareLatticeDigraph;
            }

            SubscribeToDigraphEvents();
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
                digraph = triangle.TriangularLatticeDigraph;
            }

            SubscribeToDigraphEvents();
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
            graphDrawing.DrawTheWholeGraph(digraph);
            DrawingSurface.Image = graphDrawing.Image;
            ArcName.Items.Clear();
            ArcName.Text = String.Empty;
            ArcLength.Text = String.Empty;
            digraph.Arcs.ForEach(arc => ArcName.Items.Add((arc.StartVertex + 1) + "-" + (arc.EndVertex + 1)));
            DigraphInformationDemonstration.DisplayGraphAdjacencyInfo(digraph.AdjacencyMatrix, GridAdjacencyMatrix);
            DigraphInformationDemonstration.DisplayGraphParameters(digraph, GridParameters);
        }

        /// <summary>
        /// Returns all the variables to its initial state
        /// </summary>
        private void RefreshVariables()
        {
            CursorButton_Click(null, null);
            if (isOnMovement) ResetToolStripMenuItem_Click(null, null);

            digraph = new Digraph();
            SubscribeToDigraphEvents();

            graphDrawing.ClearTheSurface();
            DrawingSurface.Image = graphDrawing.Image;

            ArcName.Items.Clear();
            ArcName.Text = String.Empty;
            ArcLength.Text = String.Empty;
            GridAdjacencyMatrix.Columns.Clear();
            GridParameters.Columns.Clear();

            BasicTypeCheckBox.Checked = true;
            ChartCheckBox.Checked = SaveGifCheckBox.Checked = SandpileChartType2.Checked = false;
            SpeedNumeric.Value = 1;
            RadiusTrackBar.Value = 8;

            commandsManager = new CommandsManager();
        }

        #endregion
    }
}
