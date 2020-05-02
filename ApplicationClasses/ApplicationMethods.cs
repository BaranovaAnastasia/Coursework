using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Data;
using OfficeOpenXml.FormulaParsing.ExcelUtilities;

namespace ApplicationClasses
{
    public static class ApplicationMethods
    {
        /// <summary>
        /// Prints Adjacency Matrix in DataGridView
        /// </summary>
        public static void PrintGraphAdjacencyInfo(double[,] adjacencyMatrix, DataGridView dataGridView)
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

        public static void PrintGraphThresholds(Digraph digraph, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(String.Empty, "th");
            dataGridView.Columns[0].FillWeight = 1;
            dataGridView.Columns[0].Width = 95;
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            if (digraph.Vertices.Count <= 0) return;

            dataGridView.Rows.Add(digraph.Vertices.Count);
            for (int i = 0; i < digraph.Vertices.Count; i++)
            {
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataGridView.Rows[i].Height = 30;
                dataGridView[0, i].Value = digraph.Thresholds[i];
            }
        }

        public static void PrintGraphRefractoryPeriods(Digraph digraph, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(String.Empty, "p, ms");
            dataGridView.Columns[0].FillWeight = 1;
            dataGridView.Columns[0].Width = 95;
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            if (digraph.Vertices.Count <= 0) return;

            dataGridView.Rows.Add(digraph.Vertices.Count);
            for (int i = 0; i < digraph.Vertices.Count; i++)
            {
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataGridView.Rows[i].Height = 30;
                dataGridView[0, i].Value = digraph.RefractoryPeriods[i];
            }
        }

        public static void PrintGraphInitialState(Digraph digraph, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(String.Empty, "s");
            dataGridView.Columns[0].FillWeight = 1;
            dataGridView.Columns[0].Width = 95;
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            if (digraph.Vertices.Count <= 0) return;

            dataGridView.Rows.Add(digraph.Vertices.Count);
            for (int i = 0; i < digraph.Vertices.Count; i++)
            {
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataGridView.Rows[i].Height = 30;
                dataGridView[0, i].Value = digraph.State[i];
            }
        }

        /// <summary>
        /// Random numbers generator
        /// </summary>
        static readonly Random rnd = new Random();



        /// <summary>
        /// Checks if graph is strongly connected
        /// </summary>
        /// <param name="digraph">Digraph</param>
        public static bool IsGraphValid(Digraph digraph)
        {
            if (digraph.Vertices.Count < 3) return false;
            ConnectivityCheck check = new ConnectivityCheck(digraph.Vertices.Count);
            foreach (Arc arc in digraph.Arcs)
                check.AddEdge(arc);
            return check.IsStronglyConnected();
        }
    }
}
