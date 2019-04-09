using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astar.Data
{
    public class SceneNavigationSettings : ScriptableObject
    {
        public Vector3 GridWorldSize { get; private set; }
        public float AgentRadius { get; private set; }
        public float AgentHeight { get; private set; }

        public void UpdateValues(Vector3 gridWorldSize, float agentRadius, float agentHeight)
        {
            //World settings
            GridWorldSize = gridWorldSize;

            //agent settings
            AgentRadius = agentRadius;
            AgentHeight = agentHeight;
        }

        public float NodeDiameter
        {
            get { return AgentRadius; }
        }
        public float NodeRadius
        {
            get => NodeDiameter / 2f;
        }
        public Vector3Int GridSize
        {
            get
            {
                Vector3Int gridSize = Vector3Int.zero;
                gridSize.x = Mathf.RoundToInt(GridWorldSize.x / NodeDiameter);
                gridSize.y = Mathf.RoundToInt(GridWorldSize.y / NodeDiameter);
                gridSize.z = Mathf.RoundToInt(GridWorldSize.z / NodeDiameter);

                if (gridSize.y % 2 == 0)
                    gridSize.y++;

                return gridSize;
            }
        }
        public Vector3 WorldBottomBackLeft
        {
            get
            {
                return Vector3.zero - (Vector3.right * GridWorldSize.x / 2f) - (Vector3.up * GridWorldSize.y / 2f) - (Vector3.forward * GridWorldSize.z / 2f);
            }
        }
    }
}
