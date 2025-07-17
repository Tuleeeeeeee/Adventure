using System.Collections.Generic;
using Tuleeeeee.GridSystem;
using UnityEngine;

namespace Tuleeeeee.Pathfinding
{
    public class Pathfinding
    {
        public static Pathfinding instance { get; private set; }


        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAl_COST = 14;

        private Grid<PathNode> grid;
        private List<PathNode> openList;
        private List<PathNode> closedList;

        public Pathfinding(int width, int height, float cellSize, Vector3 originPosition)
        {
            instance = this;
            grid = new Grid<PathNode>(width, height, cellSize, originPosition,
                (g, x, y) => new PathNode(g, x, y));

            // Check for obstacles and set walkability
            CheckObstacle(width, height, cellSize);
        }

        private void CheckObstacle(int width, int height, float cellSize)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 worldPosition = grid.GetWorldPosition(x, y);
                    bool isObstacle =
                        Physics2D.OverlapPoint(worldPosition + Vector3.one * (cellSize * 0.5f),
                            LayerMask.GetMask("Object")) != null;
#if UNITY_EDITOR

                    /*DebugDrawSquare(worldPosition + Vector3.one * (cellSize * 0.5f), cellSize * 0.5f, Color.green,
                        100f);*/
#endif
                    grid.GetGridObject(x, y).SetIsWalkable(!isObstacle);
                }
            }
        }

        public void DebugDrawObstacles()
        {
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    PathNode node = grid.GetGridObject(x, y);
                    if (!node.IsWalkable)
                    {
                        Vector3 worldPosition = grid.GetWorldPosition(x, y);
                        DebugDrawSquare(worldPosition + Vector3.one * (grid.CellSize / 2.0f), grid.CellSize,
                            Color.red, 100f);
                    }
                }
            }
        }

        public void DebugDrawSquare(Vector3 center, float size, Color color, float duration = 0.1f)
        {
            // Calculate half size to get the extents of the square
            float halfSize = size / 2f;

            // Define the four corners of the square
            Vector3 bottomLeft = center + new Vector3(-halfSize, -halfSize, 0);
            Vector3 bottomRight = center + new Vector3(halfSize, -halfSize, 0);
            Vector3 topRight = center + new Vector3(halfSize, halfSize, 0);
            Vector3 topLeft = center + new Vector3(-halfSize, halfSize, 0);

            // Draw the four sides of the square
            Debug.DrawLine(bottomLeft, bottomRight, color, duration);
            Debug.DrawLine(bottomRight, topRight, color, duration);
            Debug.DrawLine(topRight, topLeft, color, duration);
            Debug.DrawLine(topLeft, bottomLeft, color, duration);
        }

        public Grid<PathNode> GetGrid()
        {
            return grid;
        }


        public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos)
        {
            grid.GetXY(startPos, out int startX, out int startY);
            grid.GetXY(endPos, out int endX, out int endY);

            List<PathNode> path = FindPath(startX, startY, endX, endY);

            if (path == null)
            {
                return new List<Vector3>();
            }
            else
            {
                List<Vector3> vectorPath = new List<Vector3>(path.Count);
                Vector3 cellOffset = Vector3.one * grid.CellSize * 0.5f;
                foreach (var pathNode in path)
                {
                    vectorPath.Add(new Vector3(pathNode.X, pathNode.Y) * grid.CellSize + cellOffset);
                }

                return vectorPath;
            }
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            PathNode startNode = grid.GetGridObject(startX, startY);
            PathNode endNode = grid.GetGridObject(endX, endY);
            openList = new List<PathNode>() { startNode };
            closedList = new List<PathNode>();

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    PathNode node = grid.GetGridObject(x, y);
                    node.gCost = int.MaxValue;
                    node.CalculateFCost();
                    node.cameFromNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            //  PathfindingDebugStepVisual.Instance.ClearSnapshots();
            // PathfindingDebugStepVisual.Instance.TakeSnapshot(grid, startNode, openList, closedList);

            while (openList.Count > 0)
            {
                PathNode currentNode = GetLowerFCostNode(openList);
                //Reach Final Node
                if (currentNode == endNode)
                {
                    //     PathfindingDebugStepVisual.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
                    //     PathfindingDebugStepVisual.Instance.TakeSnapshotFinalPath(grid, CalculatePath(endNode));
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (var neighbourNode in GetNeighbourList(currentNode))
                {
                    if (closedList.Contains(neighbourNode))
                    {
                        continue;
                    }

                    if (!neighbourNode.IsWalkable)
                    {
                        closedList.Add(neighbourNode);
                        continue;
                    }

                    int newCostToNeighbour = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (newCostToNeighbour < neighbourNode.gCost || !openList.Contains(neighbourNode))
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = newCostToNeighbour;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);
                        }
                    }
                }

                //     PathfindingDebugStepVisual.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
            }

            //Out of node
            return null;
        }

        //Need optimize
        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            List<PathNode> neighbours = new List<PathNode>();
            if (currentNode.X - 1 >= 0)
            {
                //Left
                neighbours.Add(GetNode(currentNode.X - 1, currentNode.Y));
                //LeftDown
                if (currentNode.Y - 1 >= 0)
                {
                    neighbours.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
                }

                //LeftUp
                if (currentNode.Y + 1 < grid.Height)
                {
                    neighbours.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
                }
            }

            if (currentNode.X + 1 < grid.Width)
            {
                //right
                neighbours.Add(GetNode(currentNode.X + 1, currentNode.Y));
                //right down
                if (currentNode.Y - 1 >= 0)
                {
                    neighbours.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
                }

                //right up
                if (currentNode.Y + 1 < grid.Height)
                {
                    neighbours.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
                }
            }

            //Down
            if (currentNode.Y - 1 >= 0) neighbours.Add(GetNode(currentNode.X, currentNode.Y - 1));
            //Up
            if (currentNode.Y + 1 < grid.Height) neighbours.Add(GetNode(currentNode.X, currentNode.Y + 1));

            return neighbours;
        }


        public PathNode GetNode(int x, int y)
        {
            return grid.GetGridObject(x, y);
        }

        private List<PathNode> CalculatePath(PathNode endNode)
        {
            List<PathNode> path = new List<PathNode>();
            path.Add(endNode);
            PathNode currentNode = endNode;
            while (currentNode.cameFromNode != null)
            {
                path.Add(currentNode.cameFromNode);
                currentNode = currentNode.cameFromNode;
            }

            path.Reverse();
            return path;
        }

        private int CalculateDistanceCost(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.X - b.X);
            int yDistance = Mathf.Abs(a.Y - b.Y);
            int remaningDistance = Mathf.Abs(xDistance - yDistance);
            return MOVE_DIAGONAl_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaningDistance;
        }

        //Optimize By BinarySearch
        private PathNode GetLowerFCostNode(List<PathNode> pathNodeList)
        {
            PathNode lowerFCostNode = pathNodeList[0];
            for (int i = 0; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].gCost < lowerFCostNode.gCost)
                {
                    lowerFCostNode = pathNodeList[i];
                }
            }

            return lowerFCostNode;
        }
    }
}