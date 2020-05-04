using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ApplicationClasses;
using ApplicationClasses.Modeling;

namespace Graph_WinForms
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
                else { movement.Go(); return; }                         //restarts movement if it's not over yet but not active at the moment
            if (movement == null && isOnMovement) return;               //returns if movement is over but reset button wasn't clicked yet

            if (!CheckConnectivity()) return;   //Connectivity check before movement modeling start

            ChangeWindowStateForMovementModeling(true);

            var type = BasicTypeCheckBox.Checked
                ? MovementModelingType.Basic
                : MovementModelingType.Sandpile;

            var modes = GetModelingModes();
            var sandpileChartTypes = GetChartTypes();

            movement = new MovementModeling(Digraph, (double)SpeedNumeric.Value / 1000, type, modes)
            {
                GraphDrawing = graphDrawing,
                DrawingSurface = DrawingSurface,
                SandpileChartTypes = sandpileChartTypes
            };

            isOnMovement = true;
            movement.Tick += UpdateElapsedTime;
            movement.MovementEnded += StopToolStripMenuItem_Click;

            if (type == MovementModelingType.Sandpile)
            {
                graphDrawing.DrawTheWholeGraphSandpile(Digraph, true);
                DrawingSurface.Image = graphDrawing.Image;
                SandpilePanel.Visible = true;
                SandpilePanel.BringToFront();
                return;
            }

            if (SaveGifCheckBox.Checked) movement.MovementEnded += SaveGif;
            movement.MovementEnded += (object s, EventArgs ea) => movement = null;

            TimeTextBox.Visible = true;
            TimeTextBox.BringToFront();

            movement.StartMovementModeling();
        }

        #region Params Collecting methods
        private MovementModelingMode[] GetModelingModes()
        {
            var modes = new List<MovementModelingMode>(2);
            if (AnimationCheckBox.Checked) modes.Add(MovementModelingMode.Animation);
            if (ChartCheckBox.Checked) modes.Add(MovementModelingMode.Chart);
            if (SaveGifCheckBox.Checked) modes.Add(MovementModelingMode.Gif);
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
            if (!ConnectivityCheck.IsGraphValid(Digraph))
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

        /// <summary>
        /// Saves movement modeling gif
        /// </summary>
        private void SaveGif(object sender, EventArgs e)
        {
            using (var fileDialog = SaveFileDialogForGifSaving())
                if (fileDialog.ShowDialog() == DialogResult.OK)
                    using (FileStream stream = new FileStream(fileDialog.FileName, FileMode.Create))
                    {
                        var bmp = (DrawingSurface.Image as Bitmap).GetHbitmap();
                        var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                            bmp,
                            IntPtr.Zero,
                            System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        movement.MovementGif.Frames.Add(BitmapFrame.Create(src));
                        movement.MovementGif.Save(stream);
                        DeleteObject(bmp);
                        Text = movement.MovementGif.Frames.Count.ToString();
                    }
        }

        public void UpdateElapsedTime(object sender, MovementTickEventArgs e) =>
            TimeTextBox.Text = " Elapsed time, s:  " + (e.ElapsedTime / 1000.0);

        /// <summary>
        /// Stops movement modeling
        /// </summary>
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (movement != null && movement.IsActive) movement.Stop();
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
            for (int i = 0; i < Digraph.State.Count; i++)
            {
                Digraph.State[i] = int.Parse(GridParameters[2, i].Value.ToString());
                Digraph.TimeTillTheEndOfRefractoryPeriod[i]?.Stop();
            }
            Digraph.ResetStock();

            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;

            movement = null;

            TimeTextBox.Visible = false;
            TimeTextBox.Text = " Elapsed time, s:  0";

            SandpilePanel.Visible = false;
            SandpileLabel.Text = "Select sink vertices and then click here          ";
            SandpileLabel.Font = new Font("Segoe UI", 9, FontStyle.Underline);
            SandpilePanel.Size = new Size(SandpilePanel.Size.Width, 32);

            ChangeWindowStateForMovementModeling(false);

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
            foreach (var control in Tools.Controls)
                if (control is Button button) button.Enabled = !state;
            if (!state) CursorButton.Enabled = false;
            if (SandpileTypeCheckBox.Checked)
            {
                SandpilePalette.BringToFront();
                SandpilePalette.Visible = state;
            }

            foreach (var page in AppParameters.Controls)
                foreach (var control in (page as TabPage).Controls)
                    if (control is DataGridView dgv) dgv.ReadOnly = state;
                    else (control as Control).Enabled = !state;

            if (!state) AnimationCheckBox.Enabled = false;
        }
    }
}
