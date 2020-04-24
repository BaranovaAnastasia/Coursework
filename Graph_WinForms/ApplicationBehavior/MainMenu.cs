using System;
using ApplicationClasses;
using System.Windows.Forms;
using System.Xml.Serialization;
using  System.IO;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
        /// <summary>
        /// Opens the editor for creating a new graph
        /// </summary>
        private void Build_Click(object sender, EventArgs e)
        {
            ChangeMainMenuState(false);
            ChangeDrawingElementsState(true);
            GridThresholds.Columns.Add("th", "th");
            GridThresholds.Columns[0].Width = 95;

            GridRefractoryPeriods.Columns.Add("p", "p, ms");
            GridRefractoryPeriods.Columns[0].Width = 95;

            GridInitialState.Columns.Add("s", "s");
            GridInitialState.Columns[0].Width = 95;
        }

        /// <summary>
        /// Creates a random strongly connected digraph with chosen number of vertices
        /// and opens it in the editor
        /// </summary>
        private void RandomGraph_Click(object sender, EventArgs e)
        {
            // Opens a special form to select the number of vertices
            Form2 numberOfV = new Form2();
            numberOfV.ShowDialog();
            numberOfV.Dispose();
            if (chosenNumber == -1) return;

            ApplicationMethods.GetRandomGraph(chosenNumber, out Digraph, DrawingSurface.Width, DrawingSurface.Height);
            chosenNumber = -1;
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
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream fs = new FileStream(openDialog.FileName, FileMode.Open))
                    {
                        XmlSerializer formatter = new XmlSerializer(typeof(Digraph));
                        Digraph = (Digraph)formatter.Deserialize(fs);
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
        }

        /// <summary>
        /// Activates or deactivates graph editor controls
        /// </summary>
        private void ChangeDrawingElementsState(bool state)
        {
            Tools.Visible = state;
            DrawingSurface.Visible = state;
            AppParameters.Visible = state;
            if(!state)
                TimeTextBox.Visible = false;
            MainMenuToolStripMenuItem.Visible = state;
            MovementToolStripMenuItem.Visible = state;
            saveToolStripMenuItem.Visible = state;
            StopToolStripMenuItem.Visible = state;
            ResetToolStripMenuItem.Visible = state;
        }

        /// <summary>
        /// Activates or deactivates main menu controls
        /// </summary>
        private void ChangeMainMenuState(bool state)
        {
            Build.Visible = state;
            RandomGraph.Visible = state;
            Open.Visible = state;
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
            ApplicationMethods.PrintGraphAdjacencyInfo(Digraph.AdjacencyMatrix, GridAdjacencyMatrix);
            ApplicationMethods.PrintGraphThresholds(Digraph, GridThresholds);
            ApplicationMethods.PrintGraphRefractoryPeriods(Digraph, GridRefractoryPeriods);
            ApplicationMethods.PrintGraphInitialState(Digraph, GridInitialState);
        }
    }
}
