using System.Collections.Generic;

namespace Bomberman.AI
{
    public class PathNode
    {
        private List<PathNode> grid;
        private int x;
        private int y;

        public int gCost;
        public int hCost;
        public int fCost;

        public PathNode cameFromNode;

        public PathNode(List<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
    }
}