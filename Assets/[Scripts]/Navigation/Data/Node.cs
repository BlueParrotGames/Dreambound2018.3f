using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astar.Data
{
    public class Node : IHeapItem<Node>
    {
        public bool Walkable { get; private set; }
        public Vector3 WorldPosition { get; private set; }
        public Vector3 GroundPosition { get; private set; }
        public Vector3Int GridPosition { get; private set; }
        public bool GroundNode { get; private set; }
        public int MovementPenalty { get; set; }

        public int gCost;
        public int hCost;
        public Node Parent;

        private int _heapIndex;
        public int HeapIndex { get => _heapIndex; set => _heapIndex = value; }

        public Node(bool walkable, Vector3 worldPosition, Vector3 groundPosition, Vector3Int gridPosition, int penalty, bool groundNode)
        {
            Walkable = walkable;
            WorldPosition = worldPosition;
            GroundPosition = groundPosition;
            GridPosition = gridPosition;
            MovementPenalty = penalty;
            GroundNode = groundNode;
        }

        public int fCost { get => gCost + hCost; }
        public int CompareTo(Node node)
        {
            int compare = fCost.CompareTo(node.fCost);

            if (compare == 0)
                compare = hCost.CompareTo(node.hCost);

            return -compare;
        }
    }
}
