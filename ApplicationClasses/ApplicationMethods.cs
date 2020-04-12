using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

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

        #region ShitMethodRandomGraph
        /// <summary>
        /// Creates Random Graph
        /// </summary>
        /// <param name="numberOfVertices"></param>
        /// <param name="digraph">Digraph</param>
        /// <param name="width">Drawing surface width</param>
        /// <param name="height">Drawing surface height</param>
        public static void GetRandomGraph(int numberOfVertices, out Digraph digraph, int width, int height)
        {
            /*List<Vertex> Vertices = new List<Vertex>(NumberOfVertices);
            List<Arc>  Edges = new List<Arc>();
            double[,] AdjacencyMatrix = new double[NumberOfVertices, NumberOfVertices];
            // Shows if vertices have any connections
            int[] visited = new int[NumberOfVertices];
            // sheet into cells
            //bool[,] cells = new bool[Width / 40, Height / 40];
            // Filling vertices list
            int x, y, i;
            for (i = 0; i < NumberOfVertices; i++)
            {
                // Looking for a vacant cell
                /*do
                {
                    x = rnd.Next(0, cells.GetLength(0));
                    y = rnd.Next(0, cells.GetLength(1));
                } while (cells[x, y]);
                Vertices.Add(new Vertex(40 * x + R + rnd.Next(0, 10), Height - 40 * y - 2 * R - rnd.Next(5, 15)));
                cells[x, y] = true;* /
                x = rnd.Next(2 * GraphDrawing.R, Width - 2 * GraphDrawing.R);
                y = rnd.Next(2 * GraphDrawing.R, Height - 2 * GraphDrawing.R);
                Vertices.Add(new Vertex(x, y));
            }
            i = 0;
            int j = 0;
            int AllVisited = 0;
            while (AllVisited != Vertices.Count)
            {
                if (visited[i] == 0) AllVisited++;
                visited[i]++;
                do
                {
                    j = rnd.Next(0, Vertices.Count - 1);
                    j = j >= i ? j + 1 : j;
                } while (visited[j] >= 2 || AdjacencyMatrix[i, j] != 0);

                Edges.Add(new Arc(i, j, rnd.Next(0, 10) + rnd.NextDouble() + 0.01));
                AdjacencyMatrix[i, j] = Edges[Edges.Count - 1].Length;
                i = j;
            }
            Edges.Add(new Arc(j, 0, rnd.Next(0, 10) + rnd.NextDouble() + 0.01));*/
            digraph = new Digraph();
        }
        #endregion

        /// <summary>
        /// Defines whether the string is a number or not
        /// </summary>
        public static bool IsANumber(string numStr, out double num)
        {
            if (double.TryParse(numStr, out num)) return true;
            // Checking if it's a math constant
            if (numStr == "pi" || numStr == "Pi" || numStr == "PI")
            {
                num = Math.PI;
                return true;
            }
            if (numStr == "e" || numStr == "E" || numStr == "Exp" || numStr == "exp")
            {
                num = Math.E;
                return true;
            }
            // Checking if it's a root
            if (numStr.Length >= 7 && (numStr.Substring(0, 4) == "sqrt" || numStr.Substring(0, 4) == "Sqrt") &&
                numStr[4] == '(' && numStr[numStr.Length - 1] == ')' &&
                IsANumber(numStr.Substring(5, numStr.Length - 6), out double insideNum))
            {
                num = Math.Sqrt(insideNum);
                return true;
            }
            // Checking if it's a degree
            if (numStr.Contains("^") && IsANumber(numStr.Substring(numStr.LastIndexOf('^') + 1), out insideNum) &&
                IsANumber(numStr.Substring(0, numStr.LastIndexOf('^')), out double insideNum1))
            {
                num = Math.Pow(insideNum1, insideNum);
                return true;
            }
            // Multiplication
            if (numStr.Contains("*") && IsANumber(numStr.Substring(numStr.IndexOf('*') + 1), out insideNum) &&
                IsANumber(numStr.Substring(0, numStr.IndexOf('*')), out insideNum1))
            {
                num = insideNum * insideNum1;
                return true;
            }
            // Division
            if (numStr.Contains("/") && IsANumber(numStr.Substring(numStr.IndexOf('/') + 1), out insideNum) &&
                IsANumber(numStr.Substring(0, numStr.IndexOf('/')), out insideNum1))
            {
                num = insideNum * insideNum1;
                return true;
            }
            // Addition
            if (numStr.Contains("+") && IsANumber(numStr.Substring(numStr.IndexOf('+') + 1), out insideNum) &&
                IsANumber(numStr.Substring(0, numStr.IndexOf('+')), out insideNum1))
            {
                num = insideNum + insideNum1;
                return true;
            }
            // Subtraction
            if (numStr.Contains("-") && IsANumber(numStr.Substring(numStr.IndexOf('-') + 1), out insideNum) &&
                IsANumber(numStr.Substring(0, numStr.IndexOf('-')), out insideNum1))
            {
                num = insideNum - insideNum1;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if graph is strongly connected
        /// </summary>
        /// <param name="digraph">Digraph</param>
        public static bool IsGraphValid(Digraph digraph)
        {
            ConnectivityCheck check = new ConnectivityCheck(digraph.Vertices.Count);
            foreach (Arc arc in digraph.Arcs)
                check.AddEdge(arc);
            return check.IsStronglyConnected();
        }

        public static void TryToDeleteVertexAt(int x, int y, Digraph digraph, out int index)
        {
            for (var i = 0; i < digraph.Vertices.Count; i++)
            {
                if (Math.Pow(digraph.Vertices[i].X - x, 2) + Math.Pow(digraph.Vertices[i].Y - y, 2)
                      > GraphDrawing.R * GraphDrawing.R)
                      continue;
                digraph.RemoveVertex(i);
                index = i;
                return;
            }
            index = -1;
        }

        public static void TryToDeleteArcAt(int x, int y, Digraph digraph, out int startVertex, out int endVertex)
        {
            int selectedArc = FindSelectedArc(x, y, digraph);
            if (selectedArc != -1)
            {
                startVertex = digraph.Arcs[selectedArc].StartVertex;
                endVertex = digraph.Arcs[selectedArc].EndVertex;
                digraph.Arcs.RemoveAt(selectedArc);
                return;
            }
            startVertex = -1;
            endVertex = -1;
        }


        static int FindSelectedArc(int x, int y, Digraph digraph)
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

        static bool IsArcSelected(int x, int y, int startVertexX, int startVertexY, int endVertexX, int endVertexY)
        {
            return (Math.Abs((x - startVertexX) * (endVertexY - startVertexY) - (y - startVertexY) * (endVertexX - startVertexX)) <= 350 &&
                (x > Math.Min(startVertexX, endVertexX) && x < Math.Max(startVertexX, endVertexX) ||
                y > Math.Min(startVertexY, endVertexY) && y < Math.Max(startVertexY, endVertexY)));
        }
    }
}
