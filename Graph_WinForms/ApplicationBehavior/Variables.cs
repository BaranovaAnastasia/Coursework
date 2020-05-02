using System;
using ApplicationClasses;
using System.Windows.Forms;
using ApplicationClasses.Modeling;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
        /// <summary>
        /// GraphDrawing instance containing methods for digraph drawing
        /// </summary>
        private readonly GraphDrawing graphDrawing;

        /// <summary>
        /// Digraph instance with lists of vertices and arcs
        /// </summary>
        private Digraph Digraph = new Digraph();

        // Indices of the vertices selected for edge drawing
        private int vStart = -1; private int vEnd = -1;
        // Indicator showing whether the mouse button is pressed
        private bool IsPressed = false;
        // Index of the moving vertex
        private int MovingVertexIndex = -1;
        // Moving vertex itself
        private Vertex MovingVetrex;
        // Time during which the movement occurred
        private DateTime Ticks;

        /// <summary>
        /// Models dots movenemt on a digraph
        /// </summary>
        private MovementModeling movement = null;

        /// <summary>
        /// Shows if the program is currently modeling the movement
        /// </summary>
        private bool isOnMovement;

        /// <summary>
        /// Random values generator
        /// </summary>
        private static readonly Random rnd = new Random();

        /// <summary>
        /// Information about the application
        /// </summary>
        private static readonly string AboutApp =
            "The application developed as a part of a coursework" + Environment.NewLine +
            Environment.NewLine +
            Environment.NewLine + "Developed by Baranova Anastasia Andreevna, BSE196." +
            Environment.NewLine +
            "Supervisor: Vsevolod L. Chernyshev, Associate Professor, Big Data and Information Retrieval School, Faculty of Computer Science." +
            Environment.NewLine +
            Environment.NewLine + "Higher School of Economics, Moscow, 2020";


        #region File dialods
        private static SaveFileDialog SaveFileDialogForDataSaving() =>
            new SaveFileDialog()
            {
                FileName = "GraphData",
                DefaultExt = ".digraph",
                Filter = "Digraph data files (.digraph)|*.digraph"
            };

        private static SaveFileDialog SaveFileDialogForImageSaving() =>
            new SaveFileDialog()
            {
                FileName = "GraphImage",
                DefaultExt = ".jpg",
                Filter = "JPG Image (.jpg)|*.jpg"
            };

        private static SaveFileDialog SaveFileDialogForGifSaving() =>
            new SaveFileDialog()
            {
                FileName = "Movement",
                DefaultExt = ".gif",
                Filter = "Gif Image (.gif)|*.gif",
            };

        private static FolderBrowserDialog FolderBrowserDialogForGraphSaving() =>
            new FolderBrowserDialog() { SelectedPath = "Digraph" };

        private static OpenFileDialog DigraphOpenFileDialog() =>
            new OpenFileDialog()
            {
                DefaultExt = ".digraph",
                Filter = "Digraph data files (.digraph)|*.digraph"
            };

        #endregion
    }
}
