using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
        #region Click handlers for drawing tools buttons

        /// <summary>
        /// Disables CursorButton and enables other tools buttons
        /// </summary>
        private void CursorButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = false;
            VertexButton.Enabled = true;
            EdgeButton.Enabled = true;
            DeleteButton.Enabled = true;
        }

        /// <summary>
        /// Disables VertexButton and enables other tools buttons
        /// </summary>
        private void VertexButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = true;
            VertexButton.Enabled = false;
            EdgeButton.Enabled = true;
            DeleteButton.Enabled = true;
        }

        /// <summary>
        /// Disables EdgeButton and enables other tools buttons
        /// </summary>
        private void EdgeButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = true;
            VertexButton.Enabled = true;
            EdgeButton.Enabled = false;
            DeleteButton.Enabled = true;
        }

        /// <summary>
        /// Disables DeleteButton and enables other tools buttons
        /// </summary>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = true;
            VertexButton.Enabled = true;
            EdgeButton.Enabled = true;
            DeleteButton.Enabled = false;
        }

        #endregion

        /// <summary>
        /// Deletes drawn digraph and refreshes all the connected controls
        /// </summary>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (Digraph.Vertices.Count == 0) return;

            const string message = "Would you like to save the graph? Otherwise, your graph will be lost";
            const string caption = "Saving";

            if (SaveGraph(message, caption) != DialogResult.Cancel)
                RefreshVariables();
        }

        /// <summary>
        /// Changes vertices radius
        /// </summary>
        private void RadiusTrackBar_ValueChanged(object sender, EventArgs e)
        {
            graphDrawing.R = RadiusTrackBar.Value;
            RadiusValueLabel.Text = "R = " + RadiusTrackBar.Value;
        }


        #region EnabledChanged handlers

        private void CursorButton_EnabledChanged(object sender, EventArgs e)
        {
            if (CursorButton.Enabled)
            {
                CursorButton.BackColor = Button.DefaultBackColor;
                isPressed = false;
                movingVertexIndex = -1;
                graphDrawing.DrawTheWholeGraph(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
                return;
            }
            CursorButton.BackColor = Color.LightGray;
        }

        private void VertexButton_EnabledChanged(object sender, EventArgs e)
        {
            if (VertexButton.Enabled)
            {
                VertexButton.BackColor = Button.DefaultBackColor;
                return;
            }
            VertexButton.BackColor = Color.LightGray;
        }

        private void EdgeButton_EnabledChanged(object sender, EventArgs e)
        {
            if (EdgeButton.Enabled)
            {
                EdgeButton.BackColor = Button.DefaultBackColor;
                vStart = vEnd = -1;
                graphDrawing.DrawTheWholeGraph(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
                return;
            }
            EdgeButton.BackColor = Color.LightGray;
        }

        private void EraserButton_EnabledChanged(object sender, EventArgs e)
        {
            if (DeleteButton.Enabled)
            {
                DeleteButton.BackColor = Button.DefaultBackColor;
                return;
            }
            DeleteButton.BackColor = Color.LightGray;
        }

        private void ClearAllButton_EnabledChanged(object sender, EventArgs e)
        {
            ClearButton.Visible = ClearButton.Enabled;
        }


        #endregion
    }
}
