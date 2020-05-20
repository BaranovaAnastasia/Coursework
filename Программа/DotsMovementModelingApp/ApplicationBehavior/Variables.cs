using System;
using DotsMovementModelingAppLib;
using System.Windows.Forms;
using DotsMovementModelingAppLib.Modeling;
using DotsMovementModelingAppLib.Commands;

namespace DotsMovementModelingApp
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
        private Digraph digraph = new Digraph();

        /// <summary>
        /// Commands manager providing udo redo functions
        /// </summary>
        private CommandsManager commandsManager = new CommandsManager();

        // Indices of the vertices selected for edge drawing
        private int vStart = -1; private int vEnd = -1;
        // Indicator showing whether the mouse button is pressed
        private bool isPressed;
        // Index of the moving vertex
        private int movedVertexIndex = -1;
        // Moving vertex itself
        private Vertex movedVertex;
        // Time during which the movement occurred
        private DateTime ticks;

        /// <summary>
        /// Models dots movement on a digraph
        /// </summary>
        private MovementModeling movement;

        /// <summary>
        /// Shows if the program is currently modeling the movement
        /// </summary>
        private bool isOnMovement;

        /// <summary>
        /// Random values generator
        /// </summary>
        private static readonly Random Rnd = new Random();

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

        /// <summary>
        /// X-axis offset
        /// </summary>
        private int xCoefficient;
        /// <summary>
        /// Y-axis offset
        /// </summary>
        private int yCoefficient;
        /// <summary>
        /// Resize coefficient
        /// </summary>
        private double resizeCoefficient = 1;
        /// <summary>
        /// Vertices radius
        /// </summary>
        private int radius = 8;

        private bool radiusChanged;

        #region File dialods
        private static SaveFileDialog SaveFileDialogForDataSaving() =>
            new SaveFileDialog()
            {
                FileName = "DigraphData",
                DefaultExt = ".dgmm",
                Filter = @"Digraph data files (.dgmm)|*.dgmm"
            };

        private static SaveFileDialog SaveFileDialogForImageSaving() =>
            new SaveFileDialog()
            {
                FileName = "GraphImage",
                DefaultExt = ".jpg",
                Filter = @"JPG Image (.jpg)|*.jpg"
            };

        private static SaveFileDialog SaveFileDialogForGifSaving() =>
            new SaveFileDialog()
            {
                FileName = "Movement",
                DefaultExt = ".gif",
                Filter = @"Gif Image (.gif)|*.gif",
            };

        private static FolderBrowserDialog FolderBrowserDialogForGraphSaving() =>
            new FolderBrowserDialog() { SelectedPath = @"Digraph" };

        private static OpenFileDialog DigraphOpenFileDialog() =>
            new OpenFileDialog()
            {
                DefaultExt = ".dgmm",
                Filter = @"Digraph data files (.dgmm)|*.dgmm"
            };

        #endregion
    }
}
