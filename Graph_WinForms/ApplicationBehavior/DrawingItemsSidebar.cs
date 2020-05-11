using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationClasses;

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

        /// <summary>
        /// Enlarges digraph image by moving vertices
        /// </summary>
        private void EnlargeButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 1.1), (int)(Digraph.Vertices[i].Y * 1.1));
            UpdateImage();
        }

        /// <summary>
        /// Reduces digraph image by moving vertices
        /// </summary>
        private void ReduceButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex((int)(Digraph.Vertices[i].X * 0.9), (int)(Digraph.Vertices[i].Y * 0.9));
            UpdateImage();
        }

        #region Graph moving

        private void Up_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y - 10);
            UpdateImage();
        }

        private void Left_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X - 10, Digraph.Vertices[i].Y);
            UpdateImage();
        }

        private void Right_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X + 10, Digraph.Vertices[i].Y);
            UpdateImage();
        }

        private void Down_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Digraph.Vertices.Count; i++)
                Digraph.Vertices[i] = new Vertex(Digraph.Vertices[i].X, Digraph.Vertices[i].Y + 10);
            UpdateImage();
        }

        #endregion

        /// <summary>
        /// Redraws the digraph
        /// </summary>
        private void UpdateImage()
        {
            if (isOnMovement && SandpileTypeCheckBox.Checked)
                graphDrawing.DrawTheWholeGraphSandpile(Digraph, false);
            else graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        #region Color panels

        private void VertexColorDialogOpen_Click(object sender, EventArgs e)
        {
            if (GraphStyleColorDialog.ShowDialog() == DialogResult.Cancel) return;

            VerticesColorPanel.BackColor = GraphStyleColorDialog.Color;
            graphDrawing.VerticesPen = new Pen(GraphStyleColorDialog.Color, graphDrawing.VerticesPen.Width);
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;

        }

        private void ArcsColorDialogOpen_Click(object sender, EventArgs e)
        {
            if (GraphStyleColorDialog.ShowDialog() == DialogResult.Cancel) return;

            ArcsColorPanel.BackColor = GraphStyleColorDialog.Color;
            graphDrawing.ArcsPen = new Pen(Color.FromArgb(80, GraphStyleColorDialog.Color), graphDrawing.ArcsPen.Width);
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        private void VerticesColorPanel_Leave(object sender, EventArgs e) =>
            VertexColorDialogOpen.Visible = false;

        private void VerticesColorPanel_Enter(object sender, EventArgs e) =>
            VertexColorDialogOpen.Visible = true;

        private void VerticesColorPanel_Click(object sender, EventArgs e) =>
            VerticesColorPanel.Focus();

        private void ArcsColorPanel_Click(object sender, EventArgs e) =>
            ArcsColorPanel.Focus();

        private void ArcsColorPanel_Enter(object sender, EventArgs e) =>
            ArcsColorDialogOpen.Visible = true;

        private void ArcsColorPanel_Leave(object sender, EventArgs e) =>
            ArcsColorDialogOpen.Visible = false;

        #endregion

        private void UndoButton_Click(object sender, EventArgs e)
        {
            commandsManager.Undo();
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            commandsManager.Redo();
            graphDrawing.DrawTheWholeGraph(Digraph);
            DrawingSurface.Image = graphDrawing.Image;
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
