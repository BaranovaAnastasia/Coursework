using System;

namespace ApplicationClasses
{
    public static class DigraphBuilding
    {
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
        /// <param name="startVertex">Index of a starting vertex of deleted arc</param>
        /// <param name="endVertex">Index of an ending vertex of deleted arc</param>
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
        /// Checks if the arcif the arc is selected for deletion
        /// </summary>
        private static bool IsArcSelected(int x, int y, int startVertexX, int startVertexY, int endVertexX, int endVertexY)
        {
            return (Math.Abs((x - startVertexX) * (endVertexY - startVertexY) - (y - startVertexY) * (endVertexX - startVertexX)) <= 350 &&
                (x > Math.Min(startVertexX, endVertexX) && x < Math.Max(startVertexX, endVertexX) ||
                y > Math.Min(startVertexY, endVertexY) && y < Math.Max(startVertexY, endVertexY)));
        }
    }
}
