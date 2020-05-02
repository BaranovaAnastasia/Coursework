using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
        #region Click handlers for drawing tools buttons

        /// <summary>
        /// Disenables CursorButton and enables other tools buttons
        /// </summary>
        private void CursorButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = false;
            VertexButton.Enabled = true;
            EdgeButton.Enabled = true;
            DeleteButton.Enabled = true;
        }

        /// <summary>
        /// Disenables VertexButton and enables other tools buttons
        /// </summary>
        private void VertexButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = true;
            VertexButton.Enabled = false;
            EdgeButton.Enabled = true;
            DeleteButton.Enabled = true;
        }

        /// <summary>
        /// Disenables EdgeButton and enables other tools buttons
        /// </summary>
        private void EdgeButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = true;
            VertexButton.Enabled = true;
            EdgeButton.Enabled = false;
            DeleteButton.Enabled = true;
        }

        /// <summary>
        /// Disenables DeleteButton and enables other tools buttons
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
        private void RadiusTrackBar_ValueChanged(object sender, EventArgs e) =>
            graphDrawing.R = RadiusTrackBar.Value;


        #region EnabledChanged handlers

        private void CursorButton_EnabledChanged(object sender, EventArgs e)
        {
            if (CursorButton.Enabled)
            {
                CursorButton.Size = new Size(75, 75);
                CursorButton.Location = new Point(CursorButton.Location.X - 5, CursorButton.Location.Y - 5);
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
                return;
            }
            EdgeButton.Size = new Size(65, 65);
            EdgeButton.Location = new Point(EdgeButton.Location.X + 5, EdgeButton.Location.Y + 5);
        }

        #endregion
    }
}
