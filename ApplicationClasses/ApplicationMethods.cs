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
        /// Creates Random Graph
        /// </summary>
        /// <param name="numberOfVertices"></param>
        /// <param name="digraph">Digraph</param>
        /// <param name="width">Drawing surface width</param>
        /// <param name="height">Drawing surface height</param>
        public static void GetRandomGraph(int numberOfVertices, out Digraph digraph, int width, int height)
        {
            digraph = new Digraph();
            bool[] visitedV = new bool[numberOfVertices];
            for (int i = 0; i < numberOfVertices; i++)
            {
                int th = rnd.Next(1, 5);
                int p = rnd.Next(1, 5);
                int s = rnd.Next(0, 2 * th);
                digraph.AddVertex(new Vertex(rnd.Next(10, width - 10), rnd.Next(10, height - 10)), th, p, s);
                visitedV[i] = i == 0;
            }

            int start = 0;
            int end;
            for (int i = 0; i < numberOfVertices - 1; i++)
            {
                do { } while ((end = rnd.Next(1, visitedV.Length)) == start || visitedV[end]);
                digraph.AddArc(new Arc(start, end, rnd.Next(3, 11) + rnd.NextDouble()));
                visitedV[end] = true;
                start = end;
            }
            digraph.AddArc(new Arc(start, 0, rnd.Next(3, 11) + rnd.NextDouble()));
        }

        /// <summary>
        /// Defines whether the string is a number or not
        /// </summary>
        public static bool IsANumber(string numStr, out double num)
        {
            //((4 - 2) ^ 2 + 2) ^ 2 - (4 + 4) - 3
            string test_s_expression = "5 + sin(0.1 * 8) - cos(0.5 - (3/4))";
            ExpressionEvaluator EE = new ExpressionEvaluator();
            return EE.TryConvertToDouble(numStr, out num);

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
            double insideNum, insideNum1;

            if (numStr.Contains("(") && numStr.Contains(")"))
            {
                int index = numStr.LastIndexOf(")");
                string insideStr = numStr.Substring(numStr.IndexOf('(') + 1, index - numStr.IndexOf('(') - 1);
                while (insideStr.Contains("(") && insideStr.Contains(")")
                    && insideStr.LastIndexOf(")") < insideStr.LastIndexOf("("))
                {
                    index = insideStr.LastIndexOf(")") + numStr.IndexOf('(') + 1;
                    insideStr = numStr.Substring(numStr.IndexOf('(') + 1, index - numStr.IndexOf('(') - 1);
                    int k = 0;
                }

                if (IsANumber(insideStr, out insideNum))
                {
                    int len = index - numStr.IndexOf("(") + 1;
                    int strt = numStr.IndexOf("(");
                    numStr = numStr.Replace(numStr.Substring(strt, len), insideNum.ToString());
                    if (IsANumber(numStr, out num))
                        return true;
                }
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
                num = insideNum1 - insideNum;
                return true;
            }
            // Division
            if (numStr.Contains("/") && IsANumber(numStr.Substring(numStr.IndexOf('/') + 1), out insideNum) &&
                IsANumber(numStr.Substring(0, numStr.IndexOf('/')), out insideNum1))
            {
                num = insideNum1 / insideNum;
                return true;
            }
            // Multiplication
            if (numStr.Contains("*") && IsANumber(numStr.Substring(numStr.IndexOf('*') + 1), out insideNum) &&
                IsANumber(numStr.Substring(0, numStr.IndexOf('*')), out insideNum1))
            {
                num = insideNum * insideNum1;
                return true;
            }
            // Checking if it's a root
            if (numStr.Length >= 7 && (numStr.Substring(0, 4) == "sqrt" || numStr.Substring(0, 4) == "Sqrt") &&
                numStr[4] == '(' && numStr[numStr.Length - 1] == ')' &&
                IsANumber(numStr.Substring(5, numStr.Length - 6), out insideNum))
            {
                num = Math.Sqrt(insideNum);
                return true;
            }
            // Checking if it's a degree
            if (numStr.Contains("^") && IsANumber(numStr.Substring(numStr.LastIndexOf('^') + 1), out insideNum) &&
                IsANumber(numStr.Substring(0, numStr.LastIndexOf('^')), out insideNum1))
            {
                num = Math.Pow(insideNum1, insideNum);
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
            if (digraph.Vertices.Count < 3) return false;
            ConnectivityCheck check = new ConnectivityCheck(digraph.Vertices.Count);
            foreach (Arc arc in digraph.Arcs)
                check.AddEdge(arc);
            return check.IsStronglyConnected();
        }
    }
}
