using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using GraphClasses.Commands;

namespace CourseworkApp
{
    public partial class MainWindow
    {
        #region Adjacency and arcs

        private void GridAdjacencyMatrix_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (var arc in Digraph.Arcs)
            {
                if (arc.StartVertex != e.RowIndex || arc.EndVertex != e.ColumnIndex)
                    continue;
                ArcName.SelectedIndex = ArcName.Items.IndexOf(arc.ToString());
                ArcLength.Text = arc.Length.ToString(CultureInfo.CurrentCulture);
                return;
            }
            GridAdjacencyMatrix.ClearSelection();
            ArcName.Text = (e.RowIndex + 1) + @"-" + (e.ColumnIndex + 1);
            ArcLength.Text = @"Error";
        }

        private void ArcLength_TextChanged(object sender, EventArgs e) =>
            ArcLength.ReadOnly = ArcLength.Text == @"Error";

        private void ArcName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int start = int.Parse(ArcName.Text[0].ToString()) - 1;
                int end = int.Parse(ArcName.Text[2].ToString()) - 1;
                GridAdjacencyMatrix[end, start].Selected = true;
                if (GridAdjacencyMatrix[end, start].Value.ToString() == "0")
                {
                    ArcLength.Text = @"Error";
                    GridAdjacencyMatrix.ClearSelection();
                }
                else ArcLength.Text = GridAdjacencyMatrix[end, start].Value.ToString();
            }
            catch (Exception) { ArcLength.Text = @"Error"; }
        }

        /// <summary>
        /// Changes the length of the arc selected in a ComboBox to a value in a TextBox
        /// </summary>
        private void OkWeight_Click(object sender, EventArgs e)
        {
            ArcLength.Text = ArcLength.Text.Trim(' ');

            if (string.IsNullOrEmpty(ArcName.Text))
            {
                MessageBox.Show(@"Please, select an edge", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedArc = ArcName.Items.IndexOf(ArcName.Text);
            if (selectedArc == -1)
            {
                MessageBox.Show(@"The edge doesn't exist", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                double length = new MathParserTK.MathParser().Parse(ArcLength.Text);
                var command = new ChangeArcLengthCommand(Digraph, selectedArc, Digraph.Arcs[selectedArc].Length, length);
                command.Executed += (s, ea) => GridAdjacencyMatrix[Digraph.Arcs[selectedArc].EndVertex, Digraph.Arcs[selectedArc].StartVertex].Value = s;
                commandsManager.Execute(command);
            }
            catch (Exception)
            {
                MessageBox.Show(@"Invalid value", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void GridParameters_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            bool isValid = int.TryParse(GridParameters[e.ColumnIndex, e.RowIndex].Value.ToString(), out int value)
                           && value >= 0;

            switch (e.ColumnIndex)
            {
                case 0:
                    if (!isValid || value < 1)
                    {
                        GridParameters[e.ColumnIndex, e.RowIndex].Value = Digraph.Thresholds[e.RowIndex];
                        return;
                    }
                    Digraph.Thresholds[e.RowIndex] = value;
                    break;
                case 1:
                    if (!isValid)
                    {
                        GridParameters[e.ColumnIndex, e.RowIndex].Value = Digraph.RefractoryPeriods[e.RowIndex];
                        return;
                    }
                    Digraph.RefractoryPeriods[e.RowIndex] = value;
                    break;
                case 2:
                    if (!isValid)
                    {
                        GridParameters[e.ColumnIndex, e.RowIndex].Value = Digraph.State[e.RowIndex];
                        return;
                    }
                    Digraph.State[e.RowIndex] = value;
                    return;
            }
            GridParameters[e.ColumnIndex, e.RowIndex].Value = value;

        }

        private void GridParameters_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (GridParameters.RowCount == 0) return;
            if ((GridParameters.RowCount + 1) * GridParameters.Rows[0].Height > GridParameters.Height)
                for (int i = 0; i < GridParameters.ColumnCount; i++)
                    GridParameters.Columns[i].Width = 61;
        }

        private void GridParameters_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (GridParameters.RowCount == 0) return;
            if ((GridParameters.RowCount + 1) * GridParameters.Rows[0].Height <= GridParameters.Height)
                for (int i = 0; i < GridParameters.ColumnCount; i++)
                    GridParameters.Columns[i].Width = 70;
        }

        #region Displaying

        private void GridAdjacencyMatrix_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All);
            e.PaintHeader(DataGridViewPaintParts.Background
                          | DataGridViewPaintParts.Border
                          | DataGridViewPaintParts.Focus
                          | DataGridViewPaintParts.SelectionBackground);
            e.Handled = true;

            e.Graphics.DrawString((e.RowIndex + 1).ToString(),
                e.InheritedRowStyle.Font,
                Brushes.Black,
                new PointF(e.RowBounds.X + 5, e.RowBounds.Y + 2));
        }

        private void GridParameters_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All);
            e.PaintHeader(DataGridViewPaintParts.Background
                          | DataGridViewPaintParts.Border
                          | DataGridViewPaintParts.Focus
                          | DataGridViewPaintParts.SelectionBackground);
            e.Handled = true;

            e.Graphics.DrawString((e.RowIndex + 1).ToString(),
                e.InheritedRowStyle.Font,
                Brushes.Black,
                new PointF(e.RowBounds.X + 3, e.RowBounds.Y + 1));
        }

        private void SandpilePalette_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All);
            e.PaintHeader(DataGridViewPaintParts.Background
                          | DataGridViewPaintParts.Border
                          | DataGridViewPaintParts.Focus
                          | DataGridViewPaintParts.SelectionBackground);
            e.Handled = true;

            e.Graphics.DrawString(e.RowIndex.ToString(),
                e.InheritedRowStyle.Font,
                Brushes.Black,
                new PointF(e.RowBounds.X + 2, e.RowBounds.Y + 2));
        }

        #endregion
    }
}
