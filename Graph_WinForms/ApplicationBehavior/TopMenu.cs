using ApplicationClasses;
using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Serialization;
using System.IO;

namespace Graph_WinForms
{
    public partial class GraphBuilder
    {
        readonly string AboutApp = "The application developed as a part of a coursework" + Environment.NewLine +
                                   "«The Program for Modeling the Movement of Points on Directed Metric Graph, with the Condition of Synchronization at the Vertices»." +
                                   Environment.NewLine +
                                   Environment.NewLine + "Developed by Baranova Anastasia Andreevna, BSE196." +
                                   Environment.NewLine +
                                   "Supervisor: Vsevolod L. Chernyshev, Associate Professor, Big Data and Information Retrieval School, Faculty of Computer Science." +
                                   Environment.NewLine +
                                   Environment.NewLine + "Higher School of Economics, Moscow, 2020";



        private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e) => Build_Click(sender, e);
        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e) => Open_Click(sender, e);

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (savingDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savingDialog.FileName, FileMode.Create))
                {
                    XmlSerializer format = new XmlSerializer(typeof(Digraph));
                    format.Serialize(stream, Digraph);
                }
            }
        }

        /// <summary>
        /// Saves the graph if user wants to and closes the app
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (Digraph.Vertices.Count != 0)
            {
                SaveGraph("Would you like to save the graph before leaving?", "Saving", out result);
                if (result == DialogResult.Cancel) return;
                else Close();
            }

            result = MessageBox.Show("Are you sure you want to leave?", "Leaving", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Close();
        }

        /// <summary>
        /// Saves the graph if user wants to and goes to a start window
        /// </summary>
        private void MainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Digraph.Vertices.Count != 0)
            {
                SaveGraph(
                    "Would you like to save the graph before going to the start menu? Otherwise, your graph will be lost.",
                    "Saving", out DialogResult result);
                if (result == DialogResult.Cancel) return;
            }

            ChangeDrawingElementsState(false);
            RefreshVariables();
            ChangeMainMenuState(true);
        }

        /// <summary>
        /// Saves the graph if user wants to and goes to a previous window
        /// </summary>
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DrawingSurface.Visible)
            {
                if (Digraph.Vertices.Count != 0)
                {
                    SaveGraph(
                        "Would you like to save the graph before going to the start menu? Your graph will be lost.",
                        "Saving", out DialogResult result);
                    if (result == DialogResult.Cancel) return;
                }

                ChangeDrawingElementsState(false);
                ChangeMainMenuState(true);
                RefreshVariables();
            }
            else if (Build.Visible)
            {
                ChangeMainMenuState(false);
            }
        }

        /// <summary>
        /// Shows a user manual (maybe in the future)
        /// </summary>
        private void UserManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon...", "In development");
        }

        /// <summary>
        /// Giving information about the application and the developer
        /// </summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) =>
            MessageBox.Show(AboutApp, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);

        /// <summary>
        /// Checks if the digraph is strongly connected and goes to the next step if it is
        /// </summary>
        private void NextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ApplicationMethods.IsGraphValid(Digraph))
            {
                MessageBox.Show("The graph is not strongly connected", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            /*MessageBox.Show("The next step is in development...");
            Text = Calculating.GetNumber(Digraph, 0.54, 10, this, out Series data).ToString() + "все";
            
            if (results.chart1.Series.Count > 0)
                results.chart1.Series[0] = data;
            else
            {
                results.chart1.Series.Add(data);
                results.chart1.ChartAreas.Add("Chart");
                results.chart1.ChartAreas[0].AxisX.Interval = 0.1;
            }
            results.ShowDialog();*/
            MovementModelingType type = BasicTypeCheckBox.Checked
                ? MovementModelingType.Basic
                : MovementModelingType.Sandpile;
            MovementModelingMode mode = MovementModelingMode.Animation;

            if (AnimationCheckBox.Checked && ChartCheckBox.Checked && SaveGifCheckBox.Checked)
                mode = MovementModelingMode.AnimationAndChartAndGif;
            else if (AnimationCheckBox.Checked && ChartCheckBox.Checked)
                mode = MovementModelingMode.AnimationAndChart;
            else if (ChartCheckBox.Checked && SaveGifCheckBox.Checked)
                mode = MovementModelingMode.ChartAndGif;
            else if (AnimationCheckBox.Checked && SaveGifCheckBox.Checked)
                mode = MovementModelingMode.AnimationAndGif;
            else if (AnimationCheckBox.Checked) mode = MovementModelingMode.Animation;
            else if (ChartCheckBox.Checked) mode = MovementModelingMode.Chart;
            else if (SaveGifCheckBox.Checked) mode = MovementModelingMode.Gif;

            MovementModeling movement = new MovementModeling(Digraph, (int)TimeNumeric.Value*1000, (double)SpeedNumeric.Value/1000);
            movement.Movement(graphDrawing, DrawingSurface, type, mode);

        }

        private readonly Results results = new Results();



        /// <summary>
        /// Saves the digraph to file if user wants to
        /// </summary>
        private void SaveGraph(string message, string caption, out DialogResult result)
        {
            result = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                SaveToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Returns all the variables to its initial state
        /// </summary>
        private void RefreshVariables()
        {
            CursorButton_Click(null, null);

            Digraph = new Digraph();

            graphDrawing.DrawTheWholeGraph(Digraph);
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
            chosenNumber = -1;
        }
    }

}
