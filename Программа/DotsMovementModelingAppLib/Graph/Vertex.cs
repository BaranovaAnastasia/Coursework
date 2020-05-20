using  System;

namespace ApplicationClasses
{
    /// <summary>
    /// Represents a digraph vertex
    /// </summary>
    [Serializable]
    public struct Vertex
    {
        /// <summary>
        /// X coordinate of the vertex
        /// </summary>
        public int X;
        /// <summary>
        /// Y coordinate of the vertex
        /// </summary>
        public int Y;

        /// <summary>
        /// Initializes a new instance of the Vertex class
        /// </summary>
        /// <param name="x">X coordinate of the vertex</param>
        /// <param name="y">Y coordinate of the vertex</param>
        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
