using System;
using System.Windows.Forms;
using ApplicationClasses;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
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
            catch (Exception)
            {
                ArcLength.Text = "Error";
            }

        }

        /// <summary>
        /// Changes the length of the arc selected in a ComboBox to a value in a TextBox
        /// </summary>
        private void OkWeight_Click(object sender, EventArgs e)
        {
            ArcLength.Text = ArcLength.Text.Trim(' ');
            if (!ApplicationMethods.IsANumber(ArcLength.Text, out double length) || length <= 0)
            {
                MessageBox.Show("Invalid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(ArcName.Text))
            {
                MessageBox.Show("Please, select an edge", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedArc = ArcName.Items.IndexOf(ArcName.Text);
            if (selectedArc != -1)
            {
                Digraph.Arcs[selectedArc] =
                    new Arc(Digraph.Arcs[selectedArc].StartVertex, Digraph.Arcs[selectedArc].EndVertex, length);
                GridAdjacencyMatrix[Digraph.Arcs[selectedArc].EndVertex, Digraph.Arcs[selectedArc].StartVertex].Value = length;
                return;
            }
            MessageBox.Show("The edge doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GridThresholds_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (int.TryParse(GridThresholds[e.ColumnIndex, e.RowIndex].Value.ToString(), out int th) && th >= 1)
                Digraph.Thresholds[e.RowIndex] = th;
            else
                GridThresholds[e.ColumnIndex, e.RowIndex].Value = Digraph.Thresholds[e.RowIndex];
        }

        private void GridRefractoryPeriods_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (int.TryParse(GridRefractoryPeriods[e.ColumnIndex, e.RowIndex].Value.ToString(), out var p) && p >= 0)
                Digraph.RefractoryPeriods[e.RowIndex] = p;
            else
                GridRefractoryPeriods[e.ColumnIndex, e.RowIndex].Value = Digraph.RefractoryPeriods[e.RowIndex];
        }

        private void GridInitialState_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (int.TryParse(GridInitialState[e.ColumnIndex, e.RowIndex].Value.ToString(), out var s) && s >= 0)
                Digraph.State[e.RowIndex] = s;
            else
                GridInitialState[e.ColumnIndex, e.RowIndex].Value = Digraph.State[e.RowIndex];

            if (SandpileTypeCheckBox.Checked)
            {
                graphDrawing.DrawTheWholeGraphSandpile(Digraph);
                DrawingSurface.Image = graphDrawing.Image;
            }
        }
    }
}
