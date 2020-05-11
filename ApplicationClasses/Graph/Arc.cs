using System;

namespace ApplicationClasses
{
    /// <summary>
    /// Graph arcs
    /// </summary>
    [Serializable]
    public struct Arc
    {
        /// <summary>
        /// Index of the starting vertex of the Arc
        /// </summary>
        private int startVertex;
        /// <summary>
        /// Index of the ending vertex of the Arc
        /// </summary>
        /// 
        private int endVertex;
        /// <summary>
        /// Length of the arc
        /// </summary>
        private double length;

        /// <summary>
        /// Initializes a new instance of the Arc class
        /// </summary>
        /// <param name="startVertex">Index of the starting vertex of the Arc</param>
        /// <param name="endVertex">Index of the ending vertex of the Arc</param>
        /// <param name="length">Length of the arc</param>
        public Arc(int startVertex, int endVertex, double length = 1)
        {
            if(startVertex == endVertex)
                throw new ArgumentException(nameof(endVertex), "Arc cannot be a loop");
            if (startVertex < 0)
                throw new ArgumentOutOfRangeException(nameof(startVertex), "Index of the vertex was negative");
            if (endVertex < 0)
                throw new ArgumentOutOfRangeException(nameof(endVertex), "Index of the vertex was negative");
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length of the arc should be a positive number");
            this.startVertex = startVertex;
            this.endVertex = endVertex;
            this.length = length;
        }

        /// <summary>
        /// Index of the starting vertex of the Arc
        /// </summary>
        public int StartVertex
        {
            get => startVertex;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Index of the vertex was negative");
                startVertex = value;
            }
        }

        /// <summary>
        /// Index of the ending vertex of the Arc
        /// </summary>
        public int EndVertex
        {
            get => endVertex;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(EndVertex), "Index of the vertex was negative");
                endVertex = value;
            }
        }

        /// <summary>
        /// Length of the arc
        /// </summary>
        public double Length
        {
            get => length;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Length), "Length of the arc should be a positive number");
                length = value;
            }
        }

        public override string ToString() => $"{startVertex + 1}-{endVertex + 1}";
    }
}
