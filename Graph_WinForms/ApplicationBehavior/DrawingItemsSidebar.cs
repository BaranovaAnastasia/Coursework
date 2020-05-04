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
                CursorButton.Size = new Size(75, 75);
                CursorButton.Location = new Point(CursorButton.Location.X - 5, CursorButton.Location.Y - 5);
                isPressed = false;
                movingVertexIndex = -1;
                graphDrawing.DrawTheWholeGraph(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
                return;
            }
            CursorButton.Size = new Size(65, 65);
            CursorButton.Location = new Point(CursorButton.Location.X + 5, CursorButton.Location.Y + 5);
        }

        private void VertexButton_EnabledChanged(object sender, EventArgs e)
        {
            if (VertexButton.Enabled)
            {
                VertexButton.Size = new Size(75, 75);
                VertexButton.Location = new Point(VertexButton.Location.X - 5, VertexButton.Location.Y - 5);
                return;
            }
            VertexButton.Size = new Size(65, 65);
            VertexButton.Location = new Point(VertexButton.Location.X + 5, VertexButton.Location.Y + 5);
        }

        private void EdgeButton_EnabledChanged(object sender, EventArgs e)
        {
            if (EdgeButton.Enabled)
            {
                EdgeButton.Size = new Size(75, 75);
                EdgeButton.Location = new Point(EdgeButton.Location.X - 5, EdgeButton.Location.Y - 5);
                vStart = vEnd = -1;
                graphDrawing.DrawTheWholeGraph(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
                return;
            }
            EdgeButton.Size = new Size(65, 65);
            EdgeButton.Location = new Point(EdgeButton.Location.X + 5, EdgeButton.Location.Y + 5);
        }

        private void EraserButton_EnabledChanged(object sender, EventArgs e)
        {
            if (DeleteButton.Enabled)
            {
                DeleteButton.Size = new Size(75, 75);
                DeleteButton.Location = new Point(DeleteButton.Location.X - 5, DeleteButton.Location.Y - 5);
                return;
            }
            DeleteButton.Size = new Size(65, 65);
            DeleteButton.Location = new Point(DeleteButton.Location.X + 5, DeleteButton.Location.Y + 5);
        }

        private void ClearAllButton_EnabledChanged(object sender, EventArgs e)
        {
            if (ClearButton.Enabled)
            {
                ClearButton.Size = new Size(75, 75);
                ClearButton.Location = new Point(ClearButton.Location.X - 5, ClearButton.Location.Y - 5);
                return;
            }
            ClearButton.Size = new Size(65, 65);
            ClearButton.Location = new Point(ClearButton.Location.X + 5, ClearButton.Location.Y + 5);
        }


        #endregion
    }
}
