using System;
using System.Collections.Generic;
using UnityEngine;

using Astar.Managers;
using Astar.Data;

namespace Astar
{
    public class Pathfinding
    {
        private AstarManager _manager;
        private Node[,,] _grid;

        public Pathfinding(AstarManager manager)
        {
            _manager = manager;
            _grid = manager.GetGrid();
        }

        public void FindPath(PathRequest request, Action<PathResult> callback)
        {
            Vector3[] waypoints = new Vector3[0];
            bool pathSuccess = false;

            Node startNode = _manager.GetNodeFromWorldPoint(request.PathStart);
            Node targetNode = _manager.GetNodeFromWorldPoint(request.PathEnd);
            startNode.Parent = startNode;

            if (startNode.Walkable && targetNode.Walkable)
            {
                Heap<Node> openSet = new Heap<Node>(_manager.MaxSize);
                HashSet<Node> closedSet = new HashSet<Node>();
                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    Node currentNode = openSet.RemoveFirst();
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        pathSuccess = true;
                        break;
                    }

                    foreach (Node neighbour in _manager.GetNeigbours(currentNode))
                    {
                        if (!neighbour.Walkable || closedSet.Contains(neighbour) || !neighbour.GroundNode)
                            continue;

                        int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, targetNode);
                            neighbour.Parent = currentNode;

                            if (!openSet.Contains(neighbour))
                                openSet.Add(neighbour);
                            else
                                openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }

            if (pathSuccess)
            {
                waypoints = RetracePath(startNode, targetNode);
                pathSuccess = waypoints.Length > 0;
            }

            callback(new PathResult(waypoints, pathSuccess, request.Callback));
        }

        private Vector3[] RetracePath(Node startNode, Node targetNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = targetNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            Vector3[] waypoints = SimplifyPath(path);
            Array.Reverse(waypoints);

            return waypoints;
        }
        private Vector3[] SimplifyPath(List<Node> path)
        {
            List<Vector3> waypoints = new List<Vector3>();
            Vector3 oldDirection = Vector3.zero;

            for (int i = 1; i < path.Count; i++)
            {
                Vector3 newDirection = path[i - 1].WorldPosition - path[i].WorldPosition;

                if (newDirection != oldDirection)
                {
                    waypoints.Add(path[i].WorldPosition);
                }

                oldDirection = newDirection;
            }

            return waypoints.ToArray();
        }
        private int GetDistance(Node node1, Node node2)
        {
            int distanceX = Mathf.Abs(node1.GridPosition.x - node2.GridPosition.x);
            int distanceY = Mathf.Abs(node1.GridPosition.y - node2.GridPosition.y);
            int distanceZ = Mathf.Abs(node1.GridPosition.z - node2.GridPosition.z);

            //Calculate the distance between X and Y
            int xyDist;
            if (distanceX > distanceY)
                xyDist = 14 * distanceY + 10 * (distanceX - distanceY);
            else
                xyDist = 14 * distanceX + 10 * (distanceY - distanceX);

            //Calculate the Distance between xyDist and Z
            if (xyDist > distanceZ)
                return 14 * distanceZ + 10 * (xyDist - distanceZ);
            else
                return 14 * xyDist + 10 * (distanceZ - xyDist);
        }
    }
}
