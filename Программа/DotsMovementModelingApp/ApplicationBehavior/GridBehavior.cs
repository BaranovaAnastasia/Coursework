using DotsMovementModelingAppLib.Commands;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DotsMovementModelingApp
{
    public partial class MainWindow
    {
        #region Adjacency and arcs

        private void GridAdjacencyMatrix_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (var arc in digraph.Arcs)
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

            var selectedArc = ArcName.Items.IndexOf(ArcName.Text);
            if (selectedArc == -1)
            {
                MessageBox.Show(@"The edge doesn't exist", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var length = new MathParserTK.MathParser().Parse(ArcLength.Text);
                var command = new ChangeArcLengthCommand(digraph, selectedArc, digraph.Arcs[selectedArc].Length, length);
                command.Executed += (s, ea) => GridAdjacencyMatrix[digraph.Arcs[selectedArc].EndVertex, digraph.Arcs[selectedArc].StartVertex].Value = s;
                commandsManager.Execute(command);
                ArcLength.Text = length.ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                MessageBox.Show(@"Invalid value. Make sure the input value is greater than zero and is the correct mathematical expression.",
                    @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        GridParameters[e.ColumnIndex, e.RowIndex].Value = digraph.Thresholds[e.RowIndex];
                        return;
                    }
                    digraph.Thresholds[e.RowIndex] = value;
                    break;
                case 1:
                    if (!isValid)
                    {
                        GridParameters[e.ColumnIndex, e.RowIndex].Value = digraph.RefractoryPeriods[e.RowIndex];
                        return;
                    }
                    digraph.RefractoryPeriods[e.RowIndex] = value;
                    break;
                case 2:
                    if (!isValid)
                    {
                        GridParameters[e.ColumnIndex, e.RowIndex].Value = digraph.State[e.RowIndex];
                        return;
                    }
                    digraph.State[e.RowIndex] = value;
                    break;
            }

            GridParameters.CellValueChanged -= GridParameters_CellValueChanged;
            GridParameters[e.ColumnIndex, e.RowIndex].Value = value;
            GridParameters.CellValueChanged += GridParameters_CellValueChanged;
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


        #region Adding and removing vertices 

        /// <summary>
        /// Adds a new line and a row to Adjacency Matrix in DataGridView
        /// </summary>
        private void AddVertexToGridAdjacencyMatrix(int index)
        {
            var column = new DataGridViewColumn
            {
                Name = string.Empty,
                HeaderText = (index + 1).ToString(),
                FillWeight = 1,
                Width = 35,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = new DataGridViewTextBoxCell()
            };
            GridAdjacencyMatrix.Columns.Insert(index, column);
            GridAdjacencyMatrix.Rows.Insert(index);

            for (int i = 0; i < digraph.Vertices.Count; i++)
            {
                GridAdjacencyMatrix[index, i].Value = 0;
                GridAdjacencyMatrix[i, index].Value = 0;
                GridAdjacencyMatrix.Columns[i].HeaderCell.Value = (i + 1).ToString();
                GridAdjacencyMatrix.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            GridAdjacencyMatrix.Rows[index].HeaderCell.Value = (index + 1).ToString();
        }

        private void RemoveVertexFromGridAdjacencyMatrix(int index)
        {
            GridAdjacencyMatrix.Columns.RemoveAt(index);
            if (GridAdjacencyMatrix.Columns.Count != 0) GridAdjacencyMatrix.Rows.RemoveAt(index);
            for (int j = index; j < digraph.Vertices.Count; j++)
            {
                GridAdjacencyMatrix.Columns[j].HeaderCell.Value = (j + 1).ToString();
                GridAdjacencyMatrix.Rows[j].HeaderCell.Value = (j + 1).ToString();
            }
        }

        private void AddVertexToGridParameters(int index)
        {
            if (GridParameters.ColumnCount == 0)
            {
                GridParameters.Columns.Add(String.Empty, "th");
                GridParameters.Columns.Add(String.Empty, "p");
                GridParameters.Columns.Add(String.Empty, "s");
                for (int i = 0; i < GridParameters.ColumnCount; ++i)
                {
                    GridParameters.Columns[i].FillWeight = 1;
                    GridParameters.Columns[i].Width = 70;
                    GridParameters.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            GridParameters.Rows.Insert(index);
            GridParameters.Rows[index].HeaderCell.Value = digraph.Vertices.Count.ToString();
            GridParameters[0, index].Value = digraph.Thresholds[index];
            GridParameters[1, index].Value = digraph.RefractoryPeriods[index];
            GridParameters[2, index].Value = digraph.State[index];
        }
        private void RemoveVertexFromGridParameters(int index)
        {
            GridParameters.Rows.RemoveAt(index);
            for (int j = index; j < digraph.Vertices.Count; j++)
                GridParameters.Rows[j].HeaderCell.Value = (j + 1).ToString();
        }

        #endregion
    }
}
