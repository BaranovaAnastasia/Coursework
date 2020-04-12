﻿using System;
using ApplicationClasses;
using System.Drawing;
using System.Windows.Forms;

namespace Graph_WinForms
{
    public partial class GraphBuilder : Form
    {
        /// <summary>
        /// GraphDrawing instance containing methods for digraph drawing
        /// </summary>
        readonly GraphDrawing graphDrawing;

        /// <summary>
        /// Digraph instance with lists of vertices and arcs
        /// </summary>
        Digraph Digraph = new Digraph();

        readonly SaveFileDialog savingDialog = new SaveFileDialog(); // In order to save the graph
        readonly OpenFileDialog openDialog = new OpenFileDialog();   // In order to open the graph from file


        // Indices of the vertices selected for edge drawing
        int vStart = -1; int vEnd = -1;
        // Indicator showing whether the mouse button is pressed
        bool IsPressed = false;
        // Index of the moving vertex
        int MovingVertexIndex = -1;
        // Moving vertex itself
        Vertex MovingVetrex;
        // Point where moving of a vertex or the whole graph began
        Point p;
        // Time during which the movement occurred
        DateTime Ticks;

        // Number of vertices of random graph
        internal static int chosenNumber = -1;
    }
}
