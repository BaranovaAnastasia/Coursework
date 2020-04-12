using System.Drawing;
using System.Windows.Forms;

namespace Graph_WinForms
{
    public partial class GraphBuilder
    {
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

        private void GridThresholds_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
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

        private void GridRefractoryPeriods_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
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

        private void GridInitialState_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
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
    }
}
