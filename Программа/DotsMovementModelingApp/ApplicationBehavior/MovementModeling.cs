using DotsMovementModelingAppLib;
using DotsMovementModelingAppLib.Modeling;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace DotsMovementModelingApp
{
    public partial class MainWindow
    {
        /// <summary>
        /// Checks if the digraph is strongly connected and goes to the next step if it is
        /// </summary>
        private void MovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (movement != null)
                if (movement.IsActive || SandpilePanel.Visible) return; //returns is modeling is active or the app is waiting for other user's action
                else //restarts movement if it's not over yet but not active at the moment
                {
                    movement.Go();
                    MovementToolStripMenuItem.Enabled = false;
                    StopToolStripMenuItem.Enabled = true;
                    return;
                }
            if (movement == null && isOnMovement) return;   //returns if movement is over but reset button wasn't clicked yet

            if (!CheckConnectivity()) return;   //Connectivity check before movement modeling start

            ChangeWindowStateForMovementModeling(true);

            var type = BasicTypeCheckBox.Checked
                ? MovementModelingType.Basic
                : MovementModelingType.Sandpile;

            var actions = GetModelingActions();
            var sandpileChartTypes = GetChartTypes();

            if(PrepareMovementModeling(type, actions, sandpileChartTypes)) return; 

            movement.StartMovementModeling();
        }

        /// <summary>
        /// Prepares MovementModeling instance for modeling the movement
        /// </summary>
        /// <param name="type">Modeling type</param>
        /// <param name="actions">Additional actions</param>
        /// <param name="sandpileChartTypes">Sandpile chart types</param>
        private bool PrepareMovementModeling(MovementModelingType type,
            MovementModelingActions[] actions,
            SandpileChartType[] sandpileChartTypes)
        {
            movement = new MovementModeling(digraph, (double)SpeedNumeric.Value / 1000, type, actions)
            {
                GraphDrawing = graphDrawing,
                DrawingSurface = DrawingSurface,
                SandpileChartTypes = sandpileChartTypes
            };

            isOnMovement = true;
            movement.Tick += UpdateElapsedTime;
            movement.MovementEnded += StopToolStripMenuItem_Click;

            MovementToolStripMenuItem.Text = @"Continue";
            MovementToolStripMenuItem.Enabled = false;
            StopToolStripMenuItem.Enabled = true;

            if (type == MovementModelingType.Sandpile)
            {
                graphDrawing.DrawTheWholeGraphSandpile(digraph, true);
                DrawingSurface.Image = graphDrawing.Image;
                SandpilePanel.Visible = true;
                SandpilePanel.BringToFront();
                return true;
            }

            movement.MovementEnded += (s, ea) =>
            {
                movement.Tick -= UpdateElapsedTime;
                movement = null;
            };
            if (SaveGifCheckBox.Checked) movement.MovementEnded += SaveGif;

            TimeTextBox.Visible = true;
            TimeTextBox.BringToFront();
            return false;
        }

        #region Params Collecting methods

        private MovementModelingActions[] GetModelingActions()
        {
            var modes = new List<MovementModelingActions>(2);
            if (AnimationCheckBox.Checked) modes.Add(MovementModelingActions.Animation);
            if (ChartCheckBox.Checked) modes.Add(MovementModelingActions.Chart);
            if (SaveGifCheckBox.Checked) modes.Add(MovementModelingActions.Gif);
            return modes.ToArray();
        }

        private SandpileChartType[] GetChartTypes()
        {
            if (!SandpileTypeCheckBox.Checked || !ChartCheckBox.Checked) return null;
            var types = new List<SandpileChartType>(2);
            if (SandpileChartType1.Checked) types.Add(SandpileChartType.NumberOfDotsChart);
            if (SandpileChartType2.Checked) types.Add(SandpileChartType.AvalancheSizesDistributionChart);
            return types.ToArray();
        }

        #endregion

        /// <summary>
        /// Checks if graph is valid for dots movement modeling
        /// </summary>
        private bool CheckConnectivity()
        {
            if (!ConnectivityCheck.IsGraphValid(digraph))
            {
                MessageBox.Show(
                    digraph.Vertices.Count >= 3
                        ? @"The graph is not strongly connected"
                        : @"Not enough vertices",
                    @"Graph validation failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Saves movement modeling gif
        /// </summary>
        private void SaveGif(object sender, EventArgs e)
        {
            using (var fileDialog = SaveFileDialogForGifSaving())
                if (fileDialog.ShowDialog() == DialogResult.OK)
                    using (FileStream stream = new FileStream(fileDialog.FileName, FileMode.Create))
                    {
                        var bmp = ((Bitmap)DrawingSurface.Image).GetHbitmap();
                        var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                            bmp,
                            IntPtr.Zero,
                            System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        movement.MovementGif.Frames.Add(BitmapFrame.Create(src));
                        movement.MovementGif.Save(stream);
                        DeleteObject(bmp);
                    }
        }

        public void UpdateElapsedTime(object sender, MovementTickEventArgs e) =>
            TimeTextBox.Text = @" Elapsed time, s:  " + (e.ElapsedTime / 1000.0) + @"*";

        /// <summary>
        /// Stops movement modeling
        /// </summary>
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (movement == null || !movement.IsActive) return;
            movement?.Stop();
            MovementToolStripMenuItem.Enabled = true;
            StopToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Resets movement
        /// </summary>
        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopToolStripMenuItem_Click(sender, e);
            isOnMovement = false;
            //Saves gif if it's needed
            if ((movement != null || SandpileTypeCheckBox.Checked)
                && SaveGifCheckBox.Checked) SaveGif(sender, e);

            //Resets graph parameters
            for (int i = 0; i < digraph.State.Count; i++)
            {
                digraph.State[i] = int.Parse(GridParameters[2, i].Value.ToString());
                if (digraph.RefractoryPeriods[i] == 0) continue;
                digraph.TimeTillTheEndOfRefractoryPeriod?[i]?.Stop();
            }

            graphDrawing.DrawTheWholeGraph(digraph);
            DrawingSurface.Image = graphDrawing.Image;

            movement = null;

            TimeTextBox.Visible = false;
            TimeTextBox.Text = @" Elapsed time, s:  0";

            SandpilePanel.Visible = false;
            SandpileLabel.Text = @"Select sink vertices and then click here          ";
            SandpileLabel.Enabled = true;
            SandpileLabel.Font = new Font("Segoe UI", 9, FontStyle.Underline);
            SandpilePanel.Size = new Size(SandpilePanel.Size.Width, 32);

            ChangeWindowStateForMovementModeling(false);

            MovementToolStripMenuItem.Text = @"Movement";
            MovementToolStripMenuItem.Enabled = true;

            GC.Collect();
        }

        /// <summary>
        /// Clears selection from sandpile palette
        /// </summary>
        private void SandpilePalette_SelectionChanged(object sender, EventArgs e) =>
            SandpilePalette.ClearSelection();

        /// <summary>
        /// Changes selected controls state
        /// </summary>
        /// <param name="state">true if modeling starts, false if it's over</param>
        private void ChangeWindowStateForMovementModeling(bool state)
        {
            CursorButton.Enabled = VertexButton.Enabled = EdgeButton.Enabled =
                DeleteButton.Enabled = ClearButton.Enabled = !state;

            if (!state) CursorButton.Enabled = false;
            if (SandpileTypeCheckBox.Checked)
            {
                SandpilePalette.BringToFront();
                SandpilePalette.Visible = state;
            }

            foreach (var page in AppParameters.Controls)
                foreach (var control in ((TabPage)page).Controls)
                    if (control is DataGridView dgv) dgv.ReadOnly = state;
                    else ((Control)control).Enabled = !state;

            if (!state)
            {
                GridAdjacencyMatrix.ReadOnly = true;
                AnimationCheckBox.Enabled = false;
            }

            MovementToolStripMenuItem.Enabled = state;
            StopToolStripMenuItem.Visible = state;
            ResetToolStripMenuItem.Visible = state;
        }
    }
}
