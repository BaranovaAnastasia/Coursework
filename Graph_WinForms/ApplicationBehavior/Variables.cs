using System;
using ApplicationClasses;
using System.Windows.Forms;
using ApplicationClasses.Modeling;

namespace Graph_WinForms
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// GraphDrawing instance containing methods for digraph drawing
        /// </summary>
        readonly GraphDrawing graphDrawing;

        /// <summary>
        /// Digraph instance with lists of vertices and arcs
        /// </summary>
        Digraph Digraph = new Digraph();

        /// <summary>
        /// SaveFileDialog instance for digraph data saving
        /// </summary>
        readonly SaveFileDialog saveDataDialog = new SaveFileDialog();
        /// <summary>
        /// SaveFileDialog instance for digraph image saving
        /// </summary>
        readonly SaveFileDialog saveImageDialog = new SaveFileDialog();
        /// <summary>
        /// FolderBrowserDialog instance for digraph saving as data and image files folder
        /// </summary>
        readonly FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        /// <summary>
        /// OpenFileDialog instance for opening .digraph files
        /// </summary>
        readonly OpenFileDialog openDialog = new OpenFileDialog();
        /// <summary>
        /// SaveFileDialog instance for saving gif files
        /// </summary>
        readonly SaveFileDialog saveGifDialog = new SaveFileDialog();


        // Indices of the vertices selected for edge drawing
        int vStart = -1; int vEnd = -1;
        // Indicator showing whether the mouse button is pressed
        bool IsPressed = false;
        // Index of the moving vertex
        int MovingVertexIndex = -1;
        // Moving vertex itself
        Vertex MovingVetrex;
        // Time during which the movement occurred
        DateTime Ticks;

        // Number of vertices of random graph
        internal static int chosenNumber = -1;

        /// <summary>
        /// Models dots movenemt on a digraph
        /// </summary>
        private MovementModeling movement = null;

        /// <summary>
        /// Shows if the program is currently modeling the movement
        /// </summary>
        private bool isOnMovement;
    }
}
