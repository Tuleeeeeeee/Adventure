using Tuleeeeee.GridSystem;

namespace Tuleeeeee.Pathfinding
{
    public class PathNode
    {
        private Grid<PathNode> grid;
        private int x, y;

        public int gCost, hCost, fCost;
        private bool isWalkable;


        public int Y
        {
            get => y;
            set => y = value;
        }

        public int X
        {
            get => x;
            set => x = value;
        }

        public bool IsWalkable
        {
            get => isWalkable;
            set => isWalkable = value;
        }

        public void SetIsWalkable(bool isWalkable)
        {
            this.isWalkable = isWalkable;
            grid.TriggerGridObjectChanged(x,y);
        }

        public PathNode cameFromNode;

        public PathNode(Grid<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            isWalkable = true;
        }


        public override string ToString()
        {
            return $"{x}, {y}";
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
    }
}