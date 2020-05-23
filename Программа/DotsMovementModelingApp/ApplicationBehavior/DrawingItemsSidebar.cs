using DotsMovementModelingAppLib;
using DotsMovementModelingAppLib.Commands;
using System;
using System.Windows.Forms;

namespace DotsMovementModelingApp
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
            EscButton_Click(sender, e);
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
            EscButton_Click(sender, e);
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
            EscButton_Click(sender, e);
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
            EscButton_Click(sender, e);
        }

        #endregion

        /// <summary>
        /// Deletes drawn digraph and refreshes all the connected controls
        /// </summary>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (digraph.Vertices.Count == 0) return;

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
            RadiusValueLabel.Text = @"R = " + RadiusTrackBar.Value;
            if (!isCommand)
            {
                radiusChanged = true;
                MouseWheelTimer.Start();
            }
            else radius = RadiusTrackBar.Value;
        }

        /// <summary>
        /// Enlarges digraph image by moving vertices
        /// </summary>
        private void EnlargeButton_Click(object sender, EventArgs e)
        {
            var command = new ResizeDigraphCommand(digraph, 1.1);
            commandsManager.Execute(command);
            UpdateImage();
        }

        /// <summary>
        /// Reduces digraph image by moving vertices
        /// </summary>
        private void ReduceButton_Click(object sender, EventArgs e)
        {
            var command = new ResizeDigraphCommand(digraph, 0.9);
            commandsManager.Execute(command);
            UpdateImage();
        }

        #region Graph moving

        private void Up_Click(object sender, EventArgs e)
        {
            var command = new MoveDigraphCommand(digraph, 0, 10);
            commandsManager.Execute(command);
            UpdateImage();
        }

        private void Left_Click(object sender, EventArgs e)
        {
            var command = new MoveDigraphCommand(digraph, -10, 0);
            commandsManager.Execute(command);
            UpdateImage();
        }

        private void Right_Click(object sender, EventArgs e)
        {
            var command = new MoveDigraphCommand(digraph, 10, 0);
            commandsManager.Execute(command);
            UpdateImage();
        }

        private void Down_Click(object sender, EventArgs e)
        {
            var command = new MoveDigraphCommand(digraph, -10, 0);
            commandsManager.Execute(command);
            UpdateImage();
        }

        #endregion

        /// <summary>
        /// Redraws the digraph
        /// </summary>
        private void UpdateImage()
        {
            if (isOnMovement && SandpileTypeCheckBox.Checked)
                graphDrawing.DrawTheWholeGraphSandpile(digraph, false);
            else graphDrawing.DrawTheWholeGraph(digraph);
            DrawingSurface.Image = graphDrawing.Image;
        }

        #region Color panels

        /// <summary>
        /// Opens color dialog to select new vertices color
        /// </summary>
        private void VertexColorDialogOpen_Click(object sender, EventArgs e)
        {
            if (GraphStyleColorDialog.ShowDialog() == DialogResult.Cancel) return;

            var command = new ChangeColorCommand(graphDrawing, typeof(Vertex),
                graphDrawing.VerticesColor, GraphStyleColorDialog.Color);

            command.Executed += (s, ea) =>
            {
                VerticesColorPanel.BackColor = graphDrawing.VerticesColor;
                graphDrawing.DrawTheWholeGraph(digraph);
                DrawingSurface.Image = graphDrawing.Image;
            };

            commandsManager.Execute(command);

        }

        /// <summary>
        /// Opens color dialog to select new arcs color
        /// </summary>
        private void ArcsColorDialogOpen_Click(object sender, EventArgs e)
        {
            if (GraphStyleColorDialog.ShowDialog() == DialogResult.Cancel) return;

            var command = new ChangeColorCommand(graphDrawing, typeof(Arc),
                graphDrawing.ArcsColor, GraphStyleColorDialog.Color);

            command.Executed += (s, ea) =>
            {
                ArcsColorPanel.BackColor = graphDrawing.ArcsColor;
                graphDrawing.DrawTheWholeGraph(digraph);
                DrawingSurface.Image = graphDrawing.Image;
            };

            commandsManager.Execute(command);
        }

        #region behavior

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

        #endregion

        private bool isCommand;

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (isOnMovement) return;
            isCommand = true;
            commandsManager.Undo();
            graphDrawing.DrawTheWholeGraph(digraph);
            DrawingSurface.Image = graphDrawing.Image;
            isCommand = false;
        }
        private void RedoButton_Click(object sender, EventArgs e)
        {
            if (isOnMovement) return;
            isCommand = true;
            commandsManager.Redo();
            graphDrawing.DrawTheWholeGraph(digraph);
            DrawingSurface.Image = graphDrawing.Image;
            isCommand = false;
        }
    }
}
