using System;
using System.Windows.Forms;

namespace ApplicationClasses
{
    public static class DigraphBuilding
    {
        #region Vertices and arcs deletion

        /// <summary>
        /// Searches for a digraph vertex at (x, y) to delete
        /// </summary>
        /// <param name="x">X coordinate of search point</param>
        /// <param name="y">Y coordinate of search point</param>
        /// <param name="digraph">Digraph among the vertices of which to search</param>
        /// <param name="R">Vertex radius</param>
        /// <param name="index">Index of the found vertex</param>
        /// <returns>true if the vertex was found, false otherwise</returns>
        public static bool TryToDeleteVertexAt(int x, int y, Digraph digraph, float R, out int index)
        {
            for (var i = 0; i < digraph.Vertices.Count; i++)
            {
                if (Math.Pow(digraph.Vertices[i].X - x, 2) + Math.Pow(digraph.Vertices[i].Y - y, 2) > R * R)
                    continue;
                digraph.RemoveVertex(i);
                index = i;
                return true;
            }
            index = -1;
            return false;
        }

        /// <summary>
        /// Searches for a digraph arc at (x, y) and removes it
        /// </summary>
        /// <param name="x">X coordinate of search point</param>
        /// <param name="y">Y coordinate of search point</param>
        /// <param name="digraph">Digraph among the vertices of which to search</param>
        /// <param name="deletedArc">Found arc</param>
        /// <returns>true if the arc was found, false otherwise</returns>
        public static bool TryToDeleteArcAt(int x, int y, Digraph digraph, out Arc deletedArc)
        {
            int selectedArc = FindSelectedArc(x, y, digraph);
            if (selectedArc != -1)
            {
                deletedArc = digraph.Arcs[selectedArc];
                digraph.Arcs.RemoveAt(selectedArc);
                return true;
            }
            deletedArc = new Arc();
            return false;
        }

        /// <summary>
        /// Searches for a digraph arc at (x, y)
        /// </summary>
        /// <returns>Found Arc index</returns>
        private static int FindSelectedArc(int x, int y, Digraph digraph)
        {
            for (int i = 0; i < digraph.Arcs.Count; ++i)
            {
                if (IsArcSelected(x, y, digraph.Vertices[digraph.Arcs[i].StartVertex].X,
                    digraph.Vertices[digraph.Arcs[i].StartVertex].Y,
                    digraph.Vertices[digraph.Arcs[i].EndVertex].X,
                    digraph.Vertices[digraph.Arcs[i].EndVertex].Y))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Checks if the arc is selected for deletion
        /// </summary>
        private static bool IsArcSelected(int x, int y, int startVertexX, int startVertexY, int endVertexX, int endVertexY)
        {
            return (Math.Abs((x - startVertexX) * (endVertexY - startVertexY) - (y - startVertexY) * (endVertexX - startVertexX)) <= 350 &&
                (x > Math.Min(startVertexX, endVertexX) && x < Math.Max(startVertexX, endVertexX) ||
                y > Math.Min(startVertexY, endVertexY) && y < Math.Max(startVertexY, endVertexY)));
        }

        #endregion
    }

    public static class DigraphInformationDemonstration
    {
        #region DataGridView information display

        /// <summary>
        /// Shows Adjacency Matrix in DataGridView
        /// </summary>
        public static void DisplayGraphAdjacencyInfo(double[,] adjacencyMatrix, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            for (int i = 0; i < adjacencyMatrix.GetLength(1); i++)
            {
                dataGridView.Columns.Add("", (i + 1).ToString());
                dataGridView.Columns[i].FillWeight = 1;
                dataGridView.Columns[i].Width = 35;
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 0) dataGridView.Rows.Add(adjacencyMatrix.GetLength(1));
                for (int j = 0; j < adjacencyMatrix.GetLength(0); j++)
                {
                    dataGridView[i, j].Value = adjacencyMatrix[j, i];
                    dataGridView.Rows[j].HeaderCell.Value = (j + 1).ToString();
                    dataGridView.Rows[j].Height = 30;
                }
            }
        }

        /// <summary>
        /// Shows all the digraph parameters in one DataGridView
        /// </summary>
        public static void DisplayGraphParameters(Digraph digraph, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(String.Empty, "th");
            dataGridView.Columns.Add(String.Empty, "p");
            dataGridView.Columns.Add(String.Empty, "s");
            for(int i = 0; i < dataGridView.ColumnCount; ++i)
            {
                dataGridView.Columns[i].FillWeight = 1;
                dataGridView.Columns[i].Width = 70;
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            if (digraph.Vertices.Count <= 0) return;

            dataGridView.Rows.Add(digraph.Vertices.Count);
            for (int i = 0; i < digraph.Vertices.Count; i++)
            {
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataGridView.Rows[i].Height = 30;
                dataGridView[0, i].Value = digraph.Thresholds[i];
                dataGridView[1, i].Value = digraph.RefractoryPeriods[i];
                dataGridView[2, i].Value = digraph.State[i];
            }
        }

        /// <summary>
        /// Shows sandpile colors palette in DataGridView
        /// </summary>
        public static void DisplaySandpileColors(GraphDrawing graphDrawing, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("colors", "Color");
            dataGridView.Columns[0].FillWeight = 1;
            dataGridView.Columns[0].Width = 70;
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView.Rows.Add(graphDrawing.SandpilePalette.Length);
            for (int i = 0; i < graphDrawing.SandpilePalette.Length; i++)
            {
                dataGridView.Rows[i].HeaderCell.Value = i.ToString();
                dataGridView.Rows[i].Height = 25;
                dataGridView.Rows[i].Cells[0].Style.BackColor = graphDrawing.SandpilePalette[i];
            }

            dataGridView.Visible = true;
        }

        #endregion
    }
}
