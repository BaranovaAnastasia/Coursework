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
using System.Windows.Media.Imaging;
using ApplicationClasses.Modeling;

namespace Graph_WinForms
{
    public partial class MainWindow
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
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveDialog.FileName, FileMode.Create))
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
        private void MovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (movement != null)
                if (movement.IsActive) return;
                else { movement.Go(); return; }
            if (movement == null && isOnMovement) return;

            if (!CheckConnectivity()) return;


            foreach (var control in Tools.Controls)
                (control as Button).Enabled = false;
            foreach (var page in AppParameters.Controls)
                foreach (var control in (page as TabPage).Controls)
                    if (!(control is Label)) (control as Control).Enabled = false;

            var type = BasicTypeCheckBox.Checked
                ? MovementModelingType.Basic
                : MovementModelingType.Sandpile;

            var modes = GetModelingModes();

            movement = new MovementModeling(Digraph, (double)SpeedNumeric.Value / 1000, type, modes);

            movement.MovementEnded += StopToolStripMenuItem_Click;
            if (SaveGifCheckBox.Checked) movement.MovementEnded += SaveGif;
            movement.MovementEnded += (object _sender, EventArgs _e) => movement = null;
            movement.Tick += UpdateElapsedTime;

            TimeTextBox.Visible = true;
            TimeTextBox.BringToFront();
            isOnMovement = true;

            movement.Movement(graphDrawing, DrawingSurface);
        }

        private MovementModelingMode[] GetModelingModes()
        {
            MovementModelingMode[] modes = new MovementModelingMode[3];
            if (AnimationCheckBox.Checked) modes[0] = MovementModelingMode.Animation;
            if (ChartCheckBox.Checked) modes[1] = MovementModelingMode.Chart;
            if (SaveGifCheckBox.Checked) modes[2] = MovementModelingMode.Gif;
            return modes;
        }

        private bool CheckConnectivity()
        {
            if (!ApplicationMethods.IsGraphValid(Digraph))
            {
                if (Digraph.Vertices.Count >= 3)
                    MessageBox.Show("The graph is not strongly connected", "Graph validation failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                else MessageBox.Show("Not enough vertices", "Graph validation failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void SaveGif(object sender, EventArgs e)
        {
            if (saveGifDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveGifDialog.FileName, FileMode.Create))
                {
                    var bmp = (DrawingSurface.Image as Bitmap).GetHbitmap();
                    var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        bmp,
                        IntPtr.Zero,
                        System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    movement.MovementGif.Frames.Add(BitmapFrame.Create(src));
                    movement.MovementGif.Save(stream);
                    Text = movement.MovementGif.Frames.Count.ToString();
                }
            }
        }

        public void UpdateElapsedTime(object sender, MovementTickEventArgs e)
        {
            if (movement.IsMovementEndedBasic) return;
            TimeTextBox.Text = " Elapsed time, s:  " + (e.ElapsedTime / 1000.0);
        }

        /// <summary>
        /// Stops movement
        /// </summary>
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (movement != null && movement.IsActive) movement.Stop();
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopToolStripMenuItem_Click(sender, e);
            isOnMovement = false;
            if (movement != null && SaveGifCheckBox.Checked) SaveGif(sender, e);
            for (int i = 0; i < Digraph.State.Count; i++)
            {
                Digraph.State[i] = int.Parse(GridInitialState[0, i].Value.ToString());
                Digraph.TimeTillTheEndOfRefractoryPeriod[i] = 0;
            }
            if (BasicTypeCheckBox.Checked) graphDrawing.DrawTheWholeGraph(Digraph);
            else graphDrawing.DrawTheWholeGraphSandpile(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
            movement = null;
            foreach (var control in Tools.Controls)
                (control as Button).Enabled = true;
            CoursorButton.Enabled = false;
            TimeTextBox.Text = " Elapsed time, s:  0";
            TimeTextBox.Visible = false;
            foreach (var page in AppParameters.Controls)
                foreach (var control in (page as TabPage).Controls)
                    (control as Control).Enabled = true;
        }



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

            if (movement != null && movement.IsActive)
                movement.Stop();
            movement = null;
        }
    }

}
