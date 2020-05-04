using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
        #region Adjacency and arcs

        private void GridAdjacencyMatrix_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            for (var i = 0; i < Digraph.Arcs.Count; i++)
            {
                if (Digraph.Arcs[i].StartVertex != e.RowIndex || Digraph.Arcs[i].EndVertex != e.ColumnIndex)
                    continue;
                ArcName.SelectedItem = ArcName.Items[i];
                ArcLength.Text = Digraph.Arcs[i].Length.ToString();
                return;
            }
            GridAdjacencyMatrix.ClearSelection();
            ArcName.Text = (e.RowIndex + 1) + "-" + (e.ColumnIndex + 1);
            ArcLength.Text = "Error";
        }

        private void ArcLength_TextChanged(object sender, EventArgs e) =>
            ArcLength.ReadOnly = ArcLength.Text == "Error";

        private void ArcName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int start = int.Parse(ArcName.Text[0].ToString()) - 1;
                int end = int.Parse(ArcName.Text[2].ToString()) - 1;
                GridAdjacencyMatrix[end, start].Selected = true;
                if (GridAdjacencyMatrix[end, start].Value.ToString() == "0")
                {
                    ArcLength.Text = "Error";
                    GridAdjacencyMatrix.ClearSelection();
                }
                else ArcLength.Text = GridAdjacencyMatrix[end, start].Value.ToString();
            }
            catch (Exception) { ArcLength.Text = "Error"; }
        }

        /// <summary>
        /// Changes the length of the arc selected in a ComboBox to a value in a TextBox
        /// </summary>
        private void OkWeight_Click(object sender, EventArgs e)
        {
            ArcLength.Text = ArcLength.Text.Trim(' ');

            if (string.IsNullOrEmpty(ArcName.Text))
            {
                MessageBox.Show("Please, select an edge", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedArc = ArcName.Items.IndexOf(ArcName.Text);
            if (selectedArc == -1)
            {
                MessageBox.Show("The edge doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                double length = new MathParserTK.MathParser().Parse(ArcLength.Text);
                Digraph.Arcs[selectedArc] =
                    new Arc(Digraph.Arcs[selectedArc].StartVertex, Digraph.Arcs[selectedArc].EndVertex, length);
                GridAdjacencyMatrix[Digraph.Arcs[selectedArc].EndVertex, Digraph.Arcs[selectedArc].StartVertex].Value = length;
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void GridParameters_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (int.TryParse(GridParameters[e.ColumnIndex, e.RowIndex].Value.ToString(), out int value) && value >= 1)
            {
                switch (e.ColumnIndex)
                {
                    case 0: Digraph.Thresholds[e.RowIndex] = value; break;
                    case 1: Digraph.RefractoryPeriods[e.RowIndex] = value; break;
                    case 2: Digraph.State[e.RowIndex] = value; break;
                }
                GridParameters[e.ColumnIndex, e.RowIndex].Value = value;
            }
            else
                GridParameters[e.ColumnIndex, e.RowIndex].Value = Digraph.Thresholds[e.RowIndex];
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
